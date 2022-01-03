using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
   
    public static GameManager instance;
    private int diamondsInstanciado , auxDiamondsInsnciado;
    private int lifePlayerInstanciado ;
    private int numberSceneInstanciada;

    private void Awake()
    {
        if (instance == null)
        {
            lifePlayerInstanciado = 50;
            diamondsInstanciado = 0;
            numberSceneInstanciada = 4;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        auxDiamondsInsnciado = GameManager.instance.GetDiamondsInstanciado();
        PlayerController.onDeathManager += GameOver;
    }

    private void GameOver()
    {
        diamondsInstanciado = auxDiamondsInsnciado;
        lifePlayerInstanciado = 2;
    }

    public void IncreseDiamonds()
    {
        instance.diamondsInstanciado++;
    }

    public int GetDiamondsInstanciado()
    {
        return diamondsInstanciado;
    }

    public void SetDiamondsInstanciado(int diamond)
    {
        diamondsInstanciado = diamond;
    }

    public void SetAuxDiamondsInstanciado(int diamond)
    {
        auxDiamondsInsnciado = diamond;
    }

    public int GetAuxDiamondsInstanciado()
    {
        return auxDiamondsInsnciado;
    }

    public void SetLifePlayer(int life)
    {
        instance.lifePlayerInstanciado = life;
    }

    public int GetLifePlayerInstanciado()
    {
        return lifePlayerInstanciado;
    }

    public void AddNumberScene()
    {
        instance.numberSceneInstanciada++;
    }

    public void SetNumberScene(int number)
    {
        instance.numberSceneInstanciada = number;
    }

    public int GetNumberScene()
    {
        return numberSceneInstanciada;
    }

}
