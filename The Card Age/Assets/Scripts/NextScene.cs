using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
    public int level;
    IEnumerator DelaySceneLoad(int level)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(level);
    }
    public void next(int level)
    {
        StartCoroutine(DelaySceneLoad(level));
    }
}
