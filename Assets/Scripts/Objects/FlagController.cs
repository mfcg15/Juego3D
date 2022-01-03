using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagController : MonoBehaviour
{

    private int numberScene;

    void Start()
    {
        numberScene = GameManager.instance.GetNumberScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
             if (SceneManager.GetActiveScene().buildIndex == 6)
             {
                 SceneManager.LoadScene(7);
             }
            else
            {
                GameManager.instance.SetAuxDiamondsInstanciado(GameManager.instance.GetDiamondsInstanciado());
                GameManager.instance.AddNumberScene();
                SceneManager.LoadScene(3);
            }
        }
    }
}
