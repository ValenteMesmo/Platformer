using Platformer.Desktop;
using Xunit;

namespace Tests.Unit
{
    public class PlayerFallTests
    {
        [Theory, AutoNSubstituteData]
        public void Player_Fall_from_JumpBreak(
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

            for (int i = 0; i < 9; i++)
                sut.UpdateHandler();

            Assert.Equal(State.Fall, state);
        }

        [Theory, AutoNSubstituteData]
        public void Player_Fall_from_HighJump(
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
            sut.UpdateHandler();

            for (int i = 0; i < 30; i++)
                sut.UpdateHandler();

            Assert.Equal(State.Fall, state);
        }

        [Theory, AutoNSubstituteData]
        public void Player_Fall_from_Idle(
          InputController input
          , ValueKeeper<State> state
          , ValueKeeper<int> dashCooldown
          , ValueKeeper<int> grounded
          , ValueKeeper<int> hittingHead
          , ValueKeeper<bool> facingRight
          )
        {
            var sut = Player.Create(input, state, dashCooldown, grounded, hittingHead, facingRight);

            grounded.SetValue(Const.Grounded_Timer);
            sut.UpdateHandler();

            Assert.Equal(State.Idle, state);

            grounded.SetValue(0);
            sut.UpdateHandler();


            Assert.Equal(State.Fall, state);
        }

        [Theory, AutoNSubstituteData]
        public void Player_Fall_from_Walking(
          InputController input
          , ValueKeeper<State> state
          , ValueKeeper<int> dashCooldown
          , ValueKeeper<int> grounded
          , ValueKeeper<int> hittingHead
          , ValueKeeper<bool> facingRight
          )
        {
            var sut = Player.Create(input, state, dashCooldown, grounded, hittingHead, facingRight);

            grounded.SetValue(Const.Grounded_Timer);
            input.Right.Press();
            sut.UpdateHandler();

            Assert.Equal(State.Walking, state);

            grounded.SetValue(0);
            sut.UpdateHandler();


            Assert.Equal(State.Fall, state);
        }
    }
}

