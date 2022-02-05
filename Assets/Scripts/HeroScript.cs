using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class HeroScript : MonoBehaviour
{
    public event Action<int, int> OnHealthChanged;

    public Animator anim;
    //[SerializeField] public AudioSource walkingsound;
    //[SerializeField] public AudioSource crouchingsound;
    //public AudioSource LevelMusic;
    //static float musicaudio;
    //public AudioScript musicvolume;
    public bool facingRight = true;
    public int currentherohealth;
    public int maxherohealth = 100;
    public float speed = 1f;
    public bool grounded = true;
    public Transform groundCheck;
    public float JumpForce;
    public float groundRadius = 0.5f;
    public LayerMask whatIsGround;
    public Rigidbody2D hero;
    public Transform TopCheck;
    public BoxCollider2D stand;
    public BoxCollider2D sitting;
    public LayerMask roof;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackdamage = 30;
    public float timebetweenattack = 2f;
    public float nexttimeattack = 0f;
    private float topCheckRadius;

    // Start is called before the first frame update
    public void Start()
    {
        //LevelMusic = GetComponent<AudioSource>();
       // LevelMusic.volume = musicaudio;
        currentherohealth = maxherohealth;
        OnHealthChanged?.Invoke(currentherohealth, maxherohealth);
        sitting.enabled = false;
        anim = GetComponent<Animator>();
        hero = GetComponent<Rigidbody2D>();

        topCheckRadius = TopCheck.GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame

    private void Update()
    {
        Jump();
        sittingCheck();
        
        if (Time.time >= nexttimeattack)
        {
            if (Input.GetMouseButtonDown(0) && grounded)
            {
                Attack();
                nexttimeattack = Time.time + 1f / timebetweenattack;
            }
        }
    }
    void FixedUpdate()
    {
        CheckingGround();

        float move;
        move = Input.GetAxis("Horizontal");
        hero.velocity = new Vector2(move * speed, hero.velocity.y);

        /*if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            walkingsound.Play();
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            walkingsound.Stop();
        }*/

       /* if (Input.GetKeyDown(KeyCode.S) && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
        {
            crouchingsound.Play();
        }
        else if (Input.GetKeyUp(KeyCode.S) && (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)))
        {
            crouchingsound.Stop();
        }*/

        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetInteger("Anim", 1); 
        }

        if(Input.GetAxis("Horizontal") == 0)
        {
            anim.SetInteger("Anim", 0);
        }

        if ((move < 0) && facingRight)
        {
            Flip();
        }
        else if((move > 0) && !facingRight)
        {
            Flip();
        }

        if(hero.transform.position.y <= -7)
        {
            SceneManager.LoadScene(1);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void sittingCheck()
    {
        if (Input.GetKey(KeyCode.S))
        {
            speed = 0.5f;
            anim.SetBool("squat", true);
            stand.enabled = false;
            sitting.enabled = true;
        }

        else if(!Physics2D.OverlapCircle(TopCheck.position, topCheckRadius, roof))
        {
            speed = 1f;
            anim.SetBool("squat", false);
            stand.enabled = true;
            sitting.enabled = false;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded && sitting.enabled == false)
        {
            hero.velocity = new Vector2(hero.velocity.x, JumpForce);
            anim.SetBool("grounded", false);
        }

    }

    void CheckingGround()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("grounded", grounded);
    }

   public void Attack()
    {
       anim.SetTrigger("Attack");

       
       Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        var targetenemyes = new List<Skeleton2script>();
        var targetboss = new List<ModifiedMinotaurScript>();
        foreach(Collider2D enemy in hitenemies)
        {
            var enemyscript = enemy.GetComponentInParent<Skeleton2script>();
            var boss_script = enemy.GetComponentInParent<ModifiedMinotaurScript>();
            if(enemyscript != null && !targetenemyes.Contains(enemyscript))
            {
                targetenemyes.Add(enemyscript);
            }
            if(boss_script != null && !targetboss.Contains(boss_script))
            {
                targetboss.Add(boss_script);
            }
            //Debug.Log($"{enemyscript.gameObject.name}");
        }
        foreach (var target in targetenemyes)
        {
            target.TakeDamage(attackdamage);
        }

        foreach(var bb in targetboss)
        {
            bb.TakeDamage(attackdamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void takeDamage(int damage)
    {
        Debug.Log($"{currentherohealth} {damage}");

        currentherohealth -= damage;

        OnHealthChanged?.Invoke(currentherohealth, maxherohealth);

        if (currentherohealth <= 0)
        {
            Debug.Log("Hero died");
            anim.SetTrigger("Died");
            Invoke("HeroDisabled", 1f);
        }
    }

    void HeroDisabled()
    {
        SceneManager.LoadScene(1);
    }

    private void DestroyHero()
    {
        Destroy(gameObject);
    }
}


