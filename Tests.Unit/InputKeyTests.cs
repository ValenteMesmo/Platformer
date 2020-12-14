using NSubstitute;
using Platformer.Desktop;
using Xunit;

namespace Tests.Unit
{
    public class InputKeyTests
    {
        [Fact]
        public void Press_test()
        {
            var sut = new InputKey();

            Assert.False(sut.IsPressed);
            Assert.False(sut.IsPressEnding);
            Assert.False(sut.IsPressStaring);

            sut.Press();

            Assert.True(sut.IsPressed);
            Assert.False(sut.IsPressEnding);
            Assert.True(sut.IsPressStaring);

            sut.Press();

            Assert.True(sut.IsPressed);
            Assert.False(sut.IsPressEnding);
            Assert.False(sut.IsPressStaring);

            sut.Release();

            Assert.False(sut.IsPressed);
            Assert.True(sut.IsPressEnding);
            Assert.False(sut.IsPressStaring);

            sut.Release();

            Assert.False(sut.IsPressed);
            Assert.False(sut.IsPressEnding);
            Assert.False(sut.IsPressStaring);
        }

        [Fact]
        public void Toggle_test()
        {
            var sut = new InputKey();

            Assert.False(sut.IsPressed);

            sut.Press();

            Assert.True(sut.IsPressed);

            sut.Press();

            Assert.True(sut.IsPressed);

            sut.Release();

            Assert.False(sut.IsPressed);

            sut.Release();

            Assert.False(sut.IsPressed);

            sut.Press();

            Assert.True(sut.IsPressed);

            sut.Release();

            Assert.False(sut.IsPressed);


        }
    }
}
