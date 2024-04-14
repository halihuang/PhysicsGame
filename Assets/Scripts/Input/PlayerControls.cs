using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 180f;
    [SerializeField] private GameObject _forcePointer;
    [SerializeField] private float _forcePointerRadius = 0.5f;
    private Rigidbody _rb;
    private BoxCollider _playerCollider;
    private PlayerInput _playerInput;

    // input actions
    private InputAction _forceAction;
    private InputAction _rotateAction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        // print playinput.actions
        foreach (var action in _playerInput.actions)
        {
            Debug.Log(action.name);
        }
        _rotateAction = _playerInput.actions["Rotate"];
        _forceAction = _playerInput.actions["Force"];
        // add force action callback
        _forceAction.performed += ApplyForce;
    }

    private void ApplyForce(InputAction.CallbackContext obj)
    {
        Debug.Log("Force Applied");
        // get the force direction from the force pointer
        Vector3 forceDirection = _forcePointer.transform.position - transform.position;
        // check what the force pointer is colliding with using the box collider
        Collider[] colliders = Physics.OverlapBox(_forcePointer.transform.position, _forcePointerRadius * Vector3.one);
        // apply force to colliding objects
        foreach (var collider in colliders)
        {
          if (collider.gameObject == _forcePointer || collider.gameObject == gameObject)
            continue;
          Rigidbody rb = collider.GetComponent<Rigidbody>();
          if (rb != null) {
            Debug.Log("Force Applied");
            rb.AddForce(forceDirection * 1000); // add force to colliding object
            _rb.AddForce(-forceDirection * 1000); // add force to self in opposite direction
          }
        }
    }

    // fixed update is called every physics update
    private void FixedUpdate()
    {
        // get rotation value from input
        float rotationInput = _rotateAction.ReadValue<float>();
        Debug.Log("Rotation Input: " + rotationInput);
        // rotate the force pointer around the player left and right
        if (rotationInput != 0)
            _forcePointer.transform.RotateAround(transform.position, Vector3.up, rotationInput * _rotationSpeed * Time.deltaTime);
    }    


    // // Start is called before the first frame update
    // void Start()
    // {
         
    // }

    // on draw gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_forcePointer.transform.position, _forcePointerRadius * Vector3.one);
    }
}
