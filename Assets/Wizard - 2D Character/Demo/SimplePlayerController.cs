using UnityEngine;
using UnityEngine.SceneManagement;

public class SimplePlayerController : MonoBehaviour
{
    public float movePower = 10f;
    public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5
    public int hp = 20;
    public int mp = 0;
    public int maxHP;
    public int maxMP;
    public UnityEngine.UI.Slider hpSlider;
    public UnityEngine.UI.Slider mpSlider;
    public FixedJoystick fixedJoyStick;
    public ParticleSystem healEffect;
    public ParticleSystem mpEffect;
    public GameObject fireMagic;
    public GameObject shield;

    private Rigidbody2D rb;
    private Animator anim;
    Vector3 movement;
    private int direction = 1;
    bool isJumping = false;
    private bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hpSlider.value = hp;
    }


        private void Update()
        {
            Restart();
            if (alive)
            {
                Hurt();
                Die();
                Attack();
                Jump();
                Run();
                Magic();
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            anim.SetBool("isJump", false);
        }

    void Magic()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (mp > 0)
            {
                shield.SetActive(true);
                mp -= 1;
                Invoke("SetMagicFalse", 10);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (mp > 0)
            {
                mp -= 1;
                fireMagic.SetActive(true);
                Invoke("SetMagicFalse", 10);
            }
        }
        mpSlider.value = mp;
    }
    public void SetMagicFalse()
    {
        shield.SetActive(false);
        fireMagic.SetActive(false);
    }
    void Run()
    {
        if (!anim.GetBool("isAttack"))
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);
            if (Input.GetAxisRaw("Horizontal") < 0 || fixedJoyStick.Horizontal <= -0.1f)
            {
                direction = -1;
                moveVelocity = Vector3.left;

                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            if (Input.GetAxisRaw("Horizontal") > 0 || fixedJoyStick.Horizontal >= 0.1f)
            {
                direction = 1;
                moveVelocity = Vector3.right;

                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }
    }
    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0 || fixedJoyStick.Vertical >= 0.5f)
        && !anim.GetBool("isJump"))
        {
            isJumping = true;
            anim.SetBool("isJump", true);
        }
        if (!isJumping)
        {
            return;
        }

        rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger("attack");
            anim.SetBool("isAttack", true);
            Invoke("FinishAttack", 0.5f);
        }
    }

    void FinishAttack()
    {
        anim.SetBool("isAttack", false);
    }
    void Hurt()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
        }
    }
    void Die()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetTrigger("die");
            alive = false;
        }
        if (hp <= 0)
        {
            anim.SetTrigger("die");
            alive = false;
        }
    }
    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetTrigger("idle");
            alive = true;
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(2);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            anim.SetBool("isJump", false);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("hurt");
        hp -= damage;
        if (hp < 0)
        {
            Invoke("ReloadScene", 0.5f);
            anim.SetTrigger("die");        
        }
        hpSlider.value = hp;
    }

    public void GetMP(int mp)
    {
        mpEffect.Play();
        this.mp += mp;
        mpSlider.value = mp;
    }
    public void Heal(int heal)
    {
        healEffect.Play();
        hp += heal;
        if (hp > maxHP)
        {
            hp = maxHP;
        }
        hpSlider.value = hp;
    }
}
