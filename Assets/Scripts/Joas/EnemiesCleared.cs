using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCleared : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] enemies2;
    [SerializeField] private GameObject[] enemies3;
    private GameObject Obstacle;
    private GameObject Obstacle2;
    private GameObject Obstacle3;

  
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemies2 = GameObject.FindGameObjectsWithTag("Enemy2");
        enemies3 = GameObject.FindGameObjectsWithTag("Enemy3");
        Obstacle = GameObject.Find("Obstacle");
        Obstacle2 = GameObject.Find("Obstacle2");
        Obstacle3 = GameObject.Find("Obstacle3");
        
    }

    void Update()
    {
        if (enemies.Length == 0){
            Obstacle.layer = 7;
        }
    }
}
