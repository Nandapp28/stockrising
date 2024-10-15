using UnityEngine;

public class FirstSemesterState : SemesterBaseState
{
    float timeCountDown = 5.0f;
    public override void EnterState(SemesterStateManager semester)
    {
        Debug.Log("Halo dari Enter:State Semester 1");
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        Debug.Log("Halo dari Update:State Semester 1");
        if (timeCountDown >= 0)
        {
            timeCountDown -= Time.deltaTime;
        }
        else
        {
            semester.SwitchState(semester.secondSemester);
        }
    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
