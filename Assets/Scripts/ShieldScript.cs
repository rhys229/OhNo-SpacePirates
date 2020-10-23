using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public float               speed = 4f;
    public int direction;
    private Rigidbody2D        rb;
    public GameObject manager;
    public EnemyManagerScript ManagerScript;
    
    
    public CircleCollider2D collider;
    public bool exploding = false;

    public AudioSource explosion;
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        collider = GetComponent<CircleCollider2D>();
        manager = GameObject.Find("EnemyManager");
        ManagerScript = manager.GetComponent<EnemyManagerScript>();
        StartCoroutine("Explode");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy Projectile")
        {
            if (other.gameObject.name != "EnemyBoomerang(Clone)")
            {
                Destroy(other.gameObject);
            }
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
        if (other.gameObject.tag == "Enemy Projectile")
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Enemy")
        {
        }

        if (other.gameObject.tag == "wall")
        {
        }
        if (other.gameObject.tag == "floor")
        {
        }
    }
    IEnumerator Explode()
    {
        exploding = true;
        explosion.Play();
        rb.velocity = Vector2.zero;
        
        collider.radius = .1f;
        spriteRenderer.sprite = spriteArray[1];
        yield return new WaitForSeconds(.1f);
        collider.radius = .15f;
        spriteRenderer.sprite = spriteArray[2];
        
        yield return new WaitForSeconds(.1f);
        collider.radius = .2f;
        spriteRenderer.sprite = spriteArray[3];
        
        yield return new WaitForSeconds(.1f);
        collider.radius = .4f;
        spriteRenderer.sprite = spriteArray[4];
        
        yield return new WaitForSeconds(.2f);
        collider.radius = .6f;
        spriteRenderer.sprite = spriteArray[5];
        
        yield return new WaitForSeconds(.8f);
        Destroy(this.gameObject);
        exploding = false;
    }
}
