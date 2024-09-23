using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public float speed = 5.0f;
    public float distance = 10.0f;
    private float originalXPos;

    void Awake()

    {
      originalXPos = transform.position.x;

    }

    void FixedUpdate()
    {
      float newXPos = Mathf.PingPong(Time.time * speed, distance) + originalXPos;
      transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);
      //  drop cubes by instantiating them , choose a random float value to drop cubes between 1 and 3 seconds
      float randomTime = Random.Range(1, 3);
      if (Time.time % randomTime == 0)
      {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(newXPos, transform.position.y, transform.position.z-2);
        cube.AddComponent<Rigidbody>();
        // set gravity
        cube.GetComponent<Rigidbody>().useGravity = true;
      }
    }

}
