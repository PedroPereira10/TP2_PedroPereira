using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object colliding with the obstacle is the player
        if (collision.GetComponent<PlayerController>() != null)
        {
            // Call the GameOver method from GameManager
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
        }
    }
}

