namespace CarComponents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    [System.Serializable]
    public class CarEngine : CarPart, IHasTorqueOutput
    {
        #region Fields

        public float maxEngineBrake = 30f;

        [SerializeField]
        private float hp = 0;
        [SerializeField]
        private AnimationCurve RPM_Curve;
        private float _currentRPM;
        [SerializeField]
        private int _maxRPM = 7000;
        [SerializeField]
        private int _minRPM = 700;

        #endregion Fields

        #region Properties

        public float currentRPM
        {
            get
            {
                return this._currentRPM;
            }
            private set
            {
                this._currentRPM = Mathf.Max(value, this.minRPM);
                // this._currentRPM = Mathf.Min(this._currentRPM, this.maxRPM);
            }
        }

        //Car's horsepower
        public float Hp
        {
            get
            {
                return this.hp;
            }
        }

        public int maxRPM
        {
            get
            {
                return this._maxRPM;
            }
        }

        public int minRPM
        {
            get
            {
                return this._minRPM;
            }
        }

        public float outputTorque
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        public float calculateEngineTorque(float rpm)
        {
            if (this.Hp > 0)
                return getEngineTorqueForHP(rpm);
            return RPM_Curve.Evaluate(rpm);
        }

        public float calculateRPM(float gearRatio)
        {
            if (gearRatio == 0)
                return maxRPM*carControl.AccelInput;
            return car.transmission.rpm
                * ((gearRatio * car.transmission.differentialRatio));
        }

        public float getEngineBrakeTorque()
        {
            return Mathf.Lerp(0, maxEngineBrake, currentRPM / maxRPM);
        }

        public float getEngineTorque()
        {
            return calculateEngineTorque(currentRPM);
        }

        public float getEngineTorqueForHP(float rpm)
        {
            if (rpm > maxRPM)
                return outputTorque*0.5f;
            return Hp * 5252 / rpm;
        }

        public float getEngineWheelsBrakeTorque()
        {
            return getEngineBrakeTorque() *
            car.transmission.currentGearRatio *
            car.transmission.differentialRatio;
        }

        void FixedUpdate()
        {
            updateCurrentRPM();
            updateOutputTorque();
        }

        private float realRPM()
        {
            return calculateRPM(car.transmission.currentGearRatio);
        }

        private void updateCurrentRPM()
        {
            float t = Time.fixedDeltaTime *25;

             float tm = realRPM();
             if (tm < currentRPM)
                 t = Time.fixedDeltaTime * 10;

            currentRPM =  Mathf.Lerp(currentRPM, tm, t);
        }

        private void updateOutputTorque()
        {
            outputTorque = getEngineTorque();
            outputTorque *= Mathf.Abs(carControl.AccelInput);
        }

        #endregion Methods
    }
}