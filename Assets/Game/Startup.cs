using Game.StateManagement;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Startup : MonoBehaviour
    {
        [Inject] private GameStateManager gameStateManager;

        private void Start()
        {
            gameStateManager.SetState<GameLoadState>();
        }

        private void OnDestroy()
        {
            gameStateManager.ReleaseCurrentState();
        }
    }
}
