using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRayScript : MonoBehaviour
{
    public float               speed = 2f;
    public int direction;
    private Rigidbody2D        rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("beamtimer");
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
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "floor")
        {
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ally")
        {
            //award points
            Debug.Log("We Hit an Ally Ship");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
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
        yield return new WaitForSeconds(.3f);
        Destroy(this.gameObject);
    }
}
