using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachVictory : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameManager _gameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == _playerObject)
        {
            _gameManager.Victory();
        }
    }
        
}
