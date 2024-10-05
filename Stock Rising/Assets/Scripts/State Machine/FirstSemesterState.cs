using UnityEngine;

public class FirstSemesterState : SemesterBaseState
{
    // concrete states access context and factory
    public FirstSemesterState(SemesterStateMachine currentContext, SemesterStateFactory semesterStateFactory)
    : base (currentContext, semesterStateFactory) { }

    float timeCountDown = 5.0f;
    bool isFirstSemesterDone = false;

    public override void EnterState()
    {
        Debug.Log("Hello dari EnterState: Semester 1");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        Debug.Log("Hello dari UpdateState: Semester 1");
        if (timeCountDown >= 0)
        {
            timeCountDown -= Time.deltaTime;
        }
        else
        {
            isFirstSemesterDone = true;
        }
    }

    public override void OnCollisionEnter()
    {

    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchStates()
    {
        if (isFirstSemesterDone)
        {
            SwitchState(_factory.SecondSemester());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}
