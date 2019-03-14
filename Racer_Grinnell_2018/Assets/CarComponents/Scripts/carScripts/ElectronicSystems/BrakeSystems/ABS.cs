namespace CarComponents.ElectronicSystems.BrakeSystems
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Helpers;
    using CarComponents.Wheels;

    using UnityEngine;

    public class ABS : WheelPart
    {
        #region Fields

        PID_Runner pid_runner;

        #endregion Fields

        #region Methods

        private void checkBrake()
        {
            if (pid_runner.isRunning)
                return;
            StartCoroutine(pid_runner.run());
        }

        private float getCurrentSlip()
        {
            return wheel.slipRatio;
        }

        private float getDesiredSlip()
        {
            return -wheel.extremSlip;
        }

        private void reset()
        {
            pid_runner.stop();
            wheel.brake.maxAllowedBrakeForce = wheel.brake.maxBrakeForce;
        }

        private void setBrake(float value)
        {
            wheel.brake.maxAllowedBrakeForce = -value;
        }

        void Start()
        {
            ABS_Manager absManager = car.gameObject.GetComponentInChildren<ABS_Manager>();

            pid_runner = new PID_Runner(absManager.kp, absManager.ki, absManager.kd,
                1, -1,
                 - wheel.brake.maxBrakeForce * absManager.minBrakeLimitPercent
                 , -wheel.brake.maxBrakeForce,
                getCurrentSlip, getDesiredSlip, setBrake
                , absManager.fixedTime
                );

            carControl.brakingListner += checkBrake;
            carControl.forwardListner += reset;
            carControl.backwardListner += reset;
            //carControl.neutralListner += reset;
            carControl.stoppedListner += reset;
        }

        #endregion Methods
    }
}