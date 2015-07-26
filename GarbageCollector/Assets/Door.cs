using UnityEngine;
using System.Collections;

public class Door : CollisionHandler {

	public int index;

	protected override void PlayerCollision (GameObject player) {
		if(index == 0) {
			GameManager.instance.UpdateDoorList(index);
			index = -1;
		}
	}
}
