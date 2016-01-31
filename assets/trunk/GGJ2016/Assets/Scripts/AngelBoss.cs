﻿using UnityEngine;
using System.Collections;


public class AngelBoss : EnemyBase 
{
    public Transform[] _teleportTargets = new Transform[0];
    public GameObject _beamPreFab;
    public GameObject _player;

    private const float _attackDelayMax = 2f;
    private float _attackDelayCur = 0;
   

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        _attackDelayCur = _attackDelayMax;
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (_dead) { return; }

        if (_dead || transform.position.y <= PlayerCamera._deadY)
        {
            Kill();
            GameObject.DestroyImmediate(gameObject);
            return;
        }

        float deltaTime = Time.deltaTime;
        if (_attackDelayCur > deltaTime)
        {
            _attackDelayCur -= deltaTime;
        }
        else
        {
            _attackDelayCur = _attackDelayMax;
            //lightning
            AngelPurifyBeam();
            
            //int attack = Random.Range(0, 100);
            //if (attack > 50)
            //{
            //    //angel super purify beam
            //}
            //else
            //{
            //    //lightning strike
            //}
        }
    }

    protected override void OnCollisionEnter2D(Collision2D c)
    {
        base.OnCollisionEnter2D(c);
    }

    protected override void OnDamage()
    {
        base.OnDamage();
        Teleport();
    }

    public void Teleport()
    {
        int targetPosition = Random.Range(0, _teleportTargets.Length);

        transform.position = _teleportTargets[targetPosition].position;
    }

    public void AngelPurifyBeam()
    {
        var attack = (GameObject)GameObject.Instantiate(_beamPreFab, transform.position, Quaternion.identity);
        var beam = attack.GetComponent<AngelBeam>();
        beam._source = transform;
        beam._target = _player.transform;
    }
}