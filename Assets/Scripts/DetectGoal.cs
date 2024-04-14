using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGoal : MonoBehaviour
{
    [SerializeField] private string _playerID = "Player1";
    public bool isPlayerAtGoal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerID))
        {
            Debug.Log("Checkpoint Reached");
            isPlayerAtGoal = true;
            // delete player animation
        }
    }
}
