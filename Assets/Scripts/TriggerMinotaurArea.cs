using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMinotaurArea : MonoBehaviour
{
    private ModifiedMinotaurScript enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<ModifiedMinotaurScript>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true;
            enemyParent.pursuezone.SetActive(true);
        }
    }
}