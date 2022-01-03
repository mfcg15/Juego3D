using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArmController : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private AudioClip soundArm;

    public static event Action<int> onSaveArms;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {   
            onSaveArms?.Invoke(index);
            AudioSource.PlayClipAtPoint(soundArm, gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
