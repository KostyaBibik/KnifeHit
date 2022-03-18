using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private const string AppleHash = "CountCollectedApples";
    
    [SerializeField] private TMP_Text appleText;
    [SerializeField] private Button playBtn;
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
            _transitionHandler.StartFade();
        });
        
        easyToggle.onValueChanged.AddListener(delegate(bool arg0)
        {
            Debug.Log(_difficultyDeterminer.name);
            _difficultyDeterminer.SetDifficulty(Diffuculty.Easy);
        });
        
        normalToggle.onValueChanged.AddListener(delegate(bool arg0)
        {
            _difficultyDeterminer.SetDifficulty(Diffuculty.Normal);
        });
        
        hardToggle.onValueChanged.AddListener(delegate(bool arg0)
        {
            _difficultyDeterminer.SetDifficulty(Diffuculty.Hard);
        });
        
    }
}
