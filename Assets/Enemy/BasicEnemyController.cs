using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum EnemyState
{
    Idle,
    Attacking,
    Chasing,
    Pathing
};


public class BasicEnemyController : MonoBehaviour, IDamagable {
    
    public float maxHealth = 100.0f;
    public int killScore = 100;
    public float rotationForce = 10.0f;
    public float gunCooldown = 0.5f;
    public float wobbleRange = 2.0f;
    public float wobbleForce = 5.0f;
    public float wobbleReachDist = 0.5f;

    public float pathForce = 5.0f;
    public float pathReachDist = 0.5f;

    public float bulletSpeed = 50.0f;
    public float bulletDamage = 10.0f;
    public Vector2 shootPitchRange = new Vector2(0.9f, 1.1f);
    public float laserBeamTime = 0.25f;

    public bool chaser = false;

    [Header("Links")]
    public AudioClip shootSound;
    public AudioClip explodeSound;

    public Rigidbody body;
    public Transform player;
    public AudioSource audioSource;
    public BulletPool bulletPool;
    public NodeGraphManager graphManager;
    public GameObject bulletPrefab;
    public ParticleSystem explodeEffect;
    public GameObject explodeParts;
    public GameObject modelObj;
    public GameObject laserLight;
    public GameObject gunLight;
    public LineRenderer lineRenderer;

    private float health;
    private bool alive = true;
    private float gunHeat = 0;
    private float laserRunTime = 0;
    private Vector3 startPos;
    private Vector3 trgPos = Vector3.zero;
    private Vector3 trgLastPos = Vector3.zero;
    private EnemyState state;
    private List<Vector3> nodePath;


    
	void Start () {
        health = maxHealth;
        startPos = transform.position;
        state = EnemyState.Idle;
        nodePath = new List<Vector3>();
	}
	
	void Update () {
        if(gunHeat > 0)
        {
            gunHeat -= Time.deltaTime;
        }
        
		if(alive && gunHeat <= 0 && state == EnemyState.Attacking)
        {
            Vector3 toPlayer = player.position - transform.position;
            float ang = Vector3.Angle(toPlayer, transform.forward);
            
            if(ang <= 20)
            {
                Shoot();
                gunHeat = gunCooldown;
            }
        }
	}

    void FixedUpdate()
    {
        if(laserRunTime < laserBeamTime)
        {
            laserRunTime += Time.deltaTime;
            if(laserRunTime >= laserBeamTime)
            {
                laserLight.SetActive(false);
                gunLight.SetActive(false);
                lineRenderer.enabled = false;
            }
        }

        if (alive)
        {
            if(chaser)
            {
                startPos = player.position;
            }
            else
            {
                startPos = transform.position;
            }
            
            if (state == EnemyState.Chasing)
            {
                if(Vector3.Distance(transform.position, trgPos) <= wobbleReachDist)
                {
                    body.AddForce(-body.velocity * body.mass, ForceMode.Impulse);
                    trgPos = Vector3.zero;
                    state = EnemyState.Idle;
                }
            }
            else if(state == EnemyState.Pathing)
            {
                Vector3 trgOffset = trgPos - transform.position;
                float angleDif = Vector3.Angle(transform.position, trgPos);
                Vector3 cross = Vector3.Cross(transform.forward, trgOffset);
                float rampedSpeed = rotationForce * (cross.magnitude / angleDif);

                float appliedSpeed = Mathf.Min(rampedSpeed, rotationForce);
                Vector3 desiredTorque = cross * (appliedSpeed / cross.magnitude);

                body.AddTorque(desiredTorque - body.angularVelocity);



                Debug.DrawLine(transform.position, trgPos);
                if (Vector3.Distance(transform.position, trgPos) <= pathReachDist)
                {
                    nodePath = graphManager.MakePath(transform.position, player.position);
                    nodePath.RemoveAt(0);
                    
                    if (nodePath.Count > 0)
                    {
                        trgPos = nodePath[0];
                        nodePath.RemoveAt(0);
                        GoToTarg(pathForce);
                    }
                    else
                    {
                        trgPos = Vector3.zero;
                        state = EnemyState.Idle;
                        body.AddTorque(-body.angularVelocity);
                    }
                }
            }
            

            RaycastHit hit;
            int layerMask = LayerMask.GetMask("PathfindingObstacle", "Player");
            Vector3 targetOffset = player.position - transform.position;
            float  angleToPlayer = Vector3.Angle(targetOffset, transform.forward);
            
            if (Physics.Raycast(transform.position, targetOffset, out hit, 10000.0f, layerMask))
            {
                if (hit.transform.tag == "Player")
                {
                    if(state != EnemyState.Attacking)
                    {
                        trgPos = Vector3.zero;
                    }
                    state = EnemyState.Attacking;

                    float      angleDif = Vector3.Angle(transform.position, player.position);
                    Vector3       cross = Vector3.Cross(transform.forward, targetOffset);
                    float   rampedSpeed = rotationForce * (cross.magnitude / angleDif);

                    float    appliedSpeed = Mathf.Min(rampedSpeed, rotationForce);
                    Vector3 desiredTorque = cross * (appliedSpeed / cross.magnitude);
                    
                    body.AddTorque(desiredTorque - body.angularVelocity);

                    if (trgPos == Vector3.zero)
                    {

                        trgPos = new Vector3(startPos.x + Random.Range(-wobbleRange, wobbleRange),
                                             startPos.y + Random.Range(-wobbleRange, wobbleRange),
                                             startPos.z + Random.Range(-wobbleRange, wobbleRange));
                        GoToTarg(wobbleForce);
                    }
                }
                else
                {
                    if(state == EnemyState.Attacking)
                    {
                        body.AddTorque(-body.angularVelocity);
                        state = EnemyState.Chasing;
                        if(graphManager != null)
                        {
                            nodePath = graphManager.MakePath(transform.position, player.position);
                            
                            if (nodePath.Count == 0)
                            {
                                trgPos = trgLastPos;
                            }
                            else
                            {
                                state = EnemyState.Pathing;
                                trgPos = nodePath[0];
                                nodePath.RemoveAt(0);
                            }
                        }
                        else
                        {
                            trgPos = trgLastPos;
                        }
                        
                        if (state == EnemyState.Chasing)
                        {
                            body.AddTorque(-body.angularVelocity);
                            GoToTarg(wobbleForce);
                        }
                        else if(state == EnemyState.Pathing)
                        {
                            GoToTarg(pathForce);
                        }
                    }
                }
            }
            
            if (state == EnemyState.Attacking)
            {
                trgLastPos = player.position;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == EnemyState.Chasing)
        {
            GoToTarg(wobbleForce);
        }
        else if (state == EnemyState.Pathing)
        {
            GoToTarg(pathForce);
        }
        else if(state == EnemyState.Attacking)
        {
            trgPos = new Vector3(startPos.x + Random.Range(-wobbleRange, wobbleRange),
                                             startPos.y + Random.Range(-wobbleRange, wobbleRange),
                                             startPos.z + Random.Range(-wobbleRange, wobbleRange));
            GoToTarg(wobbleForce);
        }
    }



    public void GoToTarg(float force)
    {
        body.AddForce(-body.velocity * body.mass, ForceMode.Impulse);
        Vector3 trgDir = trgPos - transform.position;
        body.AddForce(trgDir.normalized * force);
    }

    public void Damage(float amt)
    {
        if(alive)
        {
            health = Mathf.Clamp(health - amt, 0.0f, maxHealth);
            if (health <= 0)
            {
                Kill();
            }
        }
    }

    public float GetHealth()
    {
        return health;
    }

    private void Kill()
    {
        explodeEffect.Play();
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(explodeSound);
        alive = false;

        HighScore.addPoints(killScore);
        
        explodeParts.SetActive(true);

        GetComponent<Collider>().enabled = false;
        modelObj.GetComponent<Renderer>().enabled = false;
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)
         && hit.transform.gameObject.tag != "Enemy")
        {
            audioSource.pitch = Random.Range(shootPitchRange.x, shootPitchRange.y);
            audioSource.PlayOneShot(shootSound);

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.enabled = true;
            laserRunTime = 0;
            {
                laserLight.transform.position = hit.point - (transform.forward * 0.5f);
                laserLight.SetActive(true);
                gunLight.SetActive(true);
                lineRenderer.SetPosition(1, hit.point);
                IDamagable dmg = hit.transform.GetComponent<IDamagable>();
                if (dmg != null)
                {
                    dmg.Damage(bulletDamage);
                }
            }
        }
        else
        {
            laserLight.SetActive(false);
            gunLight.SetActive(false);
            lineRenderer.SetPosition(1, transform.position + transform.forward * 1000);
        }
    }

}
