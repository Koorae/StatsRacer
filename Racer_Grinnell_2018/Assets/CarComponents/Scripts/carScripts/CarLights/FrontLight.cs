namespace CarComponents
{
    using System.Collections;

    using UnityEngine;

    public class FrontLight : CarLight
    {
        #region Methods

        void lightsHandler(bool isOn)
        {
            if (isOn)
                lightOn();
            else lightOff();
        }

        // Use this for initialization
        void Start()
        {
            lightOff();
            carControl.frontLightsTrigger += lightsHandler;
        }

        #endregion Methods
    }
}