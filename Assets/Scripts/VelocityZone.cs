using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityZone : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Vector3 _direction = Vector3.forward;

    void FixedUpdate()
    {
        // check if the velocity zone is still active
        if (DialogueManager.Instance.conversationActive) {
            return;
        }
        // get all colliders in the velocity zone
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2);
        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // set velocity to speed in the direction of the velocity zone
                rb.velocity = _direction * _speed;
            }
        }
    }
}
