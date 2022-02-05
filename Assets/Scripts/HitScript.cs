using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    public bool hit;
    public Collider2D Player;
    private List<HeroScript> currentattacktargets;
    [SerializeField] private Skeleton2script damageDiller;
    void Start()
    {
        
    }
    void Update()
    {
 
    }

    public void OnCollisionEnter2D(Collision2D collision)
     {
        if (collision.gameObject.CompareTag("Player"))
        {

            int count = collision.contacts.Length;
            for (int i = 0; i < count; i++)
            {
                Debug.Log("Имя объекта с которым столкнулись : " + collision.contacts[i].otherCollider.gameObject.name);
            }
            if (damageDiller)
            {
                Player.GetComponent<HeroScript>().takeDamage(damageDiller.skeletondamage);
            }
            else
            {
                return;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hit = false;
        }
    }


}
