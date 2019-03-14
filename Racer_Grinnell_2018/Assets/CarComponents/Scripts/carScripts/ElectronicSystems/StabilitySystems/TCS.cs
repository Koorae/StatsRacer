namespace CarComponents.ElectronicSystems.StabilitySystems
{
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Helpers;
    using CarComponents.Wheels;

    using UnityEngine;

    public class TCS : WheelPart
    {
        #region Fields

        const string TCS_BRAKE_KEY = "TCS"; //A key for the Brake Request Dictionary

        PID_Runner pid_runner;
        TCS_Manager tcsManager;

        #endregion Fields

        #region Methods

        private void checkWheel()
        {
            if (Mathf.Abs(getCurrentSlip()) < 0.05f)
            {
                reset();
                return;
            }
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
            return wheel.extremSlip;
        }

        private void reset()
        {
            pid_runner.stop();
            wheel.brake.brakeControllersRequestes[TCS_BRAKE_KEY] = 0;
        }

        private void setBrake(float value)
        {
            if (value > 0)
                value = 0;
            float brakeForceToRequest = -value * wheel.currentTorquePercent
                * wheel.parentDiffrential.currentTorquePercent;
            //* (car.transmission.currentGearRatio / 4)
            //  * Mathf.Pow(Mathf.Abs(carControl.getSpeedFactor()), 3);
            wheel.brake.brakeControllersRequestes[TCS_BRAKE_KEY] = brakeForceToRequest;
        }

        void Start()
        {
            tcsManager = car.gameObject.GetComponentInChildren<TCS_Manager>();

            pid_runner = new PID_Runner(tcsManager.kp, tcsManager.ki, tcsManager.kd,
                1, -1f,
                    tcsManager.maxBrakeLimitPercent * wheel.brake.maxBrakeForce,
                    -tcsManager.maxBrakeLimitPercent * wheel.brake.maxBrakeForce,
                getCurrentSlip, getDesiredSlip, setBrake
                , tcsManager.fixedTime
                );

            carControl.brakingListner += reset;
            carControl.forwardListner += checkWheel;
            carControl.backwardListner += checkWheel;
            carControl.neutralListner += reset;
            carControl.stoppedListner += reset;

            wheel.brake.brakeControllersRequestes.Add(TCS_BRAKE_KEY, 0);
        }

        #endregion Methods
    }
}