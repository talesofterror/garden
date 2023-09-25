using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
  PlayerSingleton playerSingleton = PlayerSingleton.playerSingleton;

  void Start()
  {

  }

  void Update()
  {
      print(playerSingleton.playerMovement.directionalMovement(playerSingleton.playerMovement));
  }
}
