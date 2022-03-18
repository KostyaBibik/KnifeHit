using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int currentStage;
    private DifficultyDeterminer _difficultyDeterminer;

    private void Awake()
    {
        _difficultyDeterminer = DifficultyDeterminer.instance;
    }

    public bool GoToNextStage()
    {
        currentStage++;
        return currentStage < _difficultyDeterminer.GetDifficulty().Count;
    }

    public Difficulty GetCurrentDifficulty()
    {
        return _difficultyDeterminer.GetDifficulty()[currentStage];
    }

    public int GetNumberStages()
    {
        return _difficultyDeterminer.GetDifficulty().Count;
    }
}
