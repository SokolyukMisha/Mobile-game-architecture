using CodeBase.Services.Input;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public GameStateMachine StateMachine;

        public Game()
        {
            StateMachine = new GameStateMachine();
        }
    }
}   