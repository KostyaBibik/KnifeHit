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
        private StageManager _stageManager;
    
        private int _usedKnives;
        private int _collectedApples;
        private int _countOfKnives;
    
        private void Awake()
        {
            _knivesManager = FindObjectOfType<KnivesManager>();
            _uiManager = FindObjectOfType<UiManager>();
            _knifeCounterUI = FindObjectOfType<KnifeCounterUI>();
            _stageManager = FindObjectOfType<StageManager>();
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
            _uiManager.SetStagesCount(_stageManager.GetNumberStages());
        
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
                if(_stageManager.GoToNextStage())
                {
                    StartCoroutine(nameof(ResetGame), true);
                }
                else
                {
                    _uiManager.ActivateWinPanel();
                    StartCoroutine(nameof(ResetGame), false);
                }
                _uiManager.CompleteStage();
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
            Vibration.Vibrate(300);
            _uiManager.ActivateLosePanel();
            _timberController.DestroyTimber(false);
            _knivesManager.StopSpawnKnives();
        }

        private void CreateGame()
        {
            _usedKnives = 0;
            _knivesManager.CreateKnife();

            var difficulty = _stageManager.GetCurrentDifficulty();
        
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
