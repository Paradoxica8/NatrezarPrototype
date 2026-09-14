using UnityEngine;
using UnityEngine.InputSystem;

public class Blacksmith : MonoBehaviour
{
    [Header("Upgrade")]
    [SerializeField] private int upgradeCost = 10;
    [SerializeField] private float damageMultiplier = 1.1f;

    [Header("Interaction")]
    [SerializeField] private float interactionDistance = 1.5f;

    private PlayerResources playerResources;
    private PlayerAttack playerAttack;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerResources resources =
            other.GetComponent<PlayerResources>();

        if (resources != null)
        {
            playerResources = resources;

            playerAttack =
                other.GetComponent<PlayerAttack>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerResources resources =
            other.GetComponent<PlayerResources>();

        if (resources == playerResources)
        {
            playerResources = null;
            playerAttack = null;
        }
    }

    private void Update()
    {
        if (playerResources == null || playerAttack == null)
            return;

        float distance = Vector2.Distance(
            transform.position,
            playerResources.transform.position
        );

        if (distance > interactionDistance)
            return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            UpgradeDaggers();
        }
    }

    private void UpgradeDaggers()
    {
        if (playerResources.GetDemonicStones() < upgradeCost)
        {
            Debug.Log(
                "Not enough Demonic Stones!"
            );

            return;
        }

        playerResources.SpendDemonicStones(upgradeCost);

        playerAttack.UpgradeDaggers(
            damageMultiplier
        );

        Debug.Log(
            "Blacksmith: Daggers upgraded!"
        );
    }
}