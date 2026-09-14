using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private int demonicStones;

    private static int savedDemonicStones;

    private void Awake()
    {
        demonicStones = savedDemonicStones;
    }

    public void AddDemonicStone(int amount)
    {
        if (amount <= 0)
            return;

        demonicStones += amount;
        savedDemonicStones = demonicStones;

        Debug.Log(
            "Demonic Stones: " +
            demonicStones
        );
    }

    public int GetDemonicStones()
    {
        return demonicStones;
    }

    public void SpendDemonicStones(int amount)
    {
        if (amount <= 0)
            return;

        if (demonicStones < amount)
            return;

        demonicStones -= amount;
        savedDemonicStones = demonicStones;

        Debug.Log(
            "Demonic Stones spent: " +
            amount +
            " | Remaining: " +
            demonicStones
        );
    }
    public void LoseAllDemonicStones()
    {
        demonicStones = 0;
        savedDemonicStones = 0;

        Debug.Log("Player lost all Demonic Stones!");
    }
}