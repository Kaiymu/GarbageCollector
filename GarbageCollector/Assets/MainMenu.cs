using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public Animator fadeInAnimator;

	public void SetAnimator() {
		fadeInAnimator.SetTrigger ("fade");
	}

	public void LoadLevel() {
		Invoke ("LoadDelayedLevel", 1f);
	}

	private void LoadDelayedLevel() {
		Application.LoadLevel(1);
	}
}
