namespace CarComponents.Helpers
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class PID_Runner
    {
        #region Fields

        float fixedTime = 0.1f;
        PID pid;

        #endregion Fields

        #region Constructors

        public PID_Runner(float pG, float iG, float dG,
            float pMax, float pMin, float oMax, float oMin,
            GetFloat pvFunc, GetFloat spFunc, SetFloat outFunc, float fixedTime)
        {
            pid = new PID(pG, iG, dG, pMax, pMin, oMax, oMin, pvFunc, spFunc, outFunc);
            this.fixedTime = fixedTime;
        }

        #endregion Constructors

        #region Properties

        public bool isRunning
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public IEnumerator run()
        {
            if (isRunning)
                yield return null;
            isRunning = true;
            while (isRunning)
            {
                pid.Compute();
                yield return new WaitForSeconds(fixedTime);
            }
        }

        public void stop()
        {
            pid.Reset();
            isRunning = false;
        }

        #endregion Methods
    }
}