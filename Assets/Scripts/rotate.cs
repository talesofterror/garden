using UnityEngine;

public class rotate : MonoBehaviour
{
   [SerializeField] public float rcsThrust = 100f;

    public bool invert = false;
    public enum Axis
    {
        X,
        Y,
        Z
    }

    public Axis axis;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        // X Axis

        if (invert == false & axis == Axis.X)
        {
            transform.Rotate(rotationThisFrame, 0, 0);
        }

        if (invert == true & axis == Axis.X)
        {
            transform.Rotate(-rotationThisFrame, 0, 0);
        }

        // Y Axis

        if (invert == false & axis == Axis.Y)
        {
            transform.Rotate(0, rotationThisFrame, 0);
        }

        if (invert == true & axis == Axis.Y)
        {
            transform.Rotate(0, -rotationThisFrame, 0);
        }

        //  Z Axis

        if (invert == false & axis == Axis.Z)
        {
            transform.Rotate(0, 0, rotationThisFrame);
        }

        if (invert == true & axis == Axis.Z)
        {
            transform.Rotate(0, 0, -rotationThisFrame);
        }




    }
}
