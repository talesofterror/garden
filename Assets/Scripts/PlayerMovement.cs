using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    float _mSpeed;
    float accelerate;
    public float accelerationSpeed = 2f;
    float h;
    float v;
    public float yPlacement;

    public Camera cam;
    Transform playerTransform;
    Vector3 pointerBeacon;
    Vector3 mousePosition;
    Vector3 directionalVector;
    Vector3 targetPosition;
    GameObject debugSphere;
    public GameObject cursorIcon;
    GameObject cursorObject;
    bool dBSphere;
    public GameObject playerObject;
    Rigidbody rB;

    GameObject gland;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Vector3 cursorYOffset = new Vector3(0, 1.2f, 0);
        cursorObject = Instantiate(cursorIcon, pointerBeacon + cursorYOffset, Quaternion.Euler(0, 180, 0));
        cursorObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        // cursorObject.GetComponent<Collider>().enabled = false;
        cursorObject.SetActive(true);
        rB = GetComponent<Rigidbody>();
        _mSpeed = moveSpeed * 1000;
        
    }

private void OnCollisionEnter(Collision collision) {

    if(collision.gameObject.tag == "EnvironmentalBorder"){

        rB.isKinematic = false;
        print("Environmental Collision");

    }

    print(collision.gameObject.tag);
}

private void onTriggerEnter() {
        if(tag == "EnvironmentalBorder"){

            print("Environmental Trigger");

        }
}

void Update(){

    debug();

    }


    // Update is called once per frame
    void FixedUpdate()
{

    rB.velocity = new Vector3(0, 0, 0);
    playerTransform = playerObject.transform;

    PlayerRotation();
    directionalMovement(directionalVector, isRunning());

    cursorObject.transform.position = new Vector3(pointerBeacon.x, pointerBeacon.y, pointerBeacon.z);
    cursorObject.transform.LookAt(cam.transform.position);

}

    private bool isRunning()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return true;
        }
        else { return false; }
    }

    private Vector3 directionalMovement(Vector3 dVector, bool running)
    {

        if (running == true)
        {
            accelerate = accelerationSpeed;
        }
        else { accelerate = 1; }

        h = Input.GetAxis("Horizontal"); // get direction 'side to side' aka 'a' and 'd'
        v = Input.GetAxis("Vertical"); // get direction 'up and down' aka 'w' and 's'

        if (h == 0 && v == 0)
        {
            rB.isKinematic = true;
        }
        else 
        {
            rB.isKinematic = false;
        }

        // print("h = " + h + " | v = " + v);


        dVector = new Vector3(h, 0, v); // assigns direction floats to  a new Vector value

        dVector = Quaternion.Euler(1, playerTransform.eulerAngles.y, 1) * dVector; // factors the object's current y rotation into dVector
        rB.AddForce(dVector * (_mSpeed * accelerate), ForceMode.Force);

        // print("Move target position = " + targetPosition + ".");

        if (playerTransform.position.y != yPlacement)
        {
            playerTransform.position = new Vector3(playerTransform.position.x, yPlacement, playerTransform.position.z);
        }

        playerTransform.position = new Vector3(playerTransform.position.x, yPlacement, playerTransform.position.z);

        // The two statements above can be removed, which will allow the player to traverse in the y direction
        // But that leads to it's own set of problems. I need to account for the current position of the 
        // player. The value of "yPlacement" is currently 0. 


        return dVector;

    }


    void PlayerRotation(){

        mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

        RaycastHit beaconHit;
        Ray ray = cam.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out beaconHit, 1000)){
            pointerBeacon = new Vector3(beaconHit.point.x, playerTransform.position.y, beaconHit.point.z);
            /*pointerBeacon = new Vector3(beaconHit.point.x, beaconHit.point.y, beaconHit.point.z);*/
            
            // beaconHit.point.y will point the player towards the surface hit by the ray
            // but also rotates the player towards the position of the beacon
        }

        playerTransform.LookAt(pointerBeacon);

    }

    private void debug() {

        debugSphere.transform.position = pointerBeacon;
        debugSphere.GetComponent<Collider>().enabled = true;

        if (dBSphere == false){
            debugSphere.SetActive(false);
        }

        if (dBSphere == true){
            debugSphere.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P)){
            dBSphere = !dBSphere;
            print("debug sphere toggle");
        }


        if (Input.GetKey(KeyCode.R)){
            transform.position = new Vector3(0, playerTransform.position.y, 0);
        }
    }

}
