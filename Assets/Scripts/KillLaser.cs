using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLaser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Player Hit");
            // play death animation
            // restart level
            if (!GameManager.instance.restartRequested)
            {
                GameManager.instance.restartRequested = true;
            }
        }
    }
}
