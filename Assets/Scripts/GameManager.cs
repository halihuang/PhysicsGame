using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // store dict of game objects
    public Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();
    [SerializeField] public int currentLevel = 0;
    public bool stageLoaded = false;
    public bool restartRequested = false;


    // define level data type
    [System.Serializable]
    public class LevelData
    {
        public string SceneName;
        public string SceneDescription;
    }
    // define level data dictionary
    [SerializeField] private LevelData[] levelData;
    
    public void loadStage(int level)
    {
        // unload current level
        if (stageLoaded)
        {
            stageLoaded = false;
            Debug.Log("Unloading level: " + levelData[currentLevel].SceneName);
            SceneManager.UnloadSceneAsync(levelData[currentLevel].SceneName);
        }
        // set active scene
        currentLevel = level;
        if (levelData.Length <= level)
        {
            Debug.Log("Game Complete");
            return;
        }
        Debug.Log("Loading level: " + levelData[level].SceneName);
        SceneManager.LoadScene(levelData[level].SceneName, LoadSceneMode.Additive);
        stageLoaded = true;
    }

    public Scene currentScene() 
    {
        return SceneManager.GetSceneByName(levelData[currentLevel].SceneName);
    }

    // Start is called before the first frame update
    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }  
    }
  

    void Start()
    {
        // load first stage
        loadStage(currentLevel);
    }

    void Update()
    {
        if (restartRequested)
        {
            restartRequested = false;
            loadStage(currentLevel);
        }
    }
}
