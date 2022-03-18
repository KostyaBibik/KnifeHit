using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class KnivesManager : MonoBehaviour
{
    [SerializeField] private GameObject knife;
    [SerializeField] private Transform knifePosSpawn;
    [SerializeField, Range(10f, 30f)] private float forceCast;

    private GameObject _currentKnife;
    private KnifeCounterUI _knifeCounterUI;
    private List<GameObject> _knives = new List<GameObject>();
    private bool _isReady = true;
    
    private void Awake()
    {
        _knifeCounterUI = FindObjectOfType<KnifeCounterUI>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(_currentKnife && _isReady)
            {
                _currentKnife.GetComponent<Rigidbody2D>().AddForce(forceCast * Vector2.up, ForceMode2D.Impulse);
                _knifeCounterUI.SetUsedKnives();
                _currentKnife = null;
            }
        }
    }

    public void CreateKnife()
    {
        _currentKnife = Instantiate(knife, knifePosSpawn.position, quaternion.identity);
        _knives.Add(_currentKnife);
    }

    public void ClearKnives()
    {
        foreach (var knife in _knives)
        {
            Destroy(knife);
        }
        _knives.Clear();
    }

    public void StopSpawnKnives()
    {
        _isReady = false;
        _knives.Remove(_currentKnife);
        Destroy(_currentKnife);
    }
}
