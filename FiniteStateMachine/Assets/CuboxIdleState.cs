﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zefugi.Unity.FiniteStateMachine;

public class CuboxIdleState : State
{
    public CuboxLookAtState LookState { get; set; }
}
