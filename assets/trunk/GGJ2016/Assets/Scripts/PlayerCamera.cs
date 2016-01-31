using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

    public GameObject _player = null;
    public const float _minimumY = 0.0f;
    public const float _deadY = -20.0f;

	void Start () {
	}
	
	void Update () {
        var position = _player.transform.position;
        position.z = transform.position.z;
        if (position.y < _minimumY)
        {
            position.y = _minimumY;
        }
        transform.position = position;
    }
}
