using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterGameObj : MonoBehaviour
{
    void Awake()
    {
        //Add this GameObject to gameObjects Dictionary when it is created
        GameManager.instance.gameObjects.Add(gameObject.name, gameObject);
        Debug.Log("Added " + gameObject.name + " to gameObjects Dictionary");
    }

    void OnDestroy()
    {
        //Remove this GameObject from the List when it is about to be destroyed
        GameManager.instance.gameObjects.Remove(gameObject.name);
    }
}
