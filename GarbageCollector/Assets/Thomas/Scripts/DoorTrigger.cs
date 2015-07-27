using UnityEngine;
using System.Collections;

public class DoorTrigger : CollisionHandler {

	public int index;

	protected override void PlayerTrigger (GameObject player) {
		if(index == 0) {
			GameManager.instance.UpdateDoorList(index);
			index = -1;

			SoundManager.instance.PlayDoorSound();
		}
	}
}
