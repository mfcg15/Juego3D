using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasLevels : MonoBehaviour
{
    [SerializeField] private Text textLifePlayer;
    [SerializeField] private Text textCountDiamond;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelExit;

    private int activate ;
    private bool isPress ;

    private void Awake()
    {
        PlayerController.onLivesChange += OnLivesChangeHandler;
        PlayerController.onDiamondsChange += OnCountDiamondHandler;
    }

    void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPress == false && activate == 0)
             {
                 panelPause.SetActive(true);
                Time.timeScale = 0;
                isPress = true;
                 activate = 1;
             }

             if(isPress == false && activate == 2)
             {
                 panelPause.SetActive(false);
                 Time.timeScale = 1;
                 isPress = true;
                 activate = 2;
             }

            /*isPress = !isPress;
            panelPause.SetActive(isPress);
            Time.timeScale = (isPress) ? 0 : 1f;*/
        }

        if(isPress == true && activate == 1)
        {
            activate = 2;
            isPress = false;
        }

        if (isPress == true && activate == 2)
        {
            activate = 0;
            isPress = false;
        }
    }

    void OnDestroy()
    {
        PlayerController.onLivesChange -= OnLivesChangeHandler;
        PlayerController.onDiamondsChange -= OnCountDiamondHandler;
    }

    public void OnLivesChangeHandler(int lives)
    {
        textLifePlayer.text = lives + "";
    }

    public void OnCountDiamondHandler(int diamond)
    {
        textCountDiamond.text = diamond + "";
    }

    public void btnContinue()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1;
        isPress = true;
        activate = 2;
    }

    public void btnReset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GameManager.instance.GetNumberScene());
    }

    public void btnMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void btnExit()
    {
        panelExit.SetActive(true);
        panelPause.SetActive(false);
    }

    public void btnCheck()
    {
        Application.Quit();
    }

    public void btnX()
    {
        panelExit.SetActive(false);
        panelPause.SetActive(true);
    }

    public void btnCloseMenuPause()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1;
        isPress = true;
        activate = 2;
    }
}
