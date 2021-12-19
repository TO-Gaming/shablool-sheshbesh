using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    

    public int curPower;




    TextMesh ans;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // when pressing & relevant makes the dice jump.
    void OnMouseUp()
    {
        // clicked Dice - > Panel On
        Debug.Log("Clicked PowerDice");
        GM.StateEffect(); // update State
        GM.EffectManage.ShowPowerPanel();
        this.gameObject.SetActive(false);
        GM.GamePanel.SetActive(false);

        // start tiles selecting torch
        GM.EffectManage.WriteEffect("Pick where you want to start a fire. \nafter 4 turns one piece will be burned in the selected area.");
        // clicked dice - > set Tiles to FireTorch
        Debug.Log("curPower is"+curPower);
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

    private void initPower()
    {
        switch (GameManager.PlayerCur.playerPower)
        {
            case GameManager.Fire:
                this.curPower = GameManager.Fire;
                this.gameObject.GetComponent<MeshRenderer>().material = FireM;
                break;
            case GameManager.Water:
                this.curPower = GameManager.Water;
                this.gameObject.GetComponent<MeshRenderer>().material = WaterM;
                break;
            case GameManager.Wind:
                this.curPower = GameManager.Wind;
                this.gameObject.GetComponent<MeshRenderer>().material = WindM;
                break;
            case GameManager.Earth:
                this.curPower = GameManager.Earth;
                this.gameObject.GetComponent<MeshRenderer>().material = EarthM;
                break;
        }
    }

    private void Update()
    {
        transform.Rotate(0, 60 * Time.deltaTime,0);
    }

    //important func for blocking and setting new results for the game.
}