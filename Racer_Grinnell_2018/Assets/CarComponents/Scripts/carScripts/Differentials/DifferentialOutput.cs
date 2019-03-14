namespace CarComponents.Differentials
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class DifferentialOutput : Differential, IDifferentialOutput
    {
        #region Fields

        public float _currentTorquePercent;
        [SerializeField]
        public float _minTorquePercent = 0f;

        [SerializeField]
        protected float _defaultTorquePercent = 0.5f;
        [SerializeField]
        protected float _maxTorquePercent = 1f;

        #endregion Fields

        #region Properties

        public float currentTorquePercent
        {
            get
            {
                return _currentTorquePercent;
            }

            set
            {
                _currentTorquePercent = Mathf.Min(value, maxTorquePercent);
                _currentTorquePercent = Mathf.Max(minTorquePercent, _currentTorquePercent);
            }
        }

        public float defaultTorquePercent
        {
            get
            {
                return _defaultTorquePercent;
            }
        }

        public virtual float differentialResult
        {
            get
            {
                return outsResultSum;
            }
        }

        public float maxTorquePercent
        {
            get
            {
                return _maxTorquePercent;
            }
        }

        public float minTorquePercent
        {
            get
            {
                return _minTorquePercent;
            }
        }

        #endregion Properties
    }
}