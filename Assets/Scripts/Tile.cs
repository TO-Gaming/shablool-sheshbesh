using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  tile class, holds pieces and also makes it possible for the tilemanager to do some tasks within the tiles.
public class Tile : MonoBehaviour
{
    
    [SerializeField]
    private GameObject White;
    [SerializeField]
    private GameObject Black;

    private int colorTile; // -1 empty  0-black  1-white 
    public float reduceZ=0f;
    [SerializeField]
    private int numCheckers=0;
    [SerializeField]
    private int index;


    private static int white = 1;
    private static int black = 0;
    private readonly int empty = -1;
    private static float space = -4.5f;

    public GameObject Torch;
    public GameObject FireTorch;
    public GameObject WaterTorch;
    public GameObject EarthTorch;
    public GameObject AirTorch;

    public MeshRenderer mr;
    public List<Piece> pieces = new List<Piece>();
    public List<GameObject> pieceObjects = new List<GameObject>();
    private bool hasStarted;
    private bool Clickable;
    public bool HasChosen;
    public bool OnFire=false;
    private static int FireEndturn=-1;
   
    void Start()
    {
        hasStarted = false;
        Clickable = true;
        HasChosen = false;
        Torch = this.gameObject.transform.GetChild(0).gameObject;
        FireTorch = this.gameObject.transform.GetChild(1).gameObject;
        FireTorch.SetActive(false);
        Torch.SetActive(false);
        
    }

    //tile is pressable when it is play time.
    void OnMouseUp()
    {
        Debug.Log("before Clickable");
        if (GameManager.PlayerCur.playerColor==colorTile)
        {
            Debug.Log("after Clickable");
            //press any tile to hide last and show current
            if (TileManage.SelectedTile != null)
                TileManage.SelectedTile.Torch.SetActive(false);
            TileManage.SelectedTile = this;
            TileManage.SelectedTile.Torch.SetActive(true);
            TileManage.SelectedTileID = index;
            GameManager.textBox.text = " Tile selected: " + index;
            HasChosen = true;
            
        }

        if (TileManage.powerTiles != 0)
            if ( GameManager.PlayerCur.playerColor != colorTile){
                Debug.Log("after Clickable");
                //press any tile to hide last and show current
                if (TileManage.SelectedTile != null)
                    TileManage.SelectedTile.FireTorch.SetActive(false);
                TileManage.SelectedTile = this;
                TileManage.SelectedTile.Torch.SetActive(true);
                TileManage.SelectedTileID = index;
                GameManager.textBox.text = " Tile selected: " + index;
            }
    }



    public void Init (int indextoset)
    {
        index = indextoset;
    }

    public void SetClickable()
    {
        Clickable = true;
    }
    public void SetNotClickable()
    {
        Clickable = false;
    }


    // func that fills the tile with the required amount and color (for the init)
    public void StartFillTiles(int num, int color)
    {
        Debug.Log("start filling tiles "+num);
        for (int i = 0; i < num; i++)
        {
            AddPiece(color);
        }
        colorTile = color;
        //Debug.Log("changed tile " + index + " color " + colorTile+"to color "+color);
        
    }

    public void setFree()
    {
        colorTile = empty;
    }

    public void setColor(int color)
    {
        colorTile = color;
    }
    public int getColor()
    {
        return colorTile;
    }
  
    // adds pieces to the Tile, counting and saving in a list. (reducez is for the spaces for each new object)
    public void AddPiece(int playerColor)
    {
        
        if(playerColor==white)
        {
            GameObject w = Instantiate(White);
            Piece Pcur = w.GetComponent<Piece>();
            w.transform.parent = this.transform;
            w.transform.localPosition = new Vector3(0, 0, space + reduceZ);
            pieces.Add(Pcur);
            pieceObjects.Add(w);
            colorTile = white;
        }
        else if(playerColor == black)
        {
            GameObject w = Instantiate(Black);
            Piece Pcur = w.GetComponent<Piece>();
            w.transform.parent = this.transform;
            w.transform.localPosition = new Vector3(0, 0, space + reduceZ);
            pieces.Add(Pcur);
            pieceObjects.Add(w);
            colorTile = black;
        }
        numCheckers++;
        reduceZ += 1f;
        //Debug.Log("NOW TILE COLOR IS " + colorTile);
    }


    // removes piece from tile, (the top one), also count and update reducez
    public void RemovePiece()
    {
        //Debug.Log("try to remove, pieces count is : "+ pieces.Count);
        //Debug.Log("try to remove, numCheckers : " + numCheckers);
        //Debug.Log("try to remove from id  = " + getIndex());
        Piece p;
        GameObject g1;
        if (numCheckers>0)
        {
            Debug.Log("piecesObject new size " + pieceObjects.Count);
            g1 = pieceObjects[pieceObjects.Count - 1];
            p = pieces[pieces.Count - 1];
            Debug.Log("p null? " + p);
            pieces.RemoveAt(pieces.Count - 1);
            pieceObjects.RemoveAt(pieceObjects.Count - 1);
            //p.deletePiece();
            Destroy(g1);
            Debug.Log("pieces new size " + pieces.Count);
            reduceZ -= 1f;
            numCheckers--;
            if(numCheckers==0)
            {
                colorTile = empty;
            }
        }
        Debug.Log("NOW TILE "+ index+ "COLOR IS " + colorTile);

    }

    public bool isEmpty()
    {
        if (numCheckers == 0)
            return true;
        return false;
    }
    public int CountCheckers()
    {
        return numCheckers;
    }

    public void setIndex(int ind)
    {
        index = ind;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.turnCount==FireEndturn)
        {
            Debug.Log("firetime ended");
            if(this.CountCheckers()>0)
            {
                Debug.Log("Burn!");
            }
            OnFire = false;
            FireEndturn = -1;
        }
    }


    public int getIndex()
    {
        return index;
    }
    public int getWorth()
    {
        return pieces[pieces.Count-1].getValue();
    }
    public void StartFire(int curTurn)
    {
        OnFire = true;
        FireEndturn = curTurn + 4;
    }

}
