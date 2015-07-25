using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorManager : MonoBehaviour {

	public static int numberPassed = 0;
	public Text scoreText;

	private void Update() {
		scoreText.text = "Score : " + numberPassed;
	}
}
