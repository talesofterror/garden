using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    float _mSpeed;
    float accelerate;
    public float accelerationSpeed = 2f;
    float localX;
    float localZ;

    public Camera cam;
    Transform playerTransform;
    Vector3 targetGroundVector;
    Vector3 mousePosition;
    public GameObject beaconObject;
    Renderer beaconRenderer;
    public GameObject playerObject;
    Rigidbody rB;
    Vector3 cursorYOffset = new Vector3(0, 2f, 0);

    int layerNumber = 8;
    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = playerObject.transform;

        layerMask = 1 << layerNumber;

        Cursor.visible = true;
        beaconObject = Instantiate(beaconObject, cursorYOffset, Quaternion.Euler(0, 0, 0));
        beaconObject.transform.parent = this.gameObject.transform;
        beaconObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        beaconRenderer = beaconObject.GetComponent<Renderer>();
        rB = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "EnvironmentalBorder")
        {
            //rB.isKinematic = false;
            print("Environmental Collision");
            rB.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }


        if (collision.gameObject.tag == "rigidbody toy")
        {
            //rB.isKinematic = false;
            print("Toy Collision");
            rB.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        // print(collision.gameObject.tag);
    }
    bool talking;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Talker"))
        {
            talking = true;
            other.GetComponent<DialogueTrigger>().TriggerDialogue();
        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Talker"))
        {
            talking = false;
            other.GetComponent<DialogueTrigger>().TriggerDialogue();
        }

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
        beaconObject.transform.position = targetGroundVector + beaconOffsetVector;
        beaconObject.transform.LookAt(cam.transform.position);

        debug();
        PlayerRotation();

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        _mSpeed = moveSpeed * 1000;
        rB.velocity = new Vector3(0, 0, 0);
        directionalMovement(isRunning());
    }

    private bool isRunning()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return true;
        }
        else { return false; }
    }

    private Vector3 directionalMovement(bool running)
    {
        localX = Input.GetAxis("Horizontal"); // get direction 'side to side' aka 'a' and 'd'
        localZ = Input.GetAxis("Vertical"); // get direction 'up and down' aka 'w' and 's'

        if (running == true)
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


        Vector3 dVector = new Vector3(localX, 0, localZ); // assigns direction floats to  a new Vector value

        dVector = Quaternion.Euler(1, playerTransform.eulerAngles.y, 1) * dVector; // factors the object's current y rotation into dVector
        rB.AddForce(dVector * (_mSpeed * accelerate), ForceMode.Force);

        // print("Move target position = " + targetPosition + ".");

        /*        if (playerTransform.position.y != yPlacement)
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
