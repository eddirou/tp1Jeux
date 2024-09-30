using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * D'une vidéo : https://www.youtube.com/watch?v=tXDgSGOEatk
 */
public class Movement : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 11f;
    Vector2 horizontalInput;

    [SerializeField] float jumpHeight = 3.5f;
    bool jump;

    [SerializeField] float gravity = -30f; // -9.81
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    private float radius = .2f;
    private float yOffset = 1f;
    bool isGrounded;

    private void Update()
    {
        //isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        RaycastHit groundInfo;
        isGrounded = Physics.SphereCast(transform.position + new Vector3(0, yOffset, 0f), radius, Vector3.down, out groundInfo, yOffset + .1f, groundMask.value);

        if (isGrounded)
        {
            verticalVelocity.y = 0f;
        }

        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        //Jump: v = sqrt(-2 * jumpHeight * gravity)
        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            jump = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
        //print(horizontalInput); 
    }

    public void OnJumpPressed() { jump = true; }
}
