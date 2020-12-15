namespace Platformer.Desktop
{
    public static class ChangeToWalkingLeft
    {
        public static void Try(
            InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            if (input.Left.IsPressed && grounded > 0)
                state.SetValue(State.WalkingLeft);
        }
    }

    public static class ChangeToWalkingRight
    {
        public static void Try(
            InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            if (input.Right.IsPressed && grounded > 0)
                state.SetValue(State.WalkingRight);
        }
    }

    public static class ChangeToIdle
    {
        public static void Try(
            InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            if (!input.Left.IsPressed && !input.Right.IsPressed && grounded > 0)
                state.SetValue(State.Idle);                
        }
    }

    public static class ChangeToJumpState
    {
        public static void Try(
            GameObject obj
            , InputController input
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            if (grounded > 0 && (input.Jump.IsPressStaring || input.Jump.Heat > 0))
                state.SetValue(State.Jump);

            if (!input.Jump.IsPressed && obj.Velocity.Y < 0)
                state.SetValue(State.BreakJump);
        }
    }

    public static class ChangeToFallingState
    {
        public static void Try(
            GameObject obj
            , ValueKeeper<int> grounded
            , ValueKeeper<State> state)
        {
            if (grounded == 0 && obj.Velocity.Y > 0)
                state.SetValue(State.Fall);
        }
    }

    public static class UpdateJump
    {
        public static void Update(
           GameObject obj
           , InputController input
           , ValueKeeper<int> grounded
        )
        {
            if (grounded > 0 && (input.Jump.IsPressStaring || input.Jump.Heat > 0))
                obj.Velocity.Y = -Const.jumpForce;

            if (!input.Jump.IsPressed && obj.Velocity.Y < 0)
                obj.Velocity.Y += Const.stoppingGravity;
        }

    }
}
