using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _cameraOffset;
    [SerializeField] private BoxCollider _cameraBounds;
    [SerializeField] private float _cameraHeightMult = 1f;



    void Awake()
    {
        _camera = GetComponent<Camera>();
        Vector3 center = _cameraBounds.center;
        // set y enough to see the whole bounds
        float boundLength = Mathf.Max(_cameraBounds.size.x, _cameraBounds.size.z);
        float offset = boundLength / (2 * Mathf.Tan(_camera.fieldOfView * 0.5f * Mathf.Deg2Rad));
        transform.position = new Vector3(center.x, offset * _cameraHeightMult, center.z);
    }

}
