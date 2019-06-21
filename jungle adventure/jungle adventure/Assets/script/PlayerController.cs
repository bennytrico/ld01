using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    bool facingRight = true;
    private Rigidbody2D rb2d;
    public Collider attackHitbox;
    public float damage;
    public float health;
    private float nextAttack;
    private float attackDelay = 1;
    public Transform healthbar;
    public Slider healthfill;
    public float maxHealth;
    protected Joystick joystick;
    protected GameObject attackButton;
    public float canWalk;
    Animator anim;
    public float stun = 1;

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        nextAttack += attackDelay;
        maxHealth = health;
        player = GameObject.FindGameObjectWithTag("Player");
        attackButton = GameObject.FindGameObjectWithTag("attackButton");
    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        float moveHorizontal = joystick.Horizontal;
 

        anim.SetFloat("speed",Mathf.Abs(moveHorizontal));

        
        Vector2 movement = new Vector2(moveHorizontal,rb2d.velocity.y);
        
        rb2d.AddForce(movement * speed);
        
        if (moveHorizontal > 0 && !facingRight)
            Flip();
        else if (moveHorizontal < 0 && facingRight)
            Flip();

        healthfill.value = health / maxHealth;
        
    } 

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void tryAttack()
    {
        anim.SetTrigger("attacking");
        Attack(attackHitbox);
        nextAttack += attackDelay;
    }

    void Attack(Collider col)
    {
        var cols = Physics.OverlapBox(col.bounds.center,col.bounds.extents,col.transform.rotation,LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform)
                continue;

            c.SendMessageUpwards("takeDamage",damage);
        }
    }

    void takeDamage(float damage)
    {
        health -= damage;

        transform.position = new Vector3(player.GetComponent<Transform>().position.x - 1, player.GetComponent<Transform>().position.y,player.GetComponent<Transform>().position.z);
        if (health <= 0)
        {
            anim.SetTrigger("dead");
        }
        
        rb2d.velocity = new Vector2(-35, 0);

    }

    void removeSelf()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("main menu");
    }

   
}
