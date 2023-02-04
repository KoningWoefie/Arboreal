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
    private BoxCollider hitBox;
    private EnhancedDoosLocomotion player;
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
    private float attackDuration = 1.2f;
    private float recoveryTime = 1.5f;
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
        player = GetComponentInParent<EnhancedDoosLocomotion>();
        hitBox = GetComponent<BoxCollider>();
        hitBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Parry();
        anim.SetBool("Attacking", attacking);
    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(0) && t.Seconds() == 0 && player.Stamina >= 10)
        {
            attacking = true;
            hitBox.enabled = true;
            player.Stamina -= 10;
            t.StartTimer();
        }
        else if(t.Seconds() >= recoveryTime)
        {
            t.StopTimer();
        }
        else if(t.Seconds() >= attackDuration)
        {
            attacking = false;
            hitBox.enabled = false;
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
