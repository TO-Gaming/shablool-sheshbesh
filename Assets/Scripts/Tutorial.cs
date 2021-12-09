using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static string Start1 = "Tutorial";

    void OnMouseUp()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Start1);
    }
}
