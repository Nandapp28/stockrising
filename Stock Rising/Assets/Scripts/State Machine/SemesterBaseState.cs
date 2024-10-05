using UnityEngine;

public abstract class SemesterBaseState
{
    protected SemesterStateMachine _ctx;
    protected SemesterStateFactory _factory;
    public SemesterBaseState(SemesterStateMachine currentContext, SemesterStateFactory semesterStateFactory)
    {
        _ctx = currentContext;
        _factory = semesterStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void OnCollisionEnter();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();
    void UpdateStates()
    {

    }
    protected void SwitchState(SemesterBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();
        _ctx.CurrentState = newState;
    }
    protected void SetSuperState()
    {

    }
    protected void SetSubState()
    {

    }

}
