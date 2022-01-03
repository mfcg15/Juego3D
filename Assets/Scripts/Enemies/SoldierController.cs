using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : Enemy
{
    
    private bool flag = true, goBack = false;
    private float minimumDistance = 1f, changeTime = 1f, temporary, finalTime;
    private int routine, currentIndex = 0;
    [SerializeField] protected GameObject objectShoot;
    [SerializeField] protected Transform position;
    [SerializeField] Transform[] waypoints;

    public override void Actions()
    {
        if ((Vector3.Distance(transform.position, player.transform.position) <= rangoOfView))
        {
            MoveTowards();
            seeThePlayer = true;
        }
        else
        {
            Conduct();
            seeThePlayer = false;
            speedEnemy = speedAux;
            anim.SetBool("isRun", false);
        }
    }

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

    private void Conduct()
    {
        if (flag)
        {
            temporary = changeTime;
            flag = false;
            finalTime = temporary;
            routine = Random.Range(0, 2);
        }

        changeTime -= Time.deltaTime;

        if (changeTime <= 0)
        {
            flag = true;
            changeTime = finalTime;
        }

        switch (routine)
        {
            case 0:
                anim.SetFloat("SpeedY", 0f);
                break;

            case 1:
                Patrol();
                anim.SetFloat("SpeedY", 1f);
                break;
        }
    }

    private void Patrol()
    {
        Vector3 deltaVector = waypoints[currentIndex].position - transform.position;
        Vector3 direction = deltaVector.normalized;

        transform.forward = Vector3.Lerp(transform.forward, direction, rotationEnemy * Time.deltaTime);
        transform.position += transform.forward * speedEnemy * Time.deltaTime;

        float distance = deltaVector.magnitude;

        if (distance < minimumDistance)
        {
            if (currentIndex >= waypoints.Length - 1)
            {
                goBack = true;
            }
            else if (currentIndex <= 0)
            {
                goBack = false;
            }

            if (!goBack)
            {
                currentIndex++;
            }
            else currentIndex--;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            lifeEnemy--;
 
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
