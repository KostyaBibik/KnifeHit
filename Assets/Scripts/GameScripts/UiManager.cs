using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScripts
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private TMP_Text applesCount;
        [SerializeField] private TMP_Text levelProgress;
        [SerializeField] private TMP_Text recordLevel;
        
        private StageCounterUI _stageCounter;
        private int _recordLevel;
        
        private void Awake()
        {
            _stageCounter = FindObjectOfType<StageCounterUI>();
        }

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
            SoundManager.instance.PlayBtnSfx();
        }
    }
}
