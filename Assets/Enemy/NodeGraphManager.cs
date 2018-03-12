using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Node
{
    public Node(Transform t, int i)
    {
        idx = i;
        trans = t;
        links = new List<Node>();
    }

    public int idx;
    public Transform trans;
    public List<Node> links;
}


public class NodeGraphManager : MonoBehaviour {

    public GameObject nodeContainer;

    [Header("Debugging")]
    public bool debugRender;

    private List<Node> graph;
    private List<Vector3> debugLines;



    private void Start()
    {
        debugLines = new List<Vector3>();

        graph = new List<Node>();

        Transform[] nodes = nodeContainer.GetComponentsInChildren<Transform>();
        for(int i = 0; i < nodes.Length; ++i)
        {
            Transform t = nodes[i];
            if(t != transform)
            {
                Node d = new Node(t, graph.Count);
                graph.Add(d);
            }
        }
        foreach(Node n in graph)
        {
            Transform ntrans = n.trans;
            foreach (Node t in graph)
            {
                Transform ttrans = t.trans;
                if (ntrans != ttrans)
                {
                    float dist = Vector3.Distance(ntrans.position, ttrans.position);

                    Vector3 dir = (ttrans.position - ntrans.position).normalized;
                    RaycastHit hit;
                    int layerMask = LayerMask.GetMask("PathfindingObstacle");
                    if (Physics.Raycast(ntrans.position, dir, out hit, 10000000.0f, layerMask))
                    {
                        if (Vector3.Distance(ntrans.position, hit.point) > dist)
                        {
                            AddLink(n, t);
                        }
                    }
                    else
                    {
                        AddLink(n, t);
                    }
                }
            }
        }
    }

    private void Update()
    {
        if(debugRender)
        {
            for (int i = 0; i < debugLines.Count; i += 2)
            {
                Debug.DrawLine(debugLines[i], debugLines[i + 1], Color.magenta);
            }
        }
    }

    private void AddLink(Node n, Node t)
    {
        n.links.Add(t);
        if (debugRender)
        {
            debugLines.Add(n.trans.position);
            debugLines.Add(t.trans.position);
        }
    }




    public List<Vector3> MakePath(Vector3 startPos, Vector3 goal)
    {
        List<Vector3> path = new List<Vector3>();
        //indices in graph
        int start = -1;
        int end = -1;

        for(int i = 0; i < graph.Count; ++i)
        {
            Node n = graph[i];

            float dist = Vector3.Distance(n.trans.position, startPos);
            Vector3 dir = (startPos - n.trans.position).normalized;
            RaycastHit hit;
            int layerMask = LayerMask.GetMask("PathfindingObstacle");
            if (Physics.Raycast(n.trans.position, dir, out hit, 10000000.0f, layerMask))
            {
                if (Vector3.Distance(n.trans.position, hit.point) > dist)
                {
                    if(start == -1 ||
                       Vector3.Distance(graph[start].trans.position, startPos) > dist)
                    {
                        start = i;
                    }
                }
            }

            dist = Vector3.Distance(n.trans.position, goal);
            dir = (goal - n.trans.position).normalized;
            if (Physics.Raycast(n.trans.position, dir, out hit, 10000000.0f, layerMask))
            {
                if (Vector3.Distance(n.trans.position, hit.point) > dist)
                {
                    if (end == -1 ||
                       Vector3.Distance(graph[end].trans.position, goal) > dist)
                    {
                        end = i;
                    }
                }
            }
        }
        

        if(start != -1 && end != -1)
        {
            Queue<int> frontier = new Queue<int>();
            frontier.Enqueue(end);
            int[] cameFrom = new int[graph.Count];
            for(int i = 0; i < cameFrom.Length; ++i)
            {
                cameFrom[i] = -1;
            }
            cameFrom[end] = -1;

            while(frontier.Count > 0)
            {
                int crnt = frontier.Dequeue();
                for(int i = 0; i < graph[crnt].links.Count; ++i)
                {
                    int idx = graph[crnt].links[i].idx;
                    if (cameFrom[idx] == -1)
                    {
                        frontier.Enqueue(idx);
                        cameFrom[idx] = crnt;
                    }
                }
            }


            int current = start;
            while(current != end)
            {
                path.Add(graph[current].trans.position);
                current = cameFrom[current];
            }
            path.Add(graph[end].trans.position);
            //path.Reverse();
        }
        
        return path;
    }


}
