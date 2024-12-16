using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdSemesterState : SemesterBaseState
{
    public override void EnterState(SemesterStateManager semester)
    {
        Debug.Log("From Semester 3");
        semester.SemesterInitialization();
    }

    public override void UpdateState(SemesterStateManager semester)
    {

    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
