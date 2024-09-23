using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DialogueEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // store dict of game objects
    public Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();
    [SerializeField] public int currentLevel = -1;
    public bool stageLoaded = false;
    public bool restartRequested = false;
    private BlackLoadCover loadCover;
    private float gravity = 1f;

    // define level data type
    [System.Serializable]
    public class LevelData
    {
        public string SceneName;
        public string SceneDescription;
        public NPCConversation SceneDialogue;
        public AudioClip SceneMusic;
        public bool gravityEnabled = false;
    }
    // define level data dictionary
    [SerializeField] public LevelData[] levelData;
    
    public void loadStage(int level, bool showDialogue = true)
    {
        // unload current level
        if (stageLoaded)
        {
            unloadStage(levelData[currentLevel].SceneName);
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
        loadCover.FadeFromBlack();
        if (showDialogue)
        {
            // start dialogue
            DialogueManager.Instance.StartConversation(level);
        }
        // play music
        if (levelData[level].SceneMusic != null)
        {
            AudioManager.instance.PlayMusic(levelData[level].SceneMusic);
        }
        // set gravity
        Physics.gravity = levelData[level].gravityEnabled ? new Vector3(0, 0, -gravity) : new Vector3(0, 0, 0);
    }


    public void unloadStage(string stage)
    {   
        loadCover.FadeToBlack();
        stageLoaded = false;
        Debug.Log("Unloading level: " + stage);
        SceneManager.UnloadSceneAsync(stage);
    }

    public void loadMainMenu()
    {
        unloadStage(levelData[currentLevel].SceneName);
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        currentLevel = -1;
        loadCover.FadeFromBlack();
        UIManager.instance.toggleMenu();
        DialogueManager.Instance.EndExistingConversation();
        AudioManager.instance.StopMusic();
    }

    public void exitGame()
    {
        Application.Quit();
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

        DontDestroyOnLoad(gameObject);      
        // get load cover script
        loadCover = GameObject.Find("LoadCover").GetComponent<BlackLoadCover>();  
    }
  

    void Start()
    {
        // load menu
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

    void Update()
    {
        if (restartRequested)
        {
            restartRequested = false;
            loadStage(currentLevel, false);
        }
    }
}
