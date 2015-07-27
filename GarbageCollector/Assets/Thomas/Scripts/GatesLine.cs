using UnityEngine;
using System.Collections;

public class GatesLine : MonoBehaviour {
	
	private LineRenderer _lineRenderer;
	private int _gatesNumber;

	private void Start() {
		_lineRenderer = GetComponent<LineRenderer>();
		_lineRenderer.SetVertexCount(GameManager.instance.doorList.Count);
		_gatesNumber = GameManager.instance.doorList.Count;
	}

	private void Update() {
		if(GameManager.instance.doorList.Count > 0) {
			for(int i = 0; i < GameManager.instance.doorList.Count; i++) {
				_lineRenderer.SetPosition(i, GameManager.instance.doorList[i].position);
			}
		} else {
			for(int i = 0; i < _gatesNumber; i++) {
				_lineRenderer.SetPosition(i, Vector3.zero);
			}
		}
	}
}
