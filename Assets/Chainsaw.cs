using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Chainsaw : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private int _vectorMovement;
    
    [Range(0.01f, 0.1f)] [SerializeField] private float startForce;
    [Range(200, 600)] [SerializeField] private int speedRotation;

    [SerializeField] private float lifetime;
    [SerializeField] private GameObject destroyPrefab;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(SpriteAnimation());
    }

    private void Update()
    {
        transform.Rotate (0,0,speedRotation * Time.deltaTime);
    }

    public void Destroy()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector2.zero;
        StartCoroutine(SpriteAnimation(false));
        var effect = Instantiate(destroyPrefab, transform);
        effect.transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    private Vector2 GetRandomVector()
    {
        return new Vector2(Random.Range(5f, 10f) / 10f, Random.Range(5f, 10f) / 10f);
    }
    
    private IEnumerator SpriteAnimation(bool show = true)
    {
        float timeElapsed = 0;
        var startA = show ? 0 : 1;
        var finishA = show ? 1 : 0;
        var color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, startA);
        _collider.enabled = !show;
        while (timeElapsed < lifetime)
        {
            var a = Mathf.Lerp(startA, finishA, timeElapsed / lifetime);
            color.a = a;
            _spriteRenderer.color = color;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        color.a = finishA;
        _spriteRenderer.color = color;
        _collider.enabled = show;

        if (show) _rigidbody.AddForce(GetRandomVector() * startForce);
        else Destroy(gameObject);
    } 
    
}
