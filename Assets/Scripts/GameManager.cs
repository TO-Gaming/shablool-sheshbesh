using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// enum states , makes it possible to switch between player states.
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
    //requiered objects

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
    private GameObject[] PlayingOptions;
    [SerializeField]
    private GameObject GamePanel;
    [SerializeField]
    private GameObject DiceField;
    [SerializeField]
    private Light DiceLight;
    [SerializeField]
    private Light RollLight;
    [SerializeField]
    private Light WhiteLight;
    [SerializeField]
    private Light BlackLight;
    [SerializeField]
    public Text CoinsAText;
    [SerializeField]
    public Text CoinsBText;
    [SerializeField]
    public TextMesh MiddleText;

    //magic numberds

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
        BlackP.curState = PlayerState.StartScreen;
        WhiteP.curState = PlayerState.WaitForTurn;
        gameOver = false;
        hasRes = false;
        playerTurn = black;
        PlayingPlayer = "Black";
        PlayerCur = BlackP;
        PlayerCur.resetRolled();
        WhiteLight.gameObject.SetActive(false);
        BlackLight.gameObject.SetActive(true);
        DiceLight.gameObject.SetActive(false);
        RollLight.gameObject.SetActive(false);
        CoinsAText.text = "Black - Shablul Coins : " + BlackP.ShabCoins;
        CoinsBText.text = "White - Shablul Coins : " + WhiteP.ShabCoins;
        T.initTiles();
        T.FillBoard();
        //StartCoroutine((IEnumerator)WaitForMovesEnd());
    }



    // Update makes the flow of the game whith states
    void Update()
    {

        switch (PlayerCur.curState)
        {
            case PlayerState.StartScreen:
                Turntext.text = "What is your power?";
                EffectPanel.SetActive(true);
                WhiteP.curState = PlayerState.WaitForTurn;
                break;
            case PlayerState.NeedToRoll:
                EffectPanel.SetActive(false);
                RollLight.gameObject.SetActive(true);
                DiceLight.gameObject.SetActive(true);
                Turntext.text = PlayerCur.getName() + ", please Roll the dice";
                TurnTextBox.SetActive(true);
                MiddleText.text = "Now it's your turn : " + PlayerCur.PlayerName;
                if (Dices.BothLanded())
                {
                    Dices.pressed = false;
                    Debug.Log("rolled dice");
                    GamePanel.SetActive(true);
                    StartCoroutine(YourTurn());
                    PlayingOptions[0].SetActive(true);
                    PlayingOptions[1].SetActive(true);
                }
                break;
            case PlayerState.waitPlayMoves:
                Turntext.text = " Waiting for player " + PlayerCur.getName() + " to Play";
                if (PlayerCur.MovesEnded())
                    PlayerCur.curState = PlayerState.FinishTurn;
                // untill played all options
                //PlayerCur.curState = PlayerState.FinishTurn;
                break;
            case PlayerState.FinishTurn:
                Turntext.text = PlayerCur.curState.ToString();
                CoinsAText.text = "Black - Shablul Coins : " + BlackP.ShabCoins;
                CoinsBText.text = "White - Shablul Coins : " + WhiteP.ShabCoins;
                Dices.FreeTurn = true;
                Debug.Log("Player finished playing");
                changeTurn();
                break;
            case PlayerState.WinGame:
                textBox.text = "Game Over ! Well Played";
                break;
            default:
                Debug.Log("Bad State");
                break;
        }

        // check for winning
        if (BlackP.numSoldiers < 10)
        {
            Turntext.text = "White Wins!";
            PlayersText.SetActive(true);
            WhiteP.curState = PlayerState.WinGame;
            PlayerCur = WhiteP;
        }
        if (WhiteP.numSoldiers < 10)
        {
            Turntext.text = "Black Wins!";
            PlayersText.SetActive(true);
            BlackP.curState = PlayerState.WinGame;
            PlayerCur = BlackP;
        }

    }


    // func to change between the turns
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
            WhiteLight.gameObject.SetActive(false);
            BlackLight.gameObject.SetActive(true);
        }
        else
        {
            PlayerCur = WhiteP;
            playerTurn = white;
            PlayingPlayer = "White";
            BlackP.curState = PlayerState.WaitForTurn;
            WhiteP.curState = PlayerState.NeedToRoll;
            WhiteLight.gameObject.SetActive(true);
            BlackLight.gameObject.SetActive(false);
        }
    }

    // IEnumerator to hold the player untill dice has been rolled.
    public IEnumerator YourTurn()
    {
        yield return new WaitForSecondsRealtime(5f);
        addPlayerMoves();
        hasRes = true;
        PlayerCur.curState = PlayerState.waitPlayMoves;
        DiceLight.gameObject.SetActive(false);
        RollLight.gameObject.SetActive(false);
    }


    // a function for the moves button (takes from player dice results)
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

    // a function for the moves button (takes from player dice results) Second option
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

    // a func that shows the relavant option on the button
    public void WriteButtonA(string s)
    {
        PlayingOptions[0].GetComponentInChildren<Text>().text = s;
    }
    public void WriteButtonB(string s)
    {
        PlayingOptions[1].GetComponentInChildren<Text>().text = s;
    }

    // after dice ans, makes player options available
    public void addPlayerMoves()
    {
        List<int> moves = new List<int>();
        moves.Add(Dices.getA());
        moves.Add(Dices.getB());
        PlayerCur.setPlayableOptions(moves);
        PlayerCur.MovesLeft = 2;
    }

    // player class which helps contol players states and currents stats.
    public class Player
    {

        public int ShabCoins;
        public string PlayerName;
        public int playerColor; // black is 0 white is 1
        public int playerPower; // 1,2,3,4 - Fire, Water, Earth, Wind
        public List<int> PlayableOptions;
        public bool HasFinishedPlaying;
        public int numSoldiers;
        private readonly int StartSoldiers = 15;
        public bool RolledTheDice;
        public int MovesLeft;
        public PlayerState curState;
        private int white = 1;
        private int black = 0;

        public Player(int a, int color)
        {
            HasFinishedPlaying = false;
            ShabCoins = 0;
            playerColor = color;
            addcoins(a);
            playerPower = 0;
            numSoldiers = StartSoldiers;
            RolledTheDice = false;
            if (playerColor == white)
                PlayerName = "White";
            else
                PlayerName = "Black";

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

        public void setPlayableOptions(List<int> moves)
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
}

