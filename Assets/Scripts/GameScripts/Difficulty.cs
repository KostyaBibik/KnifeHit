using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty", menuName = "Difficulty", order = 20)]
public class Difficulty: ScriptableObject
{
    public int countOfKnives;
    public List<RotationVariation> rotationVariations;
    public List<AppleVariations> appleVariations;
    public List<float> knifeAnglesSpawn = new List<float>();
}

[System.Serializable]
public class RotationVariation
{
    [Range(0f,2f)] public float time=0f;
    [Range(-180,180f)] public float z=0f;
    
    public AnimationCurve curve;
}

[System.Serializable]
public class AppleVariations
{
    [Range(0f,1f)] public float applePosition = 0.5f;
    
    public float appleAngle;
}