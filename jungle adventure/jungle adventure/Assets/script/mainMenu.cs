using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    void Start()
    {
        spawnMonster.score = 0;
        spawnMonster.coin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Turorial");
                       
        }   
    }

    void startLevel()
    {
    }
}
