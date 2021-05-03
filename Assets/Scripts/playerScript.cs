using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public float speed = 2.6f;
    float maxSpeed = 10f;
    float gravity = -0.007f;
    public float jumpVelocity = 35f;

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

    RaycastHit hit;
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();

        lastJump = Time.time;
        animalRefresh = Time.time;


        particleSystem = transform.GetChild(1).GetChild(1).gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        doMovement();
        doMouseTurn();
        shooting();


        if (Time.time - animalRefresh > 0.5f)
        {
            nearAnimal = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "hydrant")
        {
            nearHydrant = true;
            if (Input.GetKey("e") && waterLevel <= 100)
            {
                waterLevel += 20 * Time.fixedDeltaTime;
            }

        } else if (other.tag == "animal")
        {
            animalRefresh = Time.time;
            nearAnimal = true;
            if (Input.GetKey("e"))
            {
                other.gameObject.GetComponent<animalScript>().onRescue();
            }
        } else if (other.tag == "tree")
        {
            health -= 10 * Time.fixedDeltaTime;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "hydrant")
        {
            nearHydrant = false;
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
            waterLevel -= 0.1f;
            particleSystem.Play(); // SetActive(true);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide))
            {
                Debug.Log("a");
                if (hit.collider.tag == "tree")
                {
                    Debug.Log("b");
                    hit.collider.gameObject.GetComponent<treeScript>().damage();
                }
            }
        } else
        {
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
        moveInputs *= speed * Time.deltaTime;
        verticalSpeed += gravity;
        //forwardTest = transform.forward;
        test = moveInputs;
        
        

        if (Input.GetKey("space") && controller.isGrounded && (Time.time - lastJump > 0.1f))
        {
            verticalSpeed = 0;
            Debug.Log("Jump");
            lastJump = Time.time;
            verticalSpeed += jumpVelocity;
        }

        
        if (verticalSpeed < -0.2f)
        {
            verticalSpeed = -0.2f;
        }
        
        isground = controller.isGrounded;
        if (!controller.isGrounded)
        {
            verticalSpeed += gravity * Time.deltaTime;
            velocity.y += verticalSpeed;
        }




        Vector3 forwardMove = transform.forward * moveInputs.y * speed;
        controller.Move(new Vector3(forwardMove.x, 0, forwardMove.z));

        Vector3 rightMove = transform.right * moveInputs.x * speed;
        controller.Move(new Vector3(rightMove.x, 0, rightMove.z));

        controller.Move(new Vector3(0, verticalSpeed, 0));

    }
}
