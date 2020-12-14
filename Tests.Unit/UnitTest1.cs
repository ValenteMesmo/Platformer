using NSubstitute;
using Platformer.Desktop;
using Xunit;

namespace Tests.Unit
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var obj = GameObject.Create();
            var input = new InputController();
            var grounded = ValueKeeper<bool>.Create();

            input.Jump.Press();
            grounded.SetValue(true);

            UpdateJump.Update(obj, input, grounded);

            obj.Velocity.Y = 0;

            UpdateJump.Update(obj, input, grounded);
        }

    }
}
