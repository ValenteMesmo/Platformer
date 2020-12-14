namespace Platformer.Desktop
{
    public struct InputKey
    {
        public void Press()
        {
            IsPressEnding = false;
            IsPressStaring = !IsPressed;
            IsPressed = true;

            if (IsPressStaring)
            {
                IsToogled = !IsToogled;
                Heat = 6;
            }
            else
                Heat = Heat.DecrementUntil(0);
        }

        public void Release()
        {
            IsPressEnding = IsPressed;
            IsPressStaring = false;
            IsPressed = false;
            Heat = Heat.DecrementUntil(0);
        }

        public bool IsPressed { get; private set; }
        public int Heat { get; private set; }
        public bool IsToogled { get; private set; }
        public bool IsPressStaring { get; private set; }
        public bool IsPressEnding { get; private set; }
    }

}
