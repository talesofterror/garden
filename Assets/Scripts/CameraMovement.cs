using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

[ExecuteInEditMode]

public class CameraMovement : MonoBehaviour
{

    public GameObject targetObject;
    public Material transparentMaterial;
    Material materialMemory;
    Material struckObjectMaterialMemory;

    Vector3 targetPosition;
    Vector3 orbitalVector;
    Vector3 angledVector;

    Vector3 origCamPos;

    Camera cam;
    public float cameraAngle = 0;
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


    float cam_Radius;
    Vector3 heightValueVector;
    float heightValue;
    float radiusValue;

    [Range(1, 50)] public float radiusOffset;

    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
        origCamPos = transform.position;

        targetPosition = targetObject.transform.position;

    }

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAtOffset;

        lookAtOffset = new Vector3(0, cameraAngle, 0);

        mouseScrollFactor += Input.mouseScrollDelta.y * 0.10f;
        mouseScrollFactorClamped = Math.Clamp(mouseScrollFactor, 0, 1);

        if (mouseScrollFactor > mouseScrollFactorClamped)
        {
            mouseScrollFactor = 1;
        }
        else if (mouseScrollFactor < mouseScrollFactorClamped)
        {
            mouseScrollFactor = 0;
        }

        cam_Radius = (Vector3.Distance(origCamPos, targetPosition) * 0.3f) + radiusValue;
        float angle = (Mathf.PI * 1.52f - (cameraSwingPosition + swingControlOffset));
        float sine = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        orbitalVector = new Vector3((cam_Radius * cos), 1, (cam_Radius * sine));
        angledVector = new Vector3(0, 0, 0) + targetObject.transform.position;
        Vector3 heightValueVector = new Vector3(0f, heightValue, 0f);

        controls(orbitalVector, heightValueVector, lookAtOffset);
        orbitalRotation_2();

        obstructionClearanceRaycast(heightValueVector);
    }




    // Use a boolean trigger for raycast hit instead 


    bool rayHit = false;
    private void obstructionClearanceRaycast(Vector3 heightValueVector)
    {
        Vector3 rayTarget = targetObject.transform.position - (targetObject.transform.position + orbitalVector + heightValueVector);
        Ray ray = new Ray(transform.position, rayTarget);
        RaycastHit hit;
        GameObject struckObject;
        GameObject struckObjectMemory;

        if (Physics.Raycast(ray, out hit, Vector3.Distance(transform.position, targetPosition)))
        {
            struckObject = hit.transform.gameObject;
            struckObjectMemory = struckObject;
            struckObjectMaterialMemory = new Material(struckObjectMemory.GetComponent<MeshRenderer>().sharedMaterial);
            rayHit = true;
 // materialToggle(hit, struckObject, struckObjectMaterialMemory);
        } else { rayHit = false; }


        void materialToggle(RaycastHit hit, GameObject struckObjectMemory, Material struckObjectMaterialMemory)
        {
            if (rayHit == true)
            {
                struckObject.GetComponent<MeshRenderer>().material = transparentMaterial;
            }
            else 
            {
                struckObject.GetComponent<MeshRenderer>().material = struckObjectMaterialMemory;
            }
        }

        Debug.DrawRay(transform.position, rayTarget, Color.magenta);
    }

    private Vector3 controls(Vector3 orbitalVector, Vector3 heightValueVector, Vector3 lookAtOffset)
    {

        // radius = (Vector3.Distance(origCamPos, targetPosition) * 0.3f) + mouseScrollFactor * 5;


        transform.position = targetObject.transform.position + orbitalVector + heightValueVector;

        transform.LookAt(targetObject.transform.position + lookAtOffset);

        scrollLerpHeight = Mathf.Lerp(scrollHeightOutValue, scrollHeightInValue, scrollLerpState);
        scrollLerpRadius = Mathf.Lerp(scrollRadiusOutValue, scrollRadiusInValue, scrollLerpState);
        scrollLerpFOV = Mathf.Lerp(scrollFOVOutValue, scrollFOVInValue, scrollLerpState);

        heightValue = scrollLerpHeight;
        radiusValue = scrollLerpRadius;
        cam.fieldOfView = scrollLerpFOV;

        scrollLerpState = mouseScrollFactorClamped;

        return transform.position;

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
