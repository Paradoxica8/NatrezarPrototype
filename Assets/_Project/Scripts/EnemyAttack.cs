using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 1.5f;

    [Header("Obstacles")]
    [SerializeField] private LayerMask obstacleLayer;

    private float attackCooldownTimer;

    private void Start()
    {
        Debug.Log(
            "EnemyAttack STARTED on: " +
            gameObject.name
        );
    }

    private void Update()
    {
        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        TryAttack();
    }

    private void TryAttack()
    {
        if (attackCooldownTimer > 0f)
            return;

        Collider2D[] targets = Physics2D.OverlapCircleAll(
            transform.position,
            attackRange
        );

        foreach (Collider2D target in targets)
        {
            if (!target.CompareTag("Player"))
                continue;

            PlayerHealth playerHealth =
                target.GetComponent<PlayerHealth>();

            if (playerHealth == null)
                continue;

            Vector2 enemyPosition = transform.position;
            Vector2 playerPosition = target.transform.position;

            RaycastHit2D wall = Physics2D.Linecast(
                enemyPosition,
                playerPosition,
                obstacleLayer
            );

            if (wall.collider != null)
            {
                continue;
            }

            attackCooldownTimer = attackCooldown;

            playerHealth.TakeDamage(attackDamage);

            Debug.Log(
                "Enemy attack | Damage: " +
                attackDamage
            );

            return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            attackRange
        );
    }
}