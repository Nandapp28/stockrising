using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public enum GameState { Player1Turn, Player2Turn, Player3Turn };
    public GameState currentState;

    public GameObject[] players;
    public float moveDistance = 1f;
    public float finishLine = 5f;

    void Start()
    {
        currentState = GameState.Player1Turn;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        Debug.Log(players.Length);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentState)
            {
                case GameState.Player1Turn:
                    MovePlayer(0);
                    currentState = GameState.Player2Turn;
                    break;
                case GameState.Player2Turn:
                    MovePlayer(1);
                    currentState = GameState.Player3Turn;
                    break;
                case GameState.Player3Turn:
                    MovePlayer(2);
                    currentState = GameState.Player1Turn;
                    break;
            }
        }

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].transform.position.z >= finishLine)
            {
                Debug.Log("Player " + (i + 1) + " menang!!!");
            }
        }
    }

    void MovePlayer(int playerIndex)
    {
        GameObject playerObject = players[playerIndex];
        Rigidbody playerRigidbody = playerObject.GetComponent<Rigidbody>();

        Vector3 moveDirection = players[playerIndex].transform.forward * moveDistance;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDirection);
    }
}
