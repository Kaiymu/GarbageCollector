using UnityEngine;
using System.Collections;

public class DoorMesheCollider : CollisionHandler {

	public GameObject explosion;

	private GameObject _player;

	protected override void PlayerCollision(GameObject player) {
		explosion.transform.position = player.transform.position;
		explosion.SetActive(true);
		_player = player;
		SoundManager.instance.PlayPlayerExplosionSound();
		Destroy (_player);
	}

	private void DisappearPlayer() {
		_player.GetComponent<MeshRenderer>().enabled = false;
	}
}
