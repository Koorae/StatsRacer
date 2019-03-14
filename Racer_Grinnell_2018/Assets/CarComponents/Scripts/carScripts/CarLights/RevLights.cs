namespace CarComponents
{
    using System.Collections;

    using UnityEngine;

    public class RevLights : CarLight
    {
        #region Methods

        void offListner()
        {
            lightOff();
        }

        void revListner()
        {
            lightOn();
        }

        void Start()
        {
            lightOff();
            carControl.brakingListner += offListner;
            carControl.forwardListner += offListner;
            carControl.backwardListner += revListner;
            carControl.neutralListner += offListner;
            carControl.stoppedListner += offListner;
        }

        #endregion Methods
    }
}