using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasMain : MonoBehaviour
{

    public void btnPlay ()
   {
        SceneManager.LoadScene(4);
   }

    public void btnLevels ()
    {
        SceneManager.LoadScene(1);
    }

    public void btnExit ()
    {
        Application.Quit();
    }
}
