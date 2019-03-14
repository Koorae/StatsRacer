namespace CarComponents
{
    using System.Collections;

    using UnityEngine;

    [System.Serializable]
    public class CarLight : CarPart
    {
        #region Fields

        public Light _light;

        #endregion Fields

        #region Methods

        protected void lightOff()
        {
            _light.enabled = false;
        }

        protected void lightOn()
        {
            _light.enabled = true;
        }

        void Awake()
        {
            _light = GetComponent<Light>();
        }

        #endregion Methods
    }
}