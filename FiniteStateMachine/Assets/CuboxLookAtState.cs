using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zefugi.Unity.FiniteStateMachine;

public class CuboxLookAtState : State
{
    public CuboxIdleState IdleState { get; set; }

    public Vector3 LookAtPoint { get; set; }

    public override void OnEnter(State toState)
    {
        LookAtPoint = Vector3.zero;
    }

    public override void OnUpdate()
    {
        if (LookAtPoint != null)
            UpdateLookDirection();
    }

    private void UpdateLookDirection()
    {
        Machine.Controller.transform.LookAt(LookAtPoint);
    }
}
