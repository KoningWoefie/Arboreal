using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    //constants
    private const float DEADZONE = 0.2f;

    //GameOjects
    [SerializeField] private GameObject enemy;
    public Transform Enemy { get => enemy.transform; }	
    //components
    MeshRenderer Meshrenderer;
    Animator anim;

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
    private bool top = false;
    private bool right = false;
    private bool left = false;
    private bool attacking = false;

    public bool Top { get => top; }
    public bool Right { get => right; }
    public bool Left { get => left; }
    public bool Attacking { get => attacking; set => attacking = value;}
    
    // Start is called before the first frame update
    void Start()
    {
        Meshrenderer = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent.GetComponent<Player>().moveEnabled)
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y");

            float mouseChangeX = mouseX - lastMousePosX;
            float mouseChangeY = mouseY - lastMousePosY;

            lastMousePosX = mouseX;
            lastMousePosY = mouseY;

            if(mouseChangeY > DEADZONE && mouseChangeY > mouseChangeX)
            {
                right = false;
                left = false;
                top = true;
            }
            else if(mouseChangeX > DEADZONE)
            {
                right = true;
                left = false;
                top = false;
            }
            else if(mouseChangeX < DEADZONE * -1)
            {
                right = false;
                left = true;
                top = false;
            }
            Attack();
            Parry();
        }
    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(0) && t.Seconds() == 0)
        {
            if(top)
            {
                anim.SetBool("Top", true);
                attacking = true;
                t.StartTimer();
            }
            else if(left)
            {
                anim.SetBool("Left", true);
                attacking = true;
                t.StartTimer();
            }
            else if(right)
            {
                anim.SetBool("Right", true);
                attacking = true;
                t.StartTimer();
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Top", false);
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
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
        if(enemy.GetComponentInChildren<Melee>().Attacking)
        {
            if(Input.GetMouseButtonDown(1) && enemy.GetComponentInChildren<Melee>().t.Seconds() < parryWindow)
            {
                enemy.GetComponentInChildren<Melee>().t.StopTimer();
                enemy.GetComponentInChildren<Melee>().Anim.SetBool("Top", false);
                enemy.GetComponentInChildren<Melee>().Anim.SetBool("Left", false);
                enemy.GetComponentInChildren<Melee>().Anim.SetBool("Right", false);
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

            if((top && m.Top) || (right && m.Right) || (left && m.Left))
            {
                collisionInfo.gameObject.GetComponent<Player>().TakeBlockedDamage(damage);
                attacking = false;
                t.StopTimer();
            }
            else
            {
                collisionInfo.gameObject.GetComponent<Player>().TakeDamage(damage);
                attacking = false;
                t.StopTimer();
            }
        }
    }
}
