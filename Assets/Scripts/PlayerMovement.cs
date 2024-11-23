using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float moveSpeed = 1.5f;
  float _mSpeed;
  float accelerate;
  public float accelerationSpeed = 2f;
  float localX;
  float localZ;
  public bool running;

  public Camera cam;
  Transform playerTransform;
  Vector3 targetGroundVector;
  Vector3 mousePosition;
  public GameObject beaconGameObject;
  Renderer beaconRenderer;
  public GameObject playerGameObject;
  public GameObject playerMeshGameObject;
  Rigidbody rB;
  Vector3 cursorYOffset = new Vector3(0, 2f, 0);

  int layerNumber = 8;
  int layerMask;

  public enum PlayerState
  {
    hoverSelectableTalker,
    teleporting,
    unengaged
  }
  public PlayerState playerState = new PlayerState();

  // Start is called before the first frame update
  void Start()
  {
    playerState = PlayerState.unengaged;
    playerTransform = playerGameObject.transform;

    layerMask = 1 << layerNumber;

    Cursor.visible = false;
    beaconGameObject = Instantiate(beaconGameObject, cursorYOffset, Quaternion.Euler(0, 0, 0));
    beaconGameObject.transform.parent = this.gameObject.transform;
    // beaconGameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    beaconRenderer = beaconGameObject.GetComponent<Renderer>();
    rB = GetComponent<Rigidbody>();

  }

  private void OnCollisionEnter(Collision collision)
  {

    if (collision.gameObject.tag == "EnvironmentalBorder")
    {
      //rB.isKinematic = false;
      print("Environmental Collision");
      // rB.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }


    if (collision.gameObject.tag == "rigidbody toy")
    {
      //rB.isKinematic = false;
      print("Toy Collision");
      // rB.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    // print(collision.gameObject.tag);
  }

  void Update()
  {

    mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

    Ray ray = cam.ScreenPointToRay(mousePosition);

    if (Physics.Raycast(ray, out beaconHit, 1000, layerMask))
    {
      // beaconVector = new Vector3(beaconHit.point.x, playerTransform.position.y, beaconHit.point.z);
      targetGroundVector = new Vector3(beaconHit.point.x, 0, beaconHit.point.z);

      // beaconHit.point.y will point the player towards the surface hit by the ray
      // but also rotates the player towards the position of the beacon
    }

    Vector3 beaconOffsetVector = new Vector3(0, 0 + cursorYOffset.y, 0);
    beaconGameObject.transform.position = targetGroundVector + beaconOffsetVector;
    // beaconGameObject.transform.LookAt(cam.transform.position);

    debug();
    PlayerRotation();
    running = isRunning();

  }


  // Update is called once per frame
  void FixedUpdate()
  {
    _mSpeed = moveSpeed * 1000;
    rB.velocity = new Vector3(0, 0, 0);
    directionalMovement(isRunning());
  }

  public bool isRunning()
  {
    if (Input.GetKey(KeyCode.LeftShift))
    {
      return true;
    }
    else { return false; }
  }

  public Vector3 directionalMovement(bool running)
  {
    localX = Input.GetAxis("Horizontal"); // get direction 'side to side' aka 'a' and 'd'
    localZ = Input.GetAxis("Vertical"); // get direction 'up and down' aka 'w' and 's'

    if (running)
    {
      accelerate = accelerationSpeed;
    }
    else { accelerate = 1; }

    if (localX == 0 && localZ == 0)
    {
      rB.isKinematic = true;
    }
    else
    {
      rB.isKinematic = false;
    }

    // print("h = " + h + " | v = " + v);

    // Disabled local right/left movement because allowing for 
    // it felt confuding
    // Vector3 dVector = new Vector3(localX, 0, localZ); // assigns direction floats to  a new Vector value
    Vector3 dVector = new Vector3(0, 0, localZ); // assigns direction floats to  a new Vector value

    dVector = Quaternion.Euler(1, playerTransform.eulerAngles.y, 1) * dVector; // factors the object's current y rotation into dVector
    rB.AddForce(dVector * (_mSpeed * accelerate), ForceMode.Force);

    // print("Move target position = " + targetPosition + ".");

    /*
            if (playerTransform.position.y != yPlacement)
            {
                playerTransform.position = new Vector3(playerTransform.position.x, yPlacement, playerTransform.position.z);
            }

            playerTransform.position = new Vector3(playerTransform.position.x, yPlacement, playerTransform.position.z);*/

    // The two statements above can be removed, which will allow the player to traverse in the y direction
    // But that leads to it's own set of problems. I need to account for the current position of the 
    // player. The value of "yPlacement" is currently 0. 


    return dVector;

  }

  RaycastHit beaconHit;
  void PlayerRotation()
  {

    playerTransform.LookAt(targetGroundVector);
    // playerMeshGameObject.transform.LookAt(targetGroundVector);

  }

  private void debug()
  {
    if (Input.GetKeyDown("p"))
    {

      // iconOn = iconOn ? false : true;

      print("cursor sphere toggle");
    }

    ReturnHome();

    void ReturnHome()
    {
      if (Input.GetKey(KeyCode.R))
      {
        transform.position = new Vector3(0, playerTransform.position.y, 0);
      }
    }
  }

}
