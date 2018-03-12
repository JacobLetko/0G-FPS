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

        if (sceneName == "win" || sceneName == "GameOver")
        {
            SceneManager.LoadScene(HighScore.sceneName);
        }
        else if(sceneName == "reset")
        {
            SceneManager.LoadScene(HighScore.sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }

        if(sceneName == "ProBuilderTest" || sceneName == "Level2")
        {
            HighScore.sceneName = sceneName;
        }
    }
}
