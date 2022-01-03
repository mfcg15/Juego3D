using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : Enemy
{
    private BoxCollider attack;

    public override void Start()
    {
        base.Start();
        attack = GameObject.FindGameObjectWithTag("AttackGoblin").GetComponent<BoxCollider>();
    }

    private void ActivateCollider()
    {
        attack.enabled = true;
    }

    private void DesactivarCollider()
    {
        attack.enabled = false;
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Sword"))
        {

            lifeEnemy = lifeEnemy - 2;

            if (lifeEnemy >= 1)
            {
                anim.SetTrigger("isHurt");
            }

            if (lifeEnemy == 0)
            {
                anim.SetTrigger("isDeath");
                isDeath = true;
            }
        }

        if (other.CompareTag("SwordIce"))
        {

            lifeEnemy = lifeEnemy - 4;

            if (lifeEnemy >= 1)
            {
                anim.SetTrigger("isHurt");
            }

            if (lifeEnemy == 0)
            {
                anim.SetTrigger("isDeath");
                isDeath = true;
            }
        }

    }
    
}
