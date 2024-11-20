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
    public Image phaseTitleParent;
    public GameState playerState;

    // Semester 1
    [Header("Semester 1")]
    public FirstSemesterState firstSemester = new FirstSemesterState();
    public TextMeshProUGUI semester1Title;

    // Semester 2
    [Header("Semester 2")]
    public SecondSemesterState secondSemester = new SecondSemesterState();

    // Bidding Phase
    [Header("Bidding Phase")]
    public BiddingPhaseState biddingPhase = new BiddingPhaseState();
    public TextMeshProUGUI biddingTitle;
    public GameObject dices; // untuk aktifin dan non-aktifin parent dari dice-dice
    public Button rollDiceButton; // untuk aktifin dan non-aktifin Button Roll
    public GameObject[] players;
    public DiceManagerScript diceManagerScript;

    [Header("Action Phase")]
    public ActionPhaseState actionPhase = new ActionPhaseState();

    //debug
    public BPPlayersStopStateDebug bPlayersStopStateDebug;

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

}
