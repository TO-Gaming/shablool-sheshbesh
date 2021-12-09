using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    StartScreen,
    WaitForTurn,
    NeedToRoll,
    waituntillres,
    waitPlayMoves,
    FinishTurn,
    WinGame
}
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Roll;
    [SerializeField]
    private GameObject TurnTextBox;
    [SerializeField]
    private GameObject GameOverText;
    [SerializeField]
    public static GameObject PlayersText;
    [SerializeField]
    private GameObject PlaneText;
    [SerializeField]
    private GameObject EffectPanel;
    [SerializeField]
    private TileManage T;
    [SerializeField]
    private GameObject [] PlayingOptions;
    [SerializeField]
    private GameObject GamePanel;
    [SerializeField]
    private GameObject DiceField;

    private readonly int startCoins = 0;
    private readonly int white = 1;
    private readonly int black = 0;
    private readonly int Fire = 1;
    private readonly int Water = 2;
    private readonly int Earth = 3;
    private readonly int Wind = 4;
    

    //private string playerTurn;

    private TileManage t;
    public static Text textBox;
    public TextMesh Turntext;
    private int playerTurn; // 0 is black , 1 is white
    private string PlayingPlayer;
    public static Player PlayerCur;
    public PlayerState statePlaying;
    private Player BlackP;
    private Player WhiteP;
    private bool gameOver;
    private RollDice Dices;
    private bool Pickedeffect;
    private bool hasRes;

    // Start is called before the first frame update

    


    void Start()
    {
        Dices = Roll.GetComponent<RollDice>();
        Turntext = TurnTextBox.GetComponent<TextMesh>();
        textBox = PlaneText.GetComponent<Text>();
        GamePanel.SetActive(false);

        BlackP = new Player(0, black);
        WhiteP = new Player(0, white);
        Debug.Log("playerA color" + BlackP.playerColor);
        Debug.Log("playerB color" + WhiteP.playerColor);
        EffectPanel.SetActive(true);
        BlackP.curState = PlayerState.NeedToRoll;
        WhiteP.curState = PlayerState.WaitForTurn;
        gameOver = false;
        hasRes = false;
        playerTurn = black;
        PlayingPlayer = "Black";
        PlayerCur = BlackP;
        PlayerCur.resetRolled();
        PlayerCur.HasFinishedPlaying = false;
        T.initTiles();
        T.FillBoard();
        //StartCoroutine((IEnumerator)WaitForMovesEnd());
    }

    

    // Update is called once per frame
    void Update()
    {
        // PlayerCur
        // stateCur
        
        switch (PlayerCur.curState)
        {
            case PlayerState.StartScreen:
                Turntext.text = PlayerCur.curState.ToString();
                EffectPanel.SetActive(true);
                WhiteP.curState = PlayerState.WaitForTurn;
                break;
            case PlayerState.NeedToRoll:
                EffectPanel.SetActive(false);
                Turntext.text = PlayerCur.curState.ToString();
                TurnTextBox.SetActive(true);
                textBox.text = "Now it's your turn : " + PlayerCur.PlayerName;
                if(Dices.BothLanded())
                {
                    
                    Dices.pressed = false;
                    Debug.Log("rolled dice");
                    GamePanel.SetActive(true);
                    PlayingOptions[0].SetActive(true);
                    PlayingOptions[1].SetActive(true);
                    StartCoroutine(YourTurn());
                }
                break;
            case PlayerState.waitPlayMoves:
                
                Turntext.text = PlayerCur.curState.ToString();
                if (PlayerCur.MovesEnded())
                    PlayerCur.curState = PlayerState.FinishTurn;
                // untill played all options
                //PlayerCur.curState = PlayerState.FinishTurn;
                break;
            case PlayerState.FinishTurn:
                Turntext.text = PlayerCur.curState.ToString();
                Dices.FreeTurn = true;
                
                Debug.Log("PLayer finished playing");
                
                changeTurn();
                if (BlackP.numSoldiers < 10)
                {
                    Turntext.text = "White Wins!";
                    PlayersText.SetActive(true);
                    PlayerCur.curState = PlayerState.WinGame;
                }
                if (WhiteP.numSoldiers < 10)
                {
                    Turntext.text = "Black Wins!";
                    PlayersText.SetActive(true);
                    PlayerCur.curState = PlayerState.WinGame;
                }
                break;
            default:
                Debug.Log("Bad State");
                break;
        }


        if (BlackP.numSoldiers < 10)
        {
            Turntext.text = "White Wins!";
            PlayersText.SetActive(true);
        }
        if (WhiteP.numSoldiers < 10)
        {
            Turntext.text = "Black Wins!";
            PlayersText.SetActive(true);
        }

    }
    
   

    public void changeTurn()
    {
        int nowplaying = playerTurn;
        if (nowplaying == white)
        {
            PlayerCur = BlackP;
            playerTurn = black;
            PlayingPlayer = "Black";
            WhiteP.curState = PlayerState.WaitForTurn;
            BlackP.curState = PlayerState.NeedToRoll;
        }
        else
        {
            PlayerCur = WhiteP;
            playerTurn = white;
            PlayingPlayer = "White";
            BlackP.curState = PlayerState.WaitForTurn;
            WhiteP.curState = PlayerState.NeedToRoll;   
        }
        PlayerCur.HasFinishedPlaying = false;
    }

    private void StartTurn(int playerTurn)

    {
        Turntext.text = "it is Player " + PlayingPlayer + " Turn, Please Roll the dice";

        Roll.SetActive(true); // turn on dice button
        DiceField.SetActive(true); // turn on dices area.

        Debug.Log("start corotine drop");
        
        Debug.Log("end corotine drop");

        PlayerCur.curState = PlayerState.waitPlayMoves;

        //waituntill played. coroutine ->


        //all click blocked, Dice alowed.
        // Roll the Dice!
        //while rolls isnotempty:
        //Choose tile and steps:
        //move(ChosenTile,num)
        //if ismove -1 : " you cant move there!"
        //
    }

    public IEnumerator PlayerRollCoroutine()
    {
        while(!PlayerCur.HasFinishedPlaying)
        {
            yield return new WaitForSeconds(1f);
        }
        yield return null;
                
    }

    public IEnumerator YourTurn()
    { 
        yield return new WaitForSecondsRealtime(5f);
        addPlayerMoves();
        hasRes = true;
        PlayerCur.curState = PlayerState.waitPlayMoves;
    }

    


    public void PlayerMoveA()
    {
        Debug.Log("A-playerA color" + BlackP.playerColor);
        Debug.Log("A-playerB color" + WhiteP.playerColor);

        if (PlayerCur.PlayableOptions.Count == 2)
        {
            T.MoveCheckers(PlayerCur.playerColor, T.getTileID(), PlayerCur.getOptions()[0]); //PlayerCur.getOptions()[1]
            PlayerCur.PlayableOptions.RemoveAt(0);
        }
        else
        {
            T.MoveCheckers(PlayerCur.playerColor, T.getTileID(), PlayerCur.getOptions()[0]);
            PlayerCur.PlayableOptions.RemoveAt(0);
        }
        PlayingOptions[0].SetActive(false);

    }
    public void PlayerMoveB()
    {
        Debug.Log("B-playerA color" + BlackP.playerColor);
        Debug.Log("B-playerB color" + WhiteP.playerColor);

        if (PlayerCur.PlayableOptions.Count == 2)
        {
            T.MoveCheckers(PlayerCur.playerColor, T.getTileID(), PlayerCur.getOptions()[1]); //PlayerCur.getOptions()[1]
            PlayerCur.PlayableOptions.RemoveAt(1);
        }
        else
        {
            T.MoveCheckers(PlayerCur.playerColor, T.getTileID(), PlayerCur.getOptions()[0]);
            PlayerCur.PlayableOptions.RemoveAt(0);
        }
            
        PlayingOptions[1].SetActive(false);
    }

    public void WriteButtonA(string s)
    {
        PlayingOptions[0].GetComponentInChildren<Text>().text = s;
    }
    public void WriteButtonB(string s)
    {
        PlayingOptions[1].GetComponentInChildren<Text>().text = s;
    }


    public void addPlayerMoves()
    {
        List<int> moves = new List<int>();
        moves.Add(Dices.getA());
        moves.Add(Dices.getB());
        PlayerCur.setPlayableOptions(moves);
        PlayerCur.MovesLeft = 2;
    }

    public class Player{
        
        public int ShabCoins;
        public string PlayerName;
        public int playerColor; // black is 0 white is 1
        public int playerPower; // 1,2,3,4 - Fire, Water, Earth, Wind
        public List <int> PlayableOptions;
        public bool HasFinishedPlaying;
        public int numSoldiers;
        private readonly int StartSoldiers = 15;
        public bool RolledTheDice;
        public int MovesLeft;
        public PlayerState curState;

        public Player(int a, int color)
        {
            HasFinishedPlaying = false;
            playerColor = color;
            addcoins(a);
            playerPower = 0;
            numSoldiers = StartSoldiers;
            RolledTheDice = false;
        }
        public void addcoins(int a)
        {
            ShabCoins += a;
        }
        public PlayerState getState()
        {
            return curState;
        }

        public void setPower(int power)
        {
            playerPower = power;
        }

        public void setName(string name)
        {
            PlayerName = name;
        }

        public void setPlayableOptions (List<int> moves)
        {
            PlayableOptions = moves;
            Debug.Log("new moves added");
        }
        
        public List<int> getOptions()
        {
            return PlayableOptions;
        }

        public void setRolled()
        {
            RolledTheDice = true;
        }

        public void resetRolled()
        {
            RolledTheDice = false;
        }
            
        public bool isRolled()
        {
            return RolledTheDice;
        }

        public string getName()
        {
            return PlayerName;
        }

        public bool MovesEnded()
        {
            return this.PlayableOptions.Count == 0;
        }
    }
    
    public void PickFireA()
    {
        WhiteP.setPower(Fire);
        //ApickingEffect = false;
    }
    public void PickWaterA()
    {
        WhiteP.setPower(Water);
        //ApickingEffect = false;
    }
    public void PickEarthA()
    {
        WhiteP.setPower(Earth);
        //ApickingEffect = false;
    }
    public void PickWindA()
    {
        WhiteP.setPower(Wind);
        //ApickingEffect = false;
    }
    public void PickFireB()
    {
        BlackP.setPower(Fire);
        //BpickingEffect = false;
    }
    public void PickWaterB()
    {
        BlackP.setPower(Water);
        //BpickingEffect = false;
    }
    public void PickEarthB()
    {
        BlackP.setPower(Earth);
        //BpickingEffect = false;
    }
    public void PickWindB()
    {
        BlackP.setPower(Wind);
        //BpickingEffect = false;
    }

    public void pickEffect()
    {
        Pickedeffect = true;
        EffectPanel.SetActive(false);
        PlayerCur.curState = PlayerState.NeedToRoll;
        Debug.Log("Effect panel ok ");
    }

    public void ShowPanel()
    {
        GamePanel.SetActive(true);
    }

    public void HidePanel()
    {
        GamePanel.SetActive(false);
    }

    public void WritePanel(string s)
    {
        textBox.text = s;
    }

    public IEnumerable WaitForMovesEnd()
    {
        yield return new WaitUntil(() => PlayerCur.MovesEnded());
    }
}
