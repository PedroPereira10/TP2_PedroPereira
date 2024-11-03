using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player; 
    [SerializeField] private float _constantSpeed = 2f; 
    [SerializeField] private float _playerSpeedThreshold = 3f; 
    [SerializeField] private float _followSpeed = 5f; 

    private float _targetY;
    private bool _playerjumped = false;

    void Start()
    {
        _targetY = transform.position.y;
    }

    void Update()
    {
        if (!_playerjumped && _player != null && _player.GetComponent<Rigidbody2D>() != null)
        {
            if (_player.GetComponent<Rigidbody2D>().velocity.y > 0.1f)
            {
                _playerjumped = true;
            }
        }

        if (_playerjumped)
        {
            _targetY += _constantSpeed * Time.deltaTime;

            Rigidbody2D rb = _player.GetComponent<Rigidbody2D>();
            if (rb.velocity.y > _playerSpeedThreshold)
            {
                _targetY = Mathf.Max(_targetY, _player.position.y);
            }

            float newY = Mathf.Lerp(transform.position.y, _targetY, _followSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
