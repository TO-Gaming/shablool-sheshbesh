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
        WhiteOpt.onValueChanged.AddListener(delegate { WEffectSelected(WhiteOpt); });
        BlackOpt.onValueChanged.AddListener(delegate { BEffectSelected(BlackOpt); });
    }

    public void BEffectSelected(Dropdown Effect)
    {
        switch (Effect.value)
        {
            case 0:
                BlackImg.canvasRenderer.SetTexture(FireImg);
                BlackPick = 1;
                break;
            case 1:
                BlackImg.canvasRenderer.SetTexture(WaterImg);
                BlackPick = 2;
                break;
            case 2:
                BlackImg.canvasRenderer.SetTexture(EarthImg);
                BlackPick = 3;
                break;
            case 3:
                BlackImg.canvasRenderer.SetTexture(AirIMg);
                BlackPick = 4;
                break;
        }
    }

    public void WEffectSelected(Dropdown Effect)
    {
        switch (Effect.value)
        {
            case 0:
                WhiteImg.canvasRenderer.SetTexture(FireImg);
                WhitePick = 1;
                break;
            case 1:
                WhiteImg.canvasRenderer.SetTexture(WaterImg);
                WhitePick = 2;
                break;
            case 2:
                WhiteImg.canvasRenderer.SetTexture(EarthImg);
                WhitePick = 3;
                break;
            case 3:
                WhiteImg.canvasRenderer.SetTexture(AirIMg);
                WhitePick = 4;
                break;
        }
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
