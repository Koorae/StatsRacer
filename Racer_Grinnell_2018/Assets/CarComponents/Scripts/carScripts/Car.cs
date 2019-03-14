namespace CarComponents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Helpers;
    using CarComponents.Wheels;

    using UnityEngine;

    [System.Serializable]
    public class Car : MonoBehaviour
    {
        #region Fields

        public Wheel frontRightWheel, frontLeftWheel, rearRightWheel, rearLeftWheel;
        public FloatDiffrentialAbleField yVelocity = new FloatDiffrentialAbleField(); //Car rotations speed aroung Y axis. Used by EPS

        [SerializeField]
        private Vector3 centerOfMassDiffrence; //vector to optimize the ceneter of mass of the car so it didnt flip over easly
        [SerializeField]
        private float _backBrakeTorque = 3500; // Brake torque to apply for each back wheel when Braking
        private List<Wheel> _backWheels = new List<Wheel>(); //  wheels in the back side of the car will be saved in this array for faster access
        private CarEngine _engine;
        [SerializeField]
        private float _frontBrakeTorque = 4000; // Brake torque to apply for each front wheel when Braking
        private List<Wheel> _frontWheels = new List<Wheel>(); //  wheels in the front side of the car will be saved in this array for faster access
        private HandBrake _handBrake;
        private List<Wheel> _leftWheels = new List<Wheel>(); //  wheels in the left side of the car will be saved in this array for faster access
        [SerializeField]
        private float _maxSpeed = 240; //car's Top speed//{ get { return m_Topspeed; } }
        private List<Wheel> _motorWheels = new List<Wheel>(); // motor wheels will be saved in this array for faster access
        private List<Wheel> _rightWheels = new List<Wheel>(); // wheels in the right side of the car will be saved in this array for faster access
        private Steering _steering;
        private Transmission _transmission;
        private Wheel[] _wheels = null;

        #endregion Fields

        #region Properties

        public float backBrakeTorque
        {
            get
            {
                return this._backBrakeTorque;
            }
        }

        public List<Wheel> backWheels
        {
            get
            {
                if (this._backWheels.Count == 0)
                    foreach (Wheel wheel in this.wheels)
                        if (wheel.isRear)
                        {
                            this._backWheels.Add(wheel);
                            if (wheel.isRight)
                                rearRightWheel = wheel;
                            else rearLeftWheel = wheel;
                        }
                return this._backWheels;
            }
        }

        public Vector3 centerOfMassPosition
        {
            get
            {
                return transform.InverseTransformVector(_rigidbody.centerOfMass);
            }
        }

        public float dragForce
        {
            get
            {
                return Mathf.Pow(GetComponent<CarControl>().getCarSpeedVector3().z, 2) * _rigidbody.drag;
            }
        }

        public CarEngine engine
        {
            get
            {
                if (this._engine == null)
                    this._engine = GetComponentInChildren<CarEngine>();
                return this._engine;
            }
        }

        public float frontBrakeTorque
        {
            get
            {
                return this._frontBrakeTorque;
            }
        }

        public List<Wheel> frontWheels
        {
            get
            {
                if (this._frontWheels.Count == 0)
                    foreach (Wheel wheel in this.wheels)
                        if (wheel.isFront)
                        {
                            this._frontWheels.Add(wheel);
                            if (wheel.isRight)
                                frontRightWheel = wheel;
                            else frontLeftWheel = wheel;
                        }
                return this._frontWheels;
            }
        }

        public HandBrake handBrake
        {
            get
            {
                if (this._handBrake == null)
                    this._handBrake = GetComponentInChildren<HandBrake>();
                return this._handBrake;
            }
        }

        public List<Wheel> leftWheels
        {
            get
            {
                if (this._leftWheels.Count == 0)
                    foreach (Wheel wheel in this.wheels)
                        if (wheel.isLeft)
                            this._leftWheels.Add(wheel);
                return this._leftWheels;
            }
        }

        public float maxSpeed
        {
            get
            {
                return this._maxSpeed;
            }
            set
            {
                this._maxSpeed = value;
            }
        }

        public List<Wheel> motorWheels
        {
            get
            {
                if (this._motorWheels.Count == 0)
                    foreach (Wheel wheel in this.wheels)
                        if (wheel.defaultTorquePercent * wheel.parentDiffrential.defaultTorquePercent > 0)
                            this._motorWheels.Add(wheel);
                return this._motorWheels;
            }
        }

        public List<Wheel> rightWheels
        {
            get
            {
                if (this._rightWheels.Count == 0)
                    foreach (Wheel wheel in this.wheels)
                        if (wheel.isRight)
                            this._rightWheels.Add(wheel);
                return this._rightWheels;
            }
        }

        public float slipAngle
        {
            get
            {

                Vector3 a = getVelocityDirection();
                a.y = 0;
                Vector3 b = getDesiredDirection();
                b.y = 0;
                float angle = Vector3.Angle(a, b) * Mathf.Sign(Vector3.Cross(a, b).y);
                return angle;
            }
        }

        public Steering steering
        {
            get
            {
                if (this._steering == null)
                    this._steering = GetComponentInChildren<Steering>();
                return this._steering;
            }
        }

        public Transmission transmission
        {
            get
            {
                if (this._transmission == null)
                    this._transmission = GetComponentInChildren<Transmission>();
                return this._transmission;
            }
        }

        public Wheel[] wheels
        {
            get
            {
                if (this._wheels == null)
                    this._wheels = GetComponentsInChildren<Wheel>();
                return this._wheels;
            }
        }

        public Rigidbody _rigidbody
        {
            get
            {
                return GetComponent<Rigidbody>();
            }
        }

        #endregion Properties

        #region Methods

        public Vector3 getDesiredDirection()
        {
            return Quaternion.Euler(0, steering.steerAngle, 0) * transform.TransformVector(Vector3.forward);// * GetComponent<CarControl>().speedObject.speed;
        }

        public Vector3 getVelocityDirection()
        {
            return _rigidbody.velocity;
        }

        void FixedUpdate()
        {
            yVelocity.setValue(transform.InverseTransformDirection(_rigidbody.angularVelocity).y, Time.fixedDeltaTime);
        }

        void optimizeCM()
        {
            Vector3 pos = _rigidbody.centerOfMass;// car.wheels[0].transform.position;// _rigidbody.centerOfMass;
            pos.y += centerOfMassDiffrence.y;
            pos.z += centerOfMassDiffrence.z;
            _rigidbody.centerOfMass = pos;
        }

        void Start()
        {
            optimizeCM();
            foreach (Wheel wheel in wheels)
            {
                float z = _rigidbody.centerOfMass.y - 0.001f;
                wheel.wheelCollider.forceAppPointDistance = z;
            }
        }

        // [DBG_Track("Blue")]
        // public float _slipAngle;
        void Update()
        {
            //  _slipAngle = slipAngle;
            // Debug.Log(slipAngle);
        }

        #endregion Methods
    }
}