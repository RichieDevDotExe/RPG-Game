using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerAttack playerAttack;
    private PlayerMotor playerMotor;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        playerAttack = GetComponent<PlayerAttack>();

        playerMotor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMotor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());

        onFoot.Attack.started += ctx => playerAttack.startAttack();
        onFoot.Attack.canceled += ctx => playerAttack.releaseAttack();
        onFoot.WeaponSlot1.performed += ctx => playerAttack.switchWeaponSlot(0);
        onFoot.WeaponSlot2.performed += ctx => playerAttack.switchWeaponSlot(1);

        playerMotor.playerFaceTowards();
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
