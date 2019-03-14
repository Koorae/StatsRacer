using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarData : MonoBehaviour {

	private static CarData _instance;
	public static CarData Instance { get { return _instance; } }

	private Rigidbody rigidBody;
	private UnityStandardAssets.Vehicles.Car.CarController carController;

	public float curSpeed = 0;
	public float maxSpeed = 0;
	public float timeTo30;
	public float timeTo60;
	public float timeTo100;

	private bool recorded30 = false;
	private bool recorded60 = false;
	private bool recorded100 = false;

	public bool onTrack = true;

	void Awake() {
		if (_instance == null) {
			_instance = this;
		}   else if (_instance != this) {
			Destroy (gameObject);
		}
		carController = GetComponents<UnityStandardAssets.Vehicles.Car.CarController> ()[0];
		rigidBody = GetComponent<Rigidbody> ();
	}
	// Use this for initialization
	void Start () {
		timeTo30 = -1.0f;
		timeTo60 = -1.0f;
		timeTo100 = -1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		curSpeed = rigidBody.velocity.magnitude * 2.23693629f;
		if (curSpeed > maxSpeed) {
			maxSpeed = curSpeed;
		}
		if (!recorded30 && curSpeed >= 30) {
			timeTo30 = GameController.Instance.time;
			recorded30 = true;
		}
		if (!recorded60 && curSpeed >= 60) {
			timeTo60 = GameController.Instance.time;
			recorded60 = true;
		}
		if (!recorded100 && curSpeed >= 100) {
			timeTo100 = GameController.Instance.time;
			recorded100 = true;
		}
	}

	public void setCarSettings(float mass, float drag, float torque, float radius) {
		if (rigidBody != null) {
			rigidBody.mass = mass;
			rigidBody.drag = drag;
		}
		carController.m_FullTorqueOverAllWheels = torque;
		for (int i = 0; i < 4; i++) {
			if (carController.m_WheelColliders != null) {
				carController.m_WheelColliders [i].radius = radius;
			}
		}
	}
}
