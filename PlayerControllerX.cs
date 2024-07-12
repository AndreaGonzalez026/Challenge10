using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 2;
    private float turboSpeedMultiplier = 7f;
    private GameObject focalPoint;
    public ParticleSystem smoke;
    
    public bool turboAvailable;
    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;
    public float turboDuration = 2.0f;
    public int turboWait = 5;

    private float normalStrangth = 10 // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        turboAvailable = true;
    }

    void Update()
    {
    Moveforward();

        //Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
    
    }

    private void Moveforward()
    {
       //Add force to player in direction of the focal point (and camera)
       float verticalInput = Input.GetAxis("Vertical");
       playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);

       if(Input.GetKeyDown(KeyCode.Space) && turboAvailable)
       {
          playerRb.AddForce(focalPoint.tranform.forward * speed * verticalInput * turboSpeeedMultiplier, ForceMode.Impulse);
          TurboCooldown();
          smoke.Play();

       }
       else
       {
          playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);
       }
    }

     IEnumerable TurboCooldown()
     {
         yield return new MainForSeconds(turboWait);
         turboAvailable = true;
         
     }

     // If Player collides with powerup, active powerup
}
