using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameScripts
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private TMP_Text applesCount;
        [SerializeField] private TMP_Text levelProgress;
        [SerializeField] private TMP_Text recordLevel;
        
        [Inject] private StageCounterUI _stageCounter;
        [Inject] private SoundManager _soundManager;
        
        private int _recordLevel;

        public void SetStagesCount(int count)
        {
            _stageCounter.SetStagesCounts(count);
        }

        public void CompleteStage()
        {
            _stageCounter.SetCompletedStages();
        }
    
        public void UpdateAppleCount(int count)
        {
            applesCount.text = count.ToString();
        }
    
        public void ActivateLosePanel()
        {
            losePanel.SetActive(true);
        }
    
        public void ActivateWinPanel()
        {
            winPanel.SetActive(true);
        }

        public void SetLevelProgress(int level)
        {
            if (_recordLevel < level)
            {
                SetRecord(level);
            }
            
            levelProgress.text = new StringBuilder($"Level {level + 1}").ToString();
        }

        public void SetRecord(int level)
        {
            _recordLevel = level;
            recordLevel.text = new StringBuilder($"Record - {level + 1}").ToString();
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void PlaySoundBtn()
        {
            _soundManager.PlayBtnSfx();
        }
    }
}
