using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class attack : MonoBehaviour, IPointerDownHandler
{
    public GameObject player;
    Animator anim;
    protected bool pressed;
    private float nextAttack;
    private float attackDelay = 1;
    public Collider attackHitbox;
    public float damage;

    void Start()
    {
        anim = player.GetComponentInParent<Animator>();
    }
    
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
            player.SendMessageUpwards("tryAttack");
    }
   
}
