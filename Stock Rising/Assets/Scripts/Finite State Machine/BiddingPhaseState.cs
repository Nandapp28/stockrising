using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BiddingPhaseState;

public class BiddingPhaseState : SemesterBaseState
{
    float timeCountDown = 2.0f;
    PlayerScript currentPlayerScript;
    List<PlayerOrderNum> playerOrderNums = new List<PlayerOrderNum>();


    public override void EnterState(SemesterStateManager semester)
    {
        semester.biddingTitle.gameObject.SetActive(true);
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
                CheckDuplicates(semester);
                playerOrderNums = playerOrderNums.OrderByDescending(pair => pair.orderNum).ToList();

                for (int i = 0; i < playerOrderNums.Count; i++)
                {
                    PlayerScript playerScript = playerOrderNums[i].playerGameObject.GetComponent<PlayerScript>();
                    playerScript.playerOrder = i + 1;
                }
                break;

        }
    }

    void SetInitialize(SemesterStateManager semester)
    {
        if (timeCountDown >= 0)
        {
            timeCountDown -= Time.deltaTime;
        }
        else
        {
            semester.biddingTitle.gameObject.SetActive(false);
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
