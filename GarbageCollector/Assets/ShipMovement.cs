using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	[Range(5, 12)]
	public float speed = 5f;
	public float speedRotation = 90f;

	public CNAbstractController MovementJoystick;
	public bool accelerating = false;

	private Transform shipTransform;

	private float initialSpeed;

	private void Awake() {
		initialSpeed = speed;
	}
	// Update is called once per frame
	void Update () {
		if(accelerating) {
			speed += Time.deltaTime;
		}
		else  {
			speed -= Time.deltaTime;
		}

		speed = Mathf.Clamp(speed, initialSpeed, 12);

		Debug.Log (speed);
		 transform.Rotate(
			MovementJoystick.GetAxis("Vertical") * Time.deltaTime * speedRotation,
			MovementJoystick.GetAxis("Horizontal") * Time.deltaTime * speedRotation,
			0);
		
	
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	public void IncreaseSpeed() {
		accelerating = true;
	}

	public void DecreaseSpeed() {
		accelerating = false;
	}
}
