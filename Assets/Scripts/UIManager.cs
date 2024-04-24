using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject _menu;
    private bool _menuVisible = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }  
        // hide menu
        toggleMenu();
        // add callback for menu input
        var playerInput = GetComponent<PlayerInput>();
        var menuAction = playerInput.actions["Menu"];
        menuAction.performed += ToggleMenu;
        DontDestroyOnLoad(gameObject);      
    }

    void ToggleMenu(InputAction.CallbackContext obj)
    {
      // if current scene is not the main menu, toggle menu
      if (GameManager.instance.currentLevel > -1) {
        toggleMenu();
      }
    }

    public void toggleMenu()
    {
        if (_menuVisible)
        {
          _menu.SetActive(false);
        }
        else
        {
          _menu.SetActive(true);
        }
        _menuVisible = !_menuVisible; 
    }


}
