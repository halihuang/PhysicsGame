using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 180f;
    [SerializeField] private GameObject _forcePointer;
    [SerializeField] private float _forcePointerWidth = 0.5f;
    [SerializeField] private float _forcePointerHeight = 0.5f;
    [SerializeField] private float _force = 1000f;
    private Rigidbody _rb;
    private BoxCollider _playerCollider;
    private PlayerInput _playerInput;

    // input actions
    private InputAction _forceAction;
    private InputAction _rotateAction;
    
    // 
    private bool _forceCooldown = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _rotateAction = _playerInput.actions["Rotate"];
        _forceAction = _playerInput.actions["Force"];
        // add force action callback
        _forceAction.performed += ApplyForce;
    }

    private void OnDestroy()
    {
        _forceAction.performed -= ApplyForce;
    }

    private void ApplyForce(InputAction.CallbackContext obj)
    {
        if (_forceCooldown)
            return;
        Debug.Log("Force Attempt");
        _forceCooldown = true;
        // get the force direction from the force pointer
        Vector3 forceDirection = _forcePointer.transform.position - transform.position;
        // check what the force pointer is colliding with using the box collider
        Collider[] colliders = Physics.OverlapBox(_forcePointer.transform.position, new Vector3(_forcePointerWidth, _forcePointerWidth,  _forcePointerHeight));
        // apply force to colliding objects
        foreach (var collider in colliders)
        {
          if (collider.gameObject == _forcePointer || collider.gameObject == gameObject)
            continue;
          Rigidbody rb = collider.GetComponent<Rigidbody>();
          if (rb != null) {
            // normalize the force direction
            forceDirection.Normalize();
            Debug.Log("Force Direction: " + forceDirection);
            Debug.Log("Force: " + forceDirection * _force);
            rb.AddForce(forceDirection * _force); // add force to colliding object
            _rb.AddForce(-forceDirection * _force); // add force to self in opposite direction
            AudioManager.instance.PlaySFX(0); // play sfx
            break;
          }
        }
    }

    // fixed update is called every physics update
    private void FixedUpdate()
    {
        // get rotation value from input
        float rotationInput = _rotateAction.ReadValue<float>();
        // rotate the force pointer around the player left and right
        if (rotationInput != 0)
            _forcePointer.transform.RotateAround(transform.position, Vector3.up, rotationInput * _rotationSpeed * Time.deltaTime);
        // reset force cooldown after 0.5 seconds
        if (_forceCooldown)
            StartCoroutine(ResetForceCooldown());
    }    

    private IEnumerator ResetForceCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        _forceCooldown = false;
    }


    // on draw gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_forcePointer.transform.position, new Vector3(_forcePointerWidth, _forcePointerWidth, _forcePointerHeight));
    }
}
