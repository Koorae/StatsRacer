namespace CarComponents
{
    using System.Collections;

    using CarComponents.Wheels;

    using UnityEngine;

    public class CameraControl : MonoBehaviour
    {
        #region Fields

        public Car car;
        public float defaultFOV = 50f;
        public float distance = 12.5f;
        public float height = 3.7f;
        public float heightDamping = 0.4f;
        public float rotaionDamping = 0.2f;
        public float zoomRation = 0.35f;
        public float zRotaionDamping = 3f;

        float acceleration = 0;
        Quaternion currentRotaion;
        float myAngle;
        float myHeight;
        float myZAngle;
        CarControl m_CarControl;
        float oldZ = 0;
        Vector3 position;
        Vector3 rotaionVector;
        Transform target;
        float wantedAngle;
        float wantedHeight;
        float wantedZAngle;
        Camera _camera;
        [SerializeField]
        private Transform _car;

        #endregion Fields

        #region Methods

        void calcZRotation()
        {
            Vector3 zRotation = car.transform.InverseTransformDirection(car._rigidbody.angularVelocity);
            rotaionVector.z = (car._rigidbody.rotation.z+ zRotation.y)/2;// * distance ;
        }

        void inRaceMove()
        {
            wantedAngle = rotaionVector.y;
            wantedHeight = _car.position.y + height + sumSuspintionDistance();

            wantedZAngle = rotaionVector.z;

            myAngle = transform.eulerAngles.y;
            myHeight = transform.position.y;

            myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotaionDamping * Time.deltaTime);
            myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime);
            myZAngle = Mathf.LerpAngle(oldZ, wantedZAngle, zRotaionDamping * Time.deltaTime);

            currentRotaion = Quaternion.Euler(0, myAngle, 0);
            position = _car.position;
            position -= currentRotaion * Vector3.forward * distance;
            position.y = myHeight ;
            transform.position = position;

            transform.LookAt(target);
            transform.Rotate(0, 0, myZAngle);
            oldZ = myZAngle;
        }

        void Start()
        {
            if (car == null)
                car = GameObject.FindObjectOfType<Car>();

            if (_car == null)
                _car = car.transform;

            oldZ = car.transform.rotation.eulerAngles.z;
            m_CarControl = car.GetComponent<CarControl>();
            target = car.transform.Find("CameraTarget");
            _camera = GetComponent<Camera>();
            rotaionVector = car.transform.rotation * Vector3.one;
            position = _car.position;
        }

        float sumSuspintionDistance()
        {
            float dis = 0.0f;
            foreach (Wheel wheel in car.wheels)
            {
                dis += wheel.wheelCollider.suspensionDistance;
            }
            dis /= 4;
            return dis;
        }

        void Update()
        {
            rotaionVector.y = car.transform.eulerAngles.y;
            calcZRotation();

            acceleration = Mathf.Lerp(acceleration, m_CarControl.speedObject.acceleration, Time.deltaTime);
            _camera.fieldOfView = defaultFOV + (20 * m_CarControl.getSpeedFactor()) + acceleration;
            inRaceMove();
        }

        #endregion Methods
    }
}