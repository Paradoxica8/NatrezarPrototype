using UnityEngine;

public class EnemyRangedAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float preferredDistance = 5f;
    [SerializeField] private float distanceTolerance = 0.5f;
    [SerializeField] private float detectionRange = 7f;

    [Header("Attack")]
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private float attackDamage = 8f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Obstacles")]
    [SerializeField] private LayerMask obstacleLayer;

    private Transform player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private Rigidbody2D rb;

    private float attackCooldownTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogWarning("EnemyRangedAI: Player not found!");
        }
    }

    private void FixedUpdate()
    {
        if (player == null || rb == null)
            return;

        if (enemyHealth != null && enemyHealth.IsStunned())
            return;

        Move();
    }

    private void Update()
    {
        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (player == null || playerHealth == null)
            return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance <= detectionRange &&
            distance <= attackRange)
        {
            TryAttack();
        }
    }

    private void Move()
    {
        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance > detectionRange)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction =
            ((Vector2)player.position -
             (Vector2)transform.position).normalized;

        if (distance < preferredDistance - distanceTolerance)
        {
            rb.linearVelocity = -direction * moveSpeed;
        }
        else if (distance > preferredDistance + distanceTolerance)
        {
            rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void TryAttack()
    {
        if (attackCooldownTimer > 0f)
            return;

        Vector2 enemyPosition = transform.position;
        Vector2 playerPosition = player.position;

        RaycastHit2D hit = Physics2D.Linecast(
            enemyPosition,
            playerPosition,
            obstacleLayer
        );

        if (hit.collider != null)
        {
            return;
        }

        attackCooldownTimer = attackCooldown;

        Vector2 direction =
            (playerPosition - enemyPosition).normalized;

        GameObject projectile =
            Instantiate(
                projectilePrefab,
                transform.position,
                Quaternion.identity
            );

        EnemyProjectile projectileScript =
            projectile.GetComponent<EnemyProjectile>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(direction);
        }

        Debug.Log(
            "Ranged enemy fired projectile"
        );
    }
}