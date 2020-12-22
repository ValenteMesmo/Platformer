using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Platformer.Desktop;

namespace Tests.Unit
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture();
                fixture.Register(()=> ValueKeeper<int>.Create());
                fixture.Register(()=> ValueKeeper<bool>.Create());
                fixture.Register(()=> GameObject.Create());
                fixture.Register(()=> new InputKey());
                fixture.Customize(new AutoNSubstituteCustomization());
                return fixture;
            })
        {
        }
    }
}
