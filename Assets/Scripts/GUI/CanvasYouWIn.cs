using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasYouWIn : MonoBehaviour
{
    public void botones ()
    {
        GameManager.instance.SetLifePlayer(50);
        GameManager.instance.SetDiamondsInstanciado(0);
        SceneManager.LoadScene(0);
    }
}
