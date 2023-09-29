using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    public void TriggerDialogue ()
    {
      UISingleton.uiSingleton.diaglogueManager.StartDialogue(dialogue);
    }

    public void EndDialogue () {
      UISingleton.uiSingleton.diaglogueManager.sentences.Clear();
    }

}
