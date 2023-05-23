using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class CameraMovement : MonoBehaviour
{

    public GameObject targetObject;
    Vector3 targetPosition;
    Vector3 angledVector;

    Vector3 origCamPos;

    float mouseScrollFactor = 3f;
    float mouseScrollFactorClamped;
    float mouseScrollMem;
    public float cameraSwingPosition = 0f;
    public float swingControlOffset;
    public float camSwingMultiplier = 1.78f;

    public float cameraAngle = 0;

    float cam_Radius;
    public float radiusValue;

    Vector3 heightValueVector;
    public float heightValue;

    [Range(1, 50)] public float radiusOffset;
    Vector3 orbitalVector;

    // Start is called before the first frame update
    void Start()
    {
        origCamPos = transform.position;

        targetPosition = targetObject.transform.position;
    }

    void FixedUpdate()
    {

/*        mouseScrollFactor -= Input.mouseScrollDelta.y / 2;
        mouseScrollFactorClamped = Mathf.Clamp(mouseScrollFactor, 1, 2.5f);

        if (mouseScrollFactorClamped != Input.mouseScrollDelta.y / 2)
        {
            mouseScrollMem = mouseScrollFactorClamped;
        }

        camSwingMultiplier = camSwingSpeed / 1000;
        controls();*/

    }

    // Update is called once per frame
    void Update()
    {
        mouseScrollFactor -= Input.mouseScrollDelta.y / 2;
        mouseScrollFactorClamped = Mathf.Clamp(mouseScrollFactor, 1, 2.5f);

        if (mouseScrollFactorClamped != Input.mouseScrollDelta.y / 2)
        {
            mouseScrollMem = mouseScrollFactorClamped;
        }

        controls();
        orbitalRotation_2();

        // print(Time.deltaTime);

    }

    private void controls()
    {

        // radius = (Vector3.Distance(origCamPos, targetPosition) * 0.3f) + mouseScrollFactor * 5;
        cam_Radius = (Vector3.Distance(origCamPos, targetPosition) * 0.3f) + radiusValue;
        float angle = (Mathf.PI * 1.52f - (cameraSwingPosition + swingControlOffset));
        float sine = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        // print("orbitalFact X = " + radius * cos);
        // print("orbitalFact Z = " + radius * sine);
        // print("positionalValue = " + positionValue);
        // print(radius);
        // print("Mousewheel Scroll Factor = " + mouseScrollFactor);

        orbitalVector = new Vector3((cam_Radius * cos), 3 - mouseScrollFactorClamped / 3, (cam_Radius * sine));

        angledVector = new Vector3(0, 0 + mouseScrollFactorClamped * 4, 0) + targetObject.transform.position;

        heightValueVector = new Vector3(0f, heightValue, 0f);
        transform.position = angledVector + orbitalVector * mouseScrollFactorClamped + heightValueVector;

        Vector3 lookAtOffset = new Vector3(0, 0 + 4f / mouseScrollFactorClamped + cameraAngle, 0);
        transform.LookAt(targetObject.transform.position + lookAtOffset);

    }

    private void orbitalRotation_2()
    {
        // this may have fixed the build discrepancy issue with camera panning
        // won't know until i build

        if (Input.GetKey(KeyCode.Q))
        {
            swingControlOffset += camSwingMultiplier * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.E))
        {
            swingControlOffset -= camSwingMultiplier * Time.deltaTime;
        }
    }
}
