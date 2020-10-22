using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    public float               speed = 4f;
    public int direction;
    private Rigidbody2D        rb;
    public GameObject manager;
    public EnemyManagerScript ManagerScript;

    public bool directionChanged;
    
    // Start is called before the first frame update
    void Start()
    {
        directionChanged = false;
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.Find("EnemyManager");
        ManagerScript = manager.GetComponent<EnemyManagerScript>();
        StartCoroutine("Launch");
    }

    private void Update()
    {
        if (transform.position.y > 0 && directionChanged == false)
        {
            reverseDirection();
            directionChanged = true;
        }
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

    void reverseDirection()
    {
        rb.AddForce(transform.up * speed * direction * -2);
        transform.Rotate(180,0,0);
    }
}
