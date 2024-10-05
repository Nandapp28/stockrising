using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemesterStateManager : MonoBehaviour
{
    public SemesterBaseState currentState;
    public FirstSemesterState firstSemester = new FirstSemesterState();
    public SecondSemesterState secondSemester = new SecondSemesterState();

    void Start()
    {
        currentState = firstSemester;
        currentState.EnterState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(SemesterBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
