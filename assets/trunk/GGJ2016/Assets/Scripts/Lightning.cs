using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {
    
    public Transform _target;
    public AudioClip _sound;
    public float _soundVolume = 0.5f;

    private const float _lifeTime = 1f;
    private Vector3 _targetPosition;
    private float _time;
    private Animator _animator;
    private AudioSource _audioSource;

	// Use this for initialization
	void Start () {
        _targetPosition = _target.position;
        _time = 0;
        _animator = GetComponent<Animator>();

        
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.Play();
        _audioSource.PlayOneShot(_sound, _soundVolume);
    }
	
	// Update is called once per frame
	void Update () {
        _time += Time.deltaTime;

        if (_time >= _lifeTime)
        {
            GameObject.DestroyImmediate(gameObject);
        }
    }
}
