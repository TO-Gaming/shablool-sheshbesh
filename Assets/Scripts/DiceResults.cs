using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceResults : MonoBehaviour
{
    [SerializeField]
    GameObject diceResText;
    [SerializeField]
    public static GameObject DicetoMove;
    Text ans;
    // Start is called before the first frame update
    public Rigidbody rb = DicetoMove.GetComponent<Rigidbody>();
    public void Roll()
    {
        rb.AddForce(new Vector3(0, -5, 0));
        int result = Random.Range(1, 7);
        int result2 = Random.Range(1, 7);
        ans = diceResText.GetComponent<Text>();
        ans.text = "Dice 1: " + result + " Dice 2: " + result2;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
