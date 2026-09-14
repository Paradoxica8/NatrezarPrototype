using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;

    [Header("Death")]
    [SerializeField] private float deathDelay = 1.5f;
    [SerializeField] private string citySceneName = "Natrezar";

    [Header("Hit Reaction")]
    [SerializeField] private float hitFlashDuration = 0.08f;

    private float currentHealth;
    private bool isDead;

    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        currentHealth = maxHealth;

        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        if (playerMovement != null && playerMovement.IsInvulnerable())
        {
            Debug.Log("Damage blocked by Dash!");
            return;
        }

        currentHealth -= damage;

        Debug.Log(
            "Player took damage: " +
            damage +
            " | HP: " +
            currentHealth +
            "/" +
            maxHealth
        );

        if (spriteRenderer != null)
        {
            StartCoroutine(HitFlash());
        }

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log("Player died!");

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        PlayerResources playerResources =
            GetComponent<PlayerResources>();

        if (playerResources != null)
        {
            playerResources.LoseAllDemonicStones();
        }

        StartCoroutine(ReturnToCity());
    }

    private IEnumerator ReturnToCity()
    {
        yield return new WaitForSeconds(deathDelay);

        SceneManager.LoadScene(citySceneName);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    private IEnumerator HitFlash()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(hitFlashDuration);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    public void Heal(float amount)
    {
        if (isDead)
            return;

        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log(
            "Player healed: " +
            amount +
            " | HP: " +
            currentHealth +
            "/" +
            maxHealth
        );
    }
}