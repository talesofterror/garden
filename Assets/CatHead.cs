using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHead : MonoBehaviour
{
    public GameObject target;
    Transform targetTransform;
    public float offset;
    Transform lookAtTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetTransform = target.transform;
        transform.LookAt(targetTransform);

    }
}
