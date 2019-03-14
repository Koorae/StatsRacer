using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour {

	private static DataManager _instance;
	public static DataManager Instance { get { return _instance; } }

	public int level;

	public int body;
	public int engine;
	public int tire;

	public string bodyName;
	public string engineName;
	public string tireName;
	public int car;

	public float mass;
	public float drag;
	public float traction;
	public float torque;
	public float topSpeed;
	public float tireRadius;

	public float[] checkPoints;

	void Awake() {
		if (_instance == null) {
			DontDestroyOnLoad (gameObject);
			_instance = this;
		} else if (_instance != this) {
			Destroy (gameObject);
		}
	}

	void Start() {
		SceneManager.sceneLoaded += OnLevelLoaded;
		car = -1;
		if (!SceneManager.GetActiveScene ().name.Equals ("MainMenu")
			&& !SceneManager.GetActiveScene ().name.Equals ("PartsSelect")
			&& !SceneManager.GetActiveScene ().name.Equals ("CarSelect")) {
			setCarSettings ();
		}
		for (int i = 0; i < checkPoints.Length; i++) {
			checkPoints [i] = -1.0f;
		}
		traction = 1.0f;
		topSpeed = 150.0f;
	}

	void OnLevelLoaded(Scene scene, LoadSceneMode mode) { 
		if (!scene.name.Equals("MainMenu") && !scene.name.Equals("PartsSelect") 
		&& !scene.name.Equals("CarSelect")) {
			setCarSettings ();
		} 
	}

	public void setCarSettings() {
		car = 9 * body + 3 * engine + tire + 1;
		if (tire == 0) {
			tireName = "Bayes";
			tireRadius = 0.4f;
		} else if (tire == 1) {
			tireName = "Nightingale";
			tireRadius = 0.5f;
		} else if (tire == 2) {
			tireName = "Gauss";
			tireRadius = 0.3f;
		}
		if (body == 0) {
			bodyName = "Bayes";
			if (engine == 0) {
				engineName = "Bayes";
				mass = 1500.0f;
				drag = 0.15f;
				torque = 2500.0f;
			} else if (engine == 1) {
				engineName = "Nightingale";
				mass = 1500.0f;
				drag = 0.05f;
				torque = 2150.0f;
			} else if (engine == 2) {
				engineName = "Gauss";
				mass = 1500.0f;
				drag = 0.1f;
				torque = 2500.0f;
			}
		} else if (body == 1) {
			bodyName = "Nightingale";
			if (engine == 0) {
				engineName = "Bayes";
				mass = 1900.0f;
				drag = 0.05f;
				torque = 2500.0f;
			} else if (engine == 1) {
				engineName = "Nightingale";
				mass = 1900.0f;
				drag = 0.1f;
				torque = 2500.0f;
			} else if (engine == 2) {
				engineName = "Gauss";
				mass = 1900.0f;
				drag = 0.1f;
				torque = 3000.0f;
			}
		} else if (body == 2) {
			bodyName = "Gauss";
			if (engine == 0) {
				engineName = "Bayes";
				mass = 1500.0f;
				drag = 0.1f;
				torque = 2150.0f;
			} else if (engine == 1) {
				engineName = "Nightingale";
				mass = 1900.0f;
				drag = 0.1f;
				torque = 2150.0f;
			} else if (engine == 2) {
				engineName = "Gauss";
				mass = 1500.0f;
				drag = 0.05f;
				torque = 2500.0f;
			}
		}
		GameController.Instance.setCarSettings (mass, drag, torque, tireRadius);
	}

	public void setLevel(int level) {
		this.level = level;
	}
}
