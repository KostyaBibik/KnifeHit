using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyDeterminer : MonoBehaviour
{
    [SerializeField] private List<Difficulty> easyDifficulties;
    [SerializeField] private List<Difficulty> normalDifficulties;
    [SerializeField] private List<Difficulty> hardDifficulties;

    public static DifficultyDeterminer intance;
    
    private Diffuculty _selectedDifficulty;
    private Dictionary<Diffuculty, List<Difficulty>> _dictionaryDiffuclties = new Dictionary<Diffuculty, List<Difficulty>>();

    private void Awake()
    {
        if (intance != null) 
        {
            DestroyImmediate (gameObject);
        } 
        else 
        {
            intance = this;
            DontDestroyOnLoad (gameObject);
        }
    }

    private void Start()
    {
        _dictionaryDiffuclties.Add(Diffuculty.Easy, easyDifficulties);
        _dictionaryDiffuclties.Add(Diffuculty.Normal, normalDifficulties);
        _dictionaryDiffuclties.Add(Diffuculty.Hard, hardDifficulties);
    }

    public List<Difficulty> GetDifficulty()
    {
        return _dictionaryDiffuclties[_selectedDifficulty];
    }

    public void SetDifficulty(Diffuculty difficulty)
    {
        _selectedDifficulty = difficulty;
    }
}

[Serializable]
public enum Diffuculty
{
    Easy,
    Normal,
    Hard
};