using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasPassLevel : MonoBehaviour
{
    [SerializeField] Text txtLife;
    [SerializeField] Text txtDiamonds;

    private int numberScene;

    void Start()
    {
        txtLife.text = "Life : "+GameManager.instance.GetLifePlayerInstanciado();
        txtDiamonds.text = "Diamonds : "+ GameManager.instance.GetDiamondsInstanciado();
        numberScene = GameManager.instance.GetNumberScene();

        if (numberScene >= PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", numberScene);
        }
    }

    public void btnNextLevel()
    {
        SceneManager.LoadScene(numberScene);
    }

    public void btnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void btnExit()
    {
        Application.Quit();
    }

}
