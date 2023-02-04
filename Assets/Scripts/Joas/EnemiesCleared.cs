using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCleared : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] enemies2;
    [SerializeField] private GameObject[] enemies3;
    public GameObject Obstacle;
    public GameObject Obstacle2;
    public GameObject Obstacle3;

  
    void Start()
    {

    }

    void Update()
    {
        if (enemies.Length == 0){
            Debug.Log("All enemies are dead");
            Obstacle.SetActive(false);
        }
    }
}
