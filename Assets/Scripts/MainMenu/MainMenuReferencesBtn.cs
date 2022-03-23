using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenuReferencesBtn : MonoBehaviour
    {
        private const string AppleHash = "CountCollectedApples";
    
        [SerializeField] private TMP_Text appleText;
        [SerializeField] private Button playBtn;
        
        [Header("Mode toggles")]
        [SerializeField] private Toggle easyToggle; 
        [SerializeField] private Toggle normalToggle;
        [SerializeField] private Toggle hardToggle;
    
        private TransitionHandler _transitionHandler;
        private DifficultyDeterminer _difficultyDeterminer;
    
        private void Awake()
        {
            _transitionHandler = FindObjectOfType<TransitionHandler>();
            _difficultyDeterminer = FindObjectOfType<DifficultyDeterminer>();
        }

        private void Start()
        {
            if(PlayerPrefs.HasKey(AppleHash))
            {
                appleText.text = PlayerPrefs.GetInt(AppleHash).ToString();
            }
            else
            {
                PlayerPrefs.SetInt(AppleHash, 0);
                appleText.text = 0.ToString();
            }
        
            playBtn.onClick.AddListener(delegate
            {
                SoundManager.instance.PlayBtnSfx();
                _transitionHandler.StartFade();
            });
        
            easyToggle.onValueChanged.AddListener(delegate
            {
                SoundManager.instance.PlayBtnSfx();
                _difficultyDeterminer.SetDifficulty(DifficultyMode.Easy);
            });
        
            normalToggle.onValueChanged.AddListener(delegate
            {
                SoundManager.instance.PlayBtnSfx();
                _difficultyDeterminer.SetDifficulty(DifficultyMode.Normal);
            });
        
            hardToggle.onValueChanged.AddListener(delegate
            {
                SoundManager.instance.PlayBtnSfx();
                _difficultyDeterminer.SetDifficulty(DifficultyMode.Hard);
            });
        }
    }
}
