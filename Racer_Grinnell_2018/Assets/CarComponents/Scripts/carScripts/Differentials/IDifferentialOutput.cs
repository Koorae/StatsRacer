namespace CarComponents.Differentials
{
    public interface IDifferentialOutput
    {
        #region Properties

        float currentTorquePercent
        {
            get; set;
        }

        float defaultTorquePercent
        {
            get;
        }

        float differentialResult
        {
            get;
        }

        float inputTorque
        {
            set;
        }

        float maxTorquePercent
        {
            get;
        }

        float minTorquePercent
        {
            get;
        }

        #endregion Properties
    }
}