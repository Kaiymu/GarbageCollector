using UnityEngine;
using System.Collections;

public class AudioEngine : MonoBehaviour {

	public AudioClip test;
	private ShipMovement _shipMovement;

	private bool playAccelerating = true;

	private void Start() {
		_shipMovement = GetComponent<ShipMovement>();
	}
	void Update() {
		if(_shipMovement.accelerating) {
			if(playAccelerating) {
				GetComponent<AudioSource>().PlayOneShot(test, 0.4f);
				playAccelerating = false;
			}
		} 
		else {
			playAccelerating = true;
		}
	}

}
