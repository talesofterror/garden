using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
  public static PlayerSingleton playerSingleton {get; private set;}
  public PlayerMovement playerMovement;
  public string interactionTarget;

  void Awake () {
      if (playerSingleton != null && playerSingleton != this) {
          Destroy(this);
      } else {
          playerSingleton = this;
      }
  }

}
