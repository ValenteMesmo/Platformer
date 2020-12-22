using Platformer.Desktop;
using Xunit;

namespace Tests.Unit
{
    public class PlayerJumpTests
    {
        [Theory, AutoNSubstituteData]
        public void Player_JumpStart(
            InputController input
            , ValueKeeper<State> state
            , ValueKeeper<int> dashCooldown
            , ValueKeeper<int> grounded
            , ValueKeeper<int> hittingHead
            , ValueKeeper<bool> facingRight
            )
        {
            var sut = Player.Create(input, state, dashCooldown, grounded, hittingHead, facingRight);

            input.Jump.Press();
            grounded.SetValue(1);

            sut.UpdateHandler();

            Assert.Equal(sut.Velocity.Y, -Const.jumpForce);
            Assert.Equal(State.JumpStart, state);
        }

        [Theory, AutoNSubstituteData]
        public void Player_JumpBreak(
          InputController input
          , ValueKeeper<State> state
          , ValueKeeper<int> dashCooldown
          , ValueKeeper<int> grounded
          , ValueKeeper<int> hittingHead
          , ValueKeeper<bool> facingRight
          )
        {
            var sut = Player.Create(input, state, dashCooldown, grounded, hittingHead, facingRight);

            input.Jump.Press();
            grounded.SetValue(1);
            sut.UpdateHandler();
            grounded.SetValue(0);
            input.Jump.Release();
            sut.UpdateHandler();
            sut.UpdateHandler();

            var expectedSpeed =
                -Const.jumpForce
                + Const.GRAVITY_ACCELERATION
                + Const.GRAVITY_ACCELERATION
                + Const.stoppingGravity;

            Assert.Equal(expectedSpeed, sut.Velocity.Y);
            Assert.Equal(State.JumpBreak, state);
        }
    }
}

