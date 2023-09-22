using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Queue<string> sentences;

    public enum DialogueState
    {
        Prompt,
        ClickThru,
        End
    }

    public DialogueState dialogueState = new DialogueState();

    void Start()
    {
        sentences = new Queue<string>();
    }
    void Update()
    {
        if (dialogueState == DialogueState.Prompt) {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                DisplayNextSentence();
                
            }
        }
    }

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

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        Debug.Log(sentence);
    }

    void EndDialogue()
    {
        Debug.Log("End of dialogue");
    }



}
