using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _rayOffset = 0.25f;
    [SerializeField] private float _rayCastLength = 1f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _moveSpeed = 5f;

    [SerializeField] private float bounceForce = 10f; 
    [SerializeField] private float bounceDuration = 0.5f; 
    [SerializeField] private bool isBouncing = false; 
    [SerializeField] private float originalGravityScale;

    private Rigidbody2D _rb;
    private bool _isGrounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        originalGravityScale = _rb.gravityScale; 
    }

    void Update()
    {
        Vector2 origin = (Vector2)transform.position + new Vector2(0, -_rayOffset * 0.5f);
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _rayCastLength, _layerMask);
        Debug.DrawLine(origin, origin + direction * _rayCastLength, Color.red);

        _isGrounded = hit.collider != null && hit.collider.gameObject != gameObject;

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space) && !isBouncing)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            AudioManager.Instance.PlayJumpSound();
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        if (!isBouncing) 
        {
            _rb.velocity = new Vector2(horizontalInput * _moveSpeed, _rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Trampoline trampoline = collision.GetComponent<Trampoline>();
        if (trampoline != null && !isBouncing)
        {
            StartCoroutine(Bounce());
        }
    }

    private System.Collections.IEnumerator Bounce()
    {
        isBouncing = true;
        _rb.velocity = new Vector2(_rb.velocity.x, bounceForce); 
        _rb.gravityScale = 0; 

        yield return new WaitForSeconds(bounceDuration);

        _rb.gravityScale = originalGravityScale;
        isBouncing = false;
    }
}



