using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Upgrade")]
    [SerializeField] private float daggerDamageMultiplier = 1f;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 0.3f;

    [Header("Combo")]
    [SerializeField] private float comboResetTime = 0.7f;

    [SerializeField] private float firstAttackDamage = 6f;
    [SerializeField] private float secondAttackDamage = 8f;
    [SerializeField] private float thirdAttackDamage = 14f;

    [SerializeField] private float firstAttackRange = 1.15f;
    [SerializeField] private float secondAttackRange = 1.30f;
    [SerializeField] private float thirdAttackRange = 1.45f;

    [SerializeField] private float firstAttackWidth = 0.75f;
    [SerializeField] private float secondAttackWidth = 1.0f;
    [SerializeField] private float thirdAttackWidth = 1.25f;

    [SerializeField] private float firstKnockback = 1.0f;
    [SerializeField] private float secondKnockback = 1.5f;
    [SerializeField] private float thirdKnockback = 2.5f;

    [Header("Stun")]
    [SerializeField] private float firstStunDuration = 0.15f;
    [SerializeField] private float secondStunDuration = 0.20f;
    [SerializeField] private float thirdStunDuration = 0.35f;
    
    [Header("Visual")]
    [SerializeField] private GameObject attackVisual;

    [SerializeField] private float firstVisualScale = 0.8f;
    [SerializeField] private float secondVisualScale = 1.0f;
    [SerializeField] private float thirdVisualScale = 1.35f;

    [SerializeField] private float firstVisualDuration = 0.08f;
    [SerializeField] private float secondVisualDuration = 0.12f;
    [SerializeField] private float thirdVisualDuration = 0.18f;

    [Header("Targeting")]
    [SerializeField] private LayerMask enemyLayer;

    [Header("Bleed")]
    [SerializeField] private float bleedDamage = 2f;
    [SerializeField] private float bleedDuration = 3f;
    [SerializeField] private float bleedTickInterval = 0.5f;

    [Header("Obstacles")]
    [SerializeField] private LayerMask obstacleLayer;

    private float attackCooldownTimer;
    private float comboTimer;

    private int comboStep;

    private void Update()
    {
        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (comboTimer > 0f)
        {
            comboTimer -= Time.deltaTime;

            if (comboTimer <= 0f)
            {
                comboStep = 0;
            }
        }

        if (Mouse.current.leftButton.isPressed)
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (attackCooldownTimer > 0f)
            return;

        comboStep++;

        if (comboStep > 3)
        {
            comboStep = 1;
        }

        attackCooldownTimer = attackCooldown;
        comboTimer = comboResetTime;

        Vector2 attackDirection = GetMouseDirection();

        ShowAttackVisual(attackDirection);

        float damage = GetCurrentAttackDamage() * daggerDamageMultiplier;
        float range = GetCurrentAttackRange();
        float width = GetCurrentAttackWidth();
        float knockback = GetCurrentKnockback();
        float stunDuration = GetCurrentStunDuration();



        PerformAttack(
            attackDirection,
            damage,
            range,
            width,
            knockback,
            stunDuration
        );

        Debug.Log(
            "Combo Attack " + comboStep +
            " | Damage: " + damage +
            " | Range: " + range +
            " | Width: " + width +
            " | Knockback: " + knockback
        );

        if (comboStep == 3)
        {
            comboStep = 0;
        }
    }

    private float GetCurrentAttackDamage()
    {
        switch (comboStep)
        {
            case 1:
                return firstAttackDamage;

            case 2:
                return secondAttackDamage;

            case 3:
                return thirdAttackDamage;

            default:
                return firstAttackDamage;
        }
    }

    private float GetCurrentAttackRange()
    {
        switch (comboStep)
        {
            case 1:
                return firstAttackRange;

            case 2:
                return secondAttackRange;

            case 3:
                return thirdAttackRange;

            default:
                return firstAttackRange;
        }
    }

    private float GetCurrentAttackWidth()
    {
        switch (comboStep)
        {
            case 1:
                return firstAttackWidth;

            case 2:
                return secondAttackWidth;

            case 3:
                return thirdAttackWidth;

            default:
                return firstAttackWidth;
        }
    }

    private float GetCurrentKnockback()
    {
        switch (comboStep)
        {
            case 1:
                return firstKnockback;

            case 2:
                return secondKnockback;

            case 3:
                return thirdKnockback;

            default:
                return firstKnockback;
        }
    }

    private float GetCurrentStunDuration()
    {
        switch (comboStep)
        {
            case 1:
                return firstStunDuration;

            case 2:
                return secondStunDuration;

            case 3:
                return thirdStunDuration;

            default:
                return firstStunDuration;
        }
    }
    private void PerformAttack(
        Vector2 attackDirection,
        float damage,
        float range,
        float width,
        float knockback,
        float stunDuration
    )
    {
        Vector2 attackCenter =
            (Vector2)transform.position +
            attackDirection * range;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackCenter,
            width,
            enemyLayer
        );

        foreach (Collider2D enemy in enemies)


        {
            Vector2 playerPosition = transform.position;
            Vector2 enemyPosition = enemy.transform.position;

            RaycastHit2D hit = Physics2D.Linecast(
                playerPosition,
                enemyPosition,
                obstacleLayer
            );

            if (hit.collider != null)
            {
                continue;
            }

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();

            if (health != null)
            {
                health.TakeDamage(
                    damage,
                    attackDirection,
                    knockback,
                    stunDuration
                );

                StatusEffect statusEffect =
                    enemy.GetComponent<StatusEffect>();

                if (statusEffect != null)
                {
                    statusEffect.ApplyBleed(
                        bleedDamage,
                        bleedDuration,
                        bleedTickInterval
                    );
                }
            }
        }
    }

    private Vector2 GetMouseDirection()
    {
        Vector3 mouseScreenPosition =
            Mouse.current.position.ReadValue();

        Vector3 mouseWorldPosition =
            Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 direction =
            mouseWorldPosition - transform.position;

        return direction.normalized;
    }

    private void ShowAttackVisual(Vector2 direction)
    {
        attackVisual.SetActive(true);

        attackVisual.transform.localPosition =
            direction * 0.1f;

        float angle = Mathf.Atan2(
            direction.y,
            direction.x
        ) * Mathf.Rad2Deg;

        attackVisual.transform.localRotation =
            Quaternion.Euler(0f, 0f, angle - 90f);

        float scale = GetCurrentVisualScale();
        float duration = GetCurrentVisualDuration();

        attackVisual.transform.localScale =
            new Vector3(
                0.2f,
                1.2f * scale,
                1f
            );

        StartCoroutine(HideAttackVisual(duration));
    }
    private float GetCurrentVisualScale()
    {
        switch (comboStep)
        {
            case 1:
                return firstVisualScale;

            case 2:
                return secondVisualScale;

            case 3:
                return thirdVisualScale;

            default:
                return firstVisualScale;
        }
    }

    private float GetCurrentVisualDuration()
    {
        switch (comboStep)
        {
            case 1:
                return firstVisualDuration;

            case 2:
                return secondVisualDuration;

            case 3:
                return thirdVisualDuration;

            default:
                return firstVisualDuration;
        }
    }

    public void UpgradeDaggers(float multiplier)
    {
        daggerDamageMultiplier *= multiplier;

        Debug.Log(
            "Daggers upgraded! Damage multiplier: " +
            daggerDamageMultiplier
        );
    }

    private IEnumerator HideAttackVisual(float duration)
    {
        yield return new WaitForSeconds(duration);

        attackVisual.SetActive(false);
    }}