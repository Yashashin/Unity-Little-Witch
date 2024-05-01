using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public int HP=20;
    public float jumpPower=10;
    public float moveSpeed=10f;
    public ParticleSystem deathEffect;
    public GameObject character;
    private Animator anim;
    private float countDist=0;
    private Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        moveDir = -gameObject.transform.right;
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);

        GetComponent<Rigidbody2D>().AddForce(jumpVelocity, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(gameObject.transform.position.x - character.transform.position.x) < 18)
        {
            if(gameObject.transform.position.x<character.transform.position.x)
            {
                transform.localScale = new Vector3(-0.4835f,0.4835f,0.4835f);
            }
            else
            {
                transform.localScale = new Vector3(0.4835f, 0.4835f, 0.4835f);
            }
            Vector3 dir = new Vector3();
            dir = new Vector3(character.transform.position.x - gameObject.transform.position.x, 0,0).normalized;
            transform.position += dir * moveSpeed*Time.deltaTime;
        }
        else
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            countDist += (moveDir * moveSpeed * Time.deltaTime).magnitude;
            if (countDist > 10)
            {
                countDist = 0;
                moveDir = -moveDir;
            }
        }
    }

    public void SetDefaultColor()
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    }
    public void TakeDamage(int damage)
    {   
        HP -= damage;
        if(HP<0)
        {
            Instantiate(deathEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        Invoke("SetDefaultColor", 0.5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            anim.SetTrigger("jump");
            Invoke("Jump",0.3f);
        }
    }
}
