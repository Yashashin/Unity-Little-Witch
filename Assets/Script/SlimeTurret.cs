using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTurret : MonoBehaviour
{
    public int HP = 20;
    public float moveSpeed = 3f;
    public float timeBetweenShoot = 2;
    public GameObject character;
    public GameObject bulletPrefab;
    public ParticleSystem deathEffect;
    public Transform bulletPoint;
    private Animator anim;
    private bool canShoot;
    private float countDist = 0;
    private Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canShoot = true;
         anim.SetTrigger("Shoot");
        moveDir = -gameObject.transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(gameObject.transform.position.x - character.transform.position.x) < 18)
        {
            if(canShoot)
            {
                Shoot();
            }
            if (gameObject.transform.position.x < character.transform.position.x)
            {
                transform.localScale = new Vector3(-0.407622f, 0.407622f, 0.407622f);
            }
            else
            {
                transform.localScale = new Vector3(0.407622f, 0.407622f, 0.407622f);
            }
            Vector3 dir = new Vector3();
            dir = new Vector3(character.transform.position.x - gameObject.transform.position.x, 0, 0).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
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
        if (HP < 0)
        {
            Instantiate(deathEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        Invoke("SetDefaultColor", 0.5f);
    }
    
    public void ReadyToShoot()
    {
        canShoot = true;
    }
    public void Shoot()
    {
        canShoot = false;
      
        GameObject bullet=Instantiate(bulletPrefab, bulletPoint.position, Quaternion.identity);
        if (character.transform.position.x < gameObject.transform.position.x)
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(-bullet.transform.right.normalized * 100, ForceMode2D.Impulse);
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right.normalized * 100, ForceMode2D.Impulse);
        }
        Invoke("ReadyToShoot", timeBetweenShoot);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }
}
