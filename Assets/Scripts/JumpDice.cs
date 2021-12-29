using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDice : MonoBehaviour
{
    [SerializeField]
    public Rigidbody rb;
    public bool hasLanded;
    Vector3 initPos;
    public Vector3 diceVelocity;
    [SerializeField]
    public GameObject FieldZone;
    public Vector3 StartPos;
    Vector3 CurPos;
    public bool partlyLanded;

    public int diceValue;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        partlyLanded = false;
        hasLanded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(diceVelocity.x != 0f || diceVelocity.y != 0f || diceVelocity.z != 0f)
        
            //LandedSecAgo = false;

        diceVelocity = rb.velocity;
        if (rb.IsSleeping()) //diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            hasLanded = true;
            partlyLanded = true;
            //StartCoroutine( CheckDrop());
            //CurPos = transform.position;
        }
        else
        {
            hasLanded = false;
            //yield return CheckDrop();
        }
            //hasLanded = false;
        
            
    }

    public void Jump()
    {
        // makes the dice jump
        hasLanded = false;
        rb = this.gameObject.GetComponent<Rigidbody>();
        Vector3 v1 = new Vector3(0, Random.Range(1.5f, 1.8f), 0);
        transform.position = StartPos;
        transform.Rotate(Random.Range(-90f,90f), Random.Range(-90f, 90f), Random.Range(-90f, 90f));
        transform.rotation = Quaternion.identity;
        Vector3 Torq = new Vector3(Random.Range(-870, 390), Random.Range(-310, 630), Random.Range(-480, 980));//(Random.Range(-130, 390), Random.Range(-100, 330), Random.Range(-250, 310));
        rb.AddForce(transform.up*(Random.Range(750,1050)));//Random.Range(950,1550)));
        rb.AddForce(transform.right * (-1)*(Random.Range(1050, 1550)));
        rb.AddTorque(Torq);
        
    }
    public void ResetLoc()
    {
        
    }
    public void getLoc()
    {

    }

    public IEnumerator CheckDrop()
    {
        Debug.Log("IEnumerator checkdrop - wait sec + DiceLand");
        yield return new WaitForSecondsRealtime(2f);
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f&&partlyLanded)
        {
            hasLanded = true;
        }
    }
}
