using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiedMinotaurScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D minotaur;
    public Animator anim;
    public TilemapScript map;
    public Transform target;
    public bool pursuemode;
    public int minotaurdamage;
    public GameObject pursuezone;
    public GameObject attackarea;
    public float currenthealth;
    [SerializeField] float Maxhealth;
    [SerializeField] private float timer;
    [SerializeField] private float inTimer;
    [HideInInspector] public bool inRange;
    [SerializeField] private float movespeed;
    [SerializeField] private float distance;
    [SerializeField] private float attackdistance;

    public bool attackmode { get; private set; }

    [SerializeField] private bool staticmode = false;
    [SerializeField] private Transform point;
    [SerializeField] private HitMinotaurScript hitscript;
    public List<bool> staying;
    private bool isattacking;
    private double Tolerance = 0.1;

    private void Awake()
    {
        SelectTarget();
        minotaur = GetComponent<Rigidbody2D>();
        inTimer = timer;
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        currenthealth = Maxhealth;
        anim = GetComponent<Animator>();
        staticmode = false;
    }

    private void Update()
    {

        if (!attackmode)
        {
            Move();
        }

        if (!OnthePoint() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("AttackMinotaurAnimation"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            MinotaurLogic();
        }

    }


    public void MinotaurLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > attackdistance && isattacking == true)
        {
            StopAttack();
        }

        else if (attackdistance >= distance && isattacking == false && staticmode == false)
        {
            Attack();
        }

        if(staticmode)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Attack()
    {
        Debug.Log("attack start");
        timer = inTimer;
        attackmode = true;
        anim.SetBool("CanWalk", false);
        anim.SetBool("Attack", true);
        isattacking = true;
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && attackmode && staticmode)
        {
            timer = inTimer;
            staticmode = false;
        }
    }
    void StopAttack()
    {
        Debug.Log("attack stop");
        attackmode = false;
        staticmode = false;
        anim.SetBool("Attack", false);
        isattacking = false;
    }

    void Move()
    {
            anim.SetBool("CanWalk", true);
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("AttackMinotaurAnimation"))
            {
                Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movespeed * Time.deltaTime);
            }
    }

    public void SelectTarget()
    {
        target = point;

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
            
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    public bool OnthePoint()
    {
        if (Mathf.Abs(transform.position.x - point.position.x) < Tolerance)
        {
            anim.SetBool("CanWalk", false);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{currenthealth} {damage}");
        if (currenthealth <= 0)
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
            map.eraseTiles();
            anim.SetTrigger("Died");
            Invoke("Destroy", 2.0f);
            this.enabled = false;
        }
    }
}
