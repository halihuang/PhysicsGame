using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager: MonoBehaviour
{
    private bool stageComplete;
    // player prefabs
    [SerializeField] private GameObject _player1Prefab;
    [SerializeField] private GameObject _player2Prefab;
    private PlayerInput _playerInput;
    private InputAction _restartAction;
    private GameObject _player1;
    private GameObject _player2;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _restartAction = _playerInput.actions["Restart"];
        _restartAction.performed += Restart;
    }

    void Restart(InputAction.CallbackContext obj)
    {
        Debug.Log("Restarting Level");
        GameManager.instance.loadStage(GameManager.instance.currentLevel);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SetActiveScene(GameManager.instance.currentScene());
        // load players
        stageComplete = false;
        // // spawn players  
        var p1Input = PlayerInput.Instantiate(_player1Prefab, controlScheme: "KeyboardLeft", pairWithDevice: Keyboard.current);
        var p2Input = PlayerInput.Instantiate(_player2Prefab, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);
        _player1 = p1Input.gameObject;
        _player2 = p2Input.gameObject;

        // move players to spawn point
        GameObject p1= GameObject.Find("Player1(Clone)");
        GameObject p2 = GameObject.Find("Player2(Clone)");
        
        Transform _p1SpawnPoint = GameManager.instance.gameObjects[Constants.PLAYER1_SPAWN_POINT].transform;
        Transform _p2SpawnPoint = GameManager.instance.gameObjects[Constants.PLAYER2_SPAWN_POINT].transform;
        p1.transform.position = _p1SpawnPoint.position;
        p2.transform.position = _p2SpawnPoint.position;
    }

    void playerAtGoal()
    {
        if (stageComplete) return;
        GameObject _goalPoint1 = GameManager.instance.gameObjects[Constants.GOAL_POINT1];
        GameObject _goalPoint2 = GameManager.instance.gameObjects[Constants.GOAL_POINT2];
        if (_goalPoint1 == null || _goalPoint2 == null) return;
        bool _isPlayer1AtGoal = _goalPoint1.GetComponent<DetectGoal>().isPlayerAtGoal;
        bool _isPlayer2AtGoal = _goalPoint2.GetComponent<DetectGoal>().isPlayerAtGoal;

        // delete players if they are at goal
        if (_isPlayer1AtGoal)
        {
            Destroy(_player1);
        }
        if (_isPlayer2AtGoal)
        {
            Destroy(_player2);
        }
        if (_isPlayer1AtGoal && _isPlayer2AtGoal)
        {
            Debug.Log("Both players at goal");
            stageComplete = true;
            // load next stage
            GameManager.instance.loadStage(GameManager.instance.currentLevel + 1);
        }
    }

    void OnDestroy()
    {
        _restartAction.performed -= Restart;
    }


    // Update is called once per frame
    void Update()
    {
        playerAtGoal(); 
    }
}
