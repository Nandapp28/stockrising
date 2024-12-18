using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static BiddingPhaseState;

public class BiddingPhaseState : SemesterBaseState
{
    float timeEnterCD = 2.0f;
    float timeSortCD = 2.0f;
    float timeNextStateCD = 2.0f;

    // FindPlayerTurn() needed
    List<string> playerNames = new List<string>();
    GameObject playerTurn;
    bool isPlayerTurnFound = false;

    PlayerScript currentPlayerScript;
    List<PlayerOrderNum> playerOrderNums = new List<PlayerOrderNum>();


    public override void EnterState(SemesterStateManager semester)
    {
        semester.phaseCount += 1;
        semester.phaseName = "Fase Bidding";
        semester.phaseTitleParent.gameObject.SetActive(true);
        //semester.divinationTokenManagerScript.FlipDivToken(semester);

        foreach (GameObject player in semester.players)
        {
            //PlayerScript playerScript = player.GetComponent<PlayerScript>();
            string playerName = player.name;
            playerNames.Add(playerName);
        }

        SetVariables();
    }

    void SetVariables()
    {
        timeEnterCD = 2.0f;
        timeSortCD = 2.0f;
        timeNextStateCD = 2.0f;
        isPlayerTurnFound = false;
        playerOrderNums = new List<PlayerOrderNum>();
        playerNames = new List<string>();
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        Debug.Log(playerNames.Count);
        SetInitialize(semester);
        switch (semester.playerState)
        {
            case GameState.Player1Turn:
                Debug.Log("Player 1's Turn");
                CoreBiddingPhase(semester, FindPlayerTurn(semester, 1));
                break;
            case GameState.Player2Turn:
                Debug.Log("Player 2's Turn");
                CoreBiddingPhase(semester, FindPlayerTurn(semester, 2));
                break;
            case GameState.Player3Turn:
                Debug.Log("Player 3's Turn");
                CoreBiddingPhase(semester, FindPlayerTurn(semester, 3));
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
                        semester.rollDiceButton.enabled = true;
                        semester.SwitchState(semester.actionPhase);
                    }
                }
                break;

        }
    }

    private void CoreBiddingPhase(SemesterStateManager semester, GameObject playerObj)
    {
        if (semester.diceManagerScript.CountDiceResult() != 0)
        {
            currentPlayerScript = playerObj.GetComponent<PlayerScript>();
            currentPlayerScript.playerOrder = semester.diceManagerScript.diceResult;
            PlayerOrderNum playerOrderNum = new PlayerOrderNum
            {
                orderNum = currentPlayerScript.playerOrder,
                playerGameObject = playerObj
            };
            playerOrderNums.Add(playerOrderNum);
            ResetDice(semester);
            isPlayerTurnFound = false;
            semester.SwitchPlayerState();
        }
    }

    private GameObject FindPlayerTurn(SemesterStateManager semester, int orderIndex)
    {
        if (isPlayerTurnFound == false)
        {
            //PlayerScript player0Script = semester.players[0].GetComponent<PlayerScript>();
            if (semester.semesterCount == 1)
            {
                string playerName = playerNames[Random.Range(0, playerNames.Count - 1)];
                foreach (GameObject index in semester.players)
                {
                    //PlayerScript indexScript = index.GetComponent<PlayerScript>();
                    if (index.name == playerName)
                    {
                        playerTurn = index;
                        playerNames.Remove(playerName);
                        isPlayerTurnFound = true;
                        return playerTurn;
                    }
                }
            }
            else
            {
                playerTurn = semester.CheckPlayerOrder(orderIndex);
                isPlayerTurnFound = true;
                return playerTurn;
            }
        }
        return playerTurn;
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
