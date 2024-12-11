using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumorPhaseState : SemesterBaseState
{
    public override void EnterState(SemesterStateManager semester)
    {
        semester.phaseCount += 1;
        semester.phaseName = "Fase Rumor";
        semester.phaseTitleParent.gameObject.SetActive(true);
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        Debug.Log("Update from Rumor Phase State");
    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }
}
