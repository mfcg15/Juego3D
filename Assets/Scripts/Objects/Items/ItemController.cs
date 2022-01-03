using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    [SerializeField] private AudioClip soundItem;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(soundItem, gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
