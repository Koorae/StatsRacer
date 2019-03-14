namespace CarComponents.Wheels
{
    using System;
    using System.Collections;

    using CarComponents.Differentials;
    using CarComponents.Helpers;
    using CarComponents.Physics;

    using UnityEngine;

    #region Delegates

    public delegate void SkidListner();

    public delegate void WheelSkidListner(Wheel wheel);

    #endregion Delegates

    [System.Serializable]
    public class Wheel : DifferentialOutput
    {
        #region Fields

        private Brake _brake;
        private float _extremSlip;
        private float _maxAllowedTorque = float.MaxValue;
        VectorDiffrentialAbleField _positionVector = new VectorDiffrentialAbleField();
        VectorDiffrentialAbleField _velocityVector = new VectorDiffrentialAbleField();
        private WheelCollider _wheelCollider;
        private Transform _wheelTransform;

        #endregion Fields

        #region Events

        public event SkidListner skidListner;

        public event SkidListner stopSkidListner;

        public event WheelSkidListner wheelSkidListner;

        public event WheelSkidListner wheelStopSkidListner;

        #endregion Events

        #region Properties

        public Vector3 accelerationVector
        {
            get
            {
                return transform.InverseTransformVector(_velocityVector.diffrentialValue);
            }
        }

        public SpeedObject angularSpeedObject
        {
            get; private set;
        }

        public Brake brake
        {
            get
            {
                if (this._brake == null)
                    this._brake = GetComponentInChildren<Brake>();
                return this._brake;
            }
        }

        public override float differentialResult
        {
            get
            {
                // return 1; // to disable dynamic differential
                return
                   (wheelCollider.rpm) * (wheelCollider.motorTorque + wheelCollider.brakeTorque);
            }
        }

        public float extremSlip
        {
            get
            {
                if (this._extremSlip == 0)
                    this._extremSlip = this.wheelCollider.forwardFriction.extremumSlip;
                return this._extremSlip;
            }
        }

        public float inertia
        {
            get
            {
                return wheelCollider.mass * Mathf.Pow(radius, 2);
            }
        }

        public bool isFront
        {
            get
            {
                return transform.parent.localPosition.z > 0;
            }
        }

        public bool isLeft
        {
            get
            {
                return !isRight;
            }
        }

        public bool isRear
        {
            get
            {
                return !isFront;
            }
        }

        public bool isRight
        {
            get
            {
                return transform.localPosition.x > 0;
            }
        }

        public float maxAllowedTorque
        {
            get { return _maxAllowedTorque; }
            set
            {
                _maxAllowedTorque = Mathf.Max(0, value);
            }
        }

        public float mu
        {
            get
            {
                return wheelHit.force / (wheelCollider.sprungMass * UnityEngine.Physics.gravity.magnitude);
            }
        }

        public WheelsDifferential parentDiffrential
        {
            get
            {
                return transform.parent.GetComponent<WheelsDifferential>();
            }
        }

        public float radius
        {
            get
            {
                return wheelCollider.radius;
            }
        }

        public float skidAmount
        {
            get; private set;
        }

        public float slipRatio
        {
            get
            {
                return wheelHit.forwardSlip;
            }
        }

        public SpeedObject speedObject
        {
            get; private set;
        }

        public Vector3 velocityVector
        {
            get
            {
                return transform.InverseTransformVector(_positionVector.diffrentialValue);
            }
        }

        public WheelCollider wheelCollider
        {
            get
            {
                if (_wheelCollider == null)
                    _wheelCollider = GetComponentInChildren<WheelCollider>();
                return _wheelCollider;
            }
        }

        public WheelHit wheelHit
        {
            get
            {
                WheelHit hit;
                wheelCollider.GetGroundHit(out hit);
                return hit;
            }
        }

        public Transform wheelTransform
        {
            get
            {
                if (_wheelTransform == null)
                    _wheelTransform = transform.Find("wheelTransform");
                return this._wheelTransform;
            }
        }

        #endregion Properties

        #region Methods

        public void applyMotorTorque()
        {
            wheelCollider.motorTorque = Mathf.Min(inputTorque, maxAllowedTorque);
        }

        public new void FixedUpdate()
        {
            updateSpeed();
            _positionVector.setValue(transform.position, Time.fixedDeltaTime);

            _velocityVector.setValue(_positionVector.diffrentialValue, Time.fixedDeltaTime);

            applyDrive();
        }

        void applyDrive()
        {
            if (carControl.statuse == CarStause.Forward || carControl.statuse == CarStause.Backward)
                applyMotorTorque();
            else
                wheelCollider.motorTorque = 0;
        }

        private void applyLocalPositionToVisuals()
        {
            Transform visualWheel = this.wheelTransform;
            Vector3 position; Quaternion rotation;
            this.wheelCollider.GetWorldPose(out position, out rotation);
            visualWheel.rotation = rotation;
            visualWheel.position = position;

            if (isLeft)
            {
                Vector3 r = visualWheel.localRotation.eulerAngles;
                r.y += 180;
                visualWheel.localRotation = Quaternion.Euler(r.x, r.y, r.z);
            }
        }

        void checkSkid()
        {
            if (carControl.statuse == CarStause.Neutral)
                skidAmount = Mathf.Abs(wheelHit.sidewaysSlip);
            else
                skidAmount = Mathf.Sqrt(Mathf.Pow(wheelHit.forwardSlip, 2) + Mathf.Pow(wheelHit.sidewaysSlip, 2));
            skidAmount -= 0.5f;
            if (!wheelCollider.isGrounded || skidAmount <= 0f)
            {
                stopSkidListner.Invoke();
                wheelStopSkidListner.Invoke(this);
                return;
            }
            skidListner.Invoke();
            wheelSkidListner.Invoke(this);
        }

        void emptyListners()
        {
        }

        void emptyListners(Wheel w)
        {
        }

        void Start()
        {
            currentTorquePercent = defaultTorquePercent;
            speedObject = new SpeedObject();
            angularSpeedObject = new SpeedObject();
            skidListner += emptyListners;
            stopSkidListner += emptyListners;
            wheelSkidListner += emptyListners;
            wheelStopSkidListner += emptyListners;
        }

        void Update()
        {
            applyLocalPositionToVisuals();
            checkSkid();
        }

        private void updateSpeed()
        {
            angularSpeedObject.setSpeed(wheelCollider.rpm * 2 * Mathf.PI / 60, Time.fixedDeltaTime);
            speedObject.setSpeed(angularSpeedObject.speed * wheelCollider.radius, Time.fixedDeltaTime);
        }

        #endregion Methods
    }
}