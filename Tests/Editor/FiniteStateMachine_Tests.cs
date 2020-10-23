using System;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zefugi.Unity.FiniteStateMachine;

namespace Tests
{
    [TestFixture]
    public class FiniteStateMachine_Tests
    {
        private FiniteStateMachineSystem _fsm;
        private State _subStateA;
        private State _subStateB;

        /* 
         * TODO Finish fixing tests. Start needs to be run.
         */

        [SetUp]
        public void Setup()
        {
            _subStateA = Substitute.For<State>();
            _subStateB = Substitute.For<State>();

            _fsm = new FiniteStateMachineSystem(_subStateA);
            _fsm.Start(_subStateA, null);
        }

        [Test]
        public void Ctor_SetsStates()
        {
            _fsm = new FiniteStateMachineSystem(new List<State> { _subStateA, _subStateB });

            Assert.IsTrue(_fsm.States.Contains(_subStateB));
        }

        [Test]
        public void Add_AddsStates()
        {
            _fsm.Add(_subStateB);

            Assert.IsTrue(_fsm.States.Contains(_subStateB));
        }

        [Test]
        public void Add_Throws_IfStateExists()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.Add(_subStateA);
            });
        }

        [Test]
        public void Add_Throws_IfStateIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm.Add(null);
            });
        }
        
        [Test]
        public void Add_DoesNotSetCurrenState()
        {
            _fsm.Add(_subStateB);

            Assert.AreEqual(_subStateA, _fsm.CurrentState);
        }

        [Test]
        public void Add_SetsStateMachine_ThusInvokingOnMachineSetOrOnMachineChanged()
        {
            _fsm.Add(_subStateB);

            _subStateB.Received().OnMachineSet();

            var fsm2 = new FiniteStateMachineSystem(_subStateB);

            _subStateB.Received().OnMachineChanged();
        }

        [Test]
        public void Remove_RemovesState()
        {
            _fsm.Add(_subStateB);

            _fsm.Remove(_subStateB);

            Assert.IsFalse(_fsm.States.Contains(_subStateB));
        }

        [Test]
        public void Remove_OnlyRemovesSpecifiedState()
        {
            _fsm.Add(_subStateB);

            _fsm.Remove(_subStateB);

            Assert.IsTrue(_fsm.States.Contains(_subStateA));
        }

        [Test]
        public void Remove_Throws_IfStateDoesNotExist()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.Remove(_subStateB);
            });
        }

        [Test]
        public void Remove_Throws_IfRemovingLastState()
        {
            Assert.Throws<FiniteStateMachineException>(() =>
            {
                _fsm.Remove(_subStateA);
            });
        }

        [Test]
        public void Remove_Throws_IfRemovingCurrent()
        {
            _fsm.Add(_subStateB);

            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.Remove(_subStateA);
            });
        }

        [Test]
        public void Remove_Throws_IfStateIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm.Remove(null);
            });
        }

        [Test]
        public void Transition_ChangesCurrentState()
        {
            _fsm.Add(_subStateB);

            _fsm.Transition(_subStateB);

            Assert.AreEqual(_subStateB, _fsm.CurrentState);
        }

        [Test]
        public void Transition_Throws_IfStateIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm.Transition(null);
            });
        }

        [Test]
        public void Transition_Throws_IfNotContainingState()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.Transition(_subStateB);
            });
        }

        [Test]
        public void Transition_InvokesOnEnterAndOnExit()
        {
            _subStateA.Received().OnEnter(_subStateA);

            _fsm.Add(_subStateB);

            _fsm.Transition(_subStateB);

            _subStateA.Received().OnExit(_subStateB);
            _subStateB.Received().OnEnter(_subStateA);
        }

        [Test]
        public void Update_InvokesOnUpdate()
        {
            _fsm.Update();

            _subStateA.Received().OnUpdate();
        }

        [Test]
        public void Update_ThrowsIfCurrentStateIsNull()
        {
            _fsm = new FiniteStateMachineSystem(_subStateA);

            Assert.Throws<FiniteStateMachineException>(() =>
            {
                _fsm.Update();
            });
        }
    }
}
