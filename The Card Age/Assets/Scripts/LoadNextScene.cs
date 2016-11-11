using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadNextScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void LoadLevel()
    {
        SceneManager.LoadScene("Shop");
    }
}
