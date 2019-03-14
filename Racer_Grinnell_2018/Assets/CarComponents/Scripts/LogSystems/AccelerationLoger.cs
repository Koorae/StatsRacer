namespace CarComponents.LogSystems
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using MonsterLove.StateMachine;

    using UnityEngine;

    public class AccelerationLoger : CarPart
    {
        #region Fields

        StateMachine<States> fsm;
        Vector3 startedPosition;

        // Use this for initialization
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
            Debug.Log(car.name + " Acceleration duration= " + (nowTime - startedTime));
            Debug.Log(car.name + " Acceleration distance= " + (pos - startedPosition).magnitude);
        }

        void FixedUpdate()
        {
            if (carControl.speedObject.speedInKPH >= 96)
                reached100();
        }

        private void reached100()
        {
            if (fsm.State != States.Started)
                return;
            fsm.ChangeState(States.Exit);
        }

        private void reset()
        {
            fsm.ChangeState(States.Idle); ;
        }

        void Start()
        {
            //carControl.brakingListner += startLogging;

            carControl.forwardListner += startLogging;
            // carControl.backwardListner += reset;
            carControl.stoppedListner += reset;
            fsm = StateMachine<States>.Initialize(this, States.Idle);
        }

        void Started_Enter()
        {
            startedPosition = car.transform.position;
            startedTime = DateTime.Now;
        }

        private void startLogging()
        {
            if (fsm.State == States.Idle)
            {
                fsm.ChangeState(States.Started);
                return;
            }
        }

        #endregion Methods
    }
}