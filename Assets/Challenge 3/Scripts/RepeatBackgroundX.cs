using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackgroundX : MonoBehaviour
{
   
    public bool gameOver;
    public PlayerControllerX playerControllerScript;
    
    private float repeatWidth;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position; // Establish the default starting position 
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; // Set repeat width to half of the background
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
       
    }

    private void Update()
    {
        // If background moves left by its repeat width, move it back to start position
        if ((transform.position.x < startPos.x - repeatWidth) && !playerControllerScript.gameOver)
        {
            transform.position = startPos;
        }
    }

 
}


