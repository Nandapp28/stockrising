using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndState : SemesterBaseState
{
    public override void EnterState(SemesterStateManager semester)
    {
        
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        Debug.Log("Update from GameEndState");
    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
