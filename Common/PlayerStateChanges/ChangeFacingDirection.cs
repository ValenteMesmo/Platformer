namespace Platformer.Desktop
{
    public static class ChangeFacingDirection
    {
        public static void Change(
            InputController input
            , ValueKeeper<bool> facingRight)
        {
            if (input.Left)
                facingRight.SetValue(false);
            else if (input.Right)
                facingRight.SetValue(true);
        }
    }
}
