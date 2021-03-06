using Platformer.Desktop;
using Xunit;

namespace Tests.Unit
{
    public class UpdateJumpTests
    {

        [Fact]
        public void High_Jump()
        {
            var obj = GameObject.Create();
            var input = new InputController();
            var grounded = ValueKeeper<int>.Create();

            input.Jump.Press();
            grounded.SetValue(1);

            //UpdateJump.Update(obj, input, grounded);

            Assert.Equal(obj.Velocity.Y, -Const.jumpForce);
        }

        [Fact]
        public void Low_Jump()
        {
            var obj = GameObject.Create();
            var input = new InputController();
            var grounded = ValueKeeper<int>.Create();

            input.Jump.Press();
            grounded.SetValue(6);

            //UpdateJump.Update(obj, input, grounded);
            input.Jump.Release();

            Assert.Equal(obj.Velocity.Y, -Const.jumpForce);

            //UpdateJump.Update(obj, input, grounded);

            Assert.Equal(obj.Velocity.Y, -Const.jumpForce + Const.stoppingGravity);
        }

    }
}
