using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    public float shootTime = 8f;
    bool flag = true, activate;
    float temporary, finalTime;

    private void Start()
    {
        PlayerController.onDeath += onPlayerDeathHandler;
    }

    void Update()
    {
        if (activate == true)
        {
            Activate();
        }
    }

    private void Activate()
    {
        if (flag)
        {
            temporary = shootTime;
            flag = false;
            finalTime = temporary;
            Shoot();
        }

        shootTime -= Time.deltaTime;

        if (shootTime <= 0)
        {
            flag = true;
            shootTime = finalTime;
        }
    }

    private void Shoot()
    {
        Instantiate(enemyPrefab, transform);
    }

    public void onPlayerDeathHandler(bool isDeath)
    {
        activate = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activate = true;
        }

        if (other.CompareTag("FistPlayer") || other.CompareTag("Sword") || other.CompareTag("SwordIce"))
        {
            Destroy(gameObject);
        }

    }
}
