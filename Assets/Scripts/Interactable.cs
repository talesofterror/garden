using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Interactable : MonoBehaviour
{

  private string interactableTag;
  public string interactableName;
  private GameObject playerObject;
  private float radius = 8.75f;
  private float distanceToPlayer;

  public enum InteractionState
  {
    hover,
    engaged,
    notengaged
  }

  public InteractionState interactionState = new InteractionState();

  void Start()
  {
    playerObject = null;
    interactionState = InteractionState.notengaged;
  }

  void Update()
  {
    if (interactionState == InteractionState.hover)
    {
      UISingleton.uiSingleton.infoBarContainer.gameObject.SetActive(true);
      if (interactableTag == "Talker")
      {
        TalkerBehavior();
      }
      if (interactableTag == "Enemy")
      {
        return;
      }
    }

    if (interactionState == InteractionState.notengaged)
    {
      if (UISingleton.uiSingleton.dialogueContainer.gameObject.activeSelf) {
        gameObject.GetComponent<DialogueTrigger>().EndDialogue();
      }
      UISingleton.uiSingleton.infoBarContainer.gameObject.SetActive(false);
      UISingleton.uiSingleton.targetContainer.gameObject.SetActive(false);
      UISingleton.uiSingleton.iconContainer.gameObject.SetActive(false);
      UISingleton.uiSingleton.dialogueContainer.gameObject.SetActive(false);
    }

    if (interactionState == InteractionState.engaged)
    {
      UISingleton.uiSingleton.infoBarContainer.gameObject.SetActive(false);
      UISingleton.uiSingleton.targetContainer.gameObject.SetActive(true);
      UISingleton.uiSingleton.iconContainer.gameObject.SetActive(true);
      UISingleton.uiSingleton.dialogueContainer.gameObject.SetActive(true);
    }
  }

  // Mouse Hover
    void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Cursor"))
    {
      interactionState = InteractionState.hover;
      interactableTag = gameObject.tag;
      PlayerSingleton.playerSingleton.interactionTarget = interactableName;
      playerObject = other.gameObject;
    }
  }

  void OnTriggerExit()
  {
    interactionState = InteractionState.notengaged;
    interactableTag = "";
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, radius);
    Handles.color = Color.red;
    Handles.Label(transform.position, "talk radius");
  }

  void TalkerBehavior()
  {
    distanceToPlayer = Vector3.Distance(playerObject.transform.parent.transform.position, transform.position);

    if (distanceToPlayer > radius)
    {
      UISingleton.uiSingleton.infoBarText.text = "Want to speak to " + interactableName + "? Get closer.";
    }

    if (distanceToPlayer < radius)
    {
      UISingleton.uiSingleton.infoBarText.text = "Click to speak to " + interactableName;
      if (Input.GetMouseButtonDown(0))
      {
        gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        interactionState = InteractionState.engaged;
      }
    }
  }
}
