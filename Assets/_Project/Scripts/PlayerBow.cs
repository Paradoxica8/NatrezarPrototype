using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBow : MonoBehaviour
{
    [Header("Bow")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float arrowDamage = 8f;

    [Header("Arrow")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpawnDistance = 0.5f;

    private float attackCooldownTimer;

    private void Update()
    {
        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            TryShoot();
        }
    }

    private void TryShoot()
    {
        if (attackCooldownTimer > 0f)
            return;

        attackCooldownTimer = attackCooldown;

        Vector2 direction = GetMouseDirection();

        Vector2 spawnPosition =
            (Vector2)transform.position +
            direction * arrowSpawnDistance;

        GameObject arrow = Instantiate(
            arrowPrefab,
            spawnPosition,
            Quaternion.identity
        );

        PlayerArrow arrowScript =
            arrow.GetComponent<PlayerArrow>();

        if (arrowScript != null)
        {
            arrowScript.Initialize(
                direction,
                arrowDamage
            );
        }

        Debug.Log("Bow shot!");
    }

    private Vector2 GetMouseDirection()
    {
        Vector3 mouseScreenPosition =
            Mouse.current.position.ReadValue();

        Vector3 mouseWorldPosition =
            Camera.main.ScreenToWorldPoint(
                mouseScreenPosition
            );

        Vector2 direction =
            mouseWorldPosition - transform.position;

        return direction.normalized;
    }
}