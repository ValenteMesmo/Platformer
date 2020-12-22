using System;
using System.Collections.Generic;

namespace Platformer.Desktop
{
    public class StateMachine
    {
        private static Pool<StateMachine> Pool = new Pool<StateMachine>();

        private Dictionary<State, Action> stateChanges = null;
        private Dictionary<State, Action> updates = null;

        private StateMachine()
        {
            stateChanges = new Dictionary<State, Action>();
            updates = new Dictionary<State, Action>();
        }

        public void Add(State state, Action stateChange, Action update)
        {
            stateChanges.Add(state, stateChange);
            updates.Add(state, update);
        }

        public static StateMachine Create()
        {
            var current = Pool.Get();
            return current;
        }

        public void Update(ValueKeeper<State> state)
        {
            stateChanges[state]();
            updates[state]();
        }

        public void Destroy()
        {
            stateChanges.Clear();
            updates.Clear();

            Pool.Return(this);
        }
    }


    public enum State
    {
        Idle,
        Walking,
        JumpStart,
        Jump,
        JumpBreak,
        HeadBump,
        Fall,
        Dash
    }

    public static class StateExtensions
    {
        public static bool In(this State source, params State[] targets)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (source == targets[i])
                    return true;
            }

            return false;
        }

        public static bool In(this ValueKeeper<State> source, params State[] targets)
        {
            return source.GetValue().In(targets);
        }
    }
}
