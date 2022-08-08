using CodeBase.Logic;
using CodeBase.Services.Input;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public readonly GameStateMachine StateMachine;

        public Game(ICouroutineRunner couroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(couroutineRunner), loadingCurtain);
        }
    }
}