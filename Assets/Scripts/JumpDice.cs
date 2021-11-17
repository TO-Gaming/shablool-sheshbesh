using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDice : MonoBehaviour
{
    Rigidbody rb;
    bool hasLanded;
    Vector3 initPos;

    public int diceValue;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3(0,-5,0));
    }
}
