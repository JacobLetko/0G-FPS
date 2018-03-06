using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void LoadByName(string sceneName)
    {
        Time.timeScale = 1;
        if (sceneName != "Win")
        {
            HighScore.resetScore();
        }
        SceneManager.LoadScene(sceneName);
    }
}
