using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class Enemy : MonoBehaviour
{
    public float alertRadius;
    public float amnesiaRadius;
    GameObject target;
    SphereCollider sphCollider;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, alertRadius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, amnesiaRadius);
    }

    bool alerted;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("Enemy alerted!");
            alerted = true;
            target = other.transform.gameObject;
            targetPosition = target.transform.position;
        } else { alerted = false; }
    }

    // Start is called before the first frame update
    void Start()
    {
        sphCollider = GetComponent<SphereCollider>();
    }

    public float speedMultiplier;
    Vector3 targetPosition;
    void Update()
    {
        sphCollider.radius = alertRadius;

        float time = Time.deltaTime * speedMultiplier;
        Vector3 movementFactor = transform.position + transform.forward/100 * time;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);


        if (alerted)
        {
            Vector3 targetLookAtVector = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            targetPosition = target.transform.position;
            transform.position = movementFactor;
            transform.LookAt(targetLookAtVector);
        }
        if (distanceToTarget > amnesiaRadius)
        {
            transform.position = transform.position;
            alerted = false;
        }

    }
}
