using System.Collections;
using UnityEngine;
using Zenject;

namespace GameScripts
{
    public class GameController : MonoBehaviour
    {
        private const string AppleHash = "CountCollectedApples";

        [SerializeField] private GameObject timber;
        [SerializeField] private GameObject hitTimberVFX;

        [Inject] private KnivesSpawner _knivesSpawner;
        [Inject] private UiManager _uiManager;
        [Inject] private KnifeCounterUI _knifeCounterUI;
        [Inject] private ProgressPlayer _progressPlayer;
        
        private TimberController _timberController;

        private int _usedKnives;
        private int _collectedApples;
        private int _countOfKnives;

        private void Start()
        {
            if(PlayerPrefs.HasKey(AppleHash))
            {
                _collectedApples = PlayerPrefs.GetInt(AppleHash);
            }
            else
            {
                PlayerPrefs.SetInt(AppleHash, 0);
            }
            
            _uiManager.UpdateAppleCount(_collectedApples);
            _uiManager.SetStagesCount(_progressPlayer.GetNumberStages());
            _uiManager.SetLevelProgress(_progressPlayer.currentLevel);
            _uiManager.SetRecord(_progressPlayer.GetRecordLevel());    
            
            CreateGame();
        }

        public void CorrectHit()
        {
            _usedKnives++;
            if(_usedKnives < _countOfKnives)
            {
                var timberVFX = Instantiate(hitTimberVFX, _timberController.transform.position, Quaternion.identity);
                Destroy(timberVFX, 1.0f);
                _knivesSpawner.CreateKnife();
            }
            else
            {
                if(_progressPlayer.GoToNextStage())
                {
                    StartCoroutine(nameof(ResetGame), true);
                    _uiManager.CompleteStage();
                }
                else
                {
                    if (_progressPlayer.GoToNextLevel())
                    {
                        Vibration.Vibrate(300);
                        StartCoroutine(nameof(ResetGame), true);
                        
                        _uiManager.SetStagesCount(_progressPlayer.GetNumberStages());
                        _uiManager.SetLevelProgress(_progressPlayer.currentLevel);
                    }
                    else
                    {
                        Vibration.Vibrate(300);
                        StartCoroutine(nameof(ResetGame), false);
                        _uiManager.ActivateWinPanel();
                        _uiManager.CompleteStage();
                    }
                }
            }
        }

        private IEnumerator ResetGame(bool startNewGame)
        {
            _timberController.DestroyTimber(true);
            yield return new WaitForSeconds(1.5f);
            _knivesSpawner.ClearKnives();
            if(startNewGame)
            {
                CreateGame();
            }
        }
    
        public void LoseHit()
        {
            Vibration.Vibrate(450);
            _uiManager.ActivateLosePanel();
            _timberController.DestroyTimber(false);
            _knivesSpawner.StopSpawnKnives();
        }

        private void CreateGame()
        {
            _usedKnives = 0;
            
            _knivesSpawner.CreateKnife();

            var difficulty = _progressPlayer.GetCurrentStage();
            
            var newTimber = Instantiate(timber);
        
            _countOfKnives = difficulty.countOfKnives;
            _timberController = newTimber.GetComponent<TimberController>();
            _timberController.SetParameters(difficulty.rotationVariations, difficulty.appleVariations, difficulty.knifeAnglesSpawn);
            _knifeCounterUI.SetUpCounter(_countOfKnives);
        }

        public void HitOnApple()
        {
            _collectedApples = PlayerPrefs.GetInt(AppleHash) + 1;
            PlayerPrefs.SetInt(AppleHash, _collectedApples);
            _uiManager.UpdateAppleCount(_collectedApples);
        }
    }
}
