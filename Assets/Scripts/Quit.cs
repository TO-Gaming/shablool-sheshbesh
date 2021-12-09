using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public static string Start1 = "Lobby";
    void OnMouseUp()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Start1);
    }
}
