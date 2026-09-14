using TMPro;
using UnityEngine;

public class DemonicStoneUI : MonoBehaviour
{
    [SerializeField] private TMP_Text stoneText;

    private PlayerResources playerResources;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerResources = player.GetComponent<PlayerResources>();
        }

        UpdateText();
    }

    private void Update()
    {
        if (playerResources == null)
            return;

        UpdateText();
    }

    private void UpdateText()
    {
        if (playerResources == null)
            return;

        stoneText.text =
            "Demonic Stones: " +
            playerResources.GetDemonicStones();
    }
}