using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    Transform _target;
    [SerializeField]
    public float _launchSpeed = 1;
    [SerializeField]
    public float _orbitSpeed = 1;
    [SerializeField]
    public int fuel = 5;

    bool _launch = false;

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
        if (!_target) return;
        Quaternion q = Quaternion.AngleAxis(_orbitSpeed, Vector3.forward);
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
            LaunchShip();
        }
    }

    void LaunchShip()
    {
        _launch = false;
        --fuel;
        _prevPlanet = _target;
        _target = null;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(transform.up * _launchSpeed, ForceMode2D.Impulse);
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
