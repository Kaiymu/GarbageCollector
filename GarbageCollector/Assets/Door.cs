﻿using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			DoorManager.numberPassed++;
		}
	}
}