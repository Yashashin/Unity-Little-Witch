using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int heal=5;
    public int mp=1;
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
        if(collision.gameObject.CompareTag("Character"))
        {
            if (gameObject.name == "potion")
            {
                collision.gameObject.GetComponent<SimplePlayerController>().Heal(heal);    
            }
            else
            {
                collision.gameObject.GetComponent<SimplePlayerController>().GetMP(mp);
            }
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Stick"))
        {
            if (gameObject.name == "potion")
            {
                collision.gameObject.GetComponent<Weapon>().character.GetComponent<SimplePlayerController>().Heal(heal);
            }
            else
            {
                collision.gameObject.GetComponent<Weapon>().character.GetComponent<SimplePlayerController>().GetMP(mp);
            }
            Destroy(gameObject);
        }
    }
}
