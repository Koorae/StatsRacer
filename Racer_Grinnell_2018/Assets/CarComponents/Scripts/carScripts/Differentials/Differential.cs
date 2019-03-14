namespace CarComponents.Differentials
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    [System.Serializable]
    public class Differential : CarPart
    {
        #region Fields

        public IHasTorqueOutput inputTorqueObject;
        public float limitedDiffrenceStep = 0;
        public IDifferentialOutput[] outs;

        #endregion Fields

        #region Properties

        public float inputTorque
        {
            get;
            set;
        }

        public float outsResultAvg
        {
            get
            {
                return (outsResultSum) / 2;
            }
        }

        public float outsResultSum
        {
            get
            {
                if (outs == null || outs.Length < 2)
                    return 0;
                return outs[0].differentialResult + outs[1].differentialResult;
            }
        }

        #endregion Properties

        #region Methods

        public void balanceOutsTorquePercent()
        {
            if (outsResultSum == 0)
                return;
            float tm = Time.deltaTime;
            foreach (IDifferentialOutput outObject in outs)
            {
                float torquePercentDiffrence = limitDiffrence(outObject);
                outObject.currentTorquePercent += torquePercentDiffrence;

            }
        }

        public void FixedUpdate()
        {
            balanceOutsTorquePercent();

            foreach (IDifferentialOutput outObject in outs)
                outObject.inputTorque = inputTorque * outObject.currentTorquePercent;
        }

        public float limitDiffrence(IDifferentialOutput outObject)
        {
            float torquePercentDiffrence = (outsResultAvg - outObject.differentialResult) / outsResultSum;
            if (limitedDiffrenceStep != 0)
                torquePercentDiffrence = Mathf.Min(limitedDiffrenceStep, Mathf.Abs(torquePercentDiffrence)) * Mathf.Sign(torquePercentDiffrence);
            return torquePercentDiffrence;
        }

        public void reset()
        {
            foreach (IDifferentialOutput outObject in outs)
                outObject.currentTorquePercent = outObject.defaultTorquePercent;
        }

        #endregion Methods
    }
}