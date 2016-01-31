using UnityEngine;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour {

    public bool _dead = false;
    public float _health = 1;

    // Use this for initialization
    protected virtual void Start () {
        _dead = false;
    }

    // Update is called once per frame
    protected virtual void Update () {
        if (_dead || transform.position.y <= PlayerCamera._deadY)
        {
            Kill();
            GameObject.DestroyImmediate(gameObject);
            return;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "PlayerBomb")
        {
            var grenade = c.gameObject.GetComponent<Grenade>();
            grenade.Explode();
            TakeDamage(1);
        }
    }

    public bool IsDead
    {
        get { return _dead; }
    }

    public float Health
    {
        get { return _health; }
    }

    public void TakeDamage(float value)
    {
        _health -= value;
        if (!_dead)
        {
            if (_health <= 0)
            {
                _dead = true;
                OnDeath();
            }
            else
            {
                OnDamage();
            }
        }
    }

    public void Kill()
    {
        if (!_dead)
        {
            _dead = true;
            _health = 0;
            OnDeath();
        }
    }

    protected virtual void OnDamage()
    {
    }

    protected virtual void OnDeath()
    {
    }
}
