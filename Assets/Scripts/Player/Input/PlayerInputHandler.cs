using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }

    public bool[] AttackInputs { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];

        cam = Camera.main;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            AttackInputs[(int)CombatInputs.Primary] = true;
        }
        if(ctx.canceled)
        {
            AttackInputs[(int)CombatInputs.Primary] = false;
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            AttackInputs[(int)CombatInputs.Secondary] = true;
        }
        if (ctx.canceled)
        {
            AttackInputs[(int)CombatInputs.Secondary] = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext ctx)
    {
        RawMovementInput = ctx.ReadValue<Vector2>();
        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }

    public void OnJumpInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (ctx.canceled)
        {
            JumpInputStop = true;
        }
    }

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    public void OnGrabInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            GrabInput = true;
        }

        if (ctx.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }

        if (ctx.canceled)
        {
            DashInputStop = true;
        }
    }

    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext ctx)
    {
        RawDashDirectionInput = ctx.ReadValue<Vector2>();

        if (playerInput.currentControlScheme == "Keyboard")
        {
            RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
        }

        //Normalize angle to int
        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    public void UseDashInput() => DashInput = false;
}

public enum CombatInputs
{
    Primary,
    Secondary
}