using UnityEngine;
using System.Collections;

public class HorizontalMovingPlatform : MonoBehaviour {

    public float speed;
    public int amplitude;
    public float originX;
    public float time;

    // Use this for initialization
    void Start() {
        speed = 0.5f;
        amplitude = 2;
        originX = transform.position.x;
    }

    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;
        var position = transform.position;
        position.x = originX + Mathf.Sin(time * speed) * amplitude;
        transform.position = position;
    }
}