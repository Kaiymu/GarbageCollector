using UnityEngine;
using System.Collections;

public class PlayerToGate : MonoBehaviour {

	private LineRenderer _lineRenderer;
	private GameObject _player;

	private void Start() {
		_lineRenderer = GetComponent<LineRenderer>();
		_player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update() {
		if(_player != null && GameManager.instance.doorList.Count > 0) {
			_lineRenderer.SetPosition(0, _player.transform.position);
			_lineRenderer.SetPosition(1, GameManager.instance.doorList[0].position);
		} else {
			_lineRenderer.SetPosition(0, Vector3.zero);
			_lineRenderer.SetPosition(1, Vector3.zero);
		}
	}
}
