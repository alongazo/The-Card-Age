using UnityEngine;
using System.Collections;

public class MenuCamControl : MonoBehaviour
{
    public Transform currentMount;
    public float speed = 0.1f;
    public float zoom = 1.0f;
    public Camera cameraComp;

    //private Vector3 lastPosition;
    // Use this for initialization
    void Start()
    {
       // lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, currentMount.position, speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speed);
        /*float velocity = Vector3.Magnitude(transform.position - lastPosition);
        if (currentMount.gameObject.name == "SellCameraMount")
        {
            cameraComp.fieldOfView = 60 + velocity * zoom;
        }
        lastPosition = transform.position;*/

    }

    public void setMount(Transform newMount)
    {
        currentMount = newMount;
    }
    
    public void setFieldOfView(float view)
    {
        cameraComp.fieldOfView = view;// Mathf.Lerp(cameraComp.fieldOfView, view, .5f);
    }
}