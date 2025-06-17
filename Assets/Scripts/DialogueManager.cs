using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogueManager : MonoBehaviour
{

  public TextMeshProUGUI nameText;
  public TextMeshProUGUI dialogueText;

  public Queue<string> sentences;



  public DialogueState dialogueState = new DialogueState();

  void Start()
  {
    sentences = new Queue<string>();
  }
  
  void Update()
  {
    if (dialogueState == DialogueState.Prompt)
    {
      if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
      {
        DisplayNextSentence();
      }
    }
  }

  string sentence;
  public void StartDialogue(Dialogue dialogue)
  {
    Debug.Log("Click to talk to " + dialogue.name);
    dialogueState = DialogueState.Prompt;

    nameText.text = dialogue.name;

    sentences.Clear();

    foreach (string sentence in dialogue.sentences)
    {
      sentences.Enqueue(sentence);
    }
    DisplayNextSentence();
  }

  public void DisplayNextSentence()
  {
    dialogueText.text = sentence; 
    sentence = sentences.Dequeue();
    Debug.Log(sentence);

    if (sentences.Count == 0)
    {
      EndDialogue();
      return;
    }
  }

  void EndDialogue()
  {
    Debug.Log("End of dialogue");
    UISingleton.uiSingleton.dialogueContainer.gameObject.SetActive(false);
  }

}

public enum DialogueState
{
  Prompt,
  ClickThru,
  End
}