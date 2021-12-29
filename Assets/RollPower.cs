using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RollPower : MonoBehaviour
{
    [SerializeField]
    GameManager GM;
    [SerializeField]
    Light DiceLight;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.PlayerCur.doneMoves[0] && !GameManager.PlayerCur.doneMoves[1] && GameManager.PlayerCur.curState != PlayerState.PlayPower)
        {
            transform.Rotate(0, 60 * Time.deltaTime, 0);
            DiceLight.enabled = true;
        }
        else
        {
            DiceLight.enabled = false;
        }
    }

    void OnMouseUp()
    {
        if (!GameManager.PlayerCur.doneMoves[0] && !GameManager.PlayerCur.doneMoves[1] && GameManager.PlayerCur.curState != PlayerState.PlayPower && !EventSystem.current.IsPointerOverGameObject())
        {
            GM.GamePanel.SetActive(false);
            this.gameObject.SetActive(false);
            GameManager.Dices.FreeTurn = true;
            GM.RollLight.gameObject.SetActive(true);
            GM.PowerDiceOBJ.SetActive(false);
            GameManager.PlayerCur.curState = PlayerState.NeedToRoll;
            if (TileManage.SelectedTileID != -1)
            {
                GM.T.GameTiles[GM.T.getTileID()].Torch.SetActive(false);
                GameManager.ResetTile();
            }
        }

    }
}
