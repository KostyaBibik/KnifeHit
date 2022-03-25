using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
    
        [Inject] private TransitionHandler _transitionHandler;
        [Inject] private DifficultyDeterminer _difficultyDeterminer;
        [Inject] private SoundManager _soundManager;

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
                _soundManager.PlayBtnSfx();
                _transitionHandler.StartFade();
            });
        
            easyToggle.onValueChanged.AddListener(delegate
            {
                _soundManager.PlayBtnSfx();
                _difficultyDeterminer.SetDifficulty(DifficultyMode.Easy);
            });
        
            normalToggle.onValueChanged.AddListener(delegate
            {
                _soundManager.PlayBtnSfx();
                _difficultyDeterminer.SetDifficulty(DifficultyMode.Normal);
            });
        
            hardToggle.onValueChanged.AddListener(delegate
            {
                _soundManager.PlayBtnSfx();
                _difficultyDeterminer.SetDifficulty(DifficultyMode.Hard);
            });
        }
    }
}
