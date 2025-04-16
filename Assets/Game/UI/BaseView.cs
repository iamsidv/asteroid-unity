using Game.Signals;
using Game.StateManagement;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class BaseView : MonoBehaviour
    {
        protected ISignalService _signalService;
        protected GameStateManager GameStateManager;

        [Inject]
        private void InitSignalService(ISignalService signalService, GameStateManager gameStateManager)
        {
            _signalService = signalService;
            GameStateManager = gameStateManager;
        }

        public virtual void OnScreenEnter()
        {
        }

        public virtual void OnScreenExit()
        {
        }

        public void SetVisibility(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}