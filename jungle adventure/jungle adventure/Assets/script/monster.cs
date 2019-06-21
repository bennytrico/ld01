using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monster : MonoBehaviour
{
    public GameObject enemy;
    public float health;
    Animator anim;
    public float damage;
    public Collider attackHitbox;
    public float speed;
    private Transform target;
    public float distance;
    public float attackDelay = 2;
    public float nextAttack;
    public float canWalk;
    public float stun = 1;
    private Rigidbody2D rb2d;
    public Transform healthbar;
    public Slider healthfill;
    public float maxHealth;
    public GameObject backGroundScript;


    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = GameObject.FindGameObjectWithTag("enemy");
        maxHealth = health;
        rb2d = GetComponent<Rigidbody2D>();
       
    }
    
    void Update()
    {
        distance = Mathf.Abs(transform.position.x - target.position.x);
        if (transform.position.x != target.position.x && Time.time > canWalk && health > 0)
        {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
                anim.SetFloat("speed", 1);
            
                if (distance < 1 && Time.time > nextAttack )
                {
                    anim.SetTrigger("attacking");
                    nextAttack += attackDelay;
                    Attack(attackHitbox);
                    
                }    
        }
        healthfill.value = health / maxHealth;
    }

    void Attack(Collider col)
    {
        var cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform)
                continue;

            c.SendMessageUpwards("takeDamage", damage);
        }
    }

    void takeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            anim.SetTrigger("dead");
            spawnMonster.score += 100;
            spawnMonster.coin += 10;
        }
        transform.position = new Vector3(enemy.GetComponent<Transform>().position.x + 1, enemy.GetComponent<Transform>().position.y, enemy.GetComponent<Transform>().position.z);
        canWalk += stun;
        rb2d.velocity = new Vector2(25, 0);
        
    }

    void removeSelf()
    {
        Destroy(this.gameObject);
    }

}
