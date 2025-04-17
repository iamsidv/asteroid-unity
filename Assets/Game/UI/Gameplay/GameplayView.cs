using Game.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Gameplay
{
    public class GameplayView : BaseView
    {
        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private TextMeshProUGUI titleLabel;
        [SerializeField] private GameObject stubChances;
        [SerializeField] private Image chanceImage;
        [SerializeField] private Image[] totalChances;

        public override void OnScreenEnter()
        {
            base.OnScreenEnter();
            SignalService.Subscribe<DisplayScoreSignal>(OnScoreUpdated);
            SignalService.Subscribe<UpdatePlayerLivesSignal>(OnPlayerLifeChanged);
        }

        public override void OnScreenExit()
        {
            base.OnScreenExit();

            SignalService.RemoveSignal<DisplayScoreSignal>(OnScoreUpdated);
            SignalService.RemoveSignal<UpdatePlayerLivesSignal>(OnPlayerLifeChanged);

            Clear();
        }

        public void DisplayPlayerLivesUI(int count)
        {
            Clear();

            totalChances = new Image[count];
            for (int i = 0; i < totalChances.Length; i++)
            {
                totalChances[i] = Instantiate(chanceImage, stubChances.transform);
                totalChances[i].gameObject.SetActive(true);
            }
        }

        public void DisplayScore(int currentScore)
        {
            scoreLabel.text = $"Score :{currentScore}";
        }

        public void SetTitle(string title)
        {
            titleLabel.text = title;
        }

        public void Clear()
        {
            if (totalChances != null)
            {
                for (int i = 0; i < totalChances.Length; i++)
                {
                    Destroy(totalChances[i].gameObject);
                }

                totalChances = null;
            }
        }

        private void OnScoreUpdated(DisplayScoreSignal signal) => DisplayScore(signal.Value);

        private void OnPlayerLifeChanged(UpdatePlayerLivesSignal signal)
        {
            for (int i = totalChances.Length - 1; i >= signal.Value; i--)
            {
                totalChances[i].gameObject.SetActive(false);
            }
        }
    }
}