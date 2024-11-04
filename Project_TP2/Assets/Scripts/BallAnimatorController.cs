using UnityEngine;

public class BallAnimatorController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _speedThreshold = 0.1f; 
    private string _rollParam = "isRolling"; 
    private string _jumpParam = "IsJumping"; 
    private string _idleParam = "IsStopped";    

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool isRolling = Mathf.Abs(_rb.velocity.x) > _speedThreshold;
        _animator.SetBool(_rollParam, isRolling);

        bool IsJumping = _rb.velocity.y > _speedThreshold;
        _animator.SetBool(_jumpParam, IsJumping);

        bool IsStopped = !isRolling && !IsJumping;
        _animator.SetBool(_idleParam, IsStopped);
    }
}

