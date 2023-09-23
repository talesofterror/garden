using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Interactable : MonoBehaviour
{

  public string selfTag;
  public GameObject playerObject;
  public float radius = 2f;
  public float distanceToPlayer;

  public enum InteractionState
  {
    hover,
    engaged,
    notengaged
  }

  public InteractionState interactionState = new InteractionState();

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, radius);
  }

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
      if (selfTag == "Talker")
      {
        TalkerBehavior();
      }
    }
    if (interactionState == InteractionState.notengaged)
    {
      gameObject.GetComponent<DialogueTrigger>().EndDialogue();
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

  void OnTriggerEnter(Collider other)
  {

    if (other.gameObject.CompareTag("Cursor"))
    {
      interactionState = InteractionState.hover;
      selfTag = gameObject.tag;
      playerObject = other.gameObject;
    }
  }
  void OnTriggerExit(Collider other)
  {
    
    interactionState = InteractionState.notengaged;
    selfTag = "";
  }

  private void TalkerBehavior()
  {
    distanceToPlayer = Vector3.Distance(playerObject.transform.parent.transform.position, transform.position);

    if (distanceToPlayer > radius)
    {
      UISingleton.uiSingleton.infoBarText.text = "Want to speak to [target]? Get closer.";
    }
    if (distanceToPlayer < radius)
    {
      UISingleton.uiSingleton.infoBarText.text = "Click to speak to [target].";
      if (Input.GetMouseButtonDown(0))
      {
        gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        interactionState = InteractionState.engaged;
      }
    }
  }
}
