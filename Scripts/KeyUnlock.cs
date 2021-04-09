using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUnlock : MonoBehaviour
{
    public GameObject arrowPop;
    public MonsterAICom healthPosition;
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

            arrowPop.SetActive(true);

            Destroy(gameObject);
        }
    }
}
