using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private float flashTimer;
    private float bleedTimer;
    private float bleedTickTimer;

    private float bleedDamage;
    private float bleedTickInterval;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void ApplyBleed(
        float damage,
        float duration,
        float tickInterval

    )
    {

        bleedDamage = damage;
        bleedTimer = duration;
        bleedTickInterval = tickInterval;

        bleedTickTimer = 0f;

        Debug.Log(
            "Bleed applied | Damage: " +
            damage +
            " | Duration: " +
            duration
        );
    }

    private void Update()
    {
        if (bleedTimer <= 0f)
            return;

        bleedTimer -= Time.deltaTime;
        bleedTickTimer -= Time.deltaTime;

        // Визуальный эффект Bleed
        flashTimer -= Time.deltaTime;

        if (flashTimer <= 0f)
        {
            flashTimer = 0.2f;

            if (spriteRenderer != null)
            {
                spriteRenderer.color =
                    spriteRenderer.color == originalColor
                    ? Color.red
                    : originalColor;
            }
        }

        // Урон от Bleed
        if (bleedTickTimer <= 0f)
        {
            bleedTickTimer = bleedTickInterval;

            DealBleedDamage();
        }

        if (bleedTimer <= 0f)
        {
            bleedTimer = 0f;

            if (spriteRenderer != null)
            {
                spriteRenderer.color = originalColor;
            }

            Debug.Log("Bleed ended");
        }
    }

    private void DealBleedDamage()
    {
        EnemyHealth health = GetComponent<EnemyHealth>();

        if (health != null)
        {
            health.TakeStatusDamage(bleedDamage);

            Debug.Log("Bleed damage: " + bleedDamage);
        }
    }
}