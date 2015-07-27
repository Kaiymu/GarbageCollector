using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GAME_STATUS {START, WIN, LOSE, PAUSE};

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

	public GAME_STATUS gameStatus;

	private ShipMovement _shipMovement;
	private bool calculateTime = true;

	private float[] _levelTime = new float[3];
	private float[] _levelScore = new float[3];

	public string statusTest;
	
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

		FillLevelTimeArray();
		FillLevelScoreArray();
	}

	private void FillLevelTimeArray() {
		Debug.Log (PlayerPrefs.GetFloat("timelvl1"));
		if(PlayerPrefs.GetFloat("timelvl1") != 0) {
			_levelTime[0] = PlayerPrefs.GetFloat("timelvl1");
		} else {
			_levelTime[0] = 9999;
		}

		Debug.Log (_levelTime[0]);
		_levelTime[1] = PlayerPrefs.GetFloat("timelvl2");
		_levelTime[2] = PlayerPrefs.GetFloat("timelvl3");

	}

	private void FillLevelScoreArray() {
		if(PlayerPrefs.GetFloat("scorelvl1") != 0) {
			_levelScore[0] = PlayerPrefs.GetFloat("scorelvl1");
		} else {
			_levelScore[0] = 0;
		}
		_levelScore[1] = PlayerPrefs.GetFloat("scorelvl2");
		_levelScore[2] = PlayerPrefs.GetFloat("scorelvl3");

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
			statusTest = gameStatus.ToString();
		}
	}

	private void Lose() {
		if(maxRaceTime < _playerTime) {
			gameStatus = GAME_STATUS.LOSE;
			statusTest = gameStatus.ToString();
		}

		if(player == null) {
			gameStatus = GAME_STATUS.LOSE;
			statusTest = gameStatus.ToString();
		}
	}

	private void Pause() {
		GAME_STATUS previousStatus = GAME_STATUS.START;

		if(isPause) {
			previousStatus = gameStatus;
			gameStatus = GAME_STATUS.PAUSE;
			statusTest = gameStatus.ToString();
		} else {
			gameStatus = previousStatus;
			statusTest = gameStatus.ToString();
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
		int indexLevel = Application.loadedLevel -1 ;

		if(CalculateScore() > _levelScore[indexLevel]) {
			_levelScore[indexLevel] = CalculateScore();
			PlayerPrefs.SetFloat("scorelvl"+Application.loadedLevel, _levelScore[indexLevel]);
		}

		if(TimerSinceStart() < _levelTime[indexLevel]) {
			_levelTime[indexLevel] = TimerSinceStart();
			PlayerPrefs.SetFloat("timelvl"+Application.loadedLevel, _levelTime[indexLevel]);
		}
	}

	public float BestScore() {
		if(_levelScore[Application.loadedLevel-1] != null) {
			return _levelScore[Application.loadedLevel-1];
		}

		return 999f;
	}

	public float BestTime() {
		if(_levelTime[Application.loadedLevel-1] != null) {
			return _levelTime[Application.loadedLevel-1];
		} 
		return 999f;
	}

}
