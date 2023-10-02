using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	Text titleText;
	AudioSource aud;
	public AudioClip selectButton, backButton; 


	// Use this for initialization
	void Start () {


		//titleText.text = "Contraction";
        aud = GetComponent<AudioSource>();
    }

	public void PlayGame()
	{
		//should load first level
		aud.PlayOneShot(selectButton); 
		StartCoroutine(PlayButtonSoundDelay()); //level may load quicker than the sound has time to play.
		//If that is the case, keep this here. If not, you can dummy this out if you want.
	}

	public void QuitGame()
	{
		//should quit the game
		aud.PlayOneShot(selectButton);
        Debug.Log("GAME WILL CLOSE HERE");
        Application.Quit();

    }

	public void HowToPlay()
	{
		//should take you to a new menu (HowToPlay Screen)
		aud.PlayOneShot(selectButton);
	}

	public void Back()
	{
		//takes you back to original menu (disables HowToPlay Screen and re-enables MainMenu screen)
		aud.PlayOneShot(backButton);
	}

	IEnumerator PlayButtonSoundDelay()
	{

		yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(1); //change this to whatever the first level is set to in the Build options.

    }
}
