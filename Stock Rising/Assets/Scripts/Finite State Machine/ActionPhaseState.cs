using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPhaseState : SemesterBaseState
{
    float timeEnterCD = 2.0f;

    public override void EnterState(SemesterStateManager semester)
    {
        semester.phaseCount += 1;
        semester.phaseTitleParent.gameObject.SetActive(true);
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        SetInitialize(semester);
    }

    void SetInitialize(SemesterStateManager semester)
    {
        if (timeEnterCD >= 0)
        {
            timeEnterCD -= Time.deltaTime;
        }
        else
        {
            semester.phaseTitleParent.gameObject.SetActive(false);
        }
    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
