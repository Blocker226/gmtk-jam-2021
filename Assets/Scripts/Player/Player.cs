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
    int _fuel = 5;

    Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _target)
        {
            LaunchShip();
        }
    }

    void FixedUpdate()
    {
        if (!_target) return;
        Quaternion q = Quaternion.AngleAxis(_orbitSpeed, Vector3.forward);
        Vector3 position = _target.position;
        _rb.MovePosition(q * (_rb.transform.position - position) + position);
        _rb.MoveRotation(_rb.transform.rotation * q);
    }

    void LaunchShip()
    {
        --_fuel;
        _target = null;
        _rb.AddRelativeForce(Vector2.up * _launchSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_target) return;
        if (other.CompareTag("Planet") && !_target)
        {
            _target = other.transform;
        }
    }
}
