using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerControls : MonoBehaviour
{
    public float speed = 50.0f;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Transform cameraTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector2 look = playerInput.actions["Look"].ReadValue<Vector2>();
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        move = move.x * new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z) + move.z * new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);
        controller.Move(move * Time.deltaTime * speed);

        if (look != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(look.x, look.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);
        }
    }
}
