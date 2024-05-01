using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroySelf()
    {
       Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Character") || collision.gameObject.CompareTag("Stick"))
        {
            character.GetComponent<SimplePlayerController>().TakeDamage(damage);
            Destroy(this);
        }
        else
        {
            Invoke("DestroySelf", 5);
        }
        
       
    }
}
