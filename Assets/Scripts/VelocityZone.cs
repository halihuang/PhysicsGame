using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityZone : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Vector3 _direction = Vector3.forward;
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        // set velocity to speed in the direction of the velocity zone
        rb.velocity = _direction * _speed;
    }
}
