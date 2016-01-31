using UnityEngine;
using System.Collections;

public class SwingingVine : MonoBehaviour {

    public int numOfVineSegments = 20;
    public float lengthOfVine = 10;
    LineRenderer vine;
    Vector3[] points;
    float time;
    const float speed = 0.5f;
    const float maxAngle = Mathf.PI / 6;

    // Use this for initialization
    void Start() {
        vine = GetComponent<LineRenderer>();
        vine.SetVertexCount(numOfVineSegments);
        points = new Vector3[numOfVineSegments];
    }

    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;
        int i = 0;
        float segmentLength = lengthOfVine / numOfVineSegments;
        float thetaCollider = Mathf.Sin(time * speed) * (maxAngle * (numOfVineSegments * 0.03f + 0.6f));
        transform.eulerAngles = new Vector3(0, 0, thetaCollider * 180 / Mathf.PI);
        while(i < numOfVineSegments) {
            float theta = Mathf.Sin(time * speed) * (maxAngle * (i * 0.04f + 0.6f)) - (Mathf.PI / 2) - thetaCollider;
            points[i] = segmentLength * new Vector3(i * Mathf.Cos(theta), i * Mathf.Sin(theta), 0);
            i++;
        }
        vine.SetPositions(points);
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}