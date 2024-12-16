using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthSemesterState : SemesterBaseState
{
    public override void EnterState(SemesterStateManager semester)
    {
        Debug.Log("From Semester 4");
        semester.SemesterInitialization();
    }

    public override void UpdateState(SemesterStateManager semester)
    {

    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
