using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 4.0f;
    public GameObject cam;
    public Light spotL;
    public AudioClip alarm;
    public AudioClip coin;
    public AudioClip step;
    public float footstepTime = 0.7f;

    private CharacterController _charCont;
    private AudioSource asource; //
    private float timeOut;

    // Use this for initialization
    void Start() {
        _charCont = GetComponent<CharacterController>();
        asource = GetComponent<AudioSource>(); //
    }

    // Update is called once per frame
    void Update() {

        timeOut += Time.deltaTime;
        Vector3 lookDir = cam.transform.forward;
        lookDir.y = 0;
        Quaternion rotationOfMoveDirection = Quaternion.LookRotation(lookDir, Vector3.up);
        transform.rotation = rotationOfMoveDirection;

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed); //Limits the max speed of the player

        movement *= Time.deltaTime;     //Ensures the speed the player moves does not change based on frame rate
        movement = transform.TransformDirection(movement);
        _charCont.Move(movement);

        if((Input.GetButton("Vertical") || Input.GetButton("Horizontal")) && timeOut >= footstepTime)
        {
            timeOut = 0;
            asource.PlayOneShot(step);
        }
    }

    private void OnTriggerEnter(Collider other) { //
        if(other.gameObject.tag == "Coin") {
            Destroy(other.gameObject);
            asource.clip = coin;
            asource.Play();
        }

        if(other.gameObject.tag == "Alarm")
        {
            spotL.color = new Color(150f, 0f, 0f);
            spotL.intensity = 0.1f;
            asource.clip = alarm;
            asource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Alarm")
        {
            spotL.color = new Color(255f, 255f, 255f);
            spotL.intensity = 0.01f;
            asource.Stop();
        }
    }
}
