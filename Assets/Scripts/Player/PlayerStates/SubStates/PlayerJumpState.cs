using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.AmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();
        core.Movement.SetVelocityY(playerData.JumpVelocity);
        player.InAirState.SetIsJumping();

        DecreaseAmountOfJumpsLeft();

        isAbilityDone = true;
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
            return true;
        else
            return false;
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.AmountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
