using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour
{
    public static void GoToSomeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
