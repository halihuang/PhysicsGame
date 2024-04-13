using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Controls : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 180f;
    [SerializeField] private GameObject _forcePointer;
    [SerializeField] private float _forcePointerRadius = 0.5f;
    [SerializeField] private string _playerNumber = "1";
    private PlayerActions _playerActions;
    private Rigidbody _rb;
    private BoxCollider _playerCollider;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Force" + _playerNumber].performed += ApplyForce;
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Disable();
    }

    private void ApplyForce(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
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
            if (rb != null)
            {
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
        float rotationInput = _playerActions.PlayerMap.Rotate1.ReadValue<float>();
        Debug.Log("Rotation Input: " + rotationInput);
        // rotate the force pointer around the player left and right
        if (rotationInput != 0)
            _forcePointer.transform.RotateAround(transform.position, Vector3.forward, rotationInput * _rotationSpeed * Time.deltaTime);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // on draw gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_forcePointer.transform.position, _forcePointerRadius * Vector3.one);
    }
}
