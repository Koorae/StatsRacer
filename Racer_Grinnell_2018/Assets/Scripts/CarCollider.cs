using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour {

	private Rigidbody rigidBody;

	public void Awake() {
		rigidBody = GetComponent<Rigidbody> ();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Terrain") && CarData.Instance.onTrack) {
			rigidBody.drag += 1.5f;
			CarData.Instance.onTrack = false;
		} else if (other.gameObject.tag.Equals ("Track") && !CarData.Instance.onTrack) {
			rigidBody.drag -= 1.5f;
			CarData.Instance.onTrack = true;
		} else if (other.gameObject.tag.Equals ("Submit")) {
			GameController.Instance.submitData (true);
		} else if (other.gameObject.tag.Equals ("Finished")) {
			GameController.Instance.submitData (false);
		}
	}
}
