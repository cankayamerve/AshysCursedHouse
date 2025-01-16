using System.Collections;
using UnityEngine;
public class MonsterChase : MonoBehaviour
{
    public Transform player; // Reference to the player (camera)
    public float chaseSpeed = 5f; // Creature's running speed
    public float disappearDistance = 2f; // Distance at which the creature will disappear
    public float respawnInterval = 300f; // Time for the creature to respawn (variable depending on the creature)
    public float spawnDistanceFromPlayer = 10f; // Distance at which the creature will spawn
    public float spawnHeight = 2f; // Height at which the creature will spawn
    private Vector3 spawnPosition; // Creature's starting position
    private bool isChasing = false; // Checks whether the creature is chasing the player

    void Start()
    {
        if (player == null)
        {
            player = Camera.main.transform;
        }

        StartCoroutine(RespawnMonster()); //Start the creature's spawn cycle
    }

    void Update()
    {
        if (isChasing)
        {
            //It moves towards the player, keeps its face facing the camera, and disappears when the distance between the player (camera) and the creature is 2 units.
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += new Vector3(direction.x, direction.y - 0.5f, direction.z) * chaseSpeed * Time.deltaTime;

            transform.LookAt(new Vector3(player.position.x, player.position.y - 1f, player.position.z));

            if (Vector3.Distance(transform.position, player.position) <= disappearDistance)
            {
                Debug.Log("Monster disappears!"); //for test
                isChasing = false;
                StartCoroutine(DisappearAndRespawn());
            }
        }
    }

    IEnumerator RespawnMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnInterval);
            Debug.Log("Respawn occurred!"); //for test

            // The position where the creature will spawn
            spawnPosition = player.position + player.forward * spawnDistanceFromPlayer;
            spawnPosition.y = player.position.y + spawnHeight - 1f; 
            transform.position = spawnPosition;

            isChasing = true; //Activate the creature
            Debug.Log("Monster is chasing the player.."); //for test
        }
    }

    IEnumerator DisappearAndRespawn()
    {
        // Immediately after disappearing, the creature will become invisible. Therefore, it will be moved to a position where it is not visible on the screen.
        transform.position = new Vector3(0, -1000, 0); 
        yield return new WaitForSeconds(0f); 

        yield return new WaitForSeconds(respawnInterval);
        isChasing = true;
        Debug.Log("Monster is respawning and chasing again.."); //for test
    }
}
