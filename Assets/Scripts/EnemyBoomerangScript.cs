using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoomerangScript : MonoBehaviour
{
    public float               speed = 2f;
    public int direction;
    private Rigidbody2D        rb;
    public bool directionChanged;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("Launch");
    }

    
    private IEnumerator Launch() {
        //yield return new WaitForSeconds(1);
        //rb.AddForce(transform.right * -1);
        rb.AddForce(transform.up * speed * direction);
        yield return null;
    }
    private void Update()
    {
        if (transform.position.y < -4.5f && directionChanged == false)
        {
            reverseDirection();
            directionChanged = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("We hit the player");
        }

        if (other.gameObject.tag == "wall")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Ally")
        {
            Debug.Log("We Hit an Ally Ship");
            Destroy(other.gameObject);
            
        }
        if (other.gameObject.tag == "floor")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ally")
        {
            //award points
            Debug.Log("We Hit an Ally Ship");
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
    void reverseDirection()
    {
        rb.AddForce(transform.up * speed * direction * -2);
        transform.Rotate(180,0,0);
    }
}
