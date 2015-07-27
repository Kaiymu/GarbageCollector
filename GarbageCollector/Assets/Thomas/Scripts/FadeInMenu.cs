using UnityEngine;
using System.Collections;

public class FadeInMenu : MonoBehaviour {

	public Animator imageFade;
	
	public void SetAnimator() {
		imageFade.SetTrigger ("fade");
	}
}
