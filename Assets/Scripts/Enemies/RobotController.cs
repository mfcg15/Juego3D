using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : Enemy
{
    [SerializeField] protected GameObject objectShoot;
    [SerializeField] protected Transform position;

    public override void Attack()
    {
        if (timeAttack == resetTime)
        {
            anim.SetTrigger("isAttack");
            GameObject b = Instantiate(objectShoot, position.transform.position, objectShoot.transform.rotation);
            b.GetComponent<Rigidbody>().AddForce(position.transform.TransformDirection(Vector3.forward) * 5f, ForceMode.Impulse);
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
}
