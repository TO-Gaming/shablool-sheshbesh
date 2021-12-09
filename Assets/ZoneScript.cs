using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    [SerializeField]
    public GameObject dice1;
    [SerializeField]
    public GameObject dice2;

    Vector3 diceVel;
    Vector3 diceVel2;
    JumpDice J1;
    JumpDice J2;

    

    private void Start()
    {
        J1 = dice1.gameObject.GetComponent<JumpDice>();
        J2 = dice2.gameObject.GetComponent<JumpDice>();
    }



    void FixedUpdate()
    {
        diceVel = J1.diceVelocity;
        diceVel2 = J2.diceVelocity;
        J1 = dice1.gameObject.GetComponent<JumpDice>();
        J2 = dice2.gameObject.GetComponent<JumpDice>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(J1.hasLanded&&other.transform.parent.gameObject.tag == "dice1")
        {
            switch(other.gameObject.name)
            {
                case "Side1":
                    DiceResults.res1Text = 6;
                    break;
                case "Side2":
                    DiceResults.res1Text = 5;
                    break;
                case "Side3":
                    DiceResults.res1Text = 4;
                    break;
                case "Side4":
                    DiceResults.res1Text = 2;
                    break;
                case "Side5":
                    DiceResults.res1Text = 1;
                    break;
                case "Side6":
                    DiceResults.res1Text = 3;
                    break;
            }
        }
        if (J2.hasLanded && other.transform.parent.gameObject.tag == "dice2")
        {
            switch (other.gameObject.name)
            {
                case "Side1":
                    DiceResults.res2Text = 6;
                    break;
                case "Side2":
                    DiceResults.res2Text = 5;
                    break;
                case "Side3":
                    DiceResults.res2Text = 4;
                    break;
                case "Side4":
                    DiceResults.res2Text = 2;
                    break;
                case "Side5":
                    DiceResults.res2Text = 1;
                    break;
                case "Side6":
                    DiceResults.res2Text = 3;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
