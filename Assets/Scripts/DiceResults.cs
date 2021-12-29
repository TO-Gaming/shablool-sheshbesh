using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is for holding results of current roll,
public class DiceResults : MonoBehaviour
{
    [SerializeField]
    GameObject diceResText;

    [SerializeField]
    public static int res1Text;
    [SerializeField]
    public static int res2Text;

    [SerializeField]
    public static GameObject DicetoMove;
    Text ans;
    // Start is called before the first frame update
    //public Rigidbody rb = DicetoMove.GetComponent<Rigidbody>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
