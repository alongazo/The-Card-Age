using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    GameObject menu;
    bool paused;
    void change()
    {
        paused = false;
    }
    IEnumerator addDelay()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("done");
        change();
    }
    
	// Use this for initialization
	void Start () {
        paused = false;
        menu = GameObject.Find("PauseMenu");
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
        if (paused)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
        }
        else if (!paused)
        {
            menu.SetActive(false);
            Time.timeScale = 1;
        }
        
	}
    public void Resume()
    {
        StartCoroutine(addDelay());
        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Save()
    {

    }
    public void Load()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
}
