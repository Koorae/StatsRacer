namespace CarComponents.Helpers
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class FloatDiffrentialAbleField : DiffrentialAbleField<float>
    {
        #region Constructors

        public FloatDiffrentialAbleField()
        {
            trackableValue._value = 0;
            diffrentialValue = 0;
        }

        #endregion Constructors

        #region Methods

        public override void setValue(float value, float deltaTime)
        {
            this.value = value;
            diffrentialValue = (trackableValue._value - trackableValue.oldValue) / deltaTime;
        }

        #endregion Methods
    }
}