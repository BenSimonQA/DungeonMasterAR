using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    public NewMovement giveHealth;
    public int healthBoost = 10;

    public GameObject healthPop;
    public GameObject tutorialEndPop;
    public GameObject arrowPop;
    public MonsterAI healthPosition;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = healthPosition.position; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            giveHealth.GiveHealth(healthBoost);

            healthPop.SetActive(false);
            tutorialEndPop.SetActive(true);
            arrowPop.SetActive(true);

            Destroy(gameObject);
        }
    }
}
