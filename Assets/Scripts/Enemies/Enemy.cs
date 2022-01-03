using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected GameObject player;
    protected bool isDeath = false, seeThePlayer, ability;
    protected float rotationEnemy = 70f, speedAux, resetTime;

    private bool isAttack = false , isPlayerDeath = false ;
 
    [SerializeField] protected int lifeEnemy;
    [SerializeField] protected float speedEnemy, rangoOfView, timeAttack;

    [SerializeField] private float attackDistance, timeDestroy;
    [SerializeField] private GameObject attackPoint;

    virtual public void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        resetTime = timeAttack;
        speedAux = speedEnemy;
        PlayerController.onDeath += onPlayerDeathHandler;
    }

    void Update()
    {
        if (isDeath)
        {
            timeDestroy -= Time.deltaTime;

            if (timeDestroy <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Actions();
        }
    }

    virtual public void Actions ()
    {
        if ((Vector3.Distance(transform.position, player.transform.position) <= rangoOfView))
        {
            MoveTowards();
            seeThePlayer = true;
        }
        else
        {
            seeThePlayer = false;
            speedEnemy = speedAux;
            anim.SetBool("isRun", false);
        }
    }

    virtual public void MoveTowards()
    {
        LookAtPlayer();

        Vector3 direction = (player.transform.position - transform.position).normalized;

        if (Vector3.Distance(transform.position, player.transform.position) >= attackDistance)
        {
            speedEnemy = speedAux + 2f;
            transform.position += speedEnemy * direction * Time.deltaTime;
            anim.SetBool("isRun", true);
            anim.SetFloat("SpeedY", 0f);
            isAttack = false;
        }
        else
        {
            speedEnemy = speedAux;
            anim.SetBool("isRun", false);
            RaycastHitAttack(attackPoint.transform);
            isAttack = true;
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationEnemy * Time.deltaTime);
    }

    private void RaycastHitAttack(Transform point)
    {
        RaycastHit hit;
        if (Physics.Raycast(point.position, point.TransformDirection(Vector3.forward), out hit, attackDistance))
        {
            if (hit.transform.CompareTag("Player") && isPlayerDeath == false)
            {
                Attack();
            }
        }
    }

    virtual public void Attack()
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

    private void DrawRay(Transform point)
    {
        Gizmos.color = Color.blue;
        Vector3 direction = point.TransformDirection(Vector3.forward) * attackDistance;
        Gizmos.DrawRay(point.position, direction);
    }

    private void DrawRaycast()
    {
        DrawRay(attackPoint.transform);
    }

    private void OnDrawGizmos()
    {

        if (seeThePlayer)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireSphere(transform.position, rangoOfView);

        if (isAttack)
        {
            DrawRaycast();
        }
    }

    public void onPlayerDeathHandler(bool isDeath)
    {
        isPlayerDeath = isDeath;
    }

    virtual public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FistPlayer"))
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
