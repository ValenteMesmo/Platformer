namespace Platformer.Desktop
{
    public static class UpdateGravity
    {
        public static void Update(GameObject obj)
        {
            obj.Velocity.Y = 
                obj.Velocity.Y.IncrementUntil(
                    200, 2000                    
                    );
        }
    }
}
