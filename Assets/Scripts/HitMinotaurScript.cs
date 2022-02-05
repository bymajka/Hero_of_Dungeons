using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMinotaurScript : MonoBehaviour
{
    public bool hit;
    public Collider2D Player;
    [SerializeField] private ModifiedMinotaurScript damageDiller;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageDiller)
                Player.GetComponent<HeroScript>().takeDamage(damageDiller.minotaurdamage);
            else
            {
                return;
            }
        }
    }

    public void OnCollisionEXit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hit = false;
        }
    }

}
