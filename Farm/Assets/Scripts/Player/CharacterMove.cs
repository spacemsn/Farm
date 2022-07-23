using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour
{
    [Header("Компоненты")]
    public Joystick joystick;
    private CharacterController controller;
    private Animator animator;
    public Transform camera;

    Vector3 moveDirection;
    Vector3 playerVelocity;

    private float deltaH;
    private float deltaV;

    [Header("Характеристики персонажа")]
    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float runningSpeed = 11.5f;
    [SerializeField] private float jumpValue = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float smoothTime;
    float smoothVelocity;
    bool canMove = true;

    public enum Move
    {
        PC = 0,
        Android = 1
    }

    [Header("Выбор управления")]
    public Move move = Move.PC;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Видимость курсора
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (move == Move.PC)
        {
            MovePC();
        }
        else if (move == Move.Android)
        {
            MoveAndroid();   
        }
    }

    private void MovePC()
    {
        deltaH = Input.GetAxisRaw("Horizontal");
        deltaV = Input.GetAxisRaw("Vertical");
        joystick.gameObject.SetActive(false);

        float movementDirectionY = moveDirection.y;

        moveDirection = new Vector3(deltaH, 0, deltaV).normalized;
        animator.SetFloat("isRun", Vector3.ClampMagnitude(moveDirection, 1).magnitude);

        if (moveDirection.magnitude > Mathf.Abs(0.05f))
        {
            float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(moveDirection.normalized * runningSpeed * Time.deltaTime);
        }
        else { controller.Move(moveDirection.normalized * walkingSpeed * Time.deltaTime); }

        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpValue * -2.0f * gravity);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = 2.15f;
        }
        else
        {
            controller.height = 4.3f;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    } // Движение персонажа ПК версии

    private void MoveAndroid()
    {
        deltaH = joystick.Horizontal;
        deltaV = joystick.Vertical;
        joystick.gameObject.SetActive(true);

        float movementDirectionY = moveDirection.y;

        moveDirection = new Vector3(deltaH, 0, deltaV).normalized;
        animator.SetFloat("isRun", Vector3.ClampMagnitude(moveDirection, 1).magnitude);

        if (moveDirection.magnitude > Mathf.Abs(0.05f))
        {
            float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(moveDirection.normalized * runningSpeed * Time.deltaTime);
        }
        else { controller.Move(moveDirection.normalized * walkingSpeed * Time.deltaTime); }

        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpValue * -2.0f * gravity);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = 2.15f;
        }
        else
        {
            controller.height = 4.3f;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    } // Движение персонажа андройд версии

    private void SimpleMove() // Управление персонажем без прыжков и тд
    {
        if (move == Move.PC)
        {
            deltaH = Input.GetAxisRaw("Horizontal");
            deltaV = Input.GetAxisRaw("Vertical");
            joystick.gameObject.SetActive(false);
        }
        else if (move == Move.Android)
        {
            deltaH = joystick.Horizontal;
            deltaV = joystick.Vertical;
            joystick.gameObject.SetActive(true);
        }

        Vector3 moveDirection = new Vector3(deltaH, 0, deltaV);
        animator.SetFloat("isRun", Vector3.ClampMagnitude(moveDirection, 1).magnitude);

        if (moveDirection.magnitude > Mathf.Abs(0.05f))
        {
            float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveCharacter = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;

            controller.SimpleMove(Vector3.ClampMagnitude(moveCharacter, 1) * walkingSpeed);
        }
    }
}
