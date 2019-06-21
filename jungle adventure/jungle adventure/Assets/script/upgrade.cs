using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class upgrade : MonoBehaviour, IPointerDownHandler
{
    private float coin;
    public GameObject player;
    private PlayerController playerController;
    
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }
    
    void Update()
    {
        coin = spawnMonster.coin;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (coin >= 30)
        {
            spawnMonster.coin -= 30;
            playerController.damage += 5;
            playerController.health += 50;
        }
    }

}
