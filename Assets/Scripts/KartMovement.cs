using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KartMovement : MonoBehaviour
{
    [SerializeField] float topForwardSpeed = 15;
    [SerializeField] float topTurnSpeed = 15;
    [SerializeField] float acceleration = 1;

    [SerializeField] float naturalSlowDown = 0.01f;

    bool isGrounded = false;

    float horizontalAxis = 0;
    float verticalAxis = 0;

    float forwardSpeed = 0;
    float previousSpeed;

    [SerializeField]
    GameRules Rules;

    Rigidbody playerRigidbody;

    Vector3 initialPosition;

    Vector3 movement;

    void Start()
    {
        movement = new Vector3();
        initialPosition = transform.position;
        //Rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<GameRules>();
        playerRigidbody = gameObject.GetComponent<Rigidbody>();

        //playerRigidbody.velocity = new Vector3(0, -9.8f, 0);
    }

    void OnEnable()
    {
        Rules.gameReset += OnGameReset;
    }

    void OnDisable()
    {
        Rules.gameReset -= OnGameReset;
    }

    void Update()
    {
        GetInputs();
    }

    void FixedUpdate()
    {
        ForwardMovement();
        TurningMovement();
    }

    void GetInputs()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
    }

    void TurningMovement()
    {
        if ((forwardSpeed > 0.1 || forwardSpeed < -0.1) && isGrounded)
        {
            transform.Rotate(0, Mathf.Sign(forwardSpeed) * horizontalAxis * topTurnSpeed * Time.deltaTime, 0);
        }
    }

    void ForwardMovement()
    {
        if (isGrounded)
        {
            forwardSpeed += acceleration * verticalAxis;
        
            if (forwardSpeed != 0)
            {
                forwardSpeed -= (Mathf.Sign(forwardSpeed) * naturalSlowDown);
            }
        }
        
        
        if (forwardSpeed > topForwardSpeed || forwardSpeed < -1 * topForwardSpeed)
        {
            forwardSpeed = topForwardSpeed * verticalAxis;
        }
        
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;


        //float currentMaxSpeed = topForwardSpeed * Mathf.Abs(vertical);
        //if (forwardSpeed > currentMaxSpeed || forwardSpeed < -1 * currentMaxSpeed)
        //{
        //    forwardSpeed = topForwardSpeed * vertical;
        //}



        //forwardSpeed += acceleration * vertical;
        //
        //if (forwardSpeed != 0 && isGrounded)
        //{
        //    forwardSpeed -= (Mathf.Sign(forwardSpeed) * naturalSlowDown);
        //}
        //
        //if (forwardSpeed > topForwardSpeed || forwardSpeed < -1 * topForwardSpeed)
        //{
        //    forwardSpeed = topForwardSpeed * vertical;
        //}
        //
        //
        //movement = transform.forward * forwardSpeed;
        ////playerRigidbody.velocity = transform.forward * forwardSpeed * 10;// + Vector3.up * -1;
        //
        //if (!isGrounded)
        //    movement.y = -7;
        //
        ////playerRigidbody.AddForce(movement, ForceMode.Acceleration);
        //playerRigidbody.velocity = movement;

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    public void StopMoving()
    {
        forwardSpeed = 0;
    }

    void OnGameReset()
    {
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
        playerRigidbody.velocity = new Vector3();
        playerRigidbody.angularVelocity = new Vector3();
        forwardSpeed = 0;
    }
}
