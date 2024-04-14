using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager: MonoBehaviour
{
    private bool stageComplete;
    // player prefabs
    [SerializeField] private GameObject _player1Prefab;
    [SerializeField] private GameObject _player2Prefab;
    // Start is called before the first frame update
    void Start()
    {
        // load players
        stageComplete = false;
        // // spawn players  
        PlayerInput.Instantiate(_player1Prefab, controlScheme: "KeyboardLeft", pairWithDevice: Keyboard.current);
        PlayerInput.Instantiate(_player2Prefab, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);
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
        if (_isPlayer1AtGoal && _isPlayer2AtGoal)
        {
            Debug.Log("Both players at goal");
            stageComplete = true;
            // load next stage
            GameManager.instance.loadStage(GameManager.instance.currentLevel + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerAtGoal(); 
    }
}
