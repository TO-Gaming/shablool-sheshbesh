using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public static string Start1 = "SampleScene";

    void OnMouseUp()
    {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Start1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
