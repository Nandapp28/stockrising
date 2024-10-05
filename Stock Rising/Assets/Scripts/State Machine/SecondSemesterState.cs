using UnityEngine;

public class SecondSemesterState : SemesterBaseState
{
    // concrete states access context and factory
    public SecondSemesterState(SemesterStateMachine currentContext, SemesterStateFactory semesterStateFactory)
    : base(currentContext, semesterStateFactory) { }
    public override void EnterState()
    {
        Debug.Log("Hello dari EnterState: Semester 2");
    }

    public override void UpdateState()
    {
        Debug.Log("Hello dari UpdateState: Semester 2");
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
