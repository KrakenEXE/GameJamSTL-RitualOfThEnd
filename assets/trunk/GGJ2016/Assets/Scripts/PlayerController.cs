using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class PlayerController : MonoBehaviour
{
    public Transform _feet = null;
    public float _feetRadius = 0.2f;
    public LayerMask _groundMask;
    public LayerMask _enemyMask;
    public PlayerHud _hud;
    public GameObject _bombPrefab;

    private const float _walkSpeed = 11.0f;
    private const float _jumpSpeed = 13.0f;
    private const float _jumpDelayMax = 0.2f;
    private const int _jumpsMax = 2;
    private const float _attackDelayMax = 0.5f;
    private const float _attackAngle = 45.0f;
    private const float _attackForce = 10;
    private const float _healthStart = 5;

    private Collider2D _collider;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private float _jumpDelayCur;
    private int _jumpsLeft;
    private float _attackDelayCur;
    private bool _facingRight;
    private bool _grounded;
    private bool _falling;
    private bool _dead;
    private float _healthCur;
    
    public void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _jumpDelayCur = 0.0f;
        _jumpsLeft = _jumpsMax;
        _attackDelayCur = 0.0f;
        _facingRight = true;
        _grounded = false;
        _falling = true;
        _dead = false;
        _healthCur = _healthStart;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Quit"))
        {
            Quit();
            return;
        }
        else if (Input.GetButtonDown("Restart"))
        {
            Restart();
            return;
        }

        if (transform.position.y <= PlayerCamera._deadY)
        {
            _dead = true;
            _falling = true;
            Restart();
            return;
        }

        if (!_falling)
        {
            float deltaTime = Time.deltaTime;
            bool jump = Input.GetButtonDown("Jump") || _hud._jumpPressed;
            bool attack = Input.GetButton("Attack") || _hud._attackPressed;

            // Jumping
            if (_jumpDelayCur > deltaTime)
            {
                _jumpDelayCur -= deltaTime;
            }
            else if (jump && _jumpsLeft > 0)
            {
                _jumpDelayCur = _jumpDelayMax;
                --_jumpsLeft;
                _grounded = false;
                Debug.DrawRay(transform.position, new Vector2(0.0f, 0.5f), Color.red, 1.0f);

                var velocity = _rigidBody.velocity;
                velocity.y = _jumpSpeed + Mathf.Max(0.0f, velocity.y);
                _rigidBody.velocity = velocity;
            }
            else
            {
                _jumpDelayCur = 0.0f;
            }

            // Attacking
            if (_attackDelayCur > deltaTime)
            {
                _attackDelayCur -= deltaTime;
            }
            else if (attack)
            {
                _attackDelayCur = _attackDelayMax;
                var bombVelocity = new Vector2(
                    _attackForce * Mathf.Cos(_attackAngle * Mathf.Deg2Rad),
                    _attackForce * Mathf.Sin(_attackAngle * Mathf.Deg2Rad)
                );
                if (!_facingRight)
                {
                    bombVelocity.x = -bombVelocity.x;
                }
                var bomb = (GameObject)GameObject.Instantiate(
                    _bombPrefab, 
                    transform.position + 0.1f * new Vector3(bombVelocity.x, bombVelocity.y, 0.0f), 
                    Quaternion.identity
                );
                var bombRigidBody = bomb.GetComponent<Rigidbody2D>();
                bombRigidBody.velocity = bombVelocity;
            }
            else
            {
                _attackDelayCur = 0.0f;
            }
        }
    }

    public void FixedUpdate() {
        float deltaTime = Time.fixedDeltaTime;
        float horizontal = Mathf.Clamp(
            Input.GetAxis("Horizontal") - (_hud._leftPressed ? 1 : 0) + (_hud._rightPressed ? 1 : 0),
            -1, 1
        );
        var velocity = _rigidBody.velocity;

        // Grounded
        CheckGrounded();

        // Walking
        if (!_falling)
        {
            velocity.x = horizontal * _walkSpeed;
            _rigidBody.velocity = velocity;
            if (horizontal > 0 && !_facingRight)
            {
                Flip();
            }
            else if (horizontal < 0 && _facingRight)
            {
                Flip();
            }
        }
        
        // Update Animator
        _animator.SetBool("Grounded", _grounded);
        _animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal));
        _animator.SetFloat("VerticalSpeed", velocity.y);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.right * transform.localScale.x);
        Gizmos.DrawRay(transform.position, transform.up);

        if (_feet != null)
        {
            Gizmos.DrawSphere(_feet.position, _feetRadius);
        }
    }

    public void OnCollisionEnter2D(Collision2D c)
    {
        _falling = true;
        CheckGrounded();

        if (c.gameObject.tag == "Enemy")
        {
            if (Physics2D.OverlapCircle(_feet.position, _feetRadius, _enemyMask))
            {
                var enemy = c.gameObject.GetComponent<EnemyBase>();
                enemy.TakeDamage(1);
            }
            else
            {
                this.TakeDamage(1);
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CheckGrounded()
    {
        _grounded = Physics2D.OverlapCircle(_feet.position, _feetRadius, _groundMask);
        if (_grounded && _rigidBody.velocity.y <= 0)
        {
            _jumpDelayCur = 0.0f;
            _jumpsLeft = _jumpsMax;
            _falling = false;
        }
    }

    public void Flip()
    {
        _facingRight = !_facingRight;
        var localScale = transform.localScale;
        localScale.x *= -1.0f;
        transform.localScale = localScale;
    }

    public bool IsDead
    {
        get { return _dead; }
    }

    public float Health
    {
        get { return _healthCur; }
    }

    public void TakeDamage(float value)
    {
        _healthCur -= value;
        if (_healthCur <= 0)
        {
            _dead = true;
            Restart();
        }
    }
}
