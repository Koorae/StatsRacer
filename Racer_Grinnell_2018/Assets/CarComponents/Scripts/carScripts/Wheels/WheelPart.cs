namespace CarComponents.Wheels
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class WheelPart : CarPart
    {
        #region Fields

        protected Wheel _wheel;

        #endregion Fields

        #region Properties

        public Wheel wheel
        {
            get
            {
                if (this._wheel == null)
                    this._wheel = GetComponentInParent<Wheel>();
                return this._wheel;
            }
        }

        #endregion Properties
    }
}