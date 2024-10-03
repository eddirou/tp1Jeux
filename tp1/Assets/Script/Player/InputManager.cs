using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] PickUpAllow pickUpAllow;
    [SerializeField] Weapon weapons;
    [SerializeField] Melee melee;
    [SerializeField] Switch weaponSwitch;
    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;
    PlayerControls.InteractionsActions interactionActions;
    PlayerControls.WeaponsActions weaponsActions;
    PlayerControls.MeleeActions meleeActions;
    PlayerControls.SwitchActions switchActions;


    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake()
    {

        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;
        interactionActions = controls.Interactions;
        weaponsActions = controls.Weapons;
        meleeActions = controls.Melee;
        switchActions = controls.Switch;

        // groundMovement.[action].performed += context => do something
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();


        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        weaponsActions.Shoot.performed += _ => weapons.Tirer();
        weaponsActions.Reload.performed += _ => weapons.Recharger();

        switchActions.Primaire.performed += _ => weaponSwitch.SwitchPrimaire();
        switchActions.Secondaire.performed += _ => weaponSwitch.SwitchSecondaire();

        meleeActions.Swing.performed += _ => melee.Attaque();

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
