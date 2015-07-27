using UnityEngine;
using System.Collections;

public class DoorMesheCollider : MonoBehaviour {

	private void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag == "Player") {
			Destroy(col.gameObject);
		}
	}
}
