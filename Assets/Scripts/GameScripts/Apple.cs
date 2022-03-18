using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private const string KnifeTag = "Knife";
    
    private ParticleSystem _splatApple;
    private CircleCollider2D _circleCollider;
    private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _splatApple = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag(KnifeTag))
        {
            _circleCollider.enabled = false;
            _spriteRenderer.enabled = false;
            _splatApple.Play();
            Destroy(gameObject, 3f);
        }
    }
}
