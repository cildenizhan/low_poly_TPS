using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject cam;

    public float rotSpeed;

    public GameObject abim;

    public float can = 100;

    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;


    public float groundDrag;

    public float playerheight;
    public LayerMask WhatIsGround;
    public bool grounded;

    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    public float jumpForce;
    public float jumpCooldown;
    public float airmultiplier;
    
    bool readyToJump = true;

    public KeyCode jumpkey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    public Transform Ori;

    float horizontalIn;
    float verticalIn;


    private Animator animator;

    Vector3 moveDirection;
    private bool exitingslope;

    Rigidbody rb;
    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
    }
    void Update()
    {

        if (can <= 0)
            {
                Destroy(gameObject);
            }
            StateHandler();
            SpeedControl();
            grounded = Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.5f, WhatIsGround);
            MyInput();
            if (grounded)
            {
                rb.drag = groundDrag;
            }
            else
            {
                rb.drag = 0;
            }
    }

    private void SpeedControl()
    {
        if (OnSlope() && !exitingslope)
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }

        else
        {
            Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatvel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatvel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }
    private void MyInput()
    {
        horizontalIn = Input.GetAxisRaw("Horizontal");
        verticalIn = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpkey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void StateHandler()
    { 
        // Mode - Sprinting
        if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        else
        {
            state = MovementState.air;
        }
    }
    private void MovePlayer()
    {


        if ((verticalIn != 0 || horizontalIn != 0))
        {
            animator.SetBool("HaraketHalinde", true);
        }
        else
        {
            animator.SetBool("HaraketHalinde", false);
        }

        if (grounded)
        {
            animator.SetBool("Yerdemi",false);
        }
        else
        {
            animator.SetBool("Yerdemi", true);
        }

        if (verticalIn != 0 && horizontalIn == 0)
        {
            animator.SetBool("İleriGeri", true);
        }
        else
        {
            animator.SetBool("İleriGeri", false);
        }

        if (verticalIn == 0 && horizontalIn != 0)
        {
            animator.SetBool("SagaSola", true);
        }
        else
        {
            animator.SetBool("SagaSola", false);
        }
        if ((verticalIn > 0 && horizontalIn > 0) || (verticalIn < 0 && horizontalIn < 0) ) 
        {
            animator.SetBool("SagaCaprazİleri", true);
        }
        else
        {
            animator.SetBool("SagaCaprazİleri", false);
        }
        if ((verticalIn > 0 && horizontalIn < 0) || (verticalIn < 0 && horizontalIn > 0))
        {
            animator.SetBool("SolaCaprazİleri", true);
        }
        else
        {
            animator.SetBool("SolaCaprazİleri", false);
        }

        Vector3 viewDir = transform.position -  new Vector3(Ori.position.x,transform.position.y,Ori.position.z);


       

        if (cam.GetComponent<CameraKontrol>().currentStyle == CameraKontrol.CameraStyle.Basit)
        {
            print("saaa");
            Ori.forward = viewDir.normalized;
        }

        moveDirection = Ori.forward * verticalIn + Ori.right * horizontalIn;

        

        if (OnSlope() && !exitingslope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airmultiplier, ForceMode.Force);

        rb.useGravity = !OnSlope();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void Jump()
    {
        exitingslope = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingslope = false;
    }
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerheight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
