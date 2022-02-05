using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton2script : MonoBehaviour
{

    #region Public Variables
    public float attackDistance;
    public float movespeed;
    public float timer;
    bool hit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    [SerializeField] private HitScript hitscript;
    public GameObject hotZone;
    public GameObject TriggerArea;
    public Transform left_limit;
    public Transform right_limit;
    public Rigidbody2D skeletonchuk;
    public Collider2D Player;
    public float Maxhealh;
    public float currenthealth;
    public int skeletondamage = 20;
    public LayerMask player;
    public bool AttackMode { get; private set; }
    #endregion

    #region Private Variables

    private Animator anim;
    private float distance;
    private bool Patrooling;
    private float inTimer;
    private bool isattacking;
    #endregion

    void Awake()
    {
        currenthealth = Maxhealh;
        skeletonchuk = GetComponent<Rigidbody2D>();
        SelectTarget();
        inTimer = timer;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (!AttackMode)
        {
            Move();
        }

        if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemeattack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            Enemylogic();
        }
    }

    void Enemylogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance && isattacking == true)
        { 
            StopAttack();
        }
        else if (attackDistance >= distance && Patrooling == false && isattacking == false)
        {
            Attack();
        }

        if (Patrooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }
    void Move()
    {
        anim.SetBool("canWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemeattack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movespeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        Debug.Log("attack start");
        timer = inTimer;
        AttackMode = true;
        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
        isattacking = true;
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && Patrooling && AttackMode)
        {
            Patrooling = false;
            timer = inTimer; 
        }
    }
    void StopAttack()
    {
        Debug.Log("attack stop");
        Patrooling = false;
        AttackMode = false;
        anim.SetBool("Attack", false);
        isattacking = false;
    }

    private bool InsideofLimits()
    {
        return transform.position.x > left_limit.position.x && transform.position.x < right_limit.position.x; 
    }

    public void SelectTarget()
    {
        float distancetoLeft = Vector2.Distance(transform.position, left_limit.position);
        float distancetoRight = Vector2.Distance(transform.position, right_limit.position);

        if(distancetoLeft > distancetoRight)
        {
            target = left_limit;
        }
        else
        {
            target = right_limit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{currenthealth} {damage}");
        if(currenthealth <= 0)
        {
            return;
        }
        var isdeaddamage = currenthealth > 0 && currenthealth - damage <= 0;
        StopAttack();
        currenthealth -= damage;
        if (!isdeaddamage)
        {
            anim.SetTrigger("Hurt");
        }
        else
        {
                anim.SetTrigger("Died");
                Invoke("Destroy", 2.0f);
                this.enabled = false;
        }
    }
}
