using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    [SerializeField] public GameObject menu;
    [SerializeField] private GameObject levelButtonPrefab;

    // Start is called before the first frame update
    public void hide()
    {
        Debug.Log("Hiding Level Selector");
        gameObject.SetActive(false);
        menu.SetActive(true);
    }

    public void show()
    {
        gameObject.SetActive(true);
        menu.SetActive(false);
    }

    public void closeGame()
    {
        Debug.Log("Closing Game");
        Application.Quit();
    }

    public void selectLevel(int level)
    {
      // unload current level
      Debug.Log("Loading level: " + level);
      GameManager.instance.unloadStage("Menu");
      GameManager.instance.loadStage(level);
    }

    void Awake()
    {
        hide();
        // add buttons for each level
        for (int i = 0; i < GameManager.instance.levelData.Length; i++)
        {
            int level = i;
            // create LevelSelectButton for each level
            GameObject btn = Instantiate(levelButtonPrefab) as GameObject;
            btn.transform.SetParent(gameObject.transform, false);
            // get button component
            Button buttonComponent = btn.GetComponent<Button>();
            // set button text (TextMeshPro )
            btn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Level " + (i+1);
            // add listener to button
            buttonComponent.onClick.AddListener(() => selectLevel(level));
            // move button to correct position
            RectTransform rt = btn.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, 8 + -i * 2);
        }
    }
}
