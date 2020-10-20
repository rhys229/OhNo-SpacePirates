using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    public float               speed = 4f;
    public int direction;
    private Rigidbody2D        rb;
    public GameObject manager;
    public EnemyManagerScript ManagerScript;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.Find("EnemyManager");
        ManagerScript = manager.GetComponent<EnemyManagerScript>();
        StartCoroutine("beamtimer");
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ManagerScript.tally++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "wall")
        {
            
        }    
        if (other.gameObject.tag == "floor")
        {
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            return;
        }

        if (other.gameObject.tag == "wall")
        {
            
        }
        if (other.gameObject.tag == "floor")
        {
            
        }
    }

    IEnumerator beamtimer()
    {
        yield return new WaitForSeconds(.4f);
        Destroy(this.gameObject);
    }
}
