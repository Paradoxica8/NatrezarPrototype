using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stopDistance = 1.2f;
    [SerializeField] private float detectionRange = 5f;

    [Header("Obstacles")]
    [SerializeField] private LayerMask obstacleLayer;

    private Transform player;
    private EnemyHealth enemyHealth;
    private Rigidbody2D rb;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("EnemyAI: Player not found!");
        }
    }

    private void FixedUpdate()
    {
        if (player == null || rb == null)
            return;

        if (enemyHealth != null && enemyHealth.IsStunned())
        {
            return;
        }

        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
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

        if (distance <= stopDistance)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction =
            ((Vector2)player.position -
             (Vector2)transform.position).normalized;

        RaycastHit2D wall = Physics2D.Raycast(
            transform.position,
            direction,
            0.8f,
            obstacleLayer
        );

        if (wall.collider != null)
        {
            Vector2 leftDirection =
                new Vector2(-direction.y, direction.x);

            RaycastHit2D leftWall = Physics2D.Raycast(
                transform.position,
                leftDirection,
                0.8f,
                obstacleLayer
            );

            if (leftWall.collider == null)
            {
                direction = leftDirection;
            }
            else
            {
                Vector2 rightDirection =
                    new Vector2(direction.y, -direction.x);

                direction = rightDirection;
            }
        }

        rb.linearVelocity = direction * moveSpeed;
    }
}