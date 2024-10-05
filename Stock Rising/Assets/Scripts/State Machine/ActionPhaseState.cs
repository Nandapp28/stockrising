using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPhaseState : SemesterBaseState
{
    // concrete states access context and factory
    public ActionPhaseState(SemesterStateMachine currentContext, SemesterStateFactory semesterStateFactory)
    : base(currentContext, semesterStateFactory) { }
    public override void EnterState()
    {

    }

    public override void UpdateState()
    {

    }

    public override void OnCollisionEnter()
    {

    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {

    }

    public override void InitializeSubState()
    {

    }
}
