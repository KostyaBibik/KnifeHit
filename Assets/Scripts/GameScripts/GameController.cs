using System.Collections;
using UnityEngine;

namespace GameScripts
{
    public class GameController : MonoBehaviour
    {
        private const string AppleHash = "CountCollectedApples";

        [SerializeField] private GameObject timber;
        [SerializeField] private GameObject hitTimberVFX;

        private KnivesManager _knivesManager;
        private TimberController _timberController;
        private UiManager _uiManager;
        private KnifeCounterUI _knifeCounterUI;
        private ProgressManager _progressManager;
    
        private int _usedKnives;
        private int _collectedApples;
        private int _countOfKnives;
    
        private void Awake()
        {
            _knivesManager = FindObjectOfType<KnivesManager>();
            _uiManager = FindObjectOfType<UiManager>();
            _knifeCounterUI = FindObjectOfType<KnifeCounterUI>();
            _progressManager = FindObjectOfType<ProgressManager>();
        }

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
            _uiManager.SetStagesCount(_progressManager.GetNumberStages());
            _uiManager.SetLevelProgress(_progressManager.currentLevel);
            _uiManager.SetRecord(_progressManager.GetRecordLevel());    
            
            CreateGame();
        }

        public void CorrectHit()
        {
            _usedKnives++;
            if(_usedKnives < _countOfKnives)
            {
                var timberVFX = Instantiate(hitTimberVFX, _timberController.transform.position, Quaternion.identity);
                Destroy(timberVFX, 1.0f);
                _knivesManager.CreateKnife();
            }
            else
            {
                if(_progressManager.GoToNextStage())
                {
                    StartCoroutine(nameof(ResetGame), true);
                    _uiManager.CompleteStage();
                }
                else
                {
                    if (_progressManager.GoToNextLevel())
                    {
                        Vibration.Vibrate(300);
                        StartCoroutine(nameof(ResetGame), true);
                        
                        _uiManager.SetStagesCount(_progressManager.GetNumberStages());
                        _uiManager.SetLevelProgress(_progressManager.currentLevel);
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
            _knivesManager.ClearKnives();
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
            _knivesManager.StopSpawnKnives();
        }

        private void CreateGame()
        {
            _usedKnives = 0;
            
            _knivesManager.CreateKnife();

            var difficulty = _progressManager.GetCurrentStage();
            
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
