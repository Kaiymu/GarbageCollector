using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

	public float speed;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(speed * Time.deltaTime, speed * Time.deltaTime, speed * Time.deltaTime);
	}
}
