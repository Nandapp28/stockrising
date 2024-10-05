using UnityEngine;

public abstract class SemesterBaseState
{
    protected SemesterStateMachine _ctx;
    protected SemesterStateFactory _factory;
    protected SemesterBaseState _currentSubState;
    protected SemesterBaseState _currentSuperState;
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

    // SwitchState digunakan untuk berganti dari satu state ke state lain, bukan untuk SUB-STATE yaaa
    // Untuk mengakses SUB-STATE fungsi yang digunakan adalah InitializeSubState(), UpdateStates(), SetSuperState(), SetSubState()
    protected void SwitchState(SemesterBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();
        _ctx.CurrentState = newState;
    }

    // Fungsi SetSuperState() dan SetSubState() adalah untuk membuat relasi Parent dan Child yang timbal balik
    protected void SetSuperState(SemesterBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(SemesterBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

}
