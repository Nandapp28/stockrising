using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemesterStateMachine : MonoBehaviour
{
    SemesterBaseState _currentState;
    SemesterStateFactory _states;

    // getter & setters
    public SemesterBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    private void Awake()
    {
        _states = new SemesterStateFactory(this);
        _currentState = _states.FirstSemester();
        _currentState.EnterState();
    }
    //void Start()
    //{
    //    _currentState = firstSemester;
    //    _currentState.EnterState(this);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //_currentState.OnCollisionEnter(this, collision);
    }

    void Update()
    {
        _currentState.UpdateState();
    }

    public void SwitchState(SemesterBaseState state)
    {
        //_currentState = state;
        //state.EnterState(this);
    }
}
