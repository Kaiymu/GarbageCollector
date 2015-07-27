using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {

	public Transform target;
	public Transform objectToFollow;

	void LateUpdate () {
		if(objectToFollow != null && target != null) {
			transform.position = objectToFollow.transform.position;
			transform.rotation = objectToFollow.transform.rotation;
			transform.LookAt(target.position);
		}
	}
}
