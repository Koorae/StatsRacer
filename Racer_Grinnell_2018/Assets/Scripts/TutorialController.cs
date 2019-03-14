using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {
	private static TutorialController _instance;
	public static TutorialController Instance { get { return _instance; } }

	public string playerId;
	public string groupId;
	public float nightingaleTime;
	public float bayesTime;

	public int trialNum = 0;

	void Awake () {
		if (_instance == null) {
			DontDestroyOnLoad (gameObject);
			_instance = this;
		} else if (_instance != this) {
			Destroy (gameObject);
		}
	}

	void Start() {
		SceneManager.sceneLoaded += OnLevelLoaded;
		setTutorial ();
		playerId = PlayerData.Instance.playerId;
		groupId = PlayerData.Instance.groupId;

	}

	void OnLevelLoaded(Scene scene, LoadSceneMode mode) {
		setTutorial ();
	}

	public void setTutorial() {
		if (SceneManager.GetActiveScene ().name.Equals ("TutorialInstructions")) {
			trialNum = (trialNum % 2) + 1;
			VisualManager.Instance.showInstructions ();
		} else if (SceneManager.GetActiveScene ().name.Equals ("Tutorial")) {
			if (trialNum == 1) { //Nightingale
				//CarData.Instance.setCarSettings (1500.0f, 0.05f, 2500.0f, 0.3f);
				DataManager.Instance.body = 1;
				DataManager.Instance.engine = 1;
				DataManager.Instance.tire = 1;
			} else if (trialNum == 2) { //Bayes
				//CarData.Instance.setCarSettings (1900.0f, 0.1f, 2500.0f, 0.5f);
				DataManager.Instance.body = 0;
				DataManager.Instance.engine = 0;
				DataManager.Instance.tire = 0;
			}
		} else {
		}
	}

	public void setTimes() {
		if (trialNum == 1) {
			nightingaleTime = GameController.Instance.time;
		} else if (trialNum == 2) {
			bayesTime = GameController.Instance.time;
		}
	}
}
