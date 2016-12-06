using UnityEngine;
using System.Collections;

public class MenuCamControl : MonoBehaviour
{
    public Transform currentMount;
    public Transform changeMount;
    private Vector3 newCoords;
    public float speed = 0.1f;
    public float zoom = 1.0f;
    public Camera cameraComp;
    public string setting;
    public float counter = 0;
    public float duration = 20f;
    public bool startTimer = false;
    public bool cameraInMotion = false;
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
        //Debug.Log(transform.position);
        //Debug.Log(currentMount.position);
        if (startTimer)
        {
            counter += Time.deltaTime/duration;
        }
        if(counter >=.2)
        {
            transform.position = currentMount.position;
            transform.rotation = currentMount.rotation;
            counter = 0;
            startTimer = false;
        }
        if(transform.position == newCoords)// && transform.rotation == currentMount.rotation)
        {

            
            //count++;
            GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting = setting;
            cameraInMotion = false;
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
        if (!cameraInMotion)
        {
            currentMount = newMount;
            newCoords = currentMount.position;
            startTimer = true;
            cameraInMotion = true;
        }
    }
    public void changeSetting(string changedSetting)
    {
        setting = changedSetting;
    }
    public void setFieldOfView(float view)
    {
        cameraComp.fieldOfView = view;// Mathf.Lerp(cameraComp.fieldOfView, view, .5f);
    }
}