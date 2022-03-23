using UnityEngine;

namespace GameScripts
{
    public class ProgressPlayer : MonoBehaviour
    {
        private const string LevelRecordHash = "RecordLevel";
        
        [HideInInspector] public int currentStage;
        [HideInInspector] public int currentLevel;
        
        private DifficultyDeterminer _difficultyDeterminer;

        private void Awake()
        {
            _difficultyDeterminer = DifficultyDeterminer.instance;
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey($"{_difficultyDeterminer.GetDifficulty().ToString()}{LevelRecordHash}"))
            {
                PlayerPrefs.SetInt($"{_difficultyDeterminer.GetDifficulty().ToString()}{LevelRecordHash}", 0);
            }
        }

        public bool GoToNextStage()
        {
            currentStage++;
            return currentStage < _difficultyDeterminer.GetLevel(currentLevel).GetLengthStages();
        }

        public Stage GetCurrentStage()
        {
            return _difficultyDeterminer.GetLevel(currentLevel).GetStage(currentStage);
        }

        public int GetNumberStages()
        {
            return _difficultyDeterminer.GetLevel(currentLevel).GetLengthStages();
        }

        public int GetRecordLevel()
        {
            if (PlayerPrefs.HasKey($"{_difficultyDeterminer.GetDifficulty().ToString()}{LevelRecordHash}"))
            {
                return PlayerPrefs.GetInt($"{_difficultyDeterminer.GetDifficulty().ToString()}{LevelRecordHash}");
            }
            else
            {
                PlayerPrefs.SetInt($"{_difficultyDeterminer.GetDifficulty().ToString()}{LevelRecordHash}", 0);
                return 0;
            }
        }

        public bool GoToNextLevel()
        {
            currentLevel++;
            currentStage = 0;
            PlayerPrefs.SetInt($"{_difficultyDeterminer.GetDifficulty().ToString()}{LevelRecordHash}", currentLevel);
            
            return currentLevel < _difficultyDeterminer.GetLengthLevels();
        }
    }
}
