using System;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyDeterminer : MonoBehaviour
{
    public static DifficultyDeterminer instance;

    [SerializeField] private List<Level> easyLevels;
    [SerializeField] private List<Level> normalLevels;
    [SerializeField] private List<Level> hardLevels;

    private DifficultyMode selectedDifficulty;
    private Dictionary<DifficultyMode, List<Level>> _dictionaryLevels = new Dictionary<DifficultyMode, List<Level>>();

    private void Awake()
    {
        if (instance != null) 
        {
            DestroyImmediate (gameObject);
        } 
        else 
        {
            instance = this;
            DontDestroyOnLoad (gameObject);
        }
    }

    private void Start()
    {
        _dictionaryLevels.Add(DifficultyMode.Easy, easyLevels);
        _dictionaryLevels.Add(DifficultyMode.Normal, normalLevels);
        _dictionaryLevels.Add(DifficultyMode.Hard, hardLevels);
    }

    public Level GetLevel(int level)
    {
        return _dictionaryLevels[selectedDifficulty][level];
    }

    public int GetLengthLevels()
    {
        return _dictionaryLevels[selectedDifficulty].Count;
    }
    
    public void SetDifficulty(DifficultyMode difficulty)
    {
        selectedDifficulty = difficulty;
    }

    public DifficultyMode GetDifficulty()
    {
        return selectedDifficulty;
    }
}

[Serializable]
public enum DifficultyMode
{
    Easy,
    Normal,
    Hard
};