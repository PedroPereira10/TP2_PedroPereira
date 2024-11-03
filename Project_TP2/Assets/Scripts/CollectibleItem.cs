using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private int _points = 10; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with: " + collision.name);

        if (collision.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player detected");
            GameManager gameManager = FindObjectOfType<GameManager>(); 
            if (gameManager != null)
            {
                gameManager.AddPoints(_points); 
                gameManager.UpdateItemValue(_points); 

                Destroy(gameObject); 
            }
            else
            {
                Debug.LogError("GameManager not found in the scene!");
            }
        }
    }
}
