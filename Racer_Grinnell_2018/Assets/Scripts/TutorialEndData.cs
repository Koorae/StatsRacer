using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEndData : MonoBehaviour {

	public Text playerId;
	public Text groupId;
	public Text nightingaleTime;
	public Text bayesTime;

	void Start() {
		playerId.text = "Player ID: " + TutorialController.Instance.playerId;
		groupId.text = "Group ID: " + TutorialController.Instance.groupId;
		nightingaleTime.text = "Nightingale Time: " + TutorialController.Instance.nightingaleTime;
		bayesTime.text = "Bayes Time: " + TutorialController.Instance.bayesTime;
	}
}
