using UnityEngine;

public abstract class SemesterBaseState
{
    public abstract void EnterState(SemesterStateManager semester);
    public abstract void UpdateState(SemesterStateManager semester);
    public abstract void OnCollisionEnter(SemesterStateManager semester, Collision collision);
}
