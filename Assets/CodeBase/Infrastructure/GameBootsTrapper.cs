using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootsTrapper : MonoBehaviour, ICouroutineRunner
    {
        public LoadingCurtain curtain;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, curtain);
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}
