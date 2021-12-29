using System;
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
    [SerializeField]
    Material BlackMaterial;
    [SerializeField]
    Material WhiteMaterial;


    TextMesh DiceText;

    [SerializeField]
    public GameObject dice1;
    [SerializeField]
    public GameObject dice2;

    public bool FreeTurn;
    public bool isInUse;
    public bool pressed;
    public int result=0;
    public int result2=0;
    [SerializeField]
    public static int res1Text;
    [SerializeField]
    public static int res2Text;

    private MeshRenderer meshRenderer;
    JumpDice J1;
    JumpDice J2;
    private const int white=1;
    private const int black = 0;
    private const int direction = 1;
    //private bool haslanded;
    private bool hasRes;

    // Start is called before the first frame update
    void Start()
    {
        FreeTurn = true;
        pressed = false;
        DiceText = diceResText.GetComponent<TextMesh>();
        J1 = dice1.gameObject.GetComponent<JumpDice>();
        J2 = dice2.gameObject.GetComponent<JumpDice>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // finish getting result and update it.
    public void UpdateRes()
    {
        //int result = Random.Range(1, 7);
        //int result2 = Random.Range(1, 7);
        Debug.Log("updateRes");
        result = res1Text;
        result2 = res2Text; // change back to res2

        //ans.text = "Results: \n  " + result + "  ,  " + result2;
        GM.WritePanel("  "+ result + ", " + result2);
        GM.WriteButtonA("" + result);
        GM.WriteButtonB("" + result2);
        if (res1Text == res2Text)
        {
            StartCoroutine("ShowHideText");
        }
    }

 


    // when pressing & relevant makes the dice jump.
    void OnMouseUp()
    {
        //Debug.Log("J1.hasLanded && J2.hasLanded && FreeTurn" + J1.hasLanded + " " + J2.hasLanded + "" + FreeTurn);
        if(J1.hasLanded && J2.hasLanded && FreeTurn)
        {
            GameManager.PlayerCur.RolledTheDice = true;
            hasRes = false;
            //haslanded = false;
            isInUse = true;
            J1.Jump();
            J2.Jump();
            FreeTurn = false;
            pressed = true;
            GM.RollLight.gameObject.SetActive(false);
            GM.DiceLight.gameObject.SetActive(true);
        }   
    }

    private void Update()
    {
        if (J1.hasLanded && J2.hasLanded&&GameManager.PlayerCur.curState==PlayerState.waitlanded&&!hasRes)
        {
            //Debug.Log("if inside update - hasRes now set true");
            //UpdateRes();
            hasRes = true;
        }

        if (GM.NeedRoll()|| GM.NeedMove())
        {
            //hasRes = false;
            
            //Debug.Log("need roll");
            int dir = direction;
            if (GameManager.PlayerCur.playerColor ==white)
            {
                dir = direction*-1;
                meshRenderer.material = WhiteMaterial;
            }
            else
            {
                meshRenderer.material = BlackMaterial;
            }
                    
            transform.Rotate(0, dir*50 * Time.deltaTime,0);
        }
            

    }

    //important func for blocking and setting new results for the game.
    public bool BothLanded()
    {
        //Debug.Log("pressed is " + pressed.ToString());
        return J1.hasLanded && J2.hasLanded && pressed;
    }

    public bool Landed()
    {
        return J1.hasLanded && J2.hasLanded;
    }

    public void setFinish()
    {
        isInUse = false;
    }

    public int getA()
    {
        return res1Text;
    }
    public int getB()
    {
        return res2Text;
    }

    public IEnumerator ShowHideText()
    {
        DiceText.text = "Double! you recieve 1 Shablul coin";
        diceResText.SetActive(true);
        Debug.Log("pressed is " + pressed);
        yield return new WaitForSecondsRealtime(3f);
        DiceText.text = "";
        diceResText.SetActive(false);
    }
    

}
