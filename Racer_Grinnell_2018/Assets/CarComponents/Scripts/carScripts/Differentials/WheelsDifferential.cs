namespace CarComponents.Differentials
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Wheels;

    using UnityEngine;

    [System.Serializable]
    public class WheelsDifferential : DifferentialOutput
    {
        #region Methods

        void Start()
        {
            currentTorquePercent = defaultTorquePercent;
            outs = GetComponentsInChildren<Wheel>();
        }

        #endregion Methods
    }
}