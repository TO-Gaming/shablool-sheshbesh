using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDice : MonoBehaviour
{
    [SerializeField]
    GameObject diceResText;

    [SerializeField]
    GameManager GM;

    TextMesh ans;

    [SerializeField]
    public GameObject dice1;
    [SerializeField]
    public GameObject dice2;

    public bool FreeTurn;
    public bool isInUse;
    public bool pressed;
    

    JumpDice J1;
    JumpDice J2;

    // Start is called before the first frame update
    void Start()
    {
        FreeTurn = true;
        pressed = false;
        J1 = dice1.gameObject.GetComponent<JumpDice>();
        J2 = dice2.gameObject.GetComponent<JumpDice>();
    }
    public void UpdateRes()
    {
        //int result = Random.Range(1, 7);
        //int result2 = Random.Range(1, 7);
        int result = DiceResults.res1Text;
        int result2 = DiceResults.res2Text;
        ans = diceResText.GetComponent<TextMesh>();
        ans.text = "Dice 1: " + result + "\nDice 2: " + result2;
        GM.WritePanel("Your results are : \nDice 1: " + result + "\nDice 2: " + result2);
        GM.WriteButtonA(""+result);
        GM.WriteButtonB("" + result2);
    }
    void OnMouseUp()
    {
        if(J1.hasLanded && J2.hasLanded && FreeTurn)
        {
            isInUse = true;
            J1.Jump();
            J2.Jump();
            FreeTurn = false;
            pressed = true;
        }   
    }
    private void Update()
    {
        if (J1.hasLanded && J2.hasLanded)
        {
            UpdateRes();
            //GameManager.PlayerCur.setRolled();
        }
    }

    public bool BothLanded()
    {
        
        return J1.hasLanded && J2.hasLanded && pressed;
    }

    public void setFinish()
    {
        isInUse = false;
    }

    public int getA()
    {
        return DiceResults.res1Text;
    }
    public int getB()
    {
        return DiceResults.res2Text;
    }

    public IEnumerable WaitForDrop()
    {
        Debug.Log("pressed is " + pressed);
        yield return new WaitUntil(() => BothLanded());
    }

}
