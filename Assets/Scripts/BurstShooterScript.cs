using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BurstShooterScript : MonoBehaviour
{
    public GameObject singleShooterProjectile
    ;
    public GameObject enemysingleShooterProjectile
    ;
    public int slot;
    public bool playerControlled = false;
    public GameObject playerObject;

    public float speed = 1f;
    public float yspeed = 2f;
    public float amplitude = 0.5f;
    
    public GameObject manager;
    public EnemyManagerScript ManagerScript;
    
    public PlayerScript playerScript;
    
    public bool shooting = false;

    public int direction;
    public float minDistance = -1;
    public float maxDistance = 1;
    public float offsetx = 0;

    public GameObject explosion;
    
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("EnemyManager");
        ManagerScript = manager.GetComponent<EnemyManagerScript>();
        playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<PlayerScript>();
        float delay = Random.Range(2f, 10f);
        float rate = Random.Range(2f, 8f);
        InvokeRepeating("Fire", delay, rate);
        direction = -1;
        minDistance = transform.position.x+minDistance;
        maxDistance = transform.position.x+maxDistance-.5f;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.y < -5)
        {
            Destroy(this.gameObject);
            ManagerScript.tally++;
        }
        if (playerControlled == true)
        {
            
            if (slot == 1)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    StartCoroutine("Shoot");
                }
            }
            else if (slot == 2)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    StartCoroutine("Shoot");
                }
            }
            else if (slot == 3)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    StartCoroutine("Shoot");
                }
            }
        }
        else if (playerControlled == false)
        {
            
            switch (direction)
            {
                case -1:
                    if (transform.position.x > minDistance)
                    {
                        offsetx = (-1*(speed))*Time.deltaTime;
                    }
                    else
                    {
                        direction = 1;
                    }
                    break;
                case 1:
                    if (transform.position.x < maxDistance)
                    {
                        offsetx = (1*(speed))*Time.deltaTime;
                    }
                    else
                    {
                        direction = -1;
                    }
                    break;
            }

            float offsety = (-1*(yspeed))*Time.deltaTime;
            Vector3 move = new Vector3(offsetx, y: offsety,0);
            transform.position = transform.position+move;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Abductor Collision");
        if (other.gameObject.tag == "leftAbductor")
        {
            playerControlled = true;
            transform.gameObject.tag = "Ally";
            transform.position = new Vector2(playerObject.transform.position.x-.65f, playerObject.transform.position.y+.35f);
            transform.Rotate(180,0,0);
            transform.parent = playerObject.transform;
            slot = 1;
            Destroy(other.gameObject);
            ManagerScript.tally++;
            playerScript.loadShip(1);
            spriteRenderer.sprite = spriteArray[3];
        }
        if (other.gameObject.tag == "midAbductor")
        {
            playerControlled = true;
            transform.gameObject.tag = "Ally";
            transform.position = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y+.4f);
            transform.Rotate(180,0,0);
            transform.parent = playerObject.transform;
            slot = 2;
            Destroy(other.gameObject);
            ManagerScript.tally++;
            playerScript.loadShip(2);
            spriteRenderer.sprite = spriteArray[3];
        }
        if (other.gameObject.tag == "rightAbductor")
        {
            playerControlled = true;
            transform.gameObject.tag = "Ally";
            transform.position = new Vector2(playerObject.transform.position.x+.65f, playerObject.transform.position.y+.35f);
            transform.Rotate(180,0,0);
            transform.parent = playerObject.transform;
            slot = 3;
            Destroy(other.gameObject);
            ManagerScript.tally++;
            playerScript.loadShip(3);
            spriteRenderer.sprite = spriteArray[3];
        }

        if (other.gameObject.tag == "Ally" && this.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            ManagerScript.tally++;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile" && this.gameObject.tag == "Enemy")
        {
            ManagerScript.score += 30;
        }
    }

    private void Fire()
    {
        int i = Random.Range(0, 100);
        if (i > 80 && playerControlled == false)
        {
            StartCoroutine("EnemyShoot");
        }
    }

    IEnumerator EnemyShoot()
    {
        spriteRenderer.sprite = spriteArray[1];
        yield return new WaitForSeconds(.4f);
        spriteRenderer.sprite = spriteArray[2];
        yield return new WaitForSeconds(.4f);
        spriteRenderer.sprite = spriteArray[3];
        Instantiate(enemysingleShooterProjectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(.2f);
        Instantiate(enemysingleShooterProjectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(.2f);
        Instantiate(enemysingleShooterProjectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        spriteRenderer.sprite = spriteArray[0];
    }
    IEnumerator Shoot()
    {
        if (shooting == false)
        {
            shooting = true;
            spriteRenderer.sprite = spriteArray[0];
            Instantiate(singleShooterProjectile, new Vector2(transform.position.x, transform.position.y),
                Quaternion.identity);
            yield return new WaitForSeconds(.2f);
            Instantiate(singleShooterProjectile, new Vector2(transform.position.x, transform.position.y),
                Quaternion.identity);
            yield return new WaitForSeconds(.2f);
            Instantiate(singleShooterProjectile, new Vector2(transform.position.x, transform.position.y),
                Quaternion.identity);
            yield return new WaitForSeconds(1.1f);
            spriteRenderer.sprite = spriteArray[1];
            yield return new WaitForSeconds(1.1f);
            spriteRenderer.sprite = spriteArray[2];
            yield return new WaitForSeconds(1.1f);
            spriteRenderer.sprite = spriteArray[3];
            shooting = false;
        }
    }

    public bool IsPlayerControlled()
    {
        return playerControlled;
    }
    public void OnDestroy()
    {
        Instantiate(explosion, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        if (playerControlled)
        {
            playerScript.unloadShip(slot);
        }
    }
    
}
