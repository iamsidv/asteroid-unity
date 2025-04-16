using Asteroids.Game.Signals;
using Game.PlayerState;
using Game.Signals;
using Game.StateManagement;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class MainManager : MonoBehaviour
    {
        private ISignalService _signalService;
        private IPlayerProfileService _playerProfileService;
        private GameStateManager _gameStateManager;

        [Inject]
        private void InitServices(ISignalService signalService,
            IPlayerProfileService playerProfileService, GameStateManager gameStateManager)
        {
            _signalService = signalService;
            _playerProfileService = playerProfileService;
            _gameStateManager = gameStateManager;
        }

        private void OnEnable()
        {
            _signalService.Subscribe<UpdateScoreSignal>(AddScore);
            _signalService.Subscribe<PlayerDiedSignal>(PlayerDeathSignal);
        }

        private void OnDisable()
        {
            _signalService.RemoveSignal<UpdateScoreSignal>(AddScore);
            _signalService.RemoveSignal<PlayerDiedSignal>(PlayerDeathSignal);
        }

        private void AddScore(UpdateScoreSignal signal)
        {
            _playerProfileService.AddScore(signal.Value);
            _signalService.Publish(new DisplayScoreSignal { Value = _playerProfileService.GetScore() });
        }

        private void PlayerDeathSignal(PlayerDiedSignal signal)
        {
            var lives = _playerProfileService.GetTotalLives();
            lives -= 1;
            if (lives <= 0)
            {
                lives = 0;
                _gameStateManager.SetState<GameOverState>();
            }
            else
            {
                _signalService.Publish(new UpdatePlayerLivesSignal { Value = lives });
                _signalService.Publish<PlayerReviveSignal>();
            }
            _playerProfileService.SetTotalLives(lives);
        }
    }
}