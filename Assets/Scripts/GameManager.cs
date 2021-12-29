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
    waitPlayAfterPower,
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
    private GameObject RollField;
    [SerializeField]
    public GameObject PowerDiceOBJ;
    [SerializeField]
    private GameObject RollPowerDiceOBJ;
    [SerializeField]
    public GameObject ShopDiceOBJ;
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
    private GameObject coinsPanel;

    [SerializeField]
    public TileManage T;
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
    public Text NumCheckersA;
    [SerializeField]
    public Text NumCheckersB;
    [SerializeField]
    public TextMesh MiddleText;
    
    [SerializeField]
    public int coinsforDouble=1;
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
    public TextMesh WintextBox;
    public static int turnCount = 0;
    private int playerTurn; // 0 is black , 1 is white
    public int playedOption;
    private string PlayingPlayer;
    public static Player PlayerCur;
    public static Player PlayerOther;
    public PlayerState statePlaying;
    public Player BlackP;
    public Player WhiteP;
    private bool gameOver;
    public bool canEnterShop = false;
    public static RollDice Dices;
    [SerializeField]
    public static PowerDice Powers;
    [SerializeField]
    public static ShopDice Shop;
    private bool Pickedeffect;
    public static bool clicktiles;
    private bool ClickedEffect;
    [SerializeField]
    public EffectManager EffectManage;
    // Start is called before the first frame update

    void Start()
    {
        RollPowerDiceOBJ.SetActive(false);
        ShopDiceOBJ.SetActive(false);
        WintextBox=GameOverText.GetComponent<TextMesh>();
        Pickedeffect = false;
        Dices = Roll.GetComponent<RollDice>();
        Powers = PowerDiceOBJ.GetComponent<PowerDice>();
        MiddleText.text = "";
        Turntext = TurnTextBox.GetComponent<TextMesh>();
        textBox = PlaneText.GetComponent<Text>();
        GamePanel.SetActive(false);
        PowerDiceOBJ.SetActive(false);
        RollField.SetActive(true);
        BlackP = new Player(2, black);
        WhiteP = new Player(2, white);
        Debug.Log("playerA color" + BlackP.playerColor);
        Debug.Log("playerB color" + WhiteP.playerColor);
        EffectPanel.SetActive(true);
        BlackP.curState = PlayerState.StartScreen;
        WhiteP.curState = PlayerState.WaitForTurn;
        gameOver = false;
        clicktiles = false;
        playerTurn = black;
        PlayingPlayer = "Black";
        // need to implement turn decision
        PlayerCur = BlackP;
        PlayerOther = WhiteP;
        PlayerCur.resetRolled();
        WhiteLight.gameObject.SetActive(false);
        BlackLight.gameObject.SetActive(true);
        DiceLight.gameObject.SetActive(false);
        RollLight.gameObject.SetActive(false);
        Refreshcoins();
        T.initTiles();
        T.FillBoard();
    }

    // Update makes the flow of the game whith states
    void Update()
    {
        //Debug.Log("state is " + PlayerCur.curState.ToString());
        switch (PlayerCur.curState)
        {
            case PlayerState.StartScreen:
                Turntext.text = "What is your power?";
                EffectPanel.SetActive(true);
                //Debug.Log("picke effect, effect panel on?.");
                if (Pickedeffect)
                {
                    WhiteP.curState = PlayerState.WaitForTurn;
                    Debug.Log("picked effect.");
                }
                
                
                break;
            case PlayerState.NeedToRoll:
                
                Turntext.text = PlayerCur.getName() + ", please Roll the dice";
                TurnTextBox.SetActive(true);
                MiddleText.text = "Now it's your turn : " + PlayerCur.PlayerName;
                if (Dices.BothLanded())
                {
                    Debug.Log("Both Landed");
                    Turntext.text = "waiting for land";
                    PlayerCur.curState = PlayerState.waitlanded;
                    StartCoroutine(YourTurn());
                    Debug.Log("delay -> coroutine");
                    GamePanel.SetActive(true);
                }
                break;

            case PlayerState.waitlanded:
                if (TileManage.SelectedTile != null)
                {
                    Debug.Log("reset tile in waitlanded");
                    T.GameTiles[TileManage.SelectedTileID].Torch.SetActive(false);
                    ResetTile();
                }
                ClickedEffect = false;
                break;
                
            
            case PlayerState.waitPlayMoves:

                clicktiles = true;
                //RollField.SetActive(false);
                //Debug.Log("waitmoves + Black effect is: "+BlackP.playerPower);
                //Debug.Log("waitmoves + White effect is: " + WhiteP.playerPower);
                if (!ClickedEffect)//&&PlayerCur.PlayableOptions!=null)
                {
                    if (!PlayerCur.doneMoves[0]&&!PlayerCur.doneMoves[1])
                    {
                        if (PlayerCur.Moves[0] == PlayerCur.Moves[1])
                        {
                            PlayerCur.addcoins(coinsforDouble);
                            Refreshcoins();
                            // panel update
                            Powers.initPower();      
                            Debug.Log("Results are identical");
                            PowerDiceOBJ.SetActive(true);
                            PlayerCur.finishedPowerPlaying = false;
                            PlayerCur.EffectReady = true;
                            //ResetTile(); maybe unneccecary
                            GameManager.setPower();
                            Powers.curPower = GameManager.PlayerCur.getPow();
                            ClickedEffect = true; // waiting for press -> PlayPower / press Play
                            //EffectPlay();
                            //PlayerCur.curState = PlayerState.PlayPower;
                        }
                    }
                }
                    

                Turntext.text = " Waiting for " + PlayerCur.getName() + " to Play";
                if (PlayerCur.MovesEnded())
                {
                    Debug.Log("Moves Ended "+PlayerCur.MovesEnded()+"selected tile is "+TileManage.SelectedTileID);

                    PlayerCur.curState = PlayerState.FinishTurn;
                    //turn off panel
                }
                    
                // untill played all options
                //PlayerCur.curState = PlayerState.FinishTurn;
                break;
            case PlayerState.PlayPower:
                //chage back to waitPlayMoves.

                //
                if (PlayerCur.isEffectOver())
                {
                    PlayerCur.curState = PlayerState.waitPlayMoves;
                    ShopDiceOBJ.SetActive(true);
                    Debug.Log("exit effect state. to waitplaymove.");
                }
                break;
            case PlayerState.waitPlayAfterPower:
                //Debug.Log("waitafterpower + rollfield false");

                //Debug.Log("waitmoves + Black effect is: "+BlackP.playerPower);
                //Debug.Log("waitmoves + White effect is: " + WhiteP.playerPower+"movesLeft "+PlayerCur.PlayableOptions.Count);
                GamePanel.SetActive(true);
                Turntext.text = " Waiting for " + PlayerCur.getName() + " to Play";
                if (PlayerCur.MovesEnded())
                {
                    Debug.Log("Moves Ended " + PlayerCur.MovesEnded());
                    Debug.Log("exit WaitafterPower state. to finishturn.");

                    if (TileManage.SelectedTile != null)
                    {
                        
                        TileManage.SelectedTile.Torch.SetActive(false);
                        ResetTile();
                        Debug.Log("turn off fire torch here?");
                    }
                        
                    
                    //TileManage.SelectedTile.FireTorch.SetActive(false);
                    PlayerCur.curState = PlayerState.FinishTurn;
                    //turn off panel
                }

                // untill played all options
                //PlayerCur.curState = PlayerState.FinishTurn;
                break;
            case PlayerState.FinishTurn:
                RollPowerDiceOBJ.SetActive(false);
                turnCount++;
                clicktiles = false;
                ShopDiceOBJ.SetActive(false);
                Turntext.text = PlayerCur.curState.ToString();
                Refreshcoins();
                Dices.FreeTurn = true;
                Debug.Log("Player finished playing");

                
                //RollField.SetActive(true);
                if (WhiteP.numSoldiers < 5 && BlackP.numSoldiers < 5 )
                {
                    GameOverText.SetActive(true);
                    WintextBox.text = "Draw!";
                    PlayerCur.curState = PlayerState.WinGame;
                }
                else if (WhiteP.numSoldiers < 5)
                {
                    GameOverText.SetActive(true);
                    WintextBox.text = "Black Player Wins!";
                    PlayerCur.curState = PlayerState.WinGame;
                }
                else if (BlackP.numSoldiers < 5)
                {
                    GameOverText.SetActive(true);
                    WintextBox.text = "White Player Wins!";
                    PlayerCur.curState = PlayerState.WinGame;
                }
                else
                    changeTurn();

                break;
            case PlayerState.WinGame:
                textBox.text = "Game Over ! Well Played";
                break;
            default:
                Debug.Log("Bad State");
                break;
        }
        

    }

    public void pickEffect()
    {
        Pickedeffect = true; //
        //Debug.Log(EffectManage.BlackPick);
        //Debug.Log(EffectManage.WhitePick);
        // update picked effect
        BlackP.setPower(EffectManage.BlackPick);
        WhiteP.setPower(EffectManage.WhitePick);
        EffectPanel.SetActive(false);
        coinsPanel.SetActive(true);
        PlayingOptions[0].SetActive(false);
        PlayingOptions[1].SetActive(false);
        RollLight.gameObject.SetActive(true);
        PlayerCur.curState = PlayerState.NeedToRoll;
        Debug.Log("Effect panel ok ");
    }

    public void FireOKeffect()
    {
        ClickedEffect = true;
        Debug.Log("Before effect Playerstate is" + PlayerCur.curState);
        T.GameTiles[TileManage.SelectedTileID].FireTorch.SetActive(false);
        T.SetFire(TileManage.SelectedTileID);
        //TileManage.SelectedTile.Torch.SetActive(true);
        EffectManage.HidePowerPanel();
        GamePanel.SetActive(true);
        Debug.Log("okeffect Playerstate from"+ PlayerCur.curState+" to waitPlayAfterPower");
        PlayerCur.finishedPowerPlaying = true;
        ResetTile();
        //RollField.SetActive(false);
        PlayerCur.curState = PlayerState.waitPlayAfterPower;
        ShopDiceOBJ.SetActive(true);
    }

    public void WaterSOKeffect()
    {
        //selected tile saved index for moving.
        
        //GamePanel.SetActive(true);
        Debug.Log("Before effect Playerstate is" + PlayerCur.curState);
        //TileManage.SelectedTile.Torch.SetActive(true);
        
        Powers.WaterShowHide();
        Debug.Log("okeffect Playerstate from" + PlayerCur.curState + " to waitPlayAfterPower");
        T.GameTiles[TileManage.SelectedTileID].WaterTorch.SetActive(false);
        T.GameTiles[TileManage.SelectedTileID].RemovePiece();
        ResetTile();
        

    }
    public void WaterDOKeffect()
    {
        //selected tile saved index for moving.
        if (TileManage.SelectedTileID == -1)
        {
        }
        else
        {
            Debug.Log("Before effect Playerstate is" + PlayerCur.curState);
            T.GameTiles[TileManage.SelectedTileID].WaterTorch.SetActive(false);
            T.GameTiles[TileManage.SelectedTileID].AddPiece(PlayerCur.playerColor);
            EffectManage.HidePowerPanel();
            GamePanel.SetActive(true);
            Debug.Log("okeffect Playerstate from" + PlayerCur.curState + " to waitPlayAfterPower");
            PlayerCur.finishedPowerPlaying = true;
            ResetTile();
            //RollField.SetActive(false);
            ShopDiceOBJ.SetActive(true);
            PlayerCur.curState = PlayerState.waitPlayAfterPower;
        }
    }
    public void WindOKeffect()
    {
        if (TileManage.SelectedTileID == -1)
        {
        }
        else
        {
            
            int dest = T.canMoveStepsback((PlayerCur.playerColor + 1) % 2, TileManage.SelectedTileID, -1);
            Debug.Log("canmove dest for air is " + dest+ "src is "+ TileManage.SelectedTileID);
            if(dest!=-1)
            {
                //move piece
                //cancel button
                T.GameTiles[TileManage.SelectedTileID].RemovePiece();
                T.GameTiles[TileManage.SelectedTileID].AirTorch.SetActive(false);
                T.GameTiles[dest].AddPiece((PlayerCur.playerColor + 1) % 2);
                EffectManage.HidePowerPanel();
                PlayerCur.finishedPowerPlaying = true;
                ResetTile();
                Debug.Log("Before effect Playerstate is" + PlayerCur.curState);
                //TileManage.SelectedTile.Torch.SetActive(true);
                Debug.Log("okeffect Playerstate from" + PlayerCur.curState + " to waitPlayAfterPower");
                //RollField.SetActive(false);
                ShopDiceOBJ.SetActive(true);
                PlayerCur.curState = PlayerState.waitPlayAfterPower;
            }
        }

        
    }
    public void EarthOKeffect()
    {
        //selected tile saved index for moving.
        if(T.getSelectedTile()!=null)
        {
            T.GameTiles[TileManage.SelectedTileID].EarthTorch.SetActive(false);
            T.GameTiles[TileManage.SelectedTileID].AddPiece(PlayerCur.playerColor);
            PlayerCur.numSoldiers ++;
            Debug.Log("Before effect Playerstate is" + PlayerCur.curState);
            //TileManage.SelectedTile.Torch.SetActive(true);
            EffectManage.HidePowerPanel();
            GamePanel.SetActive(true);
            PlayerCur.finishedPowerPlaying = true;
            ResetTile();
            Debug.Log("Earth okeffect Playerstate from" + PlayerCur.curState + " to waitPlayAfterPower");
            //RollField.SetActive(false);
            ShopDiceOBJ.SetActive(true);
            PlayerCur.curState = PlayerState.waitPlayAfterPower;
        }
    }


    // func to change between the turns
    public void changeTurn()
    {
        int nowplaying = playerTurn;
        PlayerOther.RolledTheDice = false;
        refreshLives();
        if (nowplaying == white)
        {
            PlayerCur = BlackP;
            PlayerOther = WhiteP;
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
            PlayerOther = BlackP;
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
        yield return new WaitForSecondsRealtime(1.3f);
        //yield return new WaitUntil(()=>Dices.Landed());
        Debug.Log("finish DiceLand");
        textBox.text = "Landed";
        PlayerCur.curState = PlayerState.waitPlayMoves;
        canEnterShop = true;
        ShopDiceOBJ.SetActive(true);
        DiceLight.gameObject.SetActive(false);
        PlayingOptions[0].SetActive(true);
        PlayingOptions[1].SetActive(true);
        Dices.pressed = false;
        addPlayerMoves();
    }


    // a function for the moves button (takes from player dice results)
    public void PlayerMoveA()
    {
        Debug.Log("moveA");
        if (TileManage.SelectedTileID == -1)
        {
            Debug.Log("moveA without an id");
        }
        else if(!PlayerCur.doneMoves[0])
        {
            Debug.Log("Move A-pressed " + PlayerCur.playerColor + " selected tile id is " + TileManage.SelectedTileID);
            Debug.Log("T.gettileid " + T.getTileID() + "PlayerCur.getOptions()[0] " + PlayerCur.Moves[0]);
            playedOption = 0;
            int dest = T.canMoveSteps(PlayerCur.playerColor, T.getTileID(), PlayerCur.Moves[0]);
            Debug.Log("dest is " + dest);
            if (dest != -1)
            {
                T.MoveCheckers(PlayerCur.playerColor, T.getTileID(), dest); //PlayerCur.getOptions()[1]
                PlayerCur.doneMoves[0] = true;
                refreshButtons();
                if (PlayerCur.doneMoves[0] && PlayerCur.doneMoves[1] && T.getTileID() != -1)
                {
                    T.GameTiles[T.getTileID()].Torch.SetActive(false);
                    ResetTile();
                }
            }
            
        }
    }

    // a function for the moves button (takes from player dice results) Second option
    public void PlayerMoveB()
    {
        Debug.Log("moveB");
        // player cur has false false { 3, 5 }
        if (TileManage.SelectedTileID == -1)
        {
            Debug.Log("moveB without an id");
        }
        else if(!PlayerCur.doneMoves[1])
        {
            Debug.Log("!PlayerCur.doneMoves[1] , option B is" + PlayerCur.Moves[1]);
            int dest = T.canMoveSteps(PlayerCur.playerColor, T.getTileID(), PlayerCur.Moves[1]);
            if (dest != -1)
            {
                T.MoveCheckers(PlayerCur.playerColor, T.getTileID(), dest);
                PlayerCur.doneMoves[1] = true;
                refreshButtons();
                if (PlayerCur.doneMoves[0] && PlayerCur.doneMoves[1]&&T.getTileID()!=-1)
                {
                    T.GameTiles[T.getTileID()].Torch.SetActive(false);
                    ResetTile();
                }
            }
            else
                Debug.Log("dest is -1");
        }
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
        Dices.UpdateRes();
        List<int> moves = new List<int>();
        moves.Add(Dices.getA());
        moves.Add(Dices.getB());
        Debug.Log("dices results took : " + moves[0] + " and " + moves[1]);
        PlayerCur.makeMoves(moves);
        
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
        public bool[] doneMoves = new bool [2];
        public int[] Moves;
        

        public Player(int a, int color)
        {
            Moves = new int[2];
            doneMoves[0]=true;
            doneMoves[1] = true;
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
            //Debug.Log("movesEnded: finished powerPlaying" + PlayerCur.finishedPowerPlaying.ToString());
            //Debug.Log("movesEnded: Playableoptions count: " + PlayableOptions.Count);
            return (PlayerCur.doneMoves[0]&&PlayerCur.doneMoves[1]) && PlayerCur.finishedPowerPlaying;
        }
        public int getPow()
        {
            return playerPower;
        }

        public void DecreaseSoldier()
        {
            numSoldiers --;
        }

        public void makeMoves(List<int> movesGot)
        {
            int[] moves = new int[2];
            bool[] moved = { false, false };
            moves[0] = movesGot[0];
            moves[1] = movesGot[1];
            doneMoves = moved;
            Moves = moves;
        }
    }

    public Player getPlayer(int color)
    {
        if (color == 0)
            return BlackP;
        else
            return WhiteP;
    }

    

    public void WritePanel(string s)
    {
        textBox.text = s;
    }

    public bool NeedRoll()
    {
        return (PlayerCur.getState() == PlayerState.NeedToRoll);
    }

    public bool NeedMove()
    {
        return (PlayerCur.getState() == PlayerState.waitPlayMoves) || (PlayerCur.getState() == PlayerState.waitPlayAfterPower) || (PlayerCur.getState() == PlayerState.PlayPower);
    }

    public void StateEffect()
    {
        PlayerCur.curState = PlayerState.PlayPower;
        Debug.Log("State changed to "+PlayerState.PlayPower.ToString());
    }

    public static bool isPowering()
    {
        return PlayerCur.curState == PlayerState.PlayPower;
    }

    public void powerState()
    {
        PlayerCur.curState = PlayerState.PlayPower;
    }
    public static void setPower()
    {
        TileManage.powerTiles=PlayerCur.playerPower;
    }

    public static void ResetTile()
    {
        TileManage.SelectedTile = null;
        TileManage.SelectedTileID = -1;
        //T.GameTiles[TileManage.SelectedTileID].
    }

    public void refreshLives()
    {
        NumCheckersA.text = "pieces: "+BlackP.numSoldiers;
        NumCheckersB.text = "pieces: "+WhiteP.numSoldiers;
    }

    public void refreshButtons()
    {
        PlayingOptions[0].SetActive(true);
        PlayingOptions[1].SetActive(true);
        if (PlayerCur.doneMoves[0])
            PlayingOptions[0].SetActive(false);
        if(PlayerCur.doneMoves[1])
            PlayingOptions[1].SetActive(false);
    }

    public void Refreshcoins()
    {
        CoinsAText.text = "Black - Shablul Coins : " + BlackP.ShabCoins;
        CoinsBText.text = "White - Shablul Coins : " + WhiteP.ShabCoins;
    }
}

