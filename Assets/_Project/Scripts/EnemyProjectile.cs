using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private float speed = 7f;
    [SerializeField] private float damage = 8f;
    [SerializeField] private float lifetime = 3f;

    [Header("Obstacles")]
    [SerializeField] private LayerMask obstacleLayer;

    private Vector2 direction;

    public void Initialize(Vector2 targetDirection)
    {
        direction = targetDirection.normalized;

        float angle = Mathf.Atan2(
            direction.y,
            direction.x
        ) * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(0f, 0f, angle);
    }

    private void Update()
    {
        Vector2 currentPosition = transform.position;

        Vector2 nextPosition =
            currentPosition +
            direction * speed * Time.deltaTime;

        RaycastHit2D hit = Physics2D.Linecast(
            currentPosition,
            nextPosition,
            obstacleLayer
        );

        if (hit.collider != null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = nextPosition;

        lifetime -= Time.deltaTime;

        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerHealth playerHealth =
            other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}