using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance;
	
	private int _numberGatePassed = 0;

	public List<Transform> doorList;

	void Awake () {
		CreateGameManagerSingleton();
	}

	private void CreateGameManagerSingleton() {
		if(instance == null) {
			instance = this;
		} else {
			if(this != instance) {
				Destroy(this.gameObject);
			}
		}
	}	

	void Start() {
		for(int i = 0; i < doorList.Count; i++) {
			doorList[i].GetComponent<Door>().index = i;
		}
	}

	public void UpdateDoorList(int indexToRemove) {
		_numberGatePassed++;
		doorList.RemoveAt(indexToRemove);
		for(int i = 0; i < doorList.Count; i++) {
			doorList[i].GetComponent<Door>().index = i;
		}
	}

	private void Update() {
		Win();
	}

	public void Win() {
		if(doorList.Count == 0) {
			Debug.Log ("You won");
		}
	}
}
