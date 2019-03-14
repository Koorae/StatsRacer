namespace CarComponents.Utils
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class LimitedFloat
    {
        #region Fields

        private float max;
        private float min;
        private float _value;

        #endregion Fields

        #region Constructors

        public LimitedFloat(float value, float max, float min)
        {
            this.max = max;
            this.min = min;
            this.value = value;
        }

        #endregion Constructors

        #region Properties

        public float value
        {
            get
            {
                return _value;
            }
            set
            {
                this._value = Mathf.Min(value, this.max);
                this._value = Mathf.Max(min, this._value);
            }
        }

        #endregion Properties
    }
}