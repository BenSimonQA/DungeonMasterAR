using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;



public class NewMovement : MonoBehaviour
{

    protected Joystick joystick;
    protected JoyButton attackButton;
    protected DefendButton defendButton;
    private Animation anim;

    private Transform target;
    private Vector3 startPosition;
    public GameObject movePop;

    public GameObject attackPop;
    public GameObject defPop;
    public GameObject killPop;


    private bool popOnce = false;
    private bool popDefOnce = false;


    protected bool attack;
    protected bool defend;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


    public int maxHealth = 100;
    public int currentHealth;
    private float cooldown = 0.25f;
    int attackDamage = 10;

    //public MonsterAI damageToHealth;

    public Health healthBar;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        
        if(data.health != 0)
        {
            currentHealth = data.health;
        }
        if(data.position[0] != 0 && data.position[2] != 0)
        {
            Vector3 position;

            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;
        }

        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        joystick = FindObjectOfType<Joystick>();
        attackButton = FindObjectOfType<JoyButton>();
        defendButton = FindObjectOfType<DefendButton>();
        anim = GetComponent<Animation>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        startPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    public void Update()
    {

        var rigidbody = GetComponent<Rigidbody>();
       // float dist = Vector3.Distance(target.position, transform.position);

        rigidbody.velocity = new Vector3(joystick.Horizontal * 1f, rigidbody.velocity.y, joystick.Vertical * 1f);

        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, transform.eulerAngles.z);
        }

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            anim.Play("Walking");
        }
        else if (attackButton.Pressed)
        {
            if (popOnce == false)
            {
                attackPop.SetActive(false);
                defPop.SetActive(true);
                popOnce = true;
            }
            attack = true;
            anim.Play("Attack");
            if (attack == true)
            {
                cooldown -= Time.deltaTime;
                if (cooldown < -0f)
                {
                    Collider[] hitenemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

                    foreach (Collider enemy in hitenemies)
                    {
                        enemy.GetComponent<MonsterAI>().TakeDamage(attackDamage);
                        cooldown = 1.5f;
                    }

                    //damageToHealth.TakeDamage(attackDamage);
                    //cooldown = 1.5f;

                }
            }

        }
        else if (defendButton.PressedDef)
        {
            if(popDefOnce == false)
            {
                defPop.SetActive(false);
                killPop.SetActive(true);
                popDefOnce = true;
            }
            defend = true;
            anim.Play("Block");
        }
        else
        {
            anim.Play("Idle");
            attack = false;
            defend = false;
        }

        if (startPosition != this.gameObject.transform.position)
        {
            movePop.SetActive(false);
            attackPop.SetActive(true);
        }
    }

    public void TakeDamage(int damage)
    {
        if( defend != true)
        {
            currentHealth -= damage;

        }
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            death();
        }
    }

    public void GiveHealth(int healthBoost)
    {
        currentHealth += healthBoost;
        if(currentHealth > 100)
        {
            currentHealth = 100;
        }
        healthBar.SetHealth(currentHealth);
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    public void SaveGame()
    {
        SaveSystem.SavePlayer(this);
    }

    void death()
    {
        SceneManager.LoadScene(4);
    }
}
