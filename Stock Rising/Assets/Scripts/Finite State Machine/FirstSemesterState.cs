using TMPro;
using UnityEngine;

public class FirstSemesterState : SemesterBaseState
{
    //float timeCountDown = 2.0f;

    public override void EnterState(SemesterStateManager semester)
    {
        semester.semesterCount += 1;
        semester.SwitchState(semester.biddingPhase);
    }

    public override void UpdateState(SemesterStateManager semester)
    {
        //if (timeCountDown >= 0)
        //{
        //    timeCountDown -= Time.deltaTime;
        //}
        //else
        //{
        //    semester.semester1Title.gameObject.SetActive(false);
        //    semester.SwitchState(semester.biddingPhase);
        //}
    }

    public override void OnCollisionEnter(SemesterStateManager semester, Collision collision)
    {

    }


}
