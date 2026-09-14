using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEntrance : MonoBehaviour
{
    [Header("Entrance")]
    [SerializeField] private float entranceDistance = 1.2f;

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
            Debug.LogWarning("DungeonEntrance: Player not found!");
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

        if (distance <= entranceDistance)
        {
            SceneManager.LoadScene("Dungeon");
        }
    }
}