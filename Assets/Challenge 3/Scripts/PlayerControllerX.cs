using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;
    public bool isOnGround = false;
    public bool lowenough = true;
    public float downForce =40f;    
    public float floatForce;
    public float yBound = 14;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip crashSound;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;




    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
       
               
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && lowenough)
        {
            playerRb.AddForce(Vector3.up * floatForce * Time.deltaTime, ForceMode.Impulse);
        }

        // While A key is pressed and player is up enough
        if (Input.GetKey(KeyCode.A) &&!gameOver && lowenough)
        {
            playerRb.AddForce(Vector3.down * floatForce * Time.deltaTime, ForceMode.Impulse);                      
        }

        // Keep  ballon on the bound
        if (transform.position.y > yBound)
        {
            lowenough = false;
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
        }
        else
        {
            lowenough = true;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }else if(other.gameObject.CompareTag("Ground") && !gameOver)
        {
            playerAudio.PlayOneShot(crashSound, 1.0f);
            playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

        }

    }


}
