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
            var input = new GameInputs();
            var grounded = ValueKeeper<bool>.Create();
            var jumping = ValueKeeper<bool>.Create();

            input.Jump = 1;
            grounded .SetValue(true);

            UpdateJump.Update(obj, input, grounded, jumping);

            obj.Velocity.Y = 0;

            UpdateJump.Update(obj, input, grounded, jumping);
        }
    }
}
