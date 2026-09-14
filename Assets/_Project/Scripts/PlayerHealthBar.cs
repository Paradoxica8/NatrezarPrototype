using UnityEngine;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Transform fill;
    [SerializeField] private TMP_Text healthText;

    private void Update()
    {
        if (playerHealth == null)
            return;

        float currentHealth =
            playerHealth.GetCurrentHealth();

        float maxHealth =
            playerHealth.GetMaxHealth();

        float healthPercent =
            currentHealth / maxHealth;

        // Health Bar
        if (fill != null)
        {
            fill.localScale = new Vector3(
                healthPercent,
                1f,
                1f
            );

            fill.localPosition = new Vector3(
                -(1f - healthPercent) * 0.5f,
                0f,
                0f
            );
        }

        // Health Text
        if (healthText != null)
        {
            healthText.text =
                Mathf.CeilToInt(currentHealth) +
                " / " +
                Mathf.CeilToInt(maxHealth);
        }
    }
}