using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public Animator anim;
    public Collider2D player;
    public int trapdamage;
    public bool active;
    public bool active2;
    public float time = 10f;
    public float lefttime;
    public float traptimer = 5f;
    public float relaxing = 5f;

    private void Start()
    {
        lefttime = time;
    }
    private void Update()
    {
        if (lefttime > 0)
        {
            lefttime -= Time.deltaTime;
            anim.SetBool("trap", true);
        }
        else
        {
            anim.SetBool("trap", false);
            active2 = false;
            relaxing -= Time.deltaTime;
            if (relaxing <= 0)
            {
                lefttime = time;
                relaxing = 5f;
                active2 = !active2;
            }
        }

        Takingdamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            active = false;
        }
    }

    void Takingdamage()
    {
        if (active == true && active2 != false)
        {
            if (traptimer <= 0)
            {
                player.GetComponent<HeroScript>().takeDamage(trapdamage);
                traptimer = 5f;
            }
            else
            {
                traptimer -= Time.deltaTime;
            }
        }
    }
}
