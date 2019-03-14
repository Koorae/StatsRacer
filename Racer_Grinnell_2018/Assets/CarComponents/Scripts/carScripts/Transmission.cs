namespace CarComponents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    [System.Serializable]
    public class Transmission : CarPart, IHasTorqueOutput
    {
        #region Fields

        [SerializeField]
        private float blockNextShiftTime = 0.1f;
        private float blockTimer = 0;
        [SerializeField]
        private AnimationCurve GearRation_Curve;
        private bool isShifting = false;
        [SerializeField]
        private float shiftSpeed = 0.05f;
        private float wheelRadius = 0;
        private int _currentGear = 0;
        [SerializeField]
        private float _differentialRatio = 1f;

        #endregion Fields

        #region Properties

        public int currentGear
        {
            get
            {
                return this._currentGear;
            }
        }

        public float currentGearRatio
        {
            get
            {
                return getGearRatio(_currentGear);
            }
        }

        public float differentialRatio
        {
            get
            {
                return this._differentialRatio;
            }
            private set
            {
                this._differentialRatio = value;
            }
        }

        public float outputTorque
        {
            get; private set;
        }

        public float rpm
        {
            get
            {
                return carControl.getWheelsRPM();
            }
        }

        #endregion Properties

        #region Methods

        public float gearTopSpeed(float gearRatio)
        {
            float propShaftspeed = this.car.engine.maxRPM / gearRatio;
            float wheelSpeed = propShaftspeed / _differentialRatio;
            float mSpeed = wheelSpeed * Mathf.PI * 2 * wheelRadius / 60;
            mSpeed *= 3.6f; // convert to Km/h

            return Mathf.Abs(mSpeed);
        }

        public string getCurrentGearLabel()
        {
            switch (this.currentGear)
            {
                case 0:
                    return "N";
                case -1:
                    return "R";
                default:
                    return this.currentGear + string.Empty;
            }
        }

        public float getCurrentGearSpeedRang()
        {
            if (this.currentGear == 0)
                return 0;

            return gearTopSpeed(currentGearRatio);
        }

        public float getGearRatio(int gearIndex)
        {
            return GearRation_Curve.Evaluate(gearIndex);
        }

        public void neutralListner()
        {
            //  setCurrentGear(0);
        }

        public void setCurrentGear(int i)
        {
            if (i == currentGear || isShifting || blockTimer > 0)
                return;
            blockTimer = blockNextShiftTime;
            StartCoroutine(startShifting(i));
        }

        public void setGearToFrist()
        {
            if (this.currentGear == 0)
                setCurrentGear(1);
        }

        public void setGearToN()
        {
            setCurrentGear(0);
        }

        public void setGearToRevers()
        {
            if (this.currentGear == 0)
                setCurrentGear(-1);
        }

        public IEnumerator startShifting(int i)
        {
            isShifting = true;
            _currentGear = 0;
            yield return new WaitForSeconds(shiftSpeed);
            _currentGear = i;
            isShifting = false;
        }

        public void Update()
        {
            updateCurrentGear();
            if (blockTimer > 0)
                blockTimer -= Time.deltaTime;
            else
                blockTimer = 0;
            updateOutputTorque();
        }

        public void updateCurrentGear()
        {
            if (_currentGear < 1 || isShifting)
                return;
            int i = 1;
            if (carControl.getCarSpeedVector3().z < 0)
            {
                _currentGear = 1;
                return;
            }
            while (

                gearTopSpeed(getGearRatio(i)) <= carControl.getWheelsSpeed() //carControl.speedObject.speedInKPH //
                &&
                i < GearRation_Curve.keys.Length - 3)
            {
                i++;
            }
               if (i < currentGear &&
                (car.engine.calculateRPM(getGearRatio(i))>car.engine.maxRPM*0.9f
               || car.engine.calculateEngineTorque(car.engine.calculateRPM(currentGearRatio)) * currentGearRatio >=
                car.engine.calculateEngineTorque(car.engine.calculateRPM(getGearRatio(i))) * getGearRatio(i))
                )
                return;
            setCurrentGear(i);
        }

        float calculateGearOutputTorque(float gearRatio)
        {
            return car.engine.outputTorque *( gearRatio * differentialRatio);
        }

        void Start()
        {
            carControl.brakingListner += neutralListner;
            carControl.forwardListner += setGearToFrist;
            carControl.backwardListner += setGearToRevers;
            carControl.neutralListner += neutralListner;
            carControl.stoppedListner += setGearToN;
            wheelRadius = car.motorWheels[0].wheelCollider.radius;
        }

        private void updateOutputTorque()
        {
            outputTorque = calculateGearOutputTorque(currentGearRatio);
        }

        #endregion Methods
    }
}