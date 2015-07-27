using UnityEngine;
using System.Collections;

public abstract class CollisionHandler : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if(col.tag == "Player") {
			PlayerTrigger(col.gameObject);
		}
	}

	void OnCollisionEnter(Collision col) { 
		if(col.gameObject.tag == "Player") {
			PlayerCollision(col.gameObject);
		}
	}

	protected virtual void PlayerTrigger(GameObject player) {
	}

	protected virtual void PlayerCollision(GameObject player) {
	}
}
