using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zefugi.Unity.FiniteStateMachine;

public class FiniteStateMachineController : MonoBehaviour
{
    private FiniteStateMachineSystem _machine;

    private void Update()
    {
        _machine?.Update(this);
    }
}
