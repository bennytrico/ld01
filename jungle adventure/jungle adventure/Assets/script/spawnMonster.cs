using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class spawnMonster : MonoBehaviour
{
    public float level;
    public GameObject enemy;
    private List<Transform> _instances = new List<Transform>();
    private float enemyCount;
    public Collider playerBody;
    private string nextSceneName;
    public AudioClip clip;
    public AudioSource audioSource;
    public static float score;
    public Text scoreText;
    public Text coinText;
    public static float coin;

    void Start()
    {
        Debug.Log(score);
        var x = 5;
        Debug.Log(level);
        if (level != 6)
        {
            for (float i = 0; i < level - 1; i++)
            {
                Instantiate(enemy, new Vector3(enemy.transform.position.x + x, (float)-1.4, 9), Quaternion.identity);
                x += 5;

            }

            nextSceneName = "Level " + (level + 1);
            Debug.Log(nextSceneName);

            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            nextSceneName = "finish";
        }
        
    }
    
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("enemy").Length;

        if (enemyCount == 0 )
            nextLevel(playerBody);
        scoreText.text = "Score : " + score;
        coinText.text = ""+coin;
    }

    void nextLevel(Collider col)
    {
        var cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform)
                continue;
            SceneManager.LoadScene(nextSceneName);
        }
    }
    
}
