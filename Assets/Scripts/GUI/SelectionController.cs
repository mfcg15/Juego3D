using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionController : MonoBehaviour
{
    [SerializeField] private Button[] lvlButtons;
    [SerializeField] private Sprite image;
    [SerializeField] private Text[] txtsButtons;
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 4);

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if(i+ 4 > levelAt)
            {
                lvlButtons[i].interactable = false;
                lvlButtons[i].image.sprite = image;
                txtsButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
