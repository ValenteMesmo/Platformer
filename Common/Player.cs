using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Platformer.Desktop
{
    public static class Player
    {
        public static GameObject Create(
            InputController input
            , ValueKeeper<State> state
            , ValueKeeper<int> dashCooldown
            , ValueKeeper<int> grounded
            , ValueKeeper<int> hittingHead
            , ValueKeeper<bool> facingRight)
        {
            var obj = GameObject.Create();
            obj.Position.Y = -14000;
            obj.Identifier = Identifier.Player;

            {
                var collider = Collider.Create(obj);
                collider.Area = new Rectangle(
                    50 * Const.Scale
                    , 60 * Const.Scale
                    , 100 * Const.Scale
                    , 200 * Const.Scale - 60 * Const.Scale);
                collider.Handler = StopsWhenHitingBlocks.Create();
            }

            {
                var collider = Collider.Create(obj);
                collider.Area = new Rectangle(50 * Const.Scale, 210 * Const.Scale, 100 * Const.Scale, 10 * Const.Scale);
                collider.Handler = TimerTrigger.Create(grounded, Const.Grounded_Timer);
            }

            {
                var collider = Collider.Create(obj);
                collider.Area = new Rectangle(50 * Const.Scale, 40 * Const.Scale, 100 * Const.Scale, 10 * Const.Scale);
                collider.Handler = TimerTrigger.Create(hittingHead, 1);
            }

            var animationDic = new Dictionary<State, Animation> {
                { State.Idle, PlayerAnimation.Idle() }
                , { State.Walking, PlayerAnimation.Walk() }
                , { State.Fall, PlayerAnimation.Fall() }
                , { State.JumpStart, PlayerAnimation.Jump() }
                , { State.Jump, PlayerAnimation.Jump() }
                , { State.JumpBreak, PlayerAnimation.Fall() }
                , { State.HeadBump, PlayerAnimation.HeadBump() }
            };

            var stateMachine = StateMachine.Create();

            Action commonUpdate = () =>
            {
                grounded.DecrementUntil(0);
                hittingHead.DecrementUntil(0);
                dashCooldown.DecrementUntil(0);
                PlayerAnimation.Update(obj, animationDic, state, facingRight);
            };

            stateMachine.Add(
                State.Idle
                , stateChange: () =>
                {
                    ChangeToFall.Try(obj, grounded, hittingHead, state);
                    ChangeToWalking.Try(input, grounded, state, obj);
                    ChangeToIdle.Try(input, grounded, state);
                    ChangeToJumpStart.Try(input, grounded, state);
                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);
                    Friction.Apply(obj, input);

                    commonUpdate();
                });

            stateMachine.Add(
                State.Walking
                , stateChange: () =>
                {
                    ChangeFacingDirection.Change(input, facingRight);
                    ChangeToFall.Try(obj, grounded, hittingHead, state);
                    ChangeToIdle.Try(input, grounded, state);
                    ChangeToWalking.Try(input, grounded, state, obj);
                    ChangeToJumpStart.Try(input, grounded, state);
                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);
                    UpdateVelocityUsingInputs.Update(obj, input);

                    commonUpdate();
                });

            stateMachine.Add(
                State.JumpStart
                , stateChange: () =>
                {
                    ChangeFacingDirection.Change(input, facingRight);
                    ChangeToJumpState.Try(state);
                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);
                    UpdateVelocityUsingInputs.Update(obj, input);
                    UpdateJumpStart.Update(obj);
                    Friction.Apply(obj, input);

                    commonUpdate();
                });

            stateMachine.Add(
                State.Jump
                , stateChange: () =>
                {
                    ChangeFacingDirection.Change(input, facingRight);
                    ChangeToFall.Try(obj, grounded, hittingHead, state);
                    ChangeToJumpBreak.Try(obj, input, grounded, state);

                    ChangeToIdle.Try(input, grounded, state);
                    ChangeToWalking.Try(input, grounded, state, obj);
                    ChangeToBumpHead.Try(state, obj, hittingHead);
                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);
                    UpdateVelocityUsingInputs.Update(obj, input);
                    Friction.Apply(obj, input);

                    commonUpdate();
                });

            stateMachine.Add(
                State.HeadBump
                , stateChange: () =>
                {
                    ChangeFacingDirection.Change(input, facingRight);
                    ChangeToFall.Try(obj, grounded, hittingHead, state);
                    ChangeToIdle.Try(input, grounded, state);
                    ChangeToWalking.Try(input, grounded, state, obj);
                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);
                    UpdateVelocityUsingInputs.Update(obj, input);
                    Friction.Apply(obj, input);

                    commonUpdate();
                });

            stateMachine.Add(
                State.JumpBreak
                , stateChange: () =>
                {
                    ChangeFacingDirection.Change(input, facingRight);
                    ChangeToFall.Try(obj, grounded, hittingHead, state);
                    ChangeToWalking.Try(input, grounded, state, obj);
                    ChangeToIdle.Try(input, grounded, state);

                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);
                    UpdateVelocityUsingInputs.Update(obj, input);
                    UpdateJumpBreak.Update(obj);
                    Friction.Apply(obj, input);

                    commonUpdate();
                });

            stateMachine.Add(
                State.Fall
                , stateChange: () =>
                {
                    ChangeFacingDirection.Change(input, facingRight);
                    ChangeToIdle.Try(input, grounded, state);
                    ChangeToWalking.Try(input, grounded, state, obj);
                    ChangeToJumpStart.Try(input, grounded, state);

                }
                , update: () =>
                {
                    UpdateGravity.Update(obj);
                    UpdateVelocityUsingInputs.Update(obj, input);
                    Friction.Apply(obj, input);

                    commonUpdate();
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
