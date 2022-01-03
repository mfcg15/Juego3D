using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasYouLost : MonoBehaviour
{
   
    public void btnTryAgain()
    {
        SceneManager.LoadScene(GameManager.instance.GetNumberScene());
    }

    public void btnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
