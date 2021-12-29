using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PowerDice : MonoBehaviour
{
    [SerializeField]
    TextMesh PowerText;
    [SerializeField]
    GameManager GM;
    [SerializeField]
    Material FireM;
    [SerializeField]
    Material WaterM;
    [SerializeField]
    Material EarthM;
    [SerializeField]
    Material WindM;

    [SerializeField]
    private GameObject FireButton;
    [SerializeField]
    private GameObject WaterButtonS;
    [SerializeField]
    private GameObject WaterButtonD;
    [SerializeField]
    private GameObject EarthButton;
    [SerializeField]
    private GameObject AirButton;





    public int curPower;




    TextMesh ans;


    // Start is called before the first frame update
    void Start()
    {
        curPower = -1;
    }

    // when pressing & relevant makes the dice jump.
    void OnMouseUp()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {

        if (TileManage.SelectedTile != null)
        { 
        TileManage.SelectedTile.Torch.SetActive(false);
        GameManager.ResetTile();
        }
        // clicked Dice - > Panel On
        GM.ShopDiceOBJ.SetActive(false);
        GM.EffectManage.ShowPowerPanel();
        //turn on tile powers
        SetTilePower(curPower);
        Debug.Log("Clicked PowerDice");
         // update State
        
        
        GM.GamePanel.SetActive(false);
        //GameManager. after dice press
        GM.powerState();
        GameManager.setPower();
        curPower = GameManager.PlayerCur.getPow();
        TileManage.powerTiles = curPower;
        // start tiles selecting torch
        
        Debug.Log("curPower is" + curPower);
        Debug.Log("Clicked powerDice now tile effects is " + TileManage.powerTiles);
        Debug.Log("Clicked powerDice now selected tile is" + TileManage.SelectedTileID.ToString());
        


        this.gameObject.SetActive(false);

        }
    }

    private void SetTilePower(int curPower)
    {
        switch (curPower)
        {
            case GameManager.Fire:
                TileManage.powerTiles = GameManager.Fire;

                break;
            case GameManager.Water:
                TileManage.powerTiles = GameManager.Water;
                break;
            case GameManager.Wind:
                TileManage.powerTiles = GameManager.Wind;
                break;
            case GameManager.Earth:
                TileManage.powerTiles = GameManager.Earth;
                break;
        }
    }

    private void FireElement()
    {
        PowerText.text = "Fire Power";
    }

    public void initPower()
    {
        Debug.Log("initPower set powerDice to :" + GameManager.PlayerCur.playerPower);
        ResetButtons();
        switch (GameManager.PlayerCur.playerPower)
        {
            case GameManager.Fire:
                FireButton.SetActive(true);
                curPower = GameManager.Fire;
                gameObject.GetComponent<MeshRenderer>().material = FireM;
                GM.EffectManage.WriteEffect("Pick where you want to start a fire. \nafter 4 turns one piece will be burned in the selected area.");
                break;
            case GameManager.Water:
                WaterButtonS.SetActive(true);
                curPower = GameManager.Water;
                GM.EffectManage.WriteEffect("Pick one pieace you would like to ship. \n (you must choose a Destination with one or more of your pieces)");
                this.gameObject.GetComponent<MeshRenderer>().material = WaterM;
                break;
            case GameManager.Wind:
                AirButton.SetActive(true);
                GM.EffectManage.WriteEffect("Pick an enemy unit you want to move back 1 step. \n If possible, the wind will take him away!");
                curPower = GameManager.Wind;
                this.gameObject.GetComponent<MeshRenderer>().material = WindM;
                break;
            case GameManager.Earth:
                EarthButton.SetActive(true);
                curPower = GameManager.Earth;
                GM.EffectManage.WriteEffect("Choose where you want to put the seed. \n one piece will grow in the selected area. (can only grow on an empty tile) ");
                this.gameObject.GetComponent<MeshRenderer>().material = EarthM;
                break;
        }
    }

    private void ResetButtons()
    {
        FireButton.SetActive(false);
        WaterButtonD.SetActive(false);
        WaterButtonS.SetActive(false);
        EarthButton.SetActive(false);
        AirButton.SetActive(false);
    }

    public void WaterShowHide()
    {
        WaterButtonS.SetActive(false);
        WaterButtonD.SetActive(true);
    }

    private void Update()
    {
        transform.Rotate(0, 60 * Time.deltaTime,0);
    }

    //important func for blocking and setting new results for the game.
}