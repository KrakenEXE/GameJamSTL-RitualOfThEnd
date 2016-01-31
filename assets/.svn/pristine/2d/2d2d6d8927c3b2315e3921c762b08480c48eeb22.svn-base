using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

    private Animator _animator;
    private float _explodeDelay;
    private bool _exploded;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
        _explodeDelay = 3.0f;
    }
	
	// Update is called once per frame
	void Update () {
        float deltaTime = Time.deltaTime;
        _explodeDelay -= deltaTime;
        if (!_exploded && _explodeDelay <= 0)
        {
            _exploded = true;
            _animator.SetTrigger("Explode");
        }
    }

    public void Explode()
    {
        _explodeDelay = 0.0f;
    }

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
