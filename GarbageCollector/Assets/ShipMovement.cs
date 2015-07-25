using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	public float speed = 5f;
	public float speedRotation = 90f;

	public CNAbstractController MovementJoystick;


	private Transform shipTransform;
	// Update is called once per frame
	void Update () {

		 transform.Rotate(
			-MovementJoystick.GetAxis("Vertical") * Time.deltaTime * speedRotation,
			MovementJoystick.GetAxis("Horizontal") * Time.deltaTime * speedRotation,
			0);
		
	
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
