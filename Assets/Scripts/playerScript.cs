using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public float speed = 2.6f;
    float maxSpeed = 10f;
    float gravity = -0.5f;
    public float jumpVelocity = 0.14f;

    float lastJump;

    float mouseSenseX = 1f;
    float mouseSenseY = 1f;

    public Vector3 velocity = new Vector3(0, 0, 0);
    public Vector2 test;
    CharacterController controller;


    public Vector3 forwardTest;
    public bool isground;

    public float verticalSpeed;


    public float waterLevel = 100;
    public float health = 100;

    public bool nearHydrant = false;
    public bool nearAnimal = false;

    float animalRefresh;

    ParticleSystem particleSystem;

    float lastHurt;
    float lastFire;

    AudioSource audioSource;
    public AudioClip hurt;
    public AudioClip fireSound;
    public AudioClip animalRescue;

    AudioSource hoseAudioSource;
    AudioSource fillingAudioSource;

    RaycastHit hit;
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 10;
        lastHurt = Time.time;
        lastFire = Time.time;
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();

        lastJump = Time.time;
        animalRefresh = Time.time;

        audioSource = GetComponent<AudioSource>();
        hoseAudioSource = GameObject.FindWithTag("hose").GetComponent<AudioSource>();
        fillingAudioSource = GameObject.FindWithTag("filling").GetComponent<AudioSource>();
        fillingAudioSource.Pause();

        particleSystem = transform.GetChild(1).GetChild(1).gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //doMovement();
        doMouseTurn();
        shooting();


        if (Time.time - animalRefresh > 0.5f)
        {
            nearAnimal = false;
        }


        if (Time.time - lastFire > 30f)
        {
            lastFire = Time.time;
            audioSource.PlayOneShot(fireSound, 1f);
        }
    }

    void FixedUpdate()
    {
        doMovement();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "hydrant")
        {
            nearHydrant = true;
            if (Input.GetKey("e") && waterLevel <= 100)
            {
                if (!fillingAudioSource.isPlaying)
                {
                    fillingAudioSource.Play();
                }
                waterLevel += 20 * Time.fixedDeltaTime;
            } else
            {
                fillingAudioSource.Pause();
            }

        } else if (other.tag == "animal")
        {
            animalRefresh = Time.time;
            nearAnimal = true;
            if (Input.GetKey("e"))
            {
                audioSource.PlayOneShot(animalRescue, 1f);
                other.gameObject.GetComponent<animalScript>().onRescue();
            }
        } else if (other.tag == "tree")
        {
            health -= 10 * Time.fixedDeltaTime;
            if (Time.time - lastHurt> 1f)
            {
                lastHurt = Time.time;
                audioSource.PlayOneShot(hurt, 1f);
            }
            

        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "hydrant")
        {
            nearHydrant = false;
            fillingAudioSource.Pause();
        } else if (other.tag == "animal")
        {
            nearAnimal = false;
        }
    }


    void shooting()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Input.GetMouseButton(0) && waterLevel > 0)
        {
            if (!hoseAudioSource.isPlaying)
            {
                hoseAudioSource.Play();
            }
            

            waterLevel -= 0.1f;
            particleSystem.Play(); // SetActive(true);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide))
            {
                if (hit.collider.tag == "tree")
                {
                    hit.collider.gameObject.GetComponent<treeScript>().damage();
                }
            }
        } else
        {
            hoseAudioSource.Pause();
            particleSystem.Stop(); // SetActive(false);
        }

    }

    void doMouseTurn()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = -Input.GetAxis("Mouse Y");

        Vector3 currentRot = transform.eulerAngles;
        if (currentRot.x > 90 && currentRot.x < 180)
        {
            currentRot.x = 90;
            Debug.Log("Posi");
        }
        //Debug.Log(currentRot.x);
        if (currentRot.x < -90)
        {
            currentRot.x = -90;
            Debug.Log("Nega");
        }
        transform.eulerAngles = new Vector3(currentRot.x + deltaY, currentRot.y + deltaX, 0);
        
        //transform.Rotate(-deltaY, deltaX, 0);
    }

    void doMovement()
    {
        Vector2 moveInputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInputs *= speed * Time.fixedDeltaTime;
        //verticalSpeed += gravity * Time.deltaTime;
        //forwardTest = transform.forward;
        test = moveInputs;
        
        

        if (Input.GetKey("space") && controller.isGrounded && (Time.time - lastJump > 0.5f))
        {
            verticalSpeed = 0;
            Debug.Log("Jump");
            lastJump = Time.time;
            verticalSpeed = jumpVelocity;
        }
        
        
        if (verticalSpeed < -0.2f)
        {
            verticalSpeed = -0.2f;
        }
        
        isground = controller.isGrounded;
        if (!controller.isGrounded)
        {
            verticalSpeed += gravity * Time.fixedDeltaTime;
        }


        if (verticalSpeed > 0.1f)
        {
            Debug.Log(verticalSpeed);
        }

        Vector3 forwardMove = transform.forward * moveInputs.y * speed;
        controller.Move(new Vector3(forwardMove.x, 0, forwardMove.z));

        Vector3 rightMove = transform.right * moveInputs.x * speed;
        controller.Move(new Vector3(rightMove.x, 0, rightMove.z));

        controller.Move(new Vector3(0, verticalSpeed, 0));

    }
}
