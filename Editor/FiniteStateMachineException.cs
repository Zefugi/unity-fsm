﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.Unity.FiniteStateMachine
{
    public class FiniteStateMachineException : Exception
    {
        public FiniteStateMachineException(string message)
            : base(message) { }
    }
}
