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


        if(sceneName == "reset")
        {
            SceneManager.LoadScene(HighScore.resetscene);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }

        if(sceneName == "ProBuilderTest" || sceneName == "Level2")
        {
            HighScore.sceneName = sceneName;
            HighScore.resetscene = sceneName;

            HighScore2.sceneName = sceneName;
            HighScore2.resetscene = sceneName;
        }
    }
}
