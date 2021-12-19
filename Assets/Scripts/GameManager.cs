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
    PlayPower,
    EnteredShop,
    FinishTurn,
    WinGame,
    waitlanded
}
public class GameManager : MonoBehaviour
{
    //requiered objects

    [SerializeField]
    private GameObject Roll;
    [SerializeField]
    private GameObject PowerDiceOBJ;
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
    public GameObject[] PlayingOptions;
    [SerializeField]
    public GameObject GamePanel;
    [SerializeField]
    private GameObject DiceField;
    [SerializeField]
    public Light DiceLight;
    [SerializeField]
    public Light RollLight;
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
    public const int Fire = 1;
    public const int Water = 2;
    public const int Earth = 3;
    public const int Wind = 4;


    //private string playerTurn;
    [SerializeField]
    //private TileManage t;
    public static Text textBox;
    public TextMesh Turntext;

    public static int turnCount = 0;
    private int playerTurn; // 0 is black , 1 is white
    public int playedOption;
    private string PlayingPlayer;
    public static Player PlayerCur;
    public PlayerState statePlaying;
    private Player BlackP;
    private Player WhiteP;
    private bool gameOver;
    public static RollDice Dices;
    [SerializeField]
    public static PowerDice Powers;
    private bool Pickedeffect;
    private bool hasRes;
    [SerializeField]
    public EffectManager EffectManage;
    // Start is called before the first frame update




    void Start()
    {
        Dices = Roll.GetComponent<RollDice>();
        MiddleText.text = "";
        Turntext = TurnTextBox.GetComponent<TextMesh>();
        textBox = PlaneText.GetComponent<Text>();
        GamePanel.SetActive(false);
        PowerDiceOBJ.SetActive(false);

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
                
                PlayingOptions[0].SetActive(false);
                PlayingOptions[1].SetActive(false);
                Turntext.text = PlayerCur.getName() + ", please Roll the dice";
                TurnTextBox.SetActive(true);
                MiddleText.text = "Now it's your turn : " + PlayerCur.PlayerName;
                if (Dices.BothLanded())
                {
                    Debug.Log("Both Landed");
                    Turntext.text = "waiting for land";
                    PlayerCur.curState = PlayerState.waitlanded;
                    StartCoroutine(YourTurn());
                    Debug.Log("bye coroutine");

                }
                break;

            case PlayerState.waitlanded:
                if (Dices.BothLanded())
                    PlayerCur.curState = PlayerState.waitPlayMoves;
                break;
            case PlayerState.PlayPower:
                //chage back to waitPlayMoves.
                EffectPlay();
                if(PlayerCur.isEffectOver())
                {
                    PlayerCur.curState = PlayerState.waitPlayMoves;
                }
                break;
            case PlayerState.waitPlayMoves:
                
                if(PlayerCur.PlayableOptions.Count>1)
                {
                    if (PlayerCur.PlayableOptions[0] == PlayerCur.PlayableOptions[1])
                    {
                        PowerDiceOBJ.SetActive(true);
                        PlayerCur.finishedPowerPlaying = false;
                        Powers.curPower = PlayerCur.playerPower;

                    //PlayerCur.curState = PlayerState.PlayPower;
                    }
                }
                    

                Turntext.text = " Waiting for " + PlayerCur.getName() + " to Play";
                if (PlayerCur.MovesEnded())
                {
                    PlayerCur.curState = PlayerState.FinishTurn;
                    TileManage.SelectedTile.Torch.SetActive(false);
                    //turn off panel
                }
                    
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

    private void EffectPlay()
    {
        PowerDiceOBJ.SetActive(true);
        PlayerCur.EffectReady = false;
        PlayerCur.finishedPowerPlaying = false;
    }

    public void OKeffect()
    {
        
        switch(PlayerCur.playerPower)
        {
            case Fire:
                Debug.Log("case fire ok effect");
                TileManage.powerTiles = Fire;
                T.SetFire(TileManage.SelectedTileID);
                break;
            case Water:
                TileManage.powerTiles = Water;
                break;
        }
    }


    // func to change between the turns
    public void changeTurn()
    {
        int nowplaying = playerTurn;
        turnCount++;
        if (nowplaying == white)
        {
            PlayerCur = BlackP;
            playerTurn = black;
            PlayingPlayer = "Black";
            WhiteP.curState = PlayerState.WaitForTurn;
            BlackP.curState = PlayerState.NeedToRoll;
            RollLight.gameObject.SetActive(true);
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
            RollLight.gameObject.SetActive(true);
            WhiteLight.gameObject.SetActive(true);
            BlackLight.gameObject.SetActive(false);
        }
    }

    // IEnumerator to hold the player untill dice has been rolled.
    public IEnumerator YourTurn()
    {
        Debug.Log("wait 1 sec + DiceLand");
        yield return new WaitForSecondsRealtime(4f);
        //yield return new WaitUntil(()=>Dices.Landed());
        Debug.Log("finish DiceLand");
        
        textBox.text = "Landed";
        hasRes = true;
        PlayerCur.curState = PlayerState.waitPlayMoves;
        DiceLight.gameObject.SetActive(false);
        PlayingOptions[0].SetActive(true);
        PlayingOptions[1].SetActive(true);
        Dices.pressed = false;
        addPlayerMoves();
    }


    // a function for the moves button (takes from player dice results)
    public void PlayerMoveA()
    {
        Debug.Log("Move A-pressed " + PlayerCur.playerColor + "selected tile id is "+TileManage.SelectedTileID);
        Debug.Log("T.gettileid " + T.getTileID()+ "PlayerCur.getOptions()[0] "+ PlayerCur.getOptions()[0]);
        playedOption = 0;
        if (PlayerCur.PlayableOptions.Count == 2)
        {
            int dest = T.canMoveSteps(PlayerCur.playerColor, T.getTileID(), PlayerCur.getOptions()[0]);
            Debug.Log("dest is " + dest);
            if (dest != -1)
            {
                T.MoveCheckers(PlayerCur.playerColor, T.getTileID(), dest); //PlayerCur.getOptions()[1]
                PlayerCur.PlayableOptions.RemoveAt(0);
            }
        }
        else
        {
            int dest = T.canMoveSteps(PlayerCur.playerColor, T.getTileID(), PlayerCur.getOptions()[0]);
            Debug.Log("dest is " + dest);
            if (dest != -1)
            {
                T.MoveCheckers(PlayerCur.playerColor, T.getTileID(),dest);
                PlayerCur.PlayableOptions.RemoveAt(0);
            }
        }

        Debug.Log("finish Move A-pressed player color: " + PlayerCur.playerColor);

    }

    // a function for the moves button (takes from player dice results) Second option
    public void PlayerMoveB()
    {
        Debug.Log("Move B-pressed " + PlayerCur.playerColor);
        playedOption = 1;
        if (PlayerCur.PlayableOptions.Count == 2)
        {
            Debug.Log("count 2, option B is"+ PlayerCur.getOptions()[1]);
            int dest = T.canMoveSteps(PlayerCur.playerColor, T.getTileID(), PlayerCur.getOptions()[1]);
            if (dest!=-1)
            {
                T.MoveCheckers(PlayerCur.playerColor, T.getTileID(), dest); //PlayerCur.getOptions()[1]
                PlayerCur.PlayableOptions.RemoveAt(1);
            }
        }
        else
        {
            Debug.Log("count !=2, option B is "+ PlayerCur.getOptions()[0]);
            int dest = T.canMoveSteps(PlayerCur.playerColor, T.getTileID(), PlayerCur.getOptions()[0]);
            if (dest != -1)
            {
                T.MoveCheckers(PlayerCur.playerColor, T.getTileID(), dest);
                PlayerCur.PlayableOptions.RemoveAt(0);
            }
        }
        Debug.Log("finish Move B-pressed " + PlayerCur.playerColor);

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
        public bool EffectReady;
        public bool finishedPowerPlaying;

        public Player(int a, int color)
        {
            HasFinishedPlaying = false;
            ShabCoins = 0;
            playerColor = color;
            addcoins(a);
            playerPower = 0;
            numSoldiers = StartSoldiers;
            RolledTheDice = false;
            EffectReady = false;
            finishedPowerPlaying = true;
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

        public bool isEffectOver()
        {
            return finishedPowerPlaying;
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
            Debug.Log("new moves added"+moves.ToArray().ToString());
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
            return this.PlayableOptions.Count == 0 && this.finishedPowerPlaying;
        }
        
    }

  

    public void pickEffect()
    {
        Pickedeffect = true; //
        BlackP.setPower(EffectManage.BlackPick);
        WhiteP.setPower(EffectManage.WhitePick);
        EffectPanel.SetActive(false);
        GamePanel.SetActive(true);
        PlayerCur.curState = PlayerState.NeedToRoll;
        RollLight.gameObject.SetActive(true);
        Debug.Log("Effect panel ok ");
    }

    public void WritePanel(string s)
    {
        textBox.text = s;
    }

    public bool NeedRoll()
    {
        return (PlayerCur.getState() == PlayerState.NeedToRoll);
    }

    public void StateEffect()
    {
        PlayerCur.curState = PlayerState.PlayPower;
        Debug.Log("State changed to "+PlayerState.PlayPower.ToString());
    }

    public void OkFire()
    {

    }

}

