using UnityEngine;

public class SecondSemesterState : SemesterBaseState
{
    public override void EnterState(SemesterStateManager semester)
    {
        Debug.Log("Halo dari Enter:State Semester 2");
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        Debug.Log("Halo dari Update:State Semester 2");
    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
