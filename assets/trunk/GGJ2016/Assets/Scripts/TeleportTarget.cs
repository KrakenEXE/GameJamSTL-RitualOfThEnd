using UnityEngine;
using System.Collections;

public class TeleportTarget : MonoBehaviour {

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
