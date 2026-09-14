using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private LayerMask obstacleLayer;

    private Vector2 direction;
    private float damage;

    public void Initialize(
        Vector2 shootDirection,
        float arrowDamage)
    {
        direction = shootDirection.normalized;
        damage = arrowDamage;

        float angle = Mathf.Atan2(
            direction.y,
            direction.x
        ) * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(0f, 0f, angle);
    }

    private void Update()
    {
        transform.position +=
            (Vector3)(direction * speed * Time.deltaTime);

        lifetime -= Time.deltaTime;

        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & obstacleLayer) != 0)
        {
            Destroy(gameObject);
            return;
        }

        EnemyHealth enemyHealth =
            other.GetComponent<EnemyHealth>();

        if (enemyHealth == null)
            return;

        enemyHealth.TakeDamage(
            damage,
            direction,
            1f,
            0.15f
        );

        Destroy(gameObject);
    }
}