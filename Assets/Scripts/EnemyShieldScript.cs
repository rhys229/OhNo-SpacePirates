using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldScript : MonoBehaviour
{
    public float               speed = 2f;
    public int direction;
    private Rigidbody2D        rb;

    
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
        StartCoroutine("Explode");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            if (other.gameObject.name != "Boomerang(Clone)")
            {
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == "Player")
        {
            
        }

        if (other.gameObject.tag == "wall")
        {
            
        }

        if (other.gameObject.tag == "Ally")
        {
            
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
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "floor")
        {
            Destroy(this.gameObject);
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
