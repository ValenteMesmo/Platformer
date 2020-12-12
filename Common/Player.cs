using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Desktop
{
    public static class Player
    {
        public static GameObject Create(GameInputs input)
        {
            var obj = GameObject.Create();
            obj.Position.Y = -14000;
            obj.Identifier = Identifier.Player;

            var grounded = ValueKeeper<bool>.Create();
            var facingRight = ValueKeeper<bool>.Create();
            var jumping = ValueKeeper<bool>.Create();
            
            var collider = Collider.Create(obj);
            collider.Area = new Rectangle(
                50 * Constant.Scale
                , 4000
                , 10000
                , 20000 - 4000);
            collider.Handler = StopsWhenHitingBlocks.Create();

            collider = Collider.Create(obj);
            collider.Area = new Rectangle(50 * Constant.Scale, 210 * Constant.Scale, 100 * Constant.Scale, 10 * Constant.Scale);
            collider.Handler = DetectsIfGrounded.Create(grounded);

            var animation = Animation.Create();
            animation.Sprites.Add(Textures.idle);
            animation.Sprites.Add(Textures.walk);

            obj.RenderHandler = animation;
            obj.UpdateHandler = () =>
            {
                UpdateVelocityUsingInputs.Update(obj, input, grounded, facingRight);
                UpdateJump.Update(obj, input, grounded, jumping);
                UpdateGravity.Update(obj);
                UpdatePlayerAnimation.Update(input, animation, facingRight, grounded);
                grounded.SetValue(false);
            };

            obj.OnDestroy = () =>
            {
                grounded.Destroy();
            };

            return obj;
        }




    }
}
