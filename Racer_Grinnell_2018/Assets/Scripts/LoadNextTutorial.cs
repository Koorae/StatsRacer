using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextTutorial : MonoBehaviour {

	public void loadNext() {
		TutorialController.Instance.setTimes ();
		if (TutorialController.Instance.trialNum < 2) {
			SceneManager.LoadScene ("TutorialInstructions");
		} else {
			SceneManager.LoadScene ("TutorialEnd");
		}
	}
}
