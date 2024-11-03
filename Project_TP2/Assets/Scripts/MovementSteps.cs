using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSteps : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _minX = -5f;
    [SerializeField] private float _maxX = 5f;
    private float _timer;
    private float _initalY;

    private void Start()
    {
        _initalY = transform.position.y;
    }
    void Update()
    {
        _timer += Time.deltaTime * _speed;
        float x_position = Mathf.PingPong(_timer, _maxX - _minX) + _minX;
        transform.position = new Vector3(x_position, _initalY, 0);
    }


}
