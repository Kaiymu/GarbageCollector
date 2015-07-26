using UnityEngine;
using System.Collections;

public abstract class CollisionHandler : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			PlayerCollision(other.gameObject);
		}
	}

	protected abstract void PlayerCollision(GameObject player);
}
