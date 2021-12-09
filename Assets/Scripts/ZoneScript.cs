using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script stands for the Dice zone, it holds relevant objects.
public class ZoneScript : MonoBehaviour
{
    [SerializeField]
    public GameObject dice1;
    [SerializeField]
    public GameObject dice2;

    public readonly int one = 1;
    public readonly int two = 2;
    public readonly int three = 3;
    public readonly int four = 4;
    public readonly int five = 5;
    public readonly int six = 6;


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
                    DiceResults.res1Text = six;
                    break;
                case "Side2":
                    DiceResults.res1Text = five;
                    break;
                case "Side3":
                    DiceResults.res1Text = four;
                    break;
                case "Side4":
                    DiceResults.res1Text = two;
                    break;
                case "Side5":
                    DiceResults.res1Text = one;
                    break;
                case "Side6":
                    DiceResults.res1Text = three;
                    break;
            }
        }
        if (J2.hasLanded && other.transform.parent.gameObject.tag == "dice2")
        {
            switch (other.gameObject.name)
            {
                case "Side1":
                    DiceResults.res2Text = six;
                    break;
                case "Side2":
                    DiceResults.res2Text = five;
                    break;
                case "Side3":
                    DiceResults.res2Text = four;
                    break;
                case "Side4":
                    DiceResults.res2Text = two;
                    break;
                case "Side5":
                    DiceResults.res2Text = one;
                    break;
                case "Side6":
                    DiceResults.res2Text = three;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
