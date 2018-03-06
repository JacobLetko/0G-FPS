using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void LoadByName(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
        if (sceneName != "Win")
        {
            HighScore.resetScore();
        }
    }
}
