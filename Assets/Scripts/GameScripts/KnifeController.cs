using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScripts
{
    public class KnifeController : MonoBehaviour
    {
        private const string TimberTag = "Timber";
        private const string KnifeTag = "Knife";
        private const string AppleTag = "Apple";

        [SerializeField] private GameObject woodHitVFX; 
        [SerializeField] private GameObject knifeHitVFX; 
        
        private GameController _gameController;
        private Rigidbody2D _rigidbody;
        private BoxCollider2D _boxCollider;
        private TrailRenderer _trailRenderer;
    
        private void Awake()
        {
            _gameController = FindObjectOfType<GameController>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.transform.CompareTag(TimberTag))
            {
                SoundManager.instance.PlayTimberHit();
                Vibration.Vibrate(100);

                var woodHitVfx = Instantiate(woodHitVFX, transform.position, Quaternion.identity);
                _boxCollider.isTrigger = true;
                transform.parent = other.transform;
                _gameController.CorrectHit();
                if (_trailRenderer)
                {
                    Destroy(_trailRenderer.gameObject);
                }
                Destroy(woodHitVfx, 1f);
                Destroy(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.transform.CompareTag(AppleTag))
            {
                Vibration.Vibrate(100);
                _gameController.HitOnApple();
                SoundManager.instance.PlayAppleHit();
            }
            else if(other.transform.CompareTag(KnifeTag))
            {
                var knifeHitVfx = Instantiate(knifeHitVFX, transform.position, Quaternion.identity);
                SoundManager.instance.PlayKnifeHit();
                _gameController.LoseHit();
                _rigidbody.AddForce(new Vector2 (Random.Range (-10f, 10f), -30f), ForceMode2D.Impulse);
                Destroy(knifeHitVfx, 3f);
                Destroy(gameObject, 3f);
            }

            if (_trailRenderer)
            {
                Destroy(_trailRenderer.gameObject);
            }
        }
    }
}
