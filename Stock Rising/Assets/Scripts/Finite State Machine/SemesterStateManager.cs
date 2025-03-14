using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static BiddingPhaseState;
using System;

public enum GameState { Player1Turn, Player2Turn, Player3Turn, PlayersStop };

public class SemesterStateManager : MonoBehaviour
{
    public SemesterBaseState currentState;

    [Header("Game Manager")]
    public float semesterCount = 0;
    public int phaseCount = 0;
    public string phaseName;
    public Image phaseTitleParent;
    public GameState playerState;

    public Camera mainCamera;
    public GameObject cameraPost1;
    public GameObject cameraPost2;
    public GameObject transparantBgObj;

    public GameObject[] players;

    // Semester 1
    [Header("Semester 1")]
    public FirstSemesterState firstSemester = new FirstSemesterState();

    // Semester 2
    [Header("Semester 2")]
    public SecondSemesterState secondSemester = new SecondSemesterState();

    // Semester 3
    [Header("Semester 3")]
    public ThirdSemesterState thirdSemester = new ThirdSemesterState();

    // Semester 4
    [Header("Semester 4")]
    public FourthSemesterState fourthSemester = new FourthSemesterState();

    // Game End
    [Header("Game End State")]
    public GameEndState gameEnd = new GameEndState();

    // Bidding Phase
    [Header("Bidding Phase")]
    public BiddingPhaseState biddingPhase = new BiddingPhaseState();
    public GameObject dices; // untuk aktifin dan non-aktifin parent dari dice-dice
    public Button rollDiceButton; // untuk aktifin dan non-aktifin Button Roll
    public DiceManagerScript diceManagerScript;
    public DivinationTokenManagerScript divinationTokenManagerScript;

    // Action Phase
    [Header("Action Phase")]
    public ActionPhaseState actionPhase = new ActionPhaseState();
    public GameObject actionCardsObj;
    public GameObject actionCardManagerObj;
    public bool actionCardisSaved = false;
    public bool actionCardisActivated = false;
    public GameObject rumorCardButton;
    public bool coreActionPhaseIsDone = false;
    public bool helpCardIsDone = false;
    public GameObject buttonHelpCard;

    // Sales Phase
    public SalesPhaseState salesPhase = new SalesPhaseState();
    public GameObject salesPhaseButton;
    public bool isSalesPhaseSkip = false;
    public bool isSalesPhaseSell = false;
    public TextMeshProUGUI salesPhaseTimerText;

    // Rumor Phase
    public RumorPhaseState rumorPhase = new RumorPhaseState();

    // Resolution Phase
    public ResolutionPhaseState resolutionPhase = new ResolutionPhaseState();

    //debug
    //public BPPlayersStopStateDebug bPlayersStopStateDebug;

    void Start()
    {
        currentState = firstSemester;
        currentState.EnterState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(SemesterBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void SwitchSemester()
    {
        SemesterBaseState[] states = { firstSemester, secondSemester, thirdSemester, fourthSemester, gameEnd };
        int index = (int)semesterCount;
        SwitchState(states[index]);
    }

    public GameState SwitchPlayerState()
    {
        if (playerState == (GameState)0)
        {
            return playerState = (GameState)1;
        }
        else if (playerState == (GameState)1)
        {
            return playerState = (GameState)2;
        }
        else if (playerState == (GameState)2)
        {
            return playerState = (GameState)3;
        }
        return playerState = (GameState)0;
    }

    public void SemesterInitialization()
    {
        semesterCount += 1;
        GameObject[] divinationTokens = GameObject.FindGameObjectsWithTag("Divination Token");
        foreach (var divinationToken in divinationTokens)
        {
            divinationToken.gameObject.SetActive(false);
        }
        divinationTokenManagerScript.FlipDivToken(this.GetComponent<SemesterStateManager>());
        foreach (var divinationToken in divinationTokens)
        {
            divinationToken.gameObject.SetActive(true);
        }
        SwitchState(biddingPhase);
    }

    public GameObject CheckPlayerOrder(int playerOrderReq)
    {
        foreach (var player in players)
        {
            int index = 0;
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            if (playerScript.playerOrder == playerOrderReq)
            {
                return player;
            } else
            {
                index += 1;
            }
        }
        return null;
    }

    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}
