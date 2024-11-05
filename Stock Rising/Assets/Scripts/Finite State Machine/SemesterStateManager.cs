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
    public float semesterCount = 0;

    public GameState playerState;
    public TextMeshProUGUI player1OrderText;
    public TextMeshProUGUI player2OrderText;
    public TextMeshProUGUI player3OrderText;

    // Semester 1
    public FirstSemesterState firstSemester = new FirstSemesterState();
    public TextMeshProUGUI semester1Title;

    // Bidding Phase
    public BiddingPhaseState biddingPhase = new BiddingPhaseState();
    public TextMeshProUGUI biddingTitle;
    public GameObject dices; // untuk aktifin dan non-aktifin parent dari dice-dice
    public Button rollDiceButton; // untuk aktifin dan non-aktifin Button Roll
    public GameObject[] players;
    public DiceManagerScript diceManagerScript;
    //debug
    public BPPlayersStopStateDebug bPlayersStopStateDebug;

    // Semester 2
    public SecondSemesterState secondSemester = new SecondSemesterState();



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
