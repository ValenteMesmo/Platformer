using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Platformer.Desktop
{
    public static class Player
    {
        public static GameObject Create(InputController input, ValueKeeper<State> state)
        {
            var obj = GameObject.Create();
            obj.Position.Y = -14000;
            obj.Identifier = Identifier.Player;

            var grounded = ValueKeeper<int>.Create();
            var facingRight = ValueKeeper<bool>.Create();

            var collider = Collider.Create(obj);
            collider.Area = new Rectangle(
                50 * Const.Scale
                , 4000
                , 10000
                , 20000 - 4000);
            collider.Handler = StopsWhenHitingBlocks.Create();

            collider = Collider.Create(obj);
            collider.Area = new Rectangle(50 * Const.Scale, 210 * Const.Scale, 100 * Const.Scale, 10 * Const.Scale);
            collider.Handler = DetectsIfGrounded.Create(grounded);

            var animationDic = new Dictionary<State, Animation> {
                { State.Idle, PlayerAnimation.Idle() }
                , { State.Walking, PlayerAnimation.Walk() }
                , { State.Fall, PlayerAnimation.Fall() }
                , { State.JumpStart, PlayerAnimation.Jump() }
                , { State.Jump, PlayerAnimation.Jump() }
                , { State.JumpBreak, PlayerAnimation.Fall() }
            };

            var stateMachine = StateMachine.Create();

            stateMachine.Add(
                State.Idle
                , stateChange: () =>
                {
                    ChangeToFallingState.Try(obj, grounded, state);
                    ChangeToWalking.Try(input, grounded, state);
                    ChangeToJumpStart.Try(obj, input, grounded, state);                    
                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);
                    PlayerAnimation.Update(obj, animationDic, state, facingRight);
                    Friction.Apply(obj, input);
                    grounded.SetValue(grounded.GetValue().DecrementUntil(0));
                });

            stateMachine.Add(
                State.Walking
                , stateChange: () =>
                {
                    ChangeFacingDirection.Change(input, facingRight);
                    ChangeToFallingState.Try(obj, grounded, state);
                    ChangeToIdle.Try(input, grounded, state);
                    ChangeToWalking.Try(input, grounded, state);
                    ChangeToJumpStart.Try(obj, input, grounded, state);
                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);
                    UpdateVelocityUsingInputs.Update(obj, facingRight,input);
                    PlayerAnimation.Update(obj, animationDic, state, facingRight);
                    grounded.SetValue(grounded.GetValue().DecrementUntil(0));
                });

            stateMachine.Add(
                State.JumpStart
                , stateChange: () =>
                {
                    ChangeFacingDirection.Change(input, facingRight);
                    ChangeToJumpState.Try(obj, input,grounded, state);
                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);
                    UpdateVelocityUsingInputs.Update(obj, facingRight, input);
                    UpdateJump.Update(obj, state);
                    PlayerAnimation.Update(obj, animationDic, state, facingRight);
                    Friction.Apply(obj, input);

                    grounded.SetValue(grounded.GetValue().DecrementUntil(0));
                });

            stateMachine.Add(
                State.Jump
                , stateChange: () =>
                {
                    ChangeFacingDirection.Change(input, facingRight);
                    ChangeToFallingState.Try(obj, grounded, state);
                    ChangeToJumpBreak.Try(obj, input, grounded, state);
                    //ChangeToWalkingRight.Try(input, grounded, state);
                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);
                    UpdateVelocityUsingInputs.Update(obj, facingRight, input);
                    UpdateJump.Update(obj, state);
                    PlayerAnimation.Update(obj, animationDic, state, facingRight);
                    Friction.Apply(obj, input);

                    grounded.SetValue(grounded.GetValue().DecrementUntil(0));                    
                });

            stateMachine.Add(
                State.JumpBreak
                , stateChange: () =>
                {
                    ChangeFacingDirection.Change(input, facingRight);
                    ChangeToFallingState.Try(obj, grounded, state);
                    ChangeToWalking.Try(input, grounded, state);
                    ChangeToIdle.Try(input, grounded, state);
                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);
                    UpdateVelocityUsingInputs.Update(obj, facingRight, input);
                    UpdateJumpBreak.Update(obj,state);
                    PlayerAnimation.Update(obj, animationDic, state, facingRight);
                    Friction.Apply(obj, input);

                    grounded.SetValue(grounded.GetValue().DecrementUntil(0));
                });

            stateMachine.Add(
                State.Fall
                , stateChange: () =>
                {
                    ChangeFacingDirection.Change(input, facingRight);

                    ChangeToIdle.Try(input, grounded, state);

                    ChangeToWalking.Try(input, grounded, state);
                    ChangeToJumpStart.Try(obj, input, grounded, state);
                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);

                    PlayerAnimation.Update(obj, animationDic, state, facingRight);
                    UpdateVelocityUsingInputs.Update(obj, facingRight, input);

                    Friction.Apply(obj, input);
                    grounded.SetValue(grounded.GetValue().DecrementUntil(0));
                });

            obj.RenderHandler = PlayerAnimation.Idle();
            obj.UpdateHandler = () => stateMachine.Update(state);

            obj.OnDestroy = () =>
            {
                grounded.Destroy();
            };

            return obj;
        }
    }
}
