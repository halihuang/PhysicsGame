using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public bool conversationActive = false;
    private int currentConversationLevel = -1;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void StartConversation(int level) {
      EndExistingConversation();
      if (GameManager.instance.levelData[level].SceneDialogue != null) {
        ConversationManager.Instance.StartConversation(GameManager.instance.levelData[level].SceneDialogue);
        conversationActive = true;
        currentConversationLevel = level;
        ConversationManager.OnConversationEnded += EndConversationCallback;
      }
    }

    private void EndConversationCallback() {
        conversationActive = false;
        ConversationManager.OnConversationEnded -= EndConversationCallback;
    }

    public void EndExistingConversation() {
        if (conversationActive) {
            Debug.Log("Ending conversation");
            ConversationManager.Instance.EndConversation();
        }
    }
}
