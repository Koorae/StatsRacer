namespace CarComponents
{
    using System;
    using System.Collections;

    using UnityEngine;

    public class Wings : CarPart
    {
        #region Fields

        public float backDownforce = 700f;
        public float frontDownforce = 500f;

        private Vector3 backPosition;
        private Vector3 frontPosition;
        private float orginalDrag;

        #endregion Fields

        #region Methods

        public void AddDownForce()
        {
            car._rigidbody.AddForceAtPosition(-carControl.transform.up * frontDownforce *
                                                 Mathf.Abs(   carControl.getSpeedFactor())
                                                    , frontPosition);
            car._rigidbody.AddForceAtPosition(-carControl.transform.up * backDownforce *
                                                     Mathf.Abs(carControl.getSpeedFactor())
                                                    , backPosition);
        }

        public void updatePositions()
        {
            frontPosition = car.frontWheels[0].transform.parent.position;
            backPosition = car.backWheels[0].transform.parent.position;
        }

        void FixedUpdate()
        {
            updatePositions();
            AddDownForce();
            optimaizeDrag();
        }

        private void optimaizeDrag()
        {
            car._rigidbody.drag= orginalDrag*(1- Mathf.Pow( carControl.getSpeedFactor(),2));
        }

        void Start()
        {
            orginalDrag = car._rigidbody.drag;
        }

        #endregion Methods
    }
}