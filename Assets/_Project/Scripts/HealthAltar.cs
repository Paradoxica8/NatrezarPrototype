using UnityEngine;
using UnityEngine.InputSystem;

public class HealthAltar : MonoBehaviour
{
    [SerializeField] private float healAmount = 100f;

    private PlayerHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            playerHealth = health;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health == playerHealth)
        {
            playerHealth = null;
        }
    }

    private void Update()
    {
        if (playerHealth == null)
            return;

        float distance = Vector2.Distance(
            transform.position,
            playerHealth.transform.position
        );

        if (distance > 1.5f)
            return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            playerHealth.Heal(healAmount);
        }
    }
}