using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonExit : MonoBehaviour
{
    [Header("Exit")]
    [SerializeField] private float exitDistance = 1.2f;

    private Transform player;

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("DungeonExit: Player not found!");
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance <= exitDistance)
        {
            SceneManager.LoadScene("Natrezar");
        }
    }
}