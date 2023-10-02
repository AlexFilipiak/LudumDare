using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingCamera : MonoBehaviour {

	public GameObject player;
	Vector3 offset;
	// Use this for initialization
	void Start () {
		StartCoroutine (Shrink ());
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//camera position = offset + player position
		transform.position = offset + player.transform.position;
	}

	IEnumerator Shrink()
	{
		while(gameObject.GetComponent<Camera>().orthographicSize > 3f)
		{
			yield return new WaitForSeconds (0.1f);
			gameObject.GetComponent<Camera>().orthographicSize -=0.1f;
		}
	}
}
