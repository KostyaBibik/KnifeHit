using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TMP_Text applesCount;
    private StageCounterUI _stageCounter;

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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
