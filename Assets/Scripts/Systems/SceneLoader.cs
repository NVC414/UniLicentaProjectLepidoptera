using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void ReloadActive()
    {
        var s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.name);
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        Debug.Log("Quit requested (ignored in editor).");
#else
        Application.Quit();
#endif
    }
}