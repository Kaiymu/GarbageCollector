using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	[Range(5, 15)]
	public float speed = 5f;
	public float maxSpeed = 15f;
	public float speedRotation = 90f;

	public CNAbstractController MovementJoystick;
	public bool accelerating = false;

	[HideInInspector]
	public bool shipMoving = true;

	private Transform shipTransform;

	private float initialSpeed;

	private void Awake() {
		initialSpeed = speed;
	}

	void Update () {
		if(shipMoving) {
			Moving();
		}
	}

	private void Moving() {
		if(accelerating) {
			speed += Time.deltaTime * 10;
		}
		else  {
			speed -= Time.deltaTime * 10;
		}
		
		speed = Mathf.Clamp(speed, initialSpeed, maxSpeed);
		
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
