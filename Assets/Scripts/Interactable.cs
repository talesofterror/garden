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
  private GameObject cursorTransform;
  private float radius = 8.75f;
  private float distanceToPlayer;

  public InteractionState interactionState = new InteractionState();

  void Start()
  {
    cursorTransform = null;
    interactionState = InteractionState.notengaged;
  }

  void Update()
  {
    if (interactionState == InteractionState.hover)
    {

      UISingleton.uiSingleton.infoBarContainer.gameObject.SetActive(true);
      UISingleton.uiSingleton.infoBarText.text = interactableName;

      if (interactableTag == "Talker")
      {
        TalkerBehavior();
      }
      if (interactableTag == "Enemy")
      {
        // UISingleton.uiSingleton.infoBarContainer.gameObject.SetActive(true);
        // UISingleton.uiSingleton.infoBarText.text = interactableName;
        return;
      }
    }

    if (interactionState == InteractionState.notengaged)
    {
      // if (UISingleton.uiSingleton.dialogueContainer.gameObject.activeInHierarchy) {
      //   gameObject.GetComponent<DialogueTrigger>().EndDialogue();
      // }
      // UISingleton.uiSingleton.infoBarContainer.gameObject.SetActive(false);
      // UISingleton.uiSingleton.targetContainer.gameObject.SetActive(false);
      // UISingleton.uiSingleton.iconContainer.gameObject.SetActive(false);
      // UISingleton.uiSingleton.dialogueContainer.gameObject.SetActive(false);
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
  if (other.CompareTag("Cursor"))
    {
      interactionState = InteractionState.hover;
      interactableTag = gameObject.tag;
      PlayerSingleton.playerSingleton.interactionTarget = interactableName;
      cursorTransform = other.gameObject;
    }
  }

  void OnTriggerExit()
  {
    interactionState = InteractionState.notengaged;
    interactableTag = "";
    UISingleton.uiSingleton.infoBarContainer.gameObject.SetActive(false);
    UISingleton.uiSingleton.targetContainer.gameObject.SetActive(false);
    UISingleton.uiSingleton.iconContainer.gameObject.SetActive(false);
    UISingleton.uiSingleton.dialogueContainer.gameObject.SetActive(false);
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
    distanceToPlayer = Vector3.Distance(cursorTransform.transform.parent.transform.position, transform.position);

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

public enum InteractionState
{
  hover,
  engaged,
  notengaged
}