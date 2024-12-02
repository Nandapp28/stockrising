using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static BiddingPhaseState;

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

    // Bidding Phase
    [Header("Bidding Phase")]
    public BiddingPhaseState biddingPhase = new BiddingPhaseState();
    public GameObject dices; // untuk aktifin dan non-aktifin parent dari dice-dice
    public Button rollDiceButton; // untuk aktifin dan non-aktifin Button Roll
    public DiceManagerScript diceManagerScript;

    // Action Phase
    [Header("Action Phase")]
    public ActionPhaseState actionPhase = new ActionPhaseState();
    public GameObject actionCardsObj;
    public GameObject actionCardManagerObj;
    public bool actionCardisSaved = false;
    public bool actionCardisActivated = false;
    public GameObject rumorCardButton;

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

    public int CheckPlayerOrder(int playerOrderReq)
    {
        foreach (var player in players)
        {
            int index = 0;
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            if (playerScript.playerOrder == playerOrderReq)
            {
                return index;
            } else
            {
                index += 1;
            }
        }
        return -1;
    }
}
