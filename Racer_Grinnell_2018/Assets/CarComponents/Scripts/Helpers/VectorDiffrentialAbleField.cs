namespace CarComponents.Helpers
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class VectorDiffrentialAbleField : DiffrentialAbleField<Vector3>
    {
        #region Constructors

        public VectorDiffrentialAbleField()
        {
            value = Vector3.zero;
            diffrentialValue = Vector3.zero;
        }

        #endregion Constructors

        #region Methods

        public override void setValue(Vector3 value, float deltaTime)
        {
            this.value = value;
            diffrentialValue = (trackableValue._value - trackableValue.oldValue) / deltaTime;
        }

        #endregion Methods
    }
}