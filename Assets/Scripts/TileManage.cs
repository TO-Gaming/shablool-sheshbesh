using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManage : MonoBehaviour
{
    private readonly int[] posToInit = { 0, 0, 0, 0, 0, 5, 0, 3, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0,0, 2 };
    private readonly int white = 1;
    private readonly int black = 0;
    private readonly int empty = -1;
    private readonly int BoardLOOP = 24;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] Tiles = new GameObject[24];

    public static Tile SelectedTile;
    public static int SelectedTileID;

    public Tile[] GameTiles;
    void Start()
    {
        GameTiles = new Tile[24]; // 0 -23
    }

    public int getTileID()
    {
        return SelectedTileID;
    }

    public void initTiles()
    {
        Tile curTile;
        Tile[] x;
        for (int i = 0; i < posToInit.Length;i++)
        {
            x = Tiles[i].GetComponents<Tile>();
            //x.pieces = new List<Piece>();
            curTile = x[0];
            curTile.Init(i);
            GameTiles[i] = curTile;
            //GameTiles[i].setColor(white);
        }

    }

    public void FillBoard()
    {
        for(int i = 0; i<posToInit.Length;i++)
        {
            if (posToInit[i] != 0)
            {
                GameTiles[posToInit.Length-1-i].StartFillTiles(posToInit[i], black);
                GameTiles[i].StartFillTiles(posToInit[i], white);
            }
            
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    // this func checks if the player can move to the asked position.
    public int isMovable(int srcIndex, int destIndex) // -1 false 1 can move 2 can eat 
    {
        int srcColor = GameTiles[srcIndex].colorTile;
        int destColor = GameTiles[destIndex].colorTile;
        
        if (GameTiles[destIndex].CountCheckers() == 0)
            return 1;
        if (destColor!=srcColor)
        {
            if (GameTiles[destIndex].CountCheckers() == 1)
                return 2;
            else
                return -1;
        }
        else
                return 1;
        
    }

    // main function to move the right checker according to what player ask, and player color
    public void MoveCheckers(int playerColor, int srcIndex, int numofMoves)
    {
        int destIndex = srcIndex;
        int money = 1;
        if (playerColor == black)
        {
            Debug.Log("player black moving");
            if(srcIndex+numofMoves>BoardLOOP)
            {
                destIndex = (srcIndex + numofMoves) % BoardLOOP;
                GameManager.PlayerCur.addcoins(1);
                //GameManager.PlayersText.GetComponent<TextMesh>().text = "You Passed Shablul Loop and Recieved " + money + "Shablul Coins";
            }
            else
                destIndex = srcIndex + numofMoves;
        }
        if (playerColor == white)
        {
            Debug.Log("player white moving");
            if (srcIndex - numofMoves <0)
            {
                destIndex = srcIndex - numofMoves + BoardLOOP;
                GameManager.PlayerCur.addcoins(money);
                //GameManager.PlayersText.GetComponent<TextMesh>().text = "You Passed Shablul Loop and Recieved " + money + "Shablul Coins";
            }
            else
                destIndex = srcIndex - numofMoves;
        }

        int ans = isMovable(srcIndex, destIndex);
        if(ans == 1)
        {
            
            GameTiles[destIndex].setColor(playerColor);

            GameTiles[srcIndex].RemovePiece();
            GameTiles[destIndex].AddPiece();
        }
        if(ans==2)
        {
            Debug.Log("ans is 2 - can eat");
            GameTiles[destIndex].RemovePiece();
            GameTiles[destIndex].setColor(playerColor);
            GameTiles[srcIndex].RemovePiece();
            GameTiles[destIndex].AddPiece();
        }
        else
        {
            Debug.Log("ans is -1   cant move");
            GameManager.textBox.text = "you cannot do that!";

        }

    }

}
