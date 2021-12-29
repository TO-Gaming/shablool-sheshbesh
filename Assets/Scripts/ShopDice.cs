using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDice : MonoBehaviour
{
    [SerializeField]
    TextMesh PowerText;
    [SerializeField]
    GameManager GM;
    [SerializeField]
    GameObject ShopPanel;
    [SerializeField]
    GameObject RollPowerObj;
    public int curPower;

    TextMesh ans;

    // Start is called before the first frame update
    void Start()
    {
        curPower = -1;
        ShopPanel.SetActive(false);
    }

    // when pressing & relevant makes the dice jump.
    void OnMouseUp()
    {
        ShopPanel.SetActive(true);
        GM.GamePanel.SetActive(false);
        //curPower = GameManager.PlayerCur.getPow();
        this.gameObject.SetActive(false);
        // state to shop
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
        switch (GameManager.PlayerCur.playerPower)
        {
            case GameManager.Fire:
                curPower = GameManager.Fire;
                break;
            case GameManager.Water:
                curPower = GameManager.Water;
                break;
            case GameManager.Wind:
                curPower = GameManager.Wind;
                break;
            case GameManager.Earth:
                curPower = GameManager.Earth;
                break;
        }
    }

    private void Update()
    {
        transform.Rotate(0, 60 * Time.deltaTime, 0);
    }

    public void Back()
    {
        ShopPanel.SetActive(false);
        GM.GamePanel.SetActive(true);
        GM.ShopDiceOBJ.SetActive(true);
    }

    public void buyRoll()
    {
        if(!GameManager.PlayerCur.doneMoves[0]&&!GameManager.PlayerCur.doneMoves[1]&& GameManager.PlayerCur.ShabCoins>3)
        {
            GameManager.PlayerCur.ShabCoins -= 3;
            GM.Refreshcoins();
            Debug.Log("buy roll");
            RollPowerObj.SetActive(true);
            ShopPanel.SetActive(false);
            GM.GamePanel.SetActive(true);
        }
        else
        {
            Debug.Log("You cant purchase that!");
        }
    }

    //important func for blocking and setting new results for the game.
}