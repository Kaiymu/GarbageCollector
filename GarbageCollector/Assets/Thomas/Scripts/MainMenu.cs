using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public Animator fadeInAnimator;
	public GameObject menuToShow;
	public GameObject menuToUnshow;

	private int levelToLoad;
	public void SetAnimator() {
		fadeInAnimator.SetTrigger ("fade");
	}

	public void LoadLevel(int index) {
		levelToLoad = index;
		Invoke ("LoadDelayedLevel", 1f);
	}

	private void LoadDelayedLevel() {
		Application.LoadLevel(levelToLoad);
	}

	public void MenuToShow() {
		menuToShow.SetActive(true);
		menuToUnshow.SetActive(false);
	}
}
