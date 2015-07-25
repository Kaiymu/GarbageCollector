using UnityEngine;
using System.Collections;

public class TestLineRenderer : MonoBehaviour {

	public Transform[] allPoints;

	private LineRenderer _lineRenderer;

	private void Start() {
		_lineRenderer = GetComponent<LineRenderer>();
		_lineRenderer.SetVertexCount(allPoints.Length);
	}

	private void Update()
	{

		for(int i = 0; i < allPoints.Length; i++) {
			_lineRenderer.SetPosition(i, allPoints[i].position);
		}
	}
}
