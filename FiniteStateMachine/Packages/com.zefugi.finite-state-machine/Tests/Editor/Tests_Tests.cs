using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zefugi.Unity.FiniteStateMachine;

namespace Tests
{
    public class Tests_Tests
    {
        [Test]
        public void Tests_UntrueIsFalse()
        {
            var fsm = new FiniteStateMachine();

            Assert.IsFalse(fsm.Untrue);
        }
    }
}
