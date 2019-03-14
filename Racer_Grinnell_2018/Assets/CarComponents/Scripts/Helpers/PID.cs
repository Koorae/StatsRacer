namespace CarComponents.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    #region Delegates

    public delegate float GetFloat();

    public delegate void SetFloat(float value);

    #endregion Delegates

    public class PID
    {
        #region Fields

        private float errSum;
        private float kd;
        private float ki;

        //Gains
        private float kp;
        private float lastPV;

        //Running Values
        private DateTime lastUpdate;
        private float outMax;
        private float outMin;

        //Max/Min Calculation
        private float pvMax;
        private float pvMin;

        //Reading/Writing Values
        private GetFloat readPV;
        private GetFloat readSP;
        private SetFloat writeOV;

        #endregion Fields

        #region Constructors

        public PID(float pG, float iG, float dG,
            float pMax, float pMin, float oMax, float oMin,
            GetFloat pvFunc, GetFloat spFunc, SetFloat outFunc)
        {
            kp = pG;
            ki = iG;
            kd = dG;
            pvMax = pMax;
            pvMin = pMin;
            outMax = oMax;
            outMin = oMin;
            readPV = pvFunc;
            readSP = spFunc;
            writeOV = outFunc;
        }

        ~PID()
        {
            Reset();
            readPV = null;
            readSP = null;
            writeOV = null;
        }

        #endregion Constructors

        #region Properties

        public float DGain
        {
            get { return kd; }
            set { kd = value; }
        }

        public float IGain
        {
            get { return ki; }
            set { ki = value; }
        }

        public float OutMax
        {
            get { return outMax; }
            set { outMax = value; }
        }

        public float OutMin
        {
            get { return outMin; }
            set { outMin = value; }
        }

        public float PGain
        {
            get { return kp; }
            set { kp = value; }
        }

        public float PVMax
        {
            get { return pvMax; }
            set { pvMax = value; }
        }

        public float PVMin
        {
            get { return pvMin; }
            set { pvMin = value; }
        }

        #endregion Properties

        #region Methods

        public void Compute()
        {
            if (readPV == null || readSP == null || writeOV == null)
                return;

            float pv = readPV();
            float sp = readSP();

            //We need to scale the pv to +/- 100%, but first clamp it
            pv = Clamp(pv, pvMin, pvMax);
            pv = ScaleValue(pv, pvMin, pvMax, -1.0f, 1.0f);

            //We also need to scale the setpoint
            sp = Clamp(sp, pvMin, pvMax);
            sp = ScaleValue(sp, pvMin, pvMax, -1.0f, 1.0f);

            //Now the error is in percent...
            float err = sp- pv ;

            float pTerm = err * kp;
            float iTerm = 0.0f;
            float dTerm = 0.0f;

            float partialSum = 0.0f;
            DateTime nowTime = DateTime.Now;

            if (lastUpdate != null)
            {
                float dT = (float)(nowTime - lastUpdate).TotalSeconds;

                //Compute the integral if we have to...
                if (pv >= pvMin && pv <= pvMax)
                {
                    partialSum = errSum + (err * dT);
                    iTerm = ki * partialSum;
                }

                if (dT != 0.0f)
                    dTerm = kd * (pv - lastPV) / (float)dT;
            }

            lastUpdate = nowTime;
            errSum = partialSum;
            lastPV = pv;

            //Now we have to scale the output value to match the requested scale
            float outReal = pTerm + iTerm - dTerm;

            outReal = Clamp(outReal, -1, 1.0f);
            outReal = ScaleValue(outReal, -1, 1.0f, outMin, outMax);

            //Write it out to the world
            writeOV(outReal);
        }

        public void Reset()
        {
            errSum = 0.0f;
            lastUpdate = DateTime.Now;
        }

        private float Clamp(float value, float min, float max)
        {
            if (value > max)
                return max;
            if (value < min)
                return min;
            return value;
        }

        private float ScaleValue(float value, float valuemin,
            float valuemax, float scalemin, float scalemax)
        {
            float vPerc = (value - valuemin) / (valuemax - valuemin);
            float bigSpan = vPerc * (scalemax - scalemin);

            float retVal = scalemin + bigSpan;

            return retVal;
        }

        #endregion Methods
    }
}