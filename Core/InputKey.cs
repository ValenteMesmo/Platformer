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
                IsToogled = !IsToogled;
        }

        public void Release()
        {
            IsPressEnding = IsPressed;
            IsPressStaring = false;
            IsPressed = false;
        }

        public bool IsPressed { get; private set; }
        //just an idea.... to detect button mashing
        public int Heat { get; private set; }
        public bool IsToogled { get; private set; }
        public bool IsPressStaring { get; private set; }
        public bool IsPressEnding { get; private set; }
    }

}
