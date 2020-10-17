using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitShotScript : MonoBehaviour
{
    public float               speed = 4f;
    public int direction;
    private Rigidbody2D        rb;
    public float thrust = 1f;
    public GameObject manager;
    public EnemyManagerScript ManagerScript;

    public int position;
    
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
        if (position == 1)
        {
            yield return new WaitForSeconds(.2f);
            rb.velocity = new Vector2(-1.5f,5f);
            yield return null;
        }
        if (position == 2)
        {
            yield return new WaitForSeconds(.2f);
            rb.velocity = new Vector2(0,5f);
            yield return null;
        }
        if (position == 3)
        {
            yield return new WaitForSeconds(.2f);
            rb.velocity = new Vector2(1.5f,5f);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ManagerScript.tally++;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
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
