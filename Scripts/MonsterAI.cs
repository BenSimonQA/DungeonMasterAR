using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float speed;
    float x;
    float y;
    float z;
    public float xMax=2;
    public float xMin = -2;
    public float zMax = -4;
    public float zMin = -4;
    float angle;
    Vector3 moveToo;
    Vector3 direction;
    public Vector3 position;

    private Animation anim;

    private float cooldown = 1.05f;

    public int maxHealth = 30;
    public int currentHealth;

    public Health healthBar;

    private bool chase = false;
    public float stoppingDistance;
    private Transform target;

    public GameObject killPop;
    public GameObject healthPop;
    public GameObject healthPotion;


    public NewMovement attack;
    public int attackDamage = 10;

    private float cooldownDeath = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 

        anim = GetComponent<Animation>();

        x = Random.Range(xMin,zMax);
        y = 0;
        z = Random.Range(zMin, zMax);

        moveToo = new Vector3(x, y, z);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(target.position, transform.position);

        if (dist <= 3)
        {
            chase = true;
        }
        else
        {
            chase = false;
        }

        if (chase == false)
        {
            cooldown = 1.05f;
            anim.Play("MonsWalk");
            direction = (moveToo - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            if (moveToo == transform.position)
            {
                x = Random.Range(-2, 2);
                y = 0;
                z = Random.Range(-4, 4);

                moveToo = new Vector3(x, y, z);
            }
            transform.position = Vector3.MoveTowards(transform.position, moveToo, Time.deltaTime * speed);

        }
        if(chase == true)
        {
            anim.Play("MonsAttack");
            cooldown -= Time.deltaTime;
            if (cooldown < -0f)
            {
                attack.TakeDamage(attackDamage);
                cooldown = 2.5f;
            }
            direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position,target.position) > 3)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
          Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        killPop.SetActive(false);
        healthPop.SetActive(true);
        position = this.gameObject.transform.position;
        healthPotion.SetActive(true);
        Destroy(this.gameObject);

    }
}
