using System;
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

    Camera cam;

    float mouseScrollFactor = 0;
    float mouseScrollFactorClamped;
    float scrollLerpRadius;
    float scrollLerpHeight;
    float scrollLerpFOV;
    public float scrollLerpState = 0;
    public float scrollHeightOutValue = 7.37f;
    public float scrollHeightInValue = 5.1f;
    public float scrollRadiusOutValue = 2.94f;
    public float scrollRadiusInValue = 19.4f;
    public float scrollFOVOutValue = 43;
    public float scrollFOVInValue = 22;
    public float cameraSwingPosition = 0f;
    public float swingControlOffset;
    public float camSwingMultiplier = 1.78f;

    public float cameraAngle = 0;

    float cam_Radius;
    Vector3 heightValueVector;
    float heightValue;
    float radiusValue;

    [Range(1, 50)] public float radiusOffset;
    Vector3 orbitalVector;

    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
        origCamPos = transform.position;

        targetPosition = targetObject.transform.position;
    }

    void FixedUpdate()
    {
        RaycastHit hit; 

    }

    // Update is called once per frame
    void Update()
    {
        mouseScrollFactor += Input.mouseScrollDelta.y * 0.10f;
        mouseScrollFactorClamped = Math.Clamp(mouseScrollFactor, 0, 1);

        if (mouseScrollFactor > mouseScrollFactorClamped)
        {
            mouseScrollFactor = 1;
        } else if (mouseScrollFactor < mouseScrollFactorClamped) { 
            mouseScrollFactor = 0;
        }

        controls();
        orbitalRotation_2();
    }

    private void controls()
    {

        // radius = (Vector3.Distance(origCamPos, targetPosition) * 0.3f) + mouseScrollFactor * 5;
        cam_Radius = (Vector3.Distance(origCamPos, targetPosition) * 0.3f) + radiusValue;
        float angle = (Mathf.PI * 1.52f - (cameraSwingPosition + swingControlOffset));
        float sine = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        orbitalVector = new Vector3((cam_Radius * cos), 1, (cam_Radius * sine));

        angledVector = new Vector3(0, 0, 0) + targetObject.transform.position;

        heightValueVector = new Vector3(0f, heightValue, 0f);
        transform.position = targetObject.transform.position + orbitalVector + heightValueVector;

        Vector3 lookAtOffset = new Vector3(0, cameraAngle, 0);
        transform.LookAt(targetObject.transform.position + lookAtOffset);

        scrollLerpHeight = Mathf.Lerp(scrollHeightOutValue, scrollHeightInValue, scrollLerpState);
        scrollLerpRadius = Mathf.Lerp(scrollRadiusOutValue, scrollRadiusInValue, scrollLerpState);
        scrollLerpFOV = Mathf.Lerp(scrollFOVOutValue, scrollFOVInValue, scrollLerpState);

        heightValue = scrollLerpHeight;
        radiusValue = scrollLerpRadius;
        cam.fieldOfView = scrollLerpFOV;

        scrollLerpState = mouseScrollFactorClamped;

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
