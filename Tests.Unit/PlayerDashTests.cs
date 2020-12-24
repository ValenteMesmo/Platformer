using Platformer.Desktop;
using Xunit;

namespace Tests.Unit
{
    public class PlayerDashTests
    {
        [Theory, AutoNSubstituteData]
        public void Player_Dash_from_Idle(
          InputController input
          , ValueKeeper<State> state
          , ValueKeeper<int> dashCooldown
          , ValueKeeper<int> grounded
          , ValueKeeper<int> hittingHead
          , ValueKeeper<bool> facingRight
          )
        {
            var sut = Player.Create(input, state, dashCooldown, grounded, hittingHead, facingRight);

            input.Dash.Press();
            grounded.SetValue(1);
            sut.UpdateHandler();

            Assert.Equal(State.Dash, state);
        }

        [Theory, AutoNSubstituteData]
        public void Player_Dash_into_Fall(
         InputController input
         , ValueKeeper<State> state
         , ValueKeeper<int> dashCooldown
         , ValueKeeper<int> grounded
         , ValueKeeper<int> hittingHead
         , ValueKeeper<bool> facingRight
         )
        {
            var sut = Player.Create(input, state, dashCooldown, grounded, hittingHead, facingRight);

            input.Dash.Press();
            grounded.SetValue(Const.Grounded_Timer);

            for (int i = 0; i <= 9; i++)
                sut.UpdateHandler();

            Assert.Equal(State.Fall, state);
        }

        [Theory, AutoNSubstituteData]
        public void Player_Dash_into_Idle(
         InputController input
         , ValueKeeper<State> state
         , ValueKeeper<int> dashCooldown
         , ValueKeeper<int> grounded
         , ValueKeeper<int> hittingHead
         , ValueKeeper<bool> facingRight
         )
        {
            var sut = Player.Create(input, state, dashCooldown, grounded, hittingHead, facingRight);

            input.Dash.Press();
            grounded.SetValue(Const.Grounded_Timer);

            for (int i = 0; i <= 9; i++)
            {
                grounded.SetValue(Const.Grounded_Timer);
                sut.UpdateHandler();
            }

            Assert.Equal(State.Idle, state);
        }
    }
}

