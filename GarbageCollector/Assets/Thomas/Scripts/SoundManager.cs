using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;
	
	public AudioClip doorTriggerSound;
	public AudioClip playerExplosion;

	private AudioSource _mainCameraSource;

	void Awake () {
		CreateSoundManagerSingleton();
	}

	void Start() {
		_mainCameraSource = Camera.main.GetComponent<AudioSource>();
	}

	private void CreateSoundManagerSingleton() {
		if(instance == null) {
			instance = this;
		} else {
			if(this != instance) {
				Destroy(this.gameObject);
			}
		}
	}	
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayDoorSound() {
		_mainCameraSource.PlayOneShot(doorTriggerSound);
	}

	public void PlayPlayerExplosionSound() {
		_mainCameraSource.PlayOneShot(playerExplosion);
	}
}
