using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject; 
    [SerializeField] private GameManager _gameManager; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _playerObject) 
        {
            _gameManager.GameOver();
        }
    }
}
