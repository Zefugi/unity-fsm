﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zefugi.Unity.FiniteStateMachine;

public class CuboxLookAtState : State
{
    public CuboxIdleState IdleState { get; set; }

    public override void OnUpdate()
    {
    }
}