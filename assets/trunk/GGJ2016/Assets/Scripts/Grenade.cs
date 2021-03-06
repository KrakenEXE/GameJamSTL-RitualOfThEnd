﻿using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

    public AudioClip _explodeSound;

    private Animator _animator;
    private AudioSource _audioSource;
    private float _explodeDelay;
    private bool _exploded;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
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
            _audioSource.PlayOneShot(_explodeSound);
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
