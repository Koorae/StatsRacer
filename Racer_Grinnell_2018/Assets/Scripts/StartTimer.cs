using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour {

	public void startTimer() {
		GameController.Instance.timing = true;
	}
}
