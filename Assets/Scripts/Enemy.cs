using UnityEngine;

[ExecuteInEditMode]

public class Enemy : MonoBehaviour
{
    public float alertRadius; // cyan
    public float amnesiaRadius; // yellow
    GameObject target;
    Vector3 targetPosition;
    SphereCollider sphCollider;

        void Start()
    {
        sphCollider = GetComponent<SphereCollider>();
    }

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
        }
    }

    // Start is called before the first frame update


    public float speedMultiplier;
    
    void Update()
    {
        sphCollider.radius = alertRadius;

        float time = Time.deltaTime * speedMultiplier;
        Vector3 movementFactor = transform.position + transform.forward/100 * time;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);


        if (alerted)
        {
            Vector3 movementTarget = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            targetPosition = movementTarget;
            transform.position = movementFactor;
            transform.LookAt(movementTarget);
        }
        if (distanceToTarget > amnesiaRadius)
        {
            transform.position = transform.position;
            alerted = false;
        }

    }
}
