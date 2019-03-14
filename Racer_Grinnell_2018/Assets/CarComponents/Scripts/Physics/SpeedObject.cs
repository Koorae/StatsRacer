namespace CarComponents.Physics
{
    using System.Collections;

    using CarComponents.Helpers;

    using UnityEngine;

    public class SpeedObject
    {
        #region Fields

        public Vector3 _oldVelocity;
        public FloatDiffrentialAbleField _speed = new FloatDiffrentialAbleField();
        public Vector3 _velocity;

        Vector3 previous_position = Vector3.zero;

        #endregion Fields

        #region Properties

        public float acceleration
        {
            get
            {
                return _speed.diffrentialValue;
            }
        }

        public float speed
        {
            get {
               return _speed.value;
            }
        }

        public float speedInKPH
        {
            get
            {
                return this.speed * 3.6f;
            }
        }

        public Vector3 velocity
        {
            get {
                return _velocity;
            }
            private set {
                _oldVelocity = _velocity;
                velocity = value;
            }
        }

        #endregion Properties

        #region Methods

        public Vector3 getVelocityVectorRelativeTo(Transform trans)
        {
            return trans.InverseTransformDirection(velocity);
        }

        public void setSpeed(float value, float delteTime)
        {
            _speed.setValue(value, delteTime);
        }

        public void UpdateVelocity(float deltaTime, Vector3 position)
        {
            velocity = ((position - previous_position)) / deltaTime;
            previous_position = position;
        }

        #endregion Methods
    }
}