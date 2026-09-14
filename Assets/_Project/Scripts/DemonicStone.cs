using UnityEngine;

public class DemonicStone : MonoBehaviour
{
    [SerializeField] private int amount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerResources playerResources =
            other.GetComponent<PlayerResources>();

        if (playerResources == null)
            return;

        playerResources.AddDemonicStone(amount);

        Destroy(gameObject);
    }
}