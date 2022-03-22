using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "Stage", order = 20)]
public class Stage: ScriptableObject
{
    public int countOfKnives;
    public List<RotationVariation> rotationVariations;
    public List<AppleVariations> appleVariations;
    public List<float> knifeAnglesSpawn = new List<float>();
}

[System.Serializable]
public class RotationVariation
{
    [Range(0f,2f)] public float time;
    [Range(-180,180f)] public float z;
    
    public AnimationCurve curve;
}

[System.Serializable]
public class AppleVariations
{
    [Range(-180, 180)] public float angleSpawn;
    [Range(0, 100)] public int chanceToSpawn = 25;
}