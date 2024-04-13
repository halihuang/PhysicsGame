using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private float _rotationSpeed = 3.5f;
    private PlayerActions _playerActions;
    private Rigidbody _rb;
    [SerializeField] private GameObject _forcePointer;
    private BoxCollider _boxCollider;
    private BoxCollider _playerCollider;

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _rb = GetComponent<Rigidbody>();
        _playerActions.PlayerMap.Force.performed += ApplyForce;
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
        // check collisionBox of force pointer
        _boxCollider = _forcePointer.GetComponent<BoxCollider>();
        // check what the force pointer is colliding with
        Collider[] colliders = Physics.OverlapBox(_forcePointer.transform.position, _boxCollider.size / 2, _forcePointer.transform.rotation);
        // apply force to colliding objects
        foreach (var collider in colliders)
        {
          if (collider.gameObject == gameObject)
            continue;
          Debug.Log("Force Applied");
          collider.GetComponent<Rigidbody>().AddForce(forceDirection * 1000);
        }
    }

    // fixed update is called every physics update
    private void FixedUpdate()
    {
        // get rotation value from input
        float rotationInput = _playerActions.PlayerMap.Rotate.ReadValue<float>();
        Debug.Log("Rotation Input: " + rotationInput);
        // rotate the force pointer around the player left and right
        if (rotationInput != 0)
            _forcePointer.transform.RotateAround(transform.position, Vector3.forward, rotationInput * _rotationSpeed * Time.deltaTime);
    }    


    // Start is called before the first frame update
    void Start()
    {
         
    }


}
