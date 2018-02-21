using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    public void LoadByIndex(int sceneIndex)
    {
        Debug.Log("has moved on");
        SceneManager.LoadScene(sceneIndex);
    }
}
