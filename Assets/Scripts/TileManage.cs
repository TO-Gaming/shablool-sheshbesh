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
    public int curCoinVal;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] Tiles = new GameObject[24];
    [SerializeField]
    GameManager GM;

    public static int powerTiles=0;
    public static Tile SelectedTile=null;
    public static int SelectedTileID=-1;

    public Tile[] GameTiles;
    public int[] tileColors = new int[24];
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

            curTile.setColor(empty);
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
        int srcColor = GameTiles[srcIndex].getColor();
        int destColor = GameTiles[destIndex].getColor();
        Debug.Log("ismovable? src color: " + srcColor + " dest color " + destColor+" src "+srcIndex+" dest "+destIndex);
        if (GameTiles[destIndex].CountCheckers() == 0)
            return 1;
        if (destColor == srcColor)
        {
            return 1;
        }
        else if (GameTiles[destIndex].CountCheckers() == 1)
            return 2;
        else
            return -1;
        
    }

    // main function to move the right checker according to what player ask, and player color
    public void MoveCheckers(int playerColor, int srcIndex, int destIndex)
    {
        // move player color > < , from > to > 

        int ans = isMovable(srcIndex, destIndex);
        Debug.Log("ismovable: " +ans+ " src: "+srcIndex+ " dest "+destIndex);
        if (ans == 1)
        {
            GameTiles[destIndex].setColor(playerColor);
            GameTiles[srcIndex].RemovePiece();
            GameTiles[destIndex].AddPiece(playerColor);
            
            //GM.refreshButtons();
            // move succesful
            Debug.Log("Move succesful from" + srcIndex + " to " + destIndex);
            GameManager.textBox.text = "moved from " + srcIndex + " to " + destIndex;
            if((GameTiles[srcIndex].CountCheckers()==0||(GameManager.PlayerCur.doneMoves[0]&& GameManager.PlayerCur.doneMoves[1]))&& TileManage.SelectedTile!=null)
            {
                Debug.Log("entered movecheckers if ");
                
            }
                

        }
        if(ans==2)
        {
            Debug.Log("ans is 2 - can eat dest index"+destIndex);
            GameManager.PlayerOther.DecreaseSoldier();
            GM.refreshLives();
            GameTiles[destIndex].RemovePiece();
            GameTiles[destIndex].setColor(playerColor);
            GameTiles[srcIndex].RemovePiece();
            GameTiles[destIndex].AddPiece(playerColor);
            GM.PlayingOptions[GM.playedOption].SetActive(false);
            GameManager.textBox.text = "moved from "+srcIndex+" to "+destIndex;
        }
        else
        {
            Debug.Log("ismovable ans is "+ans+"  cant move from"+srcIndex+" to " +destIndex);
            GameManager.textBox.text = "you cannot do that!";
        }
        //Debug.Log("now color srcTile is" + GameTiles[srcIndex].getColor() + " aand colo dest tile: " + GameTiles[destIndex].getColor());

    }

    public Tile getSelectedTile()
    {
        return GameTiles[SelectedTileID];
    }
    public int GetDest(int playerColor,int srcIndex, int numofMoves)
    {
        int destIndex = srcIndex;
        if (playerColor == black)
        {
            Debug.Log("player black moving");
            if (srcIndex + numofMoves >= BoardLOOP)
            {
                Debug.Log("getting dest with loop");
                destIndex = (srcIndex + numofMoves) % BoardLOOP;
                if(isMovable(srcIndex,destIndex)!=-1)
                    GameManager.PlayerCur.addcoins(GameTiles[srcIndex].getWorth());
                //GameManager.PlayersText.GetComponent<TextMesh>().text = "You Passed Shablul Loop and Recieved " + money + "Shablul Coins";
            }
            else
            {
                //Debug.Log("getting dest whithout loop");
                destIndex = srcIndex + numofMoves;

            }
        }
        if (playerColor == white)
        {
            Debug.Log("player white moving");
            if (srcIndex - numofMoves < 0)
            {
                destIndex = srcIndex - numofMoves + BoardLOOP;
                if (isMovable(srcIndex, destIndex) != -1)
                    GameManager.PlayerCur.addcoins(GameTiles[srcIndex].getWorth());
                //GameManager.PlayersText.GetComponent<TextMesh>().text = "You Passed Shablul Loop and Recieved " + money + "Shablul Coins";
            }
            else
                destIndex = srcIndex - numofMoves;
        }
        //Debug.Log("dest index is: "+destIndex);
        return destIndex;
    }
    public int canMoveSteps(int playerColor, int tileID, int numMoves) // -1 cant , other - destTile
    {
        int destIndex = GetDest(playerColor,tileID, numMoves);
        int canMove = isMovable(tileID, destIndex);
        Debug.Log("ismovable " + canMove);
        if(canMove==1||canMove==2)
            return destIndex;
        return -1;
    }

    public int canMoveStepsback(int playerColor, int tileID, int numMoves) // -1 cant , other - destTile version for wind element
    {
        int destIndex = GetDest(playerColor, tileID, numMoves);
        int canMove = isMovable(tileID, destIndex);
        Debug.Log("ismovable " + canMove);
        if (canMove == 1)
            return destIndex;
        return -1;
    }

    public void SetFire(int TileId)
    {
        Debug.Log("Starting fire from turn " + GameManager.turnCount+" on tile"+TileId);
        GameTiles[TileId].StartFire(GameManager.turnCount);
    }

    // set blow back

    public bool checkCanBlow(int playerColor, int tileID, int numMoves)
    {
        bool ans = true;
        if (canMoveSteps(playerColor, tileID, numMoves) == -1)
            ans = false;
        return ans;
    }


}
