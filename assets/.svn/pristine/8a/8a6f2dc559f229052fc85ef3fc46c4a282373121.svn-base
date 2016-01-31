using UnityEngine;
using System.Collections;

public class AngelBeam : MonoBehaviour
{

    public Transform _source;
    public Transform _target;
    public LayerMask _raycastMask;
    public LayerMask _playerMask;

    private const float _minWidth = .2f;
    private const float _maxWidth = 1f;
    private const float _lifeTime = 1f;
    private const float _damageTarget = 2.0f;
    private const float _damageFactor = _damageTarget / (0.5f * _lifeTime * (_minWidth + _maxWidth));

    private Vector3 _sourcePosition;
    private Vector3 _targetPosition;
    private float _time;
    private float _width;
    private LineRenderer _beam;

    // Use this for initialization
    void Start()
    {
        _width = _minWidth;
        _time = 0;
        _beam = GetComponent<LineRenderer>();

        _sourcePosition = _source.position;
        _targetPosition = _target.position;
        Vector2 dir = (_targetPosition - _source.position).normalized;
        var hit = Physics2D.Raycast(_source.position, dir, float.MaxValue, _raycastMask);
        if (hit.collider != null)
        {
            _targetPosition = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;
        _time += deltaTime;
        if (_time >= _lifeTime)
        {
            GameObject.DestroyImmediate(gameObject);
            return;
        }

        _width = Mathf.Lerp(_minWidth, _maxWidth, Mathf.Min(1.0f, _time / _lifeTime));

        if (_source != null)
        {
            _sourcePosition = _source.position;
        }

        _beam.SetVertexCount(2);
        _beam.SetPosition(0, _sourcePosition);
        _beam.SetPosition(1, _targetPosition);
        _beam.SetWidth(_width, _width);

        float damage = _width * deltaTime * _damageFactor;
        var circleHit = Physics2D.OverlapCircle(_targetPosition, _width * 0.5f, _playerMask);
        if (circleHit != null)
        {
            var player = circleHit.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
        else
        {
            var lineHit = Physics2D.Linecast(_sourcePosition, _targetPosition, _playerMask);
            if (lineHit.collider != null)
            {
                var player = lineHit.collider.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        if (_source != null && _target != null)
        {
            Gizmos.DrawLine(_source.position, _target.position);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_sourcePosition, _targetPosition);
        Gizmos.DrawSphere(_targetPosition, _width * 0.5f);
    }
}
