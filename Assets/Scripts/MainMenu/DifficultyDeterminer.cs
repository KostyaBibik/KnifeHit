using System;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyDeterminer : MonoBehaviour
{
    [SerializeField] private List<Difficulty> easyDifficulties;
    [SerializeField] private List<Difficulty> normalDifficulties;
    [SerializeField] private List<Difficulty> hardDifficulties;

    public static DifficultyDeterminer instance;
    
    private DifficultyMode _selectedDifficulty;
    private Dictionary<DifficultyMode, List<Difficulty>> _dictionaryDifficulties = new Dictionary<DifficultyMode, List<Difficulty>>();

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
        _dictionaryDifficulties.Add(DifficultyMode.Easy, easyDifficulties);
        _dictionaryDifficulties.Add(DifficultyMode.Normal, normalDifficulties);
        _dictionaryDifficulties.Add(DifficultyMode.Hard, hardDifficulties);
    }

    public List<Difficulty> GetDifficulty()
    {
        return _dictionaryDifficulties[_selectedDifficulty];
    }

    public void SetDifficulty(DifficultyMode difficulty)
    {
        _selectedDifficulty = difficulty;
    }
}

[Serializable]
public enum DifficultyMode
{
    Easy,
    Normal,
    Hard
};