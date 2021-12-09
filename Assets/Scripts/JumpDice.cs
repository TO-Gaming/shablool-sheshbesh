using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDice : MonoBehaviour
{
    public static Rigidbody rb;
    public bool hasLanded;
    Vector3 initPos;
    public Vector3 diceVelocity;
    [SerializeField]
    public GameObject FieldZone;
    public Vector3 StartPos;
    Vector3 CurPos;

    public int diceValue;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.velocity;
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            hasLanded = true;
            CurPos = transform.position;
        }
            
    }

    public void Jump()
    {
        // makes the dice jump
        hasLanded = false;
        rb = this.gameObject.GetComponent<Rigidbody>();
        transform.position = CurPos+ new Vector3(0f,Random.Range(0.5f,1.5f),0f);
        transform.rotation = Quaternion.identity;
        Vector3 Torq = new Vector3(Random.Range(-130, 390), Random.Range(-100, 330), Random.Range(-250, 310));
        rb.AddForce(transform.up*(Random.Range(950,1550)));
        rb.AddTorque(Torq);
        
    }
}
