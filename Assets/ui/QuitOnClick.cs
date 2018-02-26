using System.Collections;
using UnityEngine;

public class QuitOnClick : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("has quit game");
        Application.Quit();
    }
}
