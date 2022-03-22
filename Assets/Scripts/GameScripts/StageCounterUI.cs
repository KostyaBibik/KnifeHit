using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts
{
    public class StageCounterUI : MonoBehaviour
    {
        [SerializeField] private GameObject stageIcon;
        [SerializeField] private Color unCompletedColor;
        [SerializeField] private Color completedColor;
        [SerializeField] private Color currentStageColor;
    
        private List<GameObject> _stagesList = new List<GameObject>();
        private List<Image> _stagesImages = new List<Image>();
    
        private int _completedStagesCounter;
        private int _currentStage;

        public void SetStagesCounts(int countStages)
        {
            foreach (var item in _stagesList)
            {
                Destroy (item);
            }

            _completedStagesCounter = 0;
            _currentStage = 0;
            _stagesList.Clear();
            _stagesImages.Clear();
        
            for (int i = 0; i < countStages; i++) 
            {
                GameObject temp = Instantiate(stageIcon, transform);

                var image = temp.GetComponent<Image>();
                image.color = unCompletedColor;
            
                _stagesList.Add(temp);
                _stagesImages.Add(image);
            }

            DrawStages();
        }
    
        public void SetCompletedStages()
        {
            _completedStagesCounter++;
            _currentStage++;

            DrawStages();
        }

        private void DrawStages()
        {
            for (int i = 0; i < _stagesList.Count; i++)
            {
                if (_currentStage == i)
                {
                    _stagesImages[i].color = currentStageColor;
                }
                else
                {
                    _stagesImages[i].color = i < _completedStagesCounter ? completedColor : unCompletedColor;
                }
            }
        }
    }
}
