using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// piece class - helps for filling tiles with this object
public class Piece : MonoBehaviour
{
    public int color;
    public int Value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deletePiece()
    {
        Debug.Log("try to destroy");
        Destroy(this.gameObject);
    }

}
