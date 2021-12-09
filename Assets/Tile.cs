using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    
    [SerializeField]
    private GameObject White;
    [SerializeField]
    private GameObject Black;
    
    private static int white = 1;
    private static int black = 0;
    private readonly int empty = -1;
    private static float space = -4.5f;

    [SerializeField]
    public int colorTile;
    public float reduceZ=0f;
    [SerializeField]
    private int numCheckers=0;
    // Start is called before the first frame update
    public List<Piece> pieces = new List<Piece>();
    public List<GameObject> pieceObjects = new List<GameObject>();
    private bool hasStarted;
    [SerializeField]
    private int index;
    private bool Clickable;
    public bool HasChosen;
    public MeshRenderer mr;

    void Start()
    {
        hasStarted = false;
        
        Clickable = true;
        HasChosen = false;
    }

    void OnMouseUp()
    {
        Debug.Log("before Clickable");
        if (Clickable)
        {
            Debug.Log("after Clickable");
            TileManage.SelectedTile = this;
            TileManage.SelectedTileID = index;
            GameManager.textBox.text = " Tile selected: " + index;
            HasChosen = true;
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



    public void StartFillTiles(int num, int color)
    {
        Debug.Log("start filling tiles "+num);
        colorTile = color;
        for (int i = 0; i < num; i++)
        {
            AddPiece();
        }
        
    }

    public void setFree()
    {
        colorTile = empty;
    }

    public void setColor(int color)
    {
        colorTile = color;
    }
  

    public void AddPiece()
    {
        
        if(colorTile==white)
        {
            GameObject w = Instantiate(White);
            Piece Pcur = w.GetComponent<Piece>();
            w.transform.parent = this.transform;
            w.transform.localPosition = new Vector3(0, 0, space + reduceZ);
            pieces.Add(Pcur);
            pieceObjects.Add(w);
            
        }
        else if(colorTile == black)
        {
            GameObject w = Instantiate(Black);
            Piece Pcur = w.GetComponent<Piece>();
            w.transform.parent = this.transform;
            w.transform.localPosition = new Vector3(0, 0, space + reduceZ);
            pieces.Add(Pcur);
            pieceObjects.Add(w);
        }
        
        numCheckers++;
        reduceZ += 1f;
    }



    public void RemovePiece()
    {
        Debug.Log("try to remove, pieces count is : "+ pieces.Count);
        Debug.Log("try to remove, numCheckers : " + numCheckers);
        Debug.Log("try to remove from id  = " + getIndex());
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
        }
        
        
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
        
    }

    public int getColor()
    {
        return colorTile;
    }

    public int getIndex()
    {
        return index;
    }
    public int getWorth()
    {
        return 1;
    }
}
