using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<PlayerController> players;
    private int currentPlayerIndex = 0;

    void Awake()
    {
        instance = this;
        players[currentPlayerIndex].isTurn = true;
    }

    void Update()
    {
        //Debug.Log(players.Count);
    }

    public void NextTurn()
    {
        Debug.Log(players[currentPlayerIndex] + "====" + players[currentPlayerIndex].isTurn);
        currentPlayerIndex = currentPlayerIndex + 1;
        if (currentPlayerIndex == players.Count)
        {
            currentPlayerIndex = 0;
        }

        players[currentPlayerIndex].isTurn = true;
        Debug.Log(players[currentPlayerIndex] + "====" + players[currentPlayerIndex].isTurn);
    }
}
