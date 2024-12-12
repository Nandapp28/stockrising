using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionPhaseState : SemesterBaseState
{
    public override void EnterState(SemesterStateManager semester)
    {
        semester.phaseCount += 1;
        semester.phaseName = "Fase Resolusi";
        semester.phaseTitleParent.gameObject.SetActive(true);
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        Debug.Log("Update from Resolution Phase State");
        switch (semester.playerState)
        {
            case GameState.Player1Turn:
                Debug.Log("Player 1's Turn " + "Sekarang adalah giliran ");
                break;

            case GameState.Player2Turn:
                Debug.Log("Player 2's Turn " + "Sekarang adalah giliran ");
                break;

            case GameState.Player3Turn:
                Debug.Log("Player 3's Turn " + "Sekarang adalah giliran ");
                break;

            case GameState.PlayersStop:
                Debug.Log("PlayerStop's turn");
                break;
        }
    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
