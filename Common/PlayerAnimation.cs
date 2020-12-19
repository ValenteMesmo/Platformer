using System.Collections.Generic;

namespace Platformer.Desktop
{
    public static class PlayerAnimation
    {
        public static void Update(
            GameObject obj
            , Dictionary<State, Animation> animations
            , ValueKeeper<State> state
            , ValueKeeper<bool> facingRight
        )
        {
            obj.RenderHandler = animations[state];
            animations[state].flipped = !facingRight;

            //if (state.Changed)
            //    animations[state].Reset();
            //else
                animations[state].Update();
        }

        public static Animation Idle()
        {
            var animation = Animation.Create();
            animation.Sprites.Add(Textures.idle);
            return animation;
        }        

        public static Animation Walk()
        {
            var animation = Animation.Create();
            animation.Sprites.Add(Textures.walk);
            animation.Sprites.Add(Textures.walk);
            animation.Sprites.Add(Textures.walk);
            animation.Sprites.Add(Textures.idle);
            animation.Sprites.Add(Textures.idle);
            animation.Sprites.Add(Textures.idle);
            return animation;
        }

        public static Animation Jump()
        {
            var animation = Animation.Create();
            animation.Sprites.Add(Textures.jump);
            return animation;
        }

        public static Animation Fall()
        {
            var animation = Animation.Create();
            animation.Sprites.Add(Textures.walk);
            return animation;
        }

        public static Animation HeadBump()
        {
            var animation = Animation.Create();
            animation.Sprites.Add(Textures.head_bump);
            return animation;
        }
    }
}
