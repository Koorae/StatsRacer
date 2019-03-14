namespace CarComponents.LogSystems
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using MonsterLove.StateMachine;

    using UnityEngine;

    public class BrakeLoger : CarPart
    {
        #region Fields

        StateMachine<States> fsm;
        Vector3 startedPosition;
        DateTime startedTime;

        #endregion Fields

        #region Enumerations

        public enum States
        {
            Idle,
            Started,
            Exit
        }

        #endregion Enumerations

        #region Methods

        void Exit_Enter()
        {
            DateTime nowTime = DateTime.Now;
            Vector3 pos = car.transform.position;
            Debug.Log(car.name + " brake duration= " + (nowTime - startedTime));
            Debug.Log(car.name + " braking distance= " + (pos - startedPosition).magnitude);
        }

        void Start()
        {
            carControl.brakingListner += startLogging;

            //  carControl.forwardListner += reset;
            // carControl.backwardListner += reset;
            carControl.stoppedListner += stoped;
            fsm = StateMachine<States>.Initialize(this, States.Idle);
        }

        void Started_Enter()
        {
            startedPosition = car.transform.position;
            startedTime = DateTime.Now;
        }

        private void startLogging()
        {
            if (fsm.State == States.Started)
                return;
            fsm.ChangeState(States.Started);
        }

        private void stoped()
        {
            if (fsm.State != States.Started)
                return;
            fsm.ChangeState(States.Exit);
        }

        #endregion Methods
    }
}