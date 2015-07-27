using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public Animator _test;
	
	public void SetAnimator() {
		_test.SetTrigger ("fade");
	}
}
