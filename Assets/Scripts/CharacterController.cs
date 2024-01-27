using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private IdContainer _IdProvider;

    private float MaxSpeed;
    private float acceleration;
    private float deceleration;

    private float jumpForce;

    [Header("Player Data")]
    public PlayerDataScriptable playerData;

    [SerializeField]
    private float rotationSpeed = 1000f;
    private Quaternion desiredRotation;


    private bool isJumping;

    private float currentDirection = 0;
    private float currentSpeed = 0;

    private Rigidbody rigidbody;
    private BoxCollider collider;

    private GameplayInputProvider _gameplayInputProvider;

    private Quaternion desiredLArmRotation = Quaternion.Euler(0, 0, 0);
    private Quaternion desiredRArmRotation = Quaternion.Euler(0, 0, 0);

    [SerializeField]
    private Transform leftArm;
    [SerializeField]
    private Transform rightArm;

    private void Awake()
    {
        _gameplayInputProvider = PlayerController.Instance.GetInput<GameplayInputProvider>(_IdProvider.Id);

        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();

        MaxSpeed = playerData.MaxSpeed;
        acceleration = playerData.acceleration;
        deceleration = playerData.deceleration;
        jumpForce = playerData.jumpForce;

        desiredRotation = transform.rotation;
    }

    private void OnEnable()
    {
        _gameplayInputProvider.OnMove += MoveCharacter;
        _gameplayInputProvider.OnJump += JumpCharacter;
    }
    private void OnDisable()
    {
        _gameplayInputProvider.OnMove -= MoveCharacter;
        _gameplayInputProvider.OnJump -= JumpCharacter;
    }

    private void JumpCharacter()
    {
        if (Physics.Raycast(transform.position, Vector3.down, collider.size.y / 2 + .5f))
        {
            //Debug.Log("JUMP");
            rigidbody.AddForce(new Vector3(0, jumpForce, 0));
        }
        else
        {
            //Debug.Log(rigidbody.velocity.y);
        }

        //Debug.DrawRay(transform.position, Vector3.down * collider.size.y / 2);
    }

    private void MoveCharacter(float value)
    {
        //Debug.LogFormat("Value: {0}", value);
        currentDirection = value;

    }

    private void FixedUpdate()
    {

        HandleMovement();
        Rotate();

        //Debug.Log(currentSpeed);
        // Movement
        if (Mathf.Abs(transform.rotation.eulerAngles.y - desiredRotation.eulerAngles.y) < 0.01f)
        {
            Vector3 movement = new Vector3(currentSpeed, rigidbody.velocity.y, 0f);

            //transform.Translate(movement * Time.deltaTime);
            rigidbody.velocity = movement;

            /*
            if (rigidbody.velocity.magnitude > MaxSpeed)
            {
                rigidbody.velocity.Normalize();
                rigidbody.velocity.Scale(new Vector3(MaxSpeed, 0, 0));
            }
             */
        }

        //Debug.DrawRay(transform.position, Vector3.down * collider.size.y / 2);
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        leftArm.localRotation = Quaternion.RotateTowards(leftArm.localRotation, desiredLArmRotation, rotationSpeed * Time.deltaTime);
        rightArm.localRotation = Quaternion.RotateTowards(rightArm.localRotation, desiredRArmRotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleMovement()
    {
        if (currentDirection != 0)
        {
            currentSpeed += acceleration * currentDirection * Time.fixedDeltaTime;
            // clamp inclination to 45 degrees
            float inclination = Mathf.Clamp(currentSpeed / MaxSpeed * 45, -30f, 30f);
            if (currentDirection > 0)
            {
                inclination *= -1;
            }
            desiredRotation = Quaternion.Euler(0, currentDirection > 0 ? 0 : 180, inclination);
            desiredLArmRotation = Quaternion.Euler(-inclination * 3, 0, 0);
            desiredRArmRotation = Quaternion.Euler(inclination * 3, 0, 0);
        }
        else
        {
            float prevSpeed = currentSpeed;

            currentSpeed += deceleration * -Math.Sign(currentSpeed) * Time.fixedDeltaTime;

            if (prevSpeed * currentSpeed < 0f)
            {
                currentSpeed = 0f;
            }
            desiredLArmRotation = Quaternion.Euler(0, 0, 0);
            desiredRArmRotation = Quaternion.Euler(0, 0, 0);
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -MaxSpeed, MaxSpeed);
    }


}
