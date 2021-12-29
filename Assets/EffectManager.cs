using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    GameManager GM;
    [SerializeField]
    RawImage BlackImg;
    [SerializeField]
    RawImage WhiteImg;

    [SerializeField]
    Texture EarthImg;
    [SerializeField]
    Texture FireImg;
    [SerializeField]
    Texture WaterImg;
    [SerializeField]
    Texture AirIMg;
    [SerializeField]
    public Text EffectText;
    [SerializeField]
    private GameObject EffectTextPanel;
    [SerializeField]
    Button OK;
    [SerializeField]
    Dropdown BlackOpt;
    [SerializeField]
    Dropdown WhiteOpt;
    [SerializeField]
    public GameObject FireAText;
    [SerializeField]
    public GameObject WaterAText;
    [SerializeField]
    public GameObject EarthAText;
    [SerializeField]
    public GameObject AirAText;
    [SerializeField]
    public GameObject FireBText;
    [SerializeField]
    public GameObject WaterBText;
    [SerializeField]
    public GameObject EarthBText;
    [SerializeField]
    public GameObject AirBText;

    public static GameObject curBText;
    public static GameObject curWText;




    public int BlackPick;
    public int WhitePick;

    
    

    public const int Fire = 1;
    public const int Water = 2;
    public const int Earth = 3;
    public const int Wind = 4;

    public GameManager.Player CurPlayer;



    // Start is called before the first frame update
    void Start()
    {
        EffectTextPanel.SetActive(false);
        curWText = FireAText;
        curBText = FireBText;
        curWText.SetActive(true);
        curBText.SetActive(true);
        BlackPick = BlackOpt.value+1;
        WhitePick = WhiteOpt.value+1;
        WhiteOpt.onValueChanged.AddListener(delegate { WEffectSelected(WhiteOpt); });
        BlackOpt.onValueChanged.AddListener(delegate { BEffectSelected(BlackOpt); });
        
    }

    public void BEffectSelected(Dropdown Effect)
    {
        //Debug.Log(Effect.value);
        curBText.SetActive(false);
        switch (Effect.value)
        {
            case 0:
                BlackImg.canvasRenderer.SetTexture(FireImg);
                curBText = FireBText;
                BlackPick = 1;
                break;
            case 1:
                BlackImg.canvasRenderer.SetTexture(WaterImg);
                curBText = WaterBText;
                BlackPick = 2;
                break;
            case 2:
                BlackImg.canvasRenderer.SetTexture(EarthImg);
                curBText = EarthBText;
                BlackPick = 3;
                break;
            case 3:
                BlackImg.canvasRenderer.SetTexture(AirIMg);
                curBText = AirBText;
                BlackPick = 4;
                break;
        }
        curBText.SetActive(true);
        Debug.Log("Black pick " + BlackPick);
    }

    public void WEffectSelected(Dropdown Effect)
    {
        //Debug.Log(Effect.value);
        curWText.SetActive(false);
        switch (Effect.value)
        {
            case 0:
                WhiteImg.canvasRenderer.SetTexture(FireImg);
                curWText = FireAText;
                //PowerText.text = FireText;
                WhitePick = 1;
                break;
            case 1:
                WhiteImg.canvasRenderer.SetTexture(WaterImg);
                curWText = WaterAText;
                WhitePick = 2;
                break;
            case 2:
                WhiteImg.canvasRenderer.SetTexture(EarthImg);
                curWText = EarthAText;
                WhitePick = 3;
                break;
            case 3:
                WhiteImg.canvasRenderer.SetTexture(AirIMg);
                curWText = AirAText;
                WhitePick = 4;
                break;
        }
        Debug.Log("White pick " + WhitePick);
        curWText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteEffect(string text)
    {
        EffectText.text = text;
    }
    public void ShowPowerPanel()
    {
        EffectTextPanel.SetActive(true);
    }
    public void HidePowerPanel()
    {
        EffectTextPanel.SetActive(false);
    }
}
