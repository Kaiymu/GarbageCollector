using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance;

	[Header("Value is in second")]
	public int maxRaceTime;
	public List<Transform> doorList;

	[HideInInspector]
	public GameObject player;

	[HideInInspector]
	public bool isPause = false;
	
	private int _numberGatePassed = 0;
	private float _playerTime = 0f;

	public enum GAME_STATUS {START, WIN, LOSE, PAUSE};
	public GAME_STATUS gameStatus;

	private ShipMovement _shipMovement;
	private bool calculateTime = true;
	
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

	private void Start() {
		for(int i = 0; i < doorList.Count; i++) {
			doorList[i].GetComponent<Door>().index = i;
		}

		player = GameObject.FindGameObjectWithTag("Player");
		_shipMovement = player.GetComponent<ShipMovement>();
	}

	public void UpdateDoorList(int indexToRemove) {
		_numberGatePassed++;
		doorList.RemoveAt(indexToRemove);
		for(int i = 0; i < doorList.Count; i++) {
			doorList[i].GetComponent<Door>().index = i;
		}
	}

	private void Update() {
		Pause();
		Win();
		Lose();

		GameStatus();
	}

	private void Win() {
		if(doorList.Count == 0) {
			CalculateScore();
			gameStatus = GAME_STATUS.WIN;
		}
	}

	private void Lose() {
		if(maxRaceTime < _playerTime) {
			gameStatus = GAME_STATUS.LOSE;
		}
	}

	private void Pause() {
		GAME_STATUS previousStatus = GAME_STATUS.START;

		if(isPause) {
			previousStatus = gameStatus;
			gameStatus = GAME_STATUS.PAUSE;
		} else {
			gameStatus = previousStatus;
		}
	}

	public void SetPause() {
		if(isPause) {
			isPause = false;
		} else {
			isPause = true;
		}
	}
	
	public float TimerSinceStart() {
		if(calculateTime) {
			return  _playerTime += Time.deltaTime;
		}

		return _playerTime;
	}

	public float CalculateScore() {
		float finalScore = maxRaceTime - _playerTime;
		return finalScore;
	}

	public void GoToLevel(int level) {
		Application.LoadLevel(level);
	}

	public void ReloadCurrentLevel() {
		Application.LoadLevel(Application.loadedLevel);
	}

	private void GameStatus() {
		switch(gameStatus) 
		{
			case GAME_STATUS.START :
			_shipMovement.shipMoving = true;
			calculateTime = true;
			break;

			case GAME_STATUS.WIN :
				_shipMovement.shipMoving = false;
				calculateTime = false;
			break;

			case GAME_STATUS.LOSE :
				_shipMovement.shipMoving = false;
				calculateTime = false;
			break;

			case GAME_STATUS.PAUSE :
				_shipMovement.shipMoving = false;
				calculateTime = false;
			break;
		}
	}

}
