using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level", order = 21)]
[System.Serializable]
public class Level : ScriptableObject
{
    [SerializeField] private List<Stage> stages;

    public Stage GetStage(int counter)
    {
        return stages[counter];
    }

    public int GetLengthStages()
    {
        return stages.Count;
    }
}
