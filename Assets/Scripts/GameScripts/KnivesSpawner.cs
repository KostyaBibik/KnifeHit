using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace GameScripts
{
    public class KnivesSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject knife;
        [SerializeField] private Transform knifePosSpawn;
        [SerializeField, Range(10f, 30f)] private float forceCast;
        [SerializeField, Range(.1f, .5f)] private float delayCastKnife;
        
        private GameObject _currentKnife;
        private List<GameObject> _knives = new List<GameObject>();
        private bool _isReady = true;
        private float _lastTimeCast;

        [Inject] private SoundManager _soundManager;
        [Inject] private DiContainer _diContainer;
        [Inject] private KnifeCounterUI _knifeCounterUI;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(_currentKnife && _isReady && Time.time - _lastTimeCast > delayCastKnife)
                {
                    _lastTimeCast = Time.time;
                    _soundManager.PlayKnifeThrow();
                    _currentKnife.GetComponent<Rigidbody2D>().AddForce(forceCast * Vector2.up, ForceMode2D.Impulse);
                    _knifeCounterUI.SetUsedKnives();
                    _currentKnife = null;
                }
            }
        }

        public void CreateKnife()
        {
            _currentKnife = _diContainer.InstantiatePrefab(knife, knifePosSpawn.position, Quaternion.identity, null);
            _knives.Add(_currentKnife);
        }

        public void ClearKnives()
        {
            foreach (var knifeObj in _knives)
            {
                Destroy(knifeObj);
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
}
