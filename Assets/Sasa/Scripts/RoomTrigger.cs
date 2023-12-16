using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    private List<enemyScript> enemiesInRoom = new List<enemyScript>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the room");
            // Player entered the room, start following for each enemy
            foreach (enemyScript enemy in enemiesInRoom)
            {
                enemy.isPlayerInRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the room");
            // Player exited the room, stop following for each enemy
            foreach (enemyScript enemy in enemiesInRoom)
            {
                enemy.isPlayerInRange = false;
            }
        }
    }

    // Register an enemy with this room
    public void RegisterEnemy(enemyScript enemy)
    {
        if (!enemiesInRoom.Contains(enemy))
        {
            enemiesInRoom.Add(enemy);
            Debug.Log("Enemy registered with the room.");
        }
    }

    // Unregister an enemy from this room
    public void UnregisterEnemy(enemyScript enemy)
    {
        enemiesInRoom.Remove(enemy);
        Debug.Log("Enemy removed from a room.");
    }
}
