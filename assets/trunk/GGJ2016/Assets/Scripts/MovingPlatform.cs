﻿using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public float speed;
    public int amplitude;
    public float originY;
    public float time;

	// Use this for initialization
	void Start () {
        speed = 0.5f;
        amplitude = 2;
        originY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        var position = transform.position;
        position.y = originY + Mathf.Sin(time * speed) * amplitude;
        transform.position = position;
    }
}
