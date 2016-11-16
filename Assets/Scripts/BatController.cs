﻿using UnityEngine;


public class BatController : MonoBehaviour {
    private const float Y_Angle_MIN = -50.0f;
    private const float Y_Angle_MAX = 50.0f;
    
    private float sensitivity = 2.0f;
    private float smoothing = 1.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private Vector2 _mouseLook;
    private Vector2 _smoothV;
    public float batSpeed;
    public GameObject Bat;
    private BatFlapController batFlapController;
    private Quaternion _batRotation;
    private Vector3 _spawnPosition;
    private Quaternion _spawnRotation;
    private float boostCD = 5;
    public float boostCoolDown = 5;
    private AudioSource audioSource;
    public AudioClip boostSound;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        _spawnPosition = transform.position;
        _spawnRotation = transform.rotation;
        batFlapController = Bat.GetComponent<BatFlapController>();
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

        batFlapController.FlapWings();
        boostCD += Time.deltaTime;
    }

    void FixedUpdate()
    {

        Move();
    }   


    void Move()
    {
        float translation = Input.GetAxis("Vertical") * batSpeed * Time.deltaTime;
        float straffe = Input.GetAxis("Horizontal") * batSpeed * Time.deltaTime;
        //transform.Translate(straffe, 0, translation);
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(straffe, 0, translation));

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {   
            if (boostCD >= boostCoolDown)
            {
                GetComponent<Rigidbody>().AddRelativeForce(new Vector3(straffe * 200, 0, translation * 200));
                audioSource.PlayOneShot(boostSound, 1);
                boostCD = 0;
            }
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        ;
    }

    public void Respawn()
    {
        transform.position = _spawnPosition;
        transform.rotation = _spawnRotation;
    }

}
