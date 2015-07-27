using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text gatesPassed;
	public Text timer;
	public Text finalScore; 

	public GameObject gameMenu;
	public GameObject pauseMenu;
	public GameObject winMenu; 
	public GameObject loseMenu;

	private int maxDoorToPass;

	private void Start() {
		maxDoorToPass = GameManager.instance.doorList.Count;
	}
	private void Update() {
		DisplayGatePassed();
		DisplayTimer();
		DisplayFinalScore();
		GameStatus();
	}

	private void DisplayGatePassed() {
		gatesPassed.text = "Gates : " + GameManager.instance.doorList.Count + " / " + maxDoorToPass;
	}

	private void DisplayTimer() {
		timer.text = "Timer : " + GameManager.instance.TimerSinceStart().ToString("f3");
	}

	private void DisplayFinalScore() {
		finalScore.text = GameManager.instance.CalculateScore().ToString("f1");
	}
	

	private void GameStatus() {
		switch(GameManager.instance.gameStatus) 
		{
		case GameManager.GAME_STATUS.START :
			ActivateDesactivateMenu(true, false, false, false);
			break;
			
		case GameManager.GAME_STATUS.WIN :
			ActivateDesactivateMenu(false, false, true, false);
			break;
			
		case GameManager.GAME_STATUS.LOSE :
			ActivateDesactivateMenu(true, false, false, true);
			break;
			
		case GameManager.GAME_STATUS.PAUSE :
			ActivateDesactivateMenu(true, true, false, false);
			break;
		}
	}

	private void ActivateDesactivateMenu(bool isActiveGameMenu, bool isActivePauseMenu, bool isActivateWinMenu, bool isActivateLoseMenu) {
		gameMenu.SetActive(isActiveGameMenu);
		pauseMenu.SetActive(isActivePauseMenu);
		winMenu.SetActive(isActivateWinMenu);
		loseMenu.SetActive(isActivateLoseMenu);
	}

}
