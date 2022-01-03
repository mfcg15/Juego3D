using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasLevelSelection : MonoBehaviour
{
    
    public void selectScene (int numberScene)
    {
        GameManager.instance.SetNumberScene(numberScene);
        SceneManager.LoadScene(numberScene);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
