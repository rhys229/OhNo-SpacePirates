﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour
{
    public float               speed = 4f;
    public int direction;
    private Rigidbody2D        rb;
    public GameObject manager;
    public EnemyManagerScript ManagerScript;
    public bool hit = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.Find("EnemyManager");
        ManagerScript = manager.GetComponent<EnemyManagerScript>();
        StartCoroutine("Launch");
    }

    
    private IEnumerator Launch() {
        //yield return new WaitForSeconds(1);
        //rb.AddForce(transform.right * -1);
        yield return new WaitForSeconds(.2f);
        rb.AddForce(transform.up * speed * direction);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ManagerScript.tally++;
            Destroy(other.gameObject);
            if (hit == true)
            {
                Destroy(this.gameObject);
            }
            else if (hit == false)
            {
                hit = true;
            }
        }

        if (other.gameObject.tag == "wall")
        {
            Destroy(this.gameObject);
        }    
        if (other.gameObject.tag == "floor")
        {
            Destroy(this.gameObject);
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
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "floor")
        {
            Destroy(this.gameObject);
        }
    }
}
