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

	private float[] _levelTime = new float[3];
	private float[] _levelScore = new float[3];
	
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
			doorList[i].GetComponent<DoorTrigger>().index = i;
		}

		player = GameObject.FindGameObjectWithTag("Player");
		_shipMovement = player.GetComponent<ShipMovement>();

		_levelScore = PlayerPrefsX.GetFloatArray("score");
		_levelTime = PlayerPrefsX.GetFloatArray("time");
	}

	public void UpdateDoorList(int indexToRemove) {
		_numberGatePassed++;
		doorList.RemoveAt(indexToRemove);
		for(int i = 0; i < doorList.Count; i++) {
			doorList[i].GetComponent<DoorTrigger>().index = i;
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

		if(player == null) {
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
		float finalScore = (maxRaceTime - _playerTime) * _numberGatePassed;
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
				SaveScoreAndTime();
			break;

			case GAME_STATUS.LOSE :
				_shipMovement.shipMoving = false;
				calculateTime = false;
				SaveScoreAndTime();
			break;

			case GAME_STATUS.PAUSE :
				_shipMovement.shipMoving = false;
				calculateTime = false;
			break;
		}
	}

	private void SaveScoreAndTime() {
		if(CalculateScore() < _levelScore[Application.loadedLevel-1]) {
			_levelScore[Application.loadedLevel-1] = CalculateScore();
			PlayerPrefsX.SetFloatArray("score", _levelScore);
		}

		if(TimerSinceStart() < _levelTime[Application.loadedLevel-1]) {
			_levelTime[Application.loadedLevel-1] = TimerSinceStart();
			PlayerPrefsX.SetFloatArray("time", _levelTime);
		}
	}

	public float BestScore() {
		return _levelScore[Application.loadedLevel-1];
	}

	public float BestTime() {
		return _levelTime[Application.loadedLevel-1];
	}

}
