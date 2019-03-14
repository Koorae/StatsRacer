namespace CarComponents.Differentials
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class CenterDifferential : Differential
    {
        #region Methods

        public new void FixedUpdate()
        {
            inputTorque = inputTorqueObject.outputTorque;
            base.FixedUpdate();
        }

        void Start()
        {
            outs = GetComponentsInChildren<WheelsDifferential>();
            inputTorqueObject = car.transmission;
        }

        #endregion Methods
    }
}