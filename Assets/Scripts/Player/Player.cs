using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    Transform _target;
    [SerializeField]
    public float launchSpeed = 1;
    [SerializeField]
    public float orbitSpeed = 1;
    [SerializeField]
    public int fuel = 5;
    [SerializeField]
    float _stopThreshold = 0.125f;
    [SerializeField]
    CinemachineVirtualCamera _playerCamera;
    [SerializeField]
    UnityEvent onPlayerStopped;

    bool _launch;
    bool _stopped;

    Transform _prevPlanet;
    Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        if (_target)
        {
            _prevPlanet = _target;
        }

        Assert.IsNotNull(_playerCamera);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _target)
        {
            _launch = true;
        }
    }

    void FixedUpdate()
    {
        if (!_stopped && !_target && _rb.velocity.magnitude < _stopThreshold)
        {
            _stopped = true;
            _rb.velocity = Vector2.zero;
            onPlayerStopped.Invoke();
        }
        
        if (!_target) return;
        Quaternion q = Quaternion.AngleAxis(orbitSpeed, Vector3.forward);
        Vector3 targetPosition = _target.position;
        Vector3 currentPosition = transform.position;
        //_rb.MoveRotation(_rb.transform.rotation * q);
        float theta = Mathf.Atan2(
            currentPosition.x - targetPosition.x,
             targetPosition.y - currentPosition.y);
        _rb.MoveRotation(Quaternion.AngleAxis(Mathf.Rad2Deg * theta - 90, Vector3.forward));
        _rb.MovePosition(q * (_rb.transform.position - targetPosition) + targetPosition);

        
        if (_launch)
        {
            // _target.GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
            LaunchShip();
        }
    }

    void LateUpdate()
    {
        if (_target && _playerCamera.enabled)
        {
            _playerCamera.enabled = false;
            // _target.GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
        }
        else if (!_playerCamera.enabled && !_target)
        {
            _playerCamera.enabled = true;
        }
    }

    void LaunchShip()
    {
        _launch = false;
        --fuel;
        _prevPlanet = _target;
        _target = null;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(transform.up * launchSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_target) return;
        if (other.CompareTag("Planet") && other.transform != _prevPlanet)
        {
            _target = other.transform;
        }
    }
}
