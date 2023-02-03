using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    //GameOjects
    [SerializeField] private GameObject enemy;
    public Transform Enemy { get => enemy.transform; }	
    //components
    MeshRenderer Meshrenderer;
    [SerializeField] private Animator anim;

    public Animator Anim { get => anim; }

    //scripts
    public Timer t;

    //integers
    protected int damage = 10;
    [SerializeField]protected int health = 100;

    public int Damage { get => damage; }
    //floats
    private float parryWindow = 0.2f;
    private float attackDuration = 0.6f;
    private float recoveryTime = 0.9f;
    //mouse positions
    private float mouseX = 0;
    private float mouseY = 0;
    private float lastMousePosX = 0;
    private float lastMousePosY = 0;

    //bools
    private bool attacking = false;

    public bool Attacking { get => attacking; set => attacking = value;}
    
    // Start is called before the first frame update
    void Start()
    {
        Meshrenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent.GetComponent<Player>().moveEnabled)
        {
            Attack();
            Parry();
        }
        anim.SetBool("Attacking", attacking);
    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(0) && t.Seconds() == 0)
        {
            attacking = true;
            t.StartTimer();
            Debug.Log("Attacking");
        }
        if(t.Seconds() >= recoveryTime)
        {
            t.StopTimer();
        }
        if(t.Seconds() >= attackDuration)
        {
            attacking = false;
        }
    }

    void Parry()
    {
        if(enemy.GetComponent<Melee>() != null && enemy.GetComponentInChildren<Melee>().Attacking)
        {
            if(Input.GetMouseButtonDown(1) && enemy.GetComponentInChildren<Melee>().t.Seconds() < parryWindow)
            {
                enemy.GetComponentInChildren<Melee>().t.StopTimer();
                enemy.GetComponentInChildren<Melee>().Attacking = false;
                Debug.Log("Parried");
            }
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Enemy" && attacking)
        {
            Melee m = collisionInfo.gameObject.GetComponentInChildren<Melee>();

            collisionInfo.gameObject.GetComponent<Player>().TakeDamage(damage);
            attacking = false;
            t.StopTimer();
        }
    }
}
