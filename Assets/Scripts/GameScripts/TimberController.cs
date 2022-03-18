using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimberController : MonoBehaviour
{
    [SerializeField] private GameObject effectorPrefab;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject knifePrefab;
    
    private List<Rigidbody2D> _partsOfTimber = new List<Rigidbody2D>();
    private bool _isLosed;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _timberCollider;
    private GameObject _activeEffector;
    private List<RotationVariation> _currentRotations;
    private List<AppleVariations> _appleVariations;
    private List<float> _knifeAnglesSpawn;
    private int _currentRotationIndex;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _timberCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        var childs = transform.GetComponentsInChildren<Rigidbody2D>(true);
        foreach(var childRb in childs)
        {
            _partsOfTimber.Add(childRb);
        }
    }

    public void SetParameters(List<RotationVariation> rotationVariations, List<AppleVariations> appleVariations, List<float> knifeAngles)
    {
        _currentRotations = rotationVariations;
        _appleVariations = appleVariations;
        _knifeAnglesSpawn = knifeAngles;
        ApplyRotation();
        SpawnApples();
        SpawnKnives();
    }

    private void ApplyRotation()
    {
        _currentRotationIndex = (_currentRotationIndex + 1) % _currentRotations.Count;
        LeanTween.rotateZ(gameObject,
            transform.localRotation.eulerAngles.z + _currentRotations[_currentRotationIndex].z, 
            _currentRotations[_currentRotationIndex].time).setOnComplete(ApplyRotation).setEase(_currentRotations[_currentRotationIndex].curve);
    }

    private void SpawnApples()
    {
        foreach (var apple in _appleVariations)
        {
            GameObject tempApple = Instantiate(applePrefab, transform);
            SetPosInTimber (tempApple.transform, apple.appleAngle, -1.7f, -90f);
        }
    }
    
    private void SpawnKnives()
    {
        foreach (float item in _knifeAnglesSpawn)
        {
            GameObject tempKnife = Instantiate (knifePrefab, transform);
            SetPosInTimber (tempKnife.transform,item, -2.1f, 90f);
            tempKnife.transform.localScale = new Vector3 (1f, 1f, 1f);
        }
    }
    
    private void SetPosInTimber(Transform obj, float angle, float spaceBetweenCircleAndObject, float objAngelOffset)
    {
        angle += 90f;
        Vector2 offset = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad),
            Mathf.Cos(angle * Mathf.Deg2Rad)) * (_timberCollider.radius + spaceBetweenCircleAndObject);
        obj.position = (Vector2)transform.position + offset;
        obj.rotation = Quaternion.Euler (0, 0, -angle + 90f + objAngelOffset);
    }
    
    public void DestroyTimber(bool withEffect)
    {
        if(!_isLosed && withEffect)
        {
            _spriteRenderer.enabled = false;
            _rigidbody.isKinematic = true;
            
            _isLosed = true;
            
            foreach (var partTimber in _partsOfTimber)
            {
                if(partTimber)
                {
                    partTimber.gameObject.SetActive(true);
                    partTimber.bodyType = RigidbodyType2D.Dynamic;
                }
            }
            _partsOfTimber.Clear();

            _activeEffector = Instantiate(effectorPrefab);
        }
        else
        {
            StartCoroutine(nameof(FadeInTimber));
        }

        StartCoroutine(nameof(ClearTimber));
    }

    private IEnumerator FadeInTimber()
    {
        float timeFade = 1f;

        Color startColorTimber = _spriteRenderer.color;
        Color fadeColorTimber = new Color(startColorTimber.r, startColorTimber.g, startColorTimber.b, 0f);
        
        var childSprites = transform.GetComponentsInChildren<SpriteRenderer>();

        List<Color> startChildColors = new List<Color>();
        List<Color> fadeChildColors = new List<Color>();
        
        foreach (var child in childSprites)
        {
            var color = child.color;
            startChildColors.Add(color);
            fadeChildColors.Add(new Color(color.r, color.g, color.b, 0f));
        }
        
        while (timeFade > 0.1f)
        {
            timeFade -= Time.deltaTime * 1.5f;
            _spriteRenderer.color = Color.Lerp(fadeColorTimber, startColorTimber, timeFade);
            
            for (int i = 0; i < childSprites.Length; i++)
            {
                childSprites[i].color = Color.Lerp(fadeChildColors[i], startChildColors[i], timeFade);
            }
            yield return null;
        }
    }
    
    private IEnumerator ClearTimber()
    {
        yield return new WaitForSeconds(1.3f);
        var childs = transform.GetComponentInChildren<Transform>();
        
        foreach (Transform child in childs)
        {
            Destroy(child.gameObject);
        }
        Destroy(_activeEffector);
        Destroy(gameObject);
    }
}
