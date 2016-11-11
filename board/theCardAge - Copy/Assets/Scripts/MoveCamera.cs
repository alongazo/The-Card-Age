using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    public Transform currentMount;
    public Transform changeMount;
    public Transform currentCamera;
    public float speed = 0.1f;
    public float zoom = 1.0f;
    public Camera cameraComp;
    public bool zoomedIn = false;
    //private Vector3 lastPosition;
    // Use this for initialization
    void Start()
    {
        // lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomedIn)
        {
            speed = .05f;
            currentCamera.transform.position = Vector3.Lerp(currentCamera.transform.position, changeMount.position, speed);
            currentCamera.transform.rotation = Quaternion.Slerp(currentCamera.transform.rotation, changeMount.rotation, speed);
        }
        if(currentCamera.transform.position==currentMount.position)
        {
            zoomedIn = false;
        }
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
    public void switchCamera()
    {
        if (!zoomedIn)
            zoomedIn = true;
        else
            zoomedIn = false;
    }
    
}