using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using UnityEngine;
using static BiddingPhaseState;

public class BiddingPhaseState : SemesterBaseState
{
    float timeEnterCD = 2.0f;
    float timeSortCD = 2.0f;
    float timeNextStateCD = 2.0f;

    PlayerScript currentPlayerScript;
    List<PlayerOrderNum> playerOrderNums = new List<PlayerOrderNum>();


    public override void EnterState(SemesterStateManager semester)
    {
        semester.phaseCount += 1;
        semester.phaseTitleParent.gameObject.SetActive(true);
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        SetInitialize(semester);
        switch (semester.playerState)
        {
            case GameState.Player1Turn:
                Debug.Log("Player 1's Turn");
                if (semester.diceManagerScript.CountDiceResult() != 0)
                {
                    currentPlayerScript = semester.players[0].GetComponent<PlayerScript>();
                    currentPlayerScript.playerOrder = semester.diceManagerScript.diceResult;
                    PlayerOrderNum playerOrderNum = new PlayerOrderNum
                    {
                        orderNum = currentPlayerScript.playerOrder,
                        playerGameObject = semester.players[0]
                    };
                    playerOrderNums.Add(playerOrderNum);
                    ResetDice(semester);
                    semester.playerState = GameState.Player2Turn;
                }
                break;
            case GameState.Player2Turn:
                Debug.Log("Player 2's Turn");
                if (semester.diceManagerScript.CountDiceResult() != 0)
                {
                    currentPlayerScript = semester.players[1].GetComponent<PlayerScript>();
                    currentPlayerScript.playerOrder = semester.diceManagerScript.diceResult;
                    PlayerOrderNum playerOrderNum = new PlayerOrderNum
                    {
                        orderNum = currentPlayerScript.playerOrder,
                        playerGameObject = semester.players[1]
                    };
                    playerOrderNums.Add(playerOrderNum);
                    ResetDice(semester);
                    semester.playerState = GameState.Player3Turn;
                }
                break;
            case GameState.Player3Turn:
                Debug.Log("Player 3's Turn");
                if (semester.diceManagerScript.CountDiceResult() != 0)
                {
                    currentPlayerScript = semester.players[2].GetComponent<PlayerScript>();
                    currentPlayerScript.playerOrder = semester.diceManagerScript.diceResult;
                    PlayerOrderNum playerOrderNum = new PlayerOrderNum
                    {
                        orderNum = currentPlayerScript.playerOrder,
                        playerGameObject = semester.players[2]
                    };
                    playerOrderNums.Add(playerOrderNum);
                    ResetDice(semester);
                    semester.playerState = GameState.PlayersStop;
                }
                break;
            case GameState.PlayersStop: 
                Debug.Log("Players Stop");

                semester.rollDiceButton.enabled = false;

                CheckDuplicates(semester);
                playerOrderNums = playerOrderNums.OrderByDescending(pair => pair.orderNum).ToList();

                // setelah player terakhir lempar dadu, kasih jeda 2 detik
                if (timeSortCD >= 0)
                {
                    timeSortCD -= Time.deltaTime;
                }
                else
                {
                    // set urutan player
                    for (int i = 0; i < playerOrderNums.Count; i++)
                    {
                        PlayerScript playerScript = playerOrderNums[i].playerGameObject.GetComponent<PlayerScript>();
                        playerScript.playerOrder = i + 1;
                    }
                    // deactivate the dices and button
                    semester.dices.SetActive(false);
                    semester.rollDiceButton.gameObject.SetActive(false);
                    if (timeNextStateCD >= 0)
                    {
                        timeNextStateCD -= Time.deltaTime;
                    } else
                    {
                        semester.SwitchState(semester.actionPhase);
                    }
                }
                break;

        }
    }

    void SetInitialize(SemesterStateManager semester)
    {
        if (timeEnterCD >= 0)
        {
            timeEnterCD -= Time.deltaTime;
        }
        else
        {
            semester.phaseTitleParent.gameObject.SetActive(false);

            // objek Bidding Phase
            semester.dices.SetActive(true);
            semester.rollDiceButton.gameObject.SetActive(true);
        }
    }

    void ResetDice(SemesterStateManager semester)
    {
        semester.diceManagerScript.dice1.indexResult = 0;
        semester.diceManagerScript.dice2.indexResult = 0;
        semester.rollDiceButton.GetComponent<RollButtonScript>().isClicked = false;
    }

    public class PlayerOrderNum
    {
        public float orderNum { get; set; }
        public GameObject playerGameObject { get; set; }
    }

    void CheckDuplicates(SemesterStateManager semester)
    {
        for (int i = 1; i < playerOrderNums.Count; i++)
        {
            if (playerOrderNums[i].orderNum == (float)Mathf.CeilToInt(playerOrderNums[i-1].orderNum))
            {
                playerOrderNums[i].orderNum -= 0.5f;
                if (playerOrderNums[i].orderNum == playerOrderNums[i - 1].orderNum)
                {
                    playerOrderNums[i].orderNum -= 0.5f;
                }
            }
        }
    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
