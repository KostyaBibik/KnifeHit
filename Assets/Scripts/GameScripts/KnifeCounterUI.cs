using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeCounterUI : MonoBehaviour 
{
    [SerializeField] private GameObject knifeIcon;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color unActiveColor;

    private List<GameObject> _knivesList = new List<GameObject>();
    private List<Image> _knivesImages = new List<Image>();
    
    private int _usedKnivesCounter;
    
    public void SetUpCounter(int totalKnife)
    {
        foreach (var item in _knivesList)
        {
            Destroy (item);
        }

        _usedKnivesCounter = 0;
        _knivesList.Clear();
        _knivesImages.Clear();
        
        for (int i = 0; i < totalKnife; i++) 
        {
            GameObject temp = Instantiate(knifeIcon, transform);

            var image = temp.GetComponent<Image>();
            image.color = activeColor;
            
            _knivesList.Add(temp);
            _knivesImages.Add(image);
        }
    }
    
    public void SetUsedKnives()
    {
        _usedKnivesCounter++;
        for (int i = 0; i < _knivesList.Count; i++)
        {
            _knivesImages[i].color = i < _usedKnivesCounter ? unActiveColor : activeColor;
        }
    }
}