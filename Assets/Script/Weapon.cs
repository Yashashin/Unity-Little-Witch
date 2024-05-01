using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject character;
    public int damage = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Debug.Log(gameObject.name);
            if (collision.gameObject.name == "slime1")
            {
                collision.gameObject.GetComponent<Slime>().TakeDamage(damage);
            }
            else
            {
                collision.gameObject.GetComponent<SlimeTurret>().TakeDamage(damage);
            }

        }
    }
}
