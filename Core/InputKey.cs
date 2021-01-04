namespace Platformer.Desktop
{
    public struct InputKey
    {
        private void Press()
        {
            IsPressEnding = false;
            IsPressStaring = !IsPressed;
            IsPressed = true;

            if (IsPressStaring)
            {
                IsToogled = !IsToogled;
                Heat = 12;
            }
            else
                Heat = Heat.DecrementUntil(0);
        }

        private void Release()
        {
            IsPressEnding = IsPressed;
            IsPressStaring = false;
            IsPressed = false;
            Heat = Heat.DecrementUntil(0);
        }

        public void Update()
        {
            if (IsPressed)
                Press();
            else
                Release();
        }

        public bool IsPressed { get; set; }
        public int Heat { get; private set; }
        public bool IsToogled { get; private set; }
        public bool IsPressStaring { get; private set; }
        public bool IsPressEnding { get; private set; }

        public static implicit operator bool(InputKey key)
        {
            return key.IsPressed;
        }

    }

}
