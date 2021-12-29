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
    [SerializeField]
    public bool everyThirdDouble ;

    public readonly int one = 1;
    public readonly int two = 2;
    public readonly int three = 3;
    public readonly int four = 4;
    public readonly int five = 5;
    public readonly int six = 6;


    Vector3 diceVel;
    Vector3 diceVel2;
    [SerializeField]
    JumpDice J1;
    [SerializeField]
    JumpDice J2;

    

    private void Start()
    {

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
                    RollDice.res1Text = six;
                    break;
                case "Side2":
                    RollDice.res1Text = five;
                    break;
                case "Side3":
                    RollDice.res1Text = four;
                    break;
                case "Side4":
                    RollDice.res1Text = two;
                    break;
                case "Side5":
                    RollDice.res1Text = one;
                    break;
                case "Side6":
                    RollDice.res1Text = three;
                    break;
            }
        }
        if (J2.hasLanded && other.transform.parent.gameObject.tag == "dice2")
        {
            switch (other.gameObject.name)
            {
                case "Side1":
                    RollDice.res2Text = six;
                    break;
                case "Side2":
                    RollDice.res2Text = five;
                    break;
                case "Side3":
                    RollDice.res2Text = four;
                    break;
                case "Side4":
                    RollDice.res2Text = two;
                    break;
                case "Side5":
                    RollDice.res2Text = one;
                    break;
                case "Side6":
                    RollDice.res2Text = three;
                    break;
            }
            
        }
        if(GameManager.turnCount%3==0 && everyThirdDouble)
            RollDice.res1Text = RollDice.res2Text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
