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

	[HideInInspector]
	public bool inverseYAxis = true;

	[HideInInspector]
	public bool inverseXAxis = true;

	private Transform shipTransform;

	private float initialSpeed;

	private void Awake() {
		initialSpeed = speed;
	}

	public void Start() {
		if(PlayerPrefs.GetString("inverseXAxis") == "True") {
			inverseXAxis = true;
		} else {
			inverseXAxis = false;
		}

		if(PlayerPrefs.GetString("inverseYAxis") == "True") {
			inverseYAxis = true;
		} else {
			inverseYAxis = false;
		}

		Debug.Log(inverseXAxis);
	}

	void Update () {
		if(shipMoving) {
			Moving();
		}
	}

	public void ChangeXAxis(bool inverse) {
		inverseXAxis = inverse;
		PlayerPrefs.SetString("inverseXAxis", inverse.ToString());

	}

	public void ChangeYAxis(bool inverse) {
		inverseYAxis = inverse;
		PlayerPrefs.SetString("inverseYAxis", inverse.ToString());
	}

	private void Moving() {
		if(accelerating) {
			speed += Time.deltaTime * 10;
		}
		else  {
			speed -= Time.deltaTime * 10;
		}

		float inputVertical = MovementJoystick.GetAxis("Vertical");
		float inputHorizontal = MovementJoystick.GetAxis("Horizontal");


		if(inverseXAxis) {
			inputVertical = -MovementJoystick.GetAxis("Vertical");
		}

		if(inverseYAxis) {
			inputHorizontal = -MovementJoystick.GetAxis("Horizontal");
		}

		speed = Mathf.Clamp(speed, initialSpeed, maxSpeed);


		transform.Rotate(
			inputVertical * Time.deltaTime * speedRotation,
			inputHorizontal * Time.deltaTime * speedRotation,
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
