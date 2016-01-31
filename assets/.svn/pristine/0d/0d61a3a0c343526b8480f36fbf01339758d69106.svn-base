using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour {

    public PlayerController _player;
    public GameObject _leftButton;
    public GameObject _rightButton;
    public GameObject _jumpButton;
    public GameObject _attackButton;
    public Slider _healthSlider;
    
    public bool _leftPressed;
    public bool _rightPressed;
    public bool _jumpPressed;
    public bool _attackPressed;
    
    // Use this for initialization
    void Start() {
        _leftPressed = false;
        _rightPressed = false;
        _jumpPressed = false;
        _attackPressed = false;
    }
	
	// Update is called once per frame
	void Update() {
        _healthSlider.value = _player.Health;
    }

    public void SetLeftPressed(bool value)
    {
        _leftPressed = value;
    }

    public void SetRightPressed(bool value)
    {
        _rightPressed = value;
    }

    public void SetJumpPressed(bool value)
    {
        _jumpPressed = value;
    }

    public void SetAttackPressed(bool value)
    {
        _attackPressed = value;
    }
}
