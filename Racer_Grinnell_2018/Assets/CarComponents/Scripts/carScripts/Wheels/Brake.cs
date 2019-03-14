namespace CarComponents.Wheels
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class Brake : WheelPart
    {
        #region Fields

        public Dictionary<string, float> brakeControllersRequestes = new Dictionary<string, float>();

        //to allow ABS to control brake limits
        public float _maxAllowedBrakeForce;

        const string CarControl_BRAKE_KEY = "Brake"; //A key for the Brake Request Dictionary

        private float _currentBrakeForce;
        private float _maxBrakeForce;

        #endregion Fields

        #region Properties

        public float currentBrakeForce
        {
            get
            {
                return _currentBrakeForce;
            }
            set
            {
                _currentBrakeForce = Mathf.Min(value, maxAllowedBrakeForce);
                _currentBrakeForce = Mathf.Max(0, _currentBrakeForce);
            }
        }

        public float maxAllowedBrakeForce
        {
            get { return _maxAllowedBrakeForce; }
            set
            {
                _maxAllowedBrakeForce = Mathf.Min(value, maxBrakeForce);
                _maxAllowedBrakeForce = Mathf.Max(0, _maxAllowedBrakeForce);
            }
        }

        public float maxBrakeForce
        {
            get
            {
                return _maxBrakeForce + getTheEngineBrake();
            }
        }

        private bool isHandBraked
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public void doBrake()
        {
            currentBrakeForce = maxBrakeRequest();
            wheel.wheelCollider.brakeTorque = currentBrakeForce ;
        }

        public void drag()
        {
            //wheel.wheelCollider.motorTorque = 0;
            brakeControllersRequestes[CarControl_BRAKE_KEY] = getTheEngineBrake();
        }

        public float getTheEngineBrake()
        {
            return Mathf.Abs(car.engine.getEngineWheelsBrakeTorque())
                * wheel.currentTorquePercent
               * wheel.parentDiffrential.currentTorquePercent;
        }

        void addCarControlBrakeRequest()
        {
            brakeControllersRequestes[CarControl_BRAKE_KEY] = _maxBrakeForce * Mathf.Abs(carControl.AccelInput);
        }

        void FixedUpdate()
        {
            doBrake();
        }

        float maxBrakeRequest()
        {
            float maxRequest = 0;
            foreach (float v in brakeControllersRequestes.Values)
                if (v > maxRequest)
                    maxRequest = v;
            return maxRequest;
        }

        void removeCarControlBrakeRequest()
        {
            brakeControllersRequestes[CarControl_BRAKE_KEY] = 0;
        }

        void Start()
        {
            _maxBrakeForce = wheel.isFront ? car.frontBrakeTorque : car.backBrakeTorque;
            maxAllowedBrakeForce = _maxBrakeForce;
            carControl.forwardListner += removeCarControlBrakeRequest;
            carControl.backwardListner += removeCarControlBrakeRequest;
            carControl.neutralListner += drag;
            carControl.stoppedListner += removeCarControlBrakeRequest;
            wheel.carControl.brakingListner += addCarControlBrakeRequest;

            brakeControllersRequestes.Add(CarControl_BRAKE_KEY, 0);
        }

        #endregion Methods
    }
}