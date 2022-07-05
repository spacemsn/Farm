using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour
{
    [Header("Управление персонажем")]
    [SerializeField] private float speed;
    [SerializeField] private float moveRotate;
    public Joystick joystick;
    private CharacterController controller;
    

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float deltaH = Input.GetAxis("Horizontal");
        float deltaV = Input.GetAxis("Vertical");

        float stickH = joystick.Horizontal;
        float stickV = joystick.Vertical;

        Vector3 moveDirection = new Vector3(-stickV, 0, stickH);
        if (moveDirection.magnitude > Mathf.Abs(0.05f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * moveRotate);
        }

        controller.SimpleMove(Vector3.ClampMagnitude(moveDirection, 1) * speed);

    }

}
