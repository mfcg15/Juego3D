using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject panelInventory;
    [SerializeField] private List<GameObject> panelArm;

    [SerializeField] private GameObject allArms;
    [SerializeField] private List<GameObject> arm;

    private List<int> typeArm = new List<int>();


    private int index = 0;

    public static event Action<int> onTypeAnimation;

    private void Awake()
    {
        ArmController.onSaveArms += OnAddArmHandler;
    }
    void Start()
    {
       // allArms = GameObject.FindWithTag("Arms");
        arm.Add(allArms.transform.GetChild(0).gameObject);
        typeArm.Add(0);
    }

    private void OnDestroy()
    {
        ArmController.onSaveArms -= OnAddArmHandler;
    }

    public void OnAddArmHandler(int index)
    {
        panelArm.Add(panelInventory.transform.GetChild(index).gameObject);
        arm.Add(allArms.transform.GetChild(index).gameObject);
        typeArm.Add(index);
        ActivatePanelArm(index);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            index++;

            if (index == panelArm.Count && index == arm.Count)
            {
                index = 0;
            }

          //  onTypeAnimation.Invoke(index);
            SwitchPanelArms(index);
        }
    }

    public void ActivatePanelArm(int index)
    {
        for (int i = 0; i < panelArm.Count; i++)
        {
            panelArm[i].SetActive(false);
        }

        for (int i = 0; i < arm.Count; i++)
        {
            arm[i].SetActive(false);
        }

        panelInventory.transform.GetChild(index).gameObject.SetActive(true);
        allArms.transform.GetChild(index).gameObject.SetActive(true);
    }

    private void SwitchPanelArms(int index)
    {

        for (int i = 0; i < panelArm.Count; i++)
        {
            if (i == index)
            {
                panelArm[i].SetActive(true);
            }
            else
            {
                panelArm[i].SetActive(false);
            }

        }

        for (int i = 0; i < arm.Count; i++)
        {
            if (i == index)
            {
                arm[i].SetActive(true);
            }
            else
            {
                arm[i].SetActive(false);
            }

        }

        for (int i = 0; i < typeArm.Count; i++)
        {
            if (i == index)
            {
                onTypeAnimation.Invoke(typeArm[i]);
            }
        }
    }
}
