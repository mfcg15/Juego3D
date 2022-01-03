using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : SoldierController
{ 

    [SerializeField] private GameObject arrowAnimation;

    public override void Attack()
    {
        if (timeAttack == resetTime)
        {
            anim.SetTrigger("isAttack");
            ability = true;
        }

        if (ability)
        {
            timeAttack -= Time.deltaTime;

            if (timeAttack <= 0)
            {
                timeAttack = resetTime;
            }
        }
    }

    private void ActivateAttack()
    {
        arrowAnimation.SetActive(true);
    }

    private void DesactivarAttack()
    {
        arrowAnimation.SetActive(false);
    }

    private void Shoot()
    {
        GameObject b = Instantiate(objectShoot, position.transform.position, objectShoot.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(position.transform.TransformDirection(Vector3.forward) * 3f, ForceMode.Impulse);
    }
}
