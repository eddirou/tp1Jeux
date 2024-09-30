using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] PickUpAllow pickUpAllow;
    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;
    PlayerControls.InteractionsActions interactionActions;
    

    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake()
    {

        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;
        interactionActions = controls.Interactions;
        // groundMovement.[action].performed += context => do something
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        groundMovement.Jump.performed += _ => movement.OnJumpPressed();

        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();


        //ligne de code pour interacation
        interactionActions.PickUp.performed += _ => pickUpAllow.SetPickupAlllowed(true);
        scriptsNote note = new scriptsNote();
        interactionActions.Drop.performed += _ => note.DropPages();

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }

    private void Update()
    {
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);
    }
}
