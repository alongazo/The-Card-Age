using UnityEngine;
using System.Collections;

public class pan : MonoBehaviour {
    public float border = 20;
    public float panSpeed = 5f;
    public float minFieldView = 20f;
    public float maxFieldView = 100f;
    public float sensitivity = 10f;

    private int _ScreenWidth;
    private int _ScreenHeight;

    // Use this for initialization
    void Start () {
        _ScreenWidth = Screen.width;
        _ScreenHeight = Screen.height;
    

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.LeftControl) && Input.mousePosition.y > _ScreenHeight - border) || Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.up * panSpeed * Time.deltaTime);
        }
        if ((Input.GetKey(KeyCode.LeftControl) && Input.mousePosition.y < border) || Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.down * panSpeed * Time.deltaTime);
        }
        if ((Input.GetKey(KeyCode.LeftControl) && Input.mousePosition.x > _ScreenWidth - border) || Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * panSpeed * Time.deltaTime);
        }
        if ((Input.GetKey(KeyCode.LeftControl) && Input.mousePosition.x < border) || Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * panSpeed * Time.deltaTime);
        }

        float fieldView = Camera.main.fieldOfView;
        fieldView -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fieldView = Mathf.Clamp(fieldView, minFieldView, maxFieldView);
        Camera.main.fieldOfView = fieldView;


    }


    //void OnGUI()
    //{
    //    GUI.Box(new Rect((Screen.width / 2) - 140, 5, 280, 25), "Mouse Position = " + Input.mousePosition);
    //    GUI.Box(new Rect((Screen.width / 2) - 70, Screen.height - 30, 140, 25), "Mouse X = " + Input.mousePosition.x);
    //    GUI.Box(new Rect(5, (Screen.height / 2) - 12, 140, 25), "Mouse  = " + Input.mousePosition.y);
    //}
}
