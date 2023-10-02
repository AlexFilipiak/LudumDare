using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    Vector3 offset;

    // Use this for initialization
    void Start()
    {

        //offset = camera position - player position
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //camera position = offset + player position
        transform.position = offset + player.transform.position;
    }
}
