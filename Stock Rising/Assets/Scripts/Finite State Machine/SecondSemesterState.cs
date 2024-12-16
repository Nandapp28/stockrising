using UnityEngine;

public class SecondSemesterState : SemesterBaseState
{
    public override void EnterState(SemesterStateManager semester)
    {
        Debug.Log("From Semester 2");
        semester.SemesterInitialization();
    }

    public override void UpdateState(SemesterStateManager semester)
    {

    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
