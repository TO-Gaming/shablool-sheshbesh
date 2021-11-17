using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRemove : MonoBehaviour
{
    [SerializeField]
    private GameObject White;
    [SerializeField]
    private GameObject Black;
    private static int white = 1;
    private static int black = 0;
    [SerializeField]
    private static float space = -4.5f;

    [SerializeField]
    public int Iswhite =white;
    [SerializeField]
    private int startNumCheckers=3;
    public float reduceZ=0f;
    // Start is called before the first frame update
    void Start()
    {
        Init(startNumCheckers, Iswhite);
    }

    private void Init(int num, int color)
    {
        for (int i = 0; i < num; i++)
        {
            AddPiece(color);
        }
    }
    private void AddPiece(int color)
    {
        if(color==white)
        {
            GameObject w = Instantiate(White);
            w.transform.parent = this.transform;
            w.transform.localPosition = new Vector3(0, 0, space + reduceZ);
        }
        else
        {
            GameObject w = Instantiate(Black);
            w.transform.parent = this.transform;
            w.transform.localPosition = new Vector3(0, 0, space + reduceZ);
        }
        reduceZ += 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
