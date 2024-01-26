using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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


    private bool isJumping;

    private float currentDirection = 0;
    private float currentSpeed = 0;

    private Rigidbody rigidbody;
    private BoxCollider collider;

    private GameplayInputProvider _gameplayInputProvider;

    private void Awake()
    {
        _gameplayInputProvider = PlayerController.Instance.GetInput<GameplayInputProvider>(_IdProvider.Id);

        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();

        MaxSpeed = playerData.MaxSpeed;
        acceleration = playerData.acceleration;
        deceleration = playerData.deceleration;
        jumpForce = playerData.jumpForce;

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
        if (Physics.Raycast(transform.position, Vector3.down, collider.size.y / 2 + 0.05f))
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

    private void Update()
    {

        // Movement
        HandleMovement();

        Vector3 movement = new Vector3(currentSpeed, 0, 0);

        transform.Translate(movement * Time.deltaTime);
        //Debug.DrawRay(transform.position, Vector3.down * collider.size.y / 2);
    }


    private void HandleMovement()
    {
        if (currentDirection != 0)
        {
            currentSpeed += acceleration * currentDirection;
        }
        else
        {
            currentSpeed += deceleration * -Math.Sign(currentSpeed);
        }

        if (Math.Abs(currentSpeed) < 1.5f * deceleration)
        {
            currentSpeed = 0;
        }

        currentSpeed = currentSpeed > MaxSpeed ? MaxSpeed : currentSpeed;
        currentSpeed = currentSpeed < -MaxSpeed ? -MaxSpeed : currentSpeed;



    }


}
