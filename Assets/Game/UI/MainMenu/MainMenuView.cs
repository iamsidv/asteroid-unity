using Game.StateManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class MainMenuView : BaseView
    {
        [SerializeField] private Button buttonStartGame;
        [SerializeField] private TextMeshProUGUI loadingText;

        public override void OnScreenEnter()
        {
            base.OnScreenEnter();

            buttonStartGame.onClick.RemoveAllListeners();
            buttonStartGame.onClick.AddListener(StartGameClicked);
        }

        private void StartGameClicked()
        {
            GameStateManager.SetState<GameRunningState>();
        }

        public void ToggleStartButton(bool isactive)
        {
            buttonStartGame.gameObject.SetActive(isactive);
            loadingText.gameObject.SetActive(!isactive);
        }
    }
}