using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    Rigidbody2D rb2d;
    public float hSpeed = 10f, vSpeed = 6f;
    private Vector3 velocity = Vector3.zero;
    int stars = 0;
    public GameObject greenDoor, purpleDoor, winDoor, winGate;
    public Text winText, starText, infoText;
    public GameObject greenDoors;

    public SpriteRenderer sr1, sr2;
    public Color defaultColorGreen;
    public Color defaultColorPurple;
    public Color invisColor;

    public bool greenButtonPressed = false, purpleButtonPressed = false;
    int greenButtonPress = 0, purpleButtonPress = 0;

    int time = 50;
    public Text timerText;
    bool canMove = true;
    bool win = false;

    AudioSource aud;
    public AudioClip pickupSound, gateOpen;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        starText.text = "Stars: ";
        infoText.text = "You need 12 stars to open the final gate! \nCollect each star and reach the pink box in 50 seconds to win!";
        timerText.text = "Time: ";
        StartCoroutine(Timer());
        aud = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            if (Input.GetKey(KeyCode.W)) //up
            {
                transform.position += new Vector3(0, vSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S)) //down
            {
                transform.position -= new Vector3(0, vSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D)) //right
            {

                transform.position += new Vector3(hSpeed * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.A)) //left
            {
                transform.position -= new Vector3(hSpeed * Time.deltaTime, 0);

            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "GreenButton")
        {
            bool greenButtonPressed = true;
            greenButtonPress++;
            UnityEngine.Debug.Log("Green button has been pressed. Collider is working.");
            //UnityEngine.Debug.Log(greenButtonPress);
            //instead of destroying it, have it's sprite disappear (turn it's opacity down) and 
            //have its colliders turned off.
            sr1.color = invisColor; //for pressed once, sr1 is greenDoor's sprite renderer
            greenDoor.GetComponent<BoxCollider2D>().enabled = false;
            
            /*
            GameObject[] gameObjectArray1 = GameObject.FindGameObjectsWithTag("GreenDoor");
            foreach (GameObject go in gameObjectArray1)
            {
                go.GetComponent<BoxCollider2D>().enabled = false;

            }
            */

            if (greenButtonPress%2 == 0) //door will close on every even number of times the button is pressed.
            {
                greenButtonPressed = false;
                UnityEngine.Debug.Log("Button has been pressed once again. Door should reappear here");

                sr1.color = defaultColorGreen; //for pressed once, sr1 is greenDoor's sprite renderer
                greenDoor.GetComponent<BoxCollider2D>().enabled = true;
            }
            
        }

        if (other.tag == "PurpleButton")
        {

            bool purpleButtonPressed = true;
            purpleButtonPress++;
            UnityEngine.Debug.Log("Purple button has been pressed. Collider is working.");
            //instead of destroying it, have it's sprite disappear (turn it's opacity down) and 
            //have its colliders turned off.
            sr2.color = invisColor; //for pressed once, sr2 is purpleDoor's sprite renderer
            purpleDoor.GetComponent<BoxCollider2D>().enabled = false;

            if (purpleButtonPress % 2 == 0) //door will close on every even number of times the button is pressed.
            {
                purpleButtonPressed = false;
                UnityEngine.Debug.Log("Button has been pressed once again. Door should reappear here");

                sr2.color = defaultColorPurple; //for pressed once, sr2 is purpleDoor's sprite renderer
                purpleDoor.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        if (other.tag == "PickUp")
        {
            stars++; //adds to star score needed to destroy WinDoor
            starText.text = "Stars: " + stars;
            UnityEngine.Debug.Log("You have picked up a star");
            Destroy(other.gameObject); //destroys pickup
            aud.PlayOneShot(pickupSound);

            if (stars == 12)
            {
                infoText.text = "You collected all the stars! Get to the Pink Box quickly!";
                aud.PlayOneShot(gateOpen);
                Destroy(winGate);
                //destroys WinDoor
            }

        }
        if (other.tag == "WinDoor")
        {
             win = true;
            //player wins the game
            winText.text = "YOU WIN!!!!!";
            

        }

    }

    IEnumerator Timer()
    {
        //while the timer is above 0
		while (time > 0 && win == false)
        {
            //wait for one second
            yield return new WaitForSeconds(1);

            //if they won, stop ticking
            if (win == true)
                break;

            //tick down the timer
            time--;
            timerText.text = "Time: " + time;

            //if time is at 0, take away movement
            if (time == 0)
            {
                canMove = false;
                winText.text = "YOU LOST"; //update text
                
            }
        }
    }

}
