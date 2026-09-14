using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 30f;

    [Header("Loot")]
    [SerializeField] private GameObject demonicStonePrefab;

    [Header("Hit Reaction")]
    [SerializeField] private float hitFlashDuration = 0.08f;
    [SerializeField] private float hitStunDuration = 0.1f;

    private float currentHealth;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isStunned;
    private float stunTimer;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(
    float damage,
    Vector2 hitDirection,
    float knockbackForce,
    float stunDuration)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        isStunned = true;
        stunTimer = stunDuration;

        Debug.Log(
            "Enemy HP: " +
            currentHealth +
            "/" +
            maxHealth
        );

        ApplyKnockback(hitDirection, knockbackForce);

        if (spriteRenderer != null)
        {
            StartCoroutine(HitFlash());
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void TakeStatusDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        Debug.Log(
            "Enemy Status Damage: " +
            damage +
            " | HP: " +
            currentHealth +
            "/" +
            maxHealth
        );

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    private void ApplyKnockback(Vector2 direction, float force)
    {
        if (rb == null)
            return;

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(
            direction * force,
            ForceMode2D.Impulse
        );
    }

    private IEnumerator HitFlash()
    {
        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(hitFlashDuration);

        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log("Enemy Died!");

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null)
        {
            collider.enabled = false;
        }

        if (demonicStonePrefab != null)
        {
            Instantiate(
                demonicStonePrefab,
                transform.position,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }

    public bool IsStunned()
    {
        return isStunned;
    }
    private void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;

            if (stunTimer <= 0f)
            {
                stunTimer = 0f;
                isStunned = false;
            }
        }
    }
}

