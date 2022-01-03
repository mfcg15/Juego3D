using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    private Animator anim;
    private BoxCollider fistPlayer, swordArmIron , swordArmIce;
    private Transform groundCheck;
    private AudioManager audioManager;

    private int lifePlayer, diamonds, indexTypeArm;
    private float moveVertical, rotation = 70f, groundDistance = 0.3f, gravity = -9.8f, auxSpeed, timeDestroy;
    private bool isShoot = false, isDeath = false, isGrounded;
    private Vector3 velocity;

    [SerializeField] float speedPlayer, forzeJump;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float attackDistance;
    [SerializeField] private GameObject shotPoint;
    [SerializeField] private Transform position;
    [SerializeField] LayerMask groundMask;


    public static event Action<int> onLivesChange;
    public static event Action<int> onDiamondsChange;
    public static event Action<bool> onDeath;
    public static event Action  onDeathManager;

    private void Awake()
    {
        lifePlayer = GameManager.instance.GetLifePlayerInstanciado();
        diamonds = GameManager.instance.GetDiamondsInstanciado();
    }

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        groundCheck = gameObject.transform.GetChild(10).GetComponent<Transform>();
        fistPlayer = GameObject.FindGameObjectWithTag("FistPlayer").GetComponent<BoxCollider>();
        onLivesChange?.Invoke(lifePlayer);
        onDiamondsChange?.Invoke(diamonds);
        ArmController.onSaveArms += onChooseArm;
        InventoryController.onTypeAnimation += onChooseAnimation;
        auxSpeed = speedPlayer;
    }

    void Update()
    {
        Gravedad();

        if(isDeath != true)
        {
            Jump();
            Move();
            Rotation();
            Attack();
            Run();
        }
        else
        {

            timeDestroy -= Time.deltaTime;

            if (timeDestroy <= 0)
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    private void Move()
    {
        moveVertical = Input.GetAxis("Vertical");

        if(moveVertical>0 || moveVertical <0)
        {
            Vector3 move = transform.forward * moveVertical;
            cc.Move(move * speedPlayer * Time.deltaTime);
            anim.SetFloat("SpeedY", moveVertical);
            audioManager.SelectAudio(0);
            Run();
        }
        
    }

    private void Rotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0.0f, Time.deltaTime * -(rotation), 0.0f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0.0f, Time.deltaTime * rotation, 0.0f);
        }
    }

    private void Gravedad()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

        if(isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
            anim.SetBool("stayFloor", true);
        }
        else
        {
            anim.SetBool("stayFloor", false);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(forzeJump * -2 * gravity);
            anim.SetTrigger("isJump");
            audioManager.SelectAudio(2);
            anim.SetBool("stayFloor", false);
        }
    }

    private void onChooseArm (int typeArm)
    {
        indexTypeArm = typeArm;

        if (indexTypeArm == 1)
        {
            swordArmIron = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider>();
        }

        if (indexTypeArm == 2)
        {
            swordArmIce = GameObject.FindGameObjectWithTag("SwordIce").GetComponent<BoxCollider>();
        }
    }

    private void onChooseAnimation (int typeArm)
    {
        indexTypeArm = typeArm;
    }

    private void Attack()
    {
        switch (indexTypeArm)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.X) && isGrounded)
                {
                    anim.SetTrigger("isCut");
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.X) && isGrounded)
                {
                    anim.SetTrigger("isCut");
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.X) && isGrounded)
                {
                    anim.SetTrigger("isShootGun");
                    RaycastShoot(position);
                    isShoot = true;
                }
                break;
            case 4:
                if (Input.GetKey(KeyCode.X) && isGrounded)
                {
                    anim.SetTrigger("isShootRifle");
                    RaycastShoot(position);
                    isShoot = true;
                }
                break;
            case 5:
                if (Input.GetKey(KeyCode.X) && isGrounded)
                {
                    anim.SetTrigger("isShootRifle");
                    RaycastShoot(position);
                    isShoot = true;
                }

                break;
            case 6:
                if (Input.GetKey(KeyCode.X) && isGrounded)
                {
                    anim.SetTrigger("isShootRifle");
                    RaycastShoot(position);
                    isShoot = true;
                }
                break;
            default:
                if (Input.GetKeyDown(KeyCode.X) && isGrounded)
                {
                    anim.SetTrigger("isPunch");
                }
                break;
        }
    }

    private void Run()
    {
       
        if (moveVertical != 0 && Input.GetKey(KeyCode.Z))
        {
            speedPlayer = auxSpeed + 2f;
            anim.SetBool("isRun", true);
            audioManager.SelectAudio(1);
        }
        else
        {
            anim.SetBool("isRun", false);
            speedPlayer = auxSpeed;
        }
    }

    private void RaycastShoot(Transform point)
    {
        RaycastHit hit;
        if (Physics.Raycast(point.position, point.TransformDirection(Vector3.forward), out hit, attackDistance))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                GameObject b = Instantiate(bullet, position.transform.position, bullet.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce(position.transform.TransformDirection(Vector3.forward) * 2f, ForceMode.Impulse);
            }
        }
    }

    private void DrawRay(Transform point)
    {
        Gizmos.color = Color.magenta;
        Vector3 direction = point.TransformDirection(Vector3.forward) * attackDistance;
        Gizmos.DrawRay(point.position, direction);
    }

    private void DrawRaycast()
    {
        DrawRay(shotPoint.transform);
    }
    private void OnDrawGizmos()
    {
        if(isShoot)
        {
            DrawRaycast();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackEnemy") || other.CompareTag("AttackGoblin") || other.CompareTag("AttackTroll") || other.CompareTag("Bullet") || other.CompareTag("Arrow")|| other.CompareTag("AttackMutant"))
        {
            lifePlayer--;
            GameManager.instance.SetLifePlayer(lifePlayer);
            onLivesChange?.Invoke(lifePlayer);

            if (lifePlayer >= 1)
            {
               anim.SetTrigger("isHurt");
            }
        }

        if (other.CompareTag("Life"))
        {
            lifePlayer++;
            GameManager.instance.SetLifePlayer(lifePlayer);
            onLivesChange?.Invoke(lifePlayer);
        }

        if (other.CompareTag("Diamond"))
        {
            diamonds++;
            GameManager.instance.IncreseDiamonds();
            onDiamondsChange?.Invoke(diamonds);
        }

        if (lifePlayer == 0)
        {
            ChooseAnimation();
            isDeath = true;
            onDeath?.Invoke(isDeath);
            onDeathManager?.Invoke();
        }
    }

    private void ChooseAnimation()
    {
        switch (indexTypeArm)
        {
            case 1:
                anim.SetTrigger("isDeathSword");
                timeDestroy = 3.25f;
                break;
            case 2:
                anim.SetTrigger("isDeathSword");
                timeDestroy = 3.25f;
                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
            default:
                anim.SetTrigger("isDeath");
                timeDestroy = 2.1f;
                break;
        }
    }

    private void ActivateCollider()
    {
        if(indexTypeArm == 0)
        {
            fistPlayer.enabled = true;
        }

        if(indexTypeArm == 1)
        {
            swordArmIron.enabled = true;
        }

        if(indexTypeArm == 2)
        {
            swordArmIce.enabled = true;
        }
        
    }

    private void DesactivarCollider()
    {
       
        if (indexTypeArm == 0)
        {
            fistPlayer.enabled = false;
        }

        if (indexTypeArm == 1)
        {
            swordArmIron.enabled = false;
        }

        if (indexTypeArm == 2)
        {
            swordArmIce.enabled = false;
        }
    }
}
