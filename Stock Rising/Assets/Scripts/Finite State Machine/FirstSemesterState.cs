using TMPro;
using UnityEngine;

public class FirstSemesterState : SemesterBaseState
{
    public override void EnterState(SemesterStateManager semester)
    {
        semester.semesterCount += 1;
        semester.SwitchState(semester.rumorPhase);
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        
    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }


}
