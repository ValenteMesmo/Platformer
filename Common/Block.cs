using Microsoft.Xna.Framework;

namespace Platformer.Desktop
{
    public static class Block
    {
        private static GameObject obj = null;
        private static Collider collider = null;
        public static GameObject Create()
        {
            obj = GameObject.Create();
            obj.Identifier = Identifier.Block;
            collider = Collider.Create(obj);
            collider.Area = new Rectangle(0, 0, 200 * Const.Scale, 200 * Const.Scale);


            obj.RenderHandler=Textures.create_block();

            return obj;
        }
    }
}
