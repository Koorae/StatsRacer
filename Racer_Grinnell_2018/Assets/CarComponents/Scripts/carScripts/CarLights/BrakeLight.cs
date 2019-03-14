namespace CarComponents
{
    using System;

    using UnityEngine;

    public class BrakeLight : CarLight
    {
        #region Methods

        void brakingListner()
        {
            lightOn();
        }

        void offListner()
        {
            lightOff();
        }

        void Start()
        {
            lightOff();
            carControl.brakingListner += brakingListner;
            carControl.forwardListner += offListner;
            carControl.backwardListner += offListner;
            carControl.neutralListner += offListner;
            carControl.stoppedListner += offListner;
        }

        #endregion Methods
    }
}