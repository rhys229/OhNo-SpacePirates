﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShieldShipScript : MonoBehaviour
{
    public GameObject Shield;
    public GameObject enemyShield;
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
    
    public AudioSource shootSound;

    public bool spedUp;
    
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spedUp = false;
        manager = GameObject.Find("EnemyManager");
        ManagerScript = manager.GetComponent<EnemyManagerScript>();
        playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<PlayerScript>();
        float delay = Random.Range(2f, 10f);
        float rate = Random.Range(2f, 8f);
        InvokeRepeating("Fire", delay, rate);
        int yposition = (int) transform.position.y;
        if (yposition % 2 == 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        minDistance = transform.position.x+minDistance;
        maxDistance = transform.position.x+maxDistance-.5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemyObjects;
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyObjects.Length < 3 && spedUp == false)
        {
            yspeed = yspeed * 2.5f;
            spedUp = true;
        }
        if (transform.position.y < -5)
        {
            Destroy(this.gameObject);
            playerScript.gameOver();
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
        if (other.gameObject.tag == "leftAbductor")
        {
            StopCoroutine("EnemyShoot");
            playerControlled = true;
            transform.gameObject.tag = "Ally";
            transform.position = new Vector2(playerObject.transform.position.x-.65f, playerObject.transform.position.y+.35f);
            transform.Rotate(180,0,0);
            transform.parent = playerObject.transform;
            slot = 1;
            Destroy(other.gameObject);
            ManagerScript.tally++;
            playerScript.loadShip(1);
            spriteRenderer.sprite = spriteArray[4];
        }
        if (other.gameObject.tag == "midAbductor")
        {
            StopCoroutine("EnemyShoot");
            playerControlled = true;
            transform.gameObject.tag = "Ally";
            transform.position = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y+.4f);
            transform.Rotate(180,0,0);
            transform.parent = playerObject.transform;
            slot = 2;
            Destroy(other.gameObject);
            ManagerScript.tally++;
            playerScript.loadShip(2);
            spriteRenderer.sprite = spriteArray[4];
        }
        if (other.gameObject.tag == "rightAbductor")
        {
            StopCoroutine("EnemyShoot");
            playerControlled = true;
            transform.gameObject.tag = "Ally";
            transform.position = new Vector2(playerObject.transform.position.x+.65f, playerObject.transform.position.y+.35f);
            transform.Rotate(180,0,0);
            transform.parent = playerObject.transform;
            slot = 3;
            Destroy(other.gameObject);
            ManagerScript.tally++;
            playerScript.loadShip(3);
            spriteRenderer.sprite = spriteArray[4];
        }

        if (other.gameObject.tag == "Ally")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            ManagerScript.tally++;
        }
        if (other.gameObject.tag == "floor")
        {
            Destroy(this.gameObject);
            ManagerScript.tally++;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile" && this.gameObject.tag == "Enemy")
        {
            ManagerScript.score += 20;
        }
    }
    

    private void Fire()
    {
        int i = Random.Range(0, 100);
        if (i > 63 && playerControlled == false)
        {
            StartCoroutine("EnemyShoot");
        }
    }

    public bool IsPlayerControlled()
    {
        return playerControlled;
    }

    IEnumerator EnemyShoot()
    {
        spriteRenderer.sprite = spriteArray[1];
        yield return new WaitForSeconds(.2f);
        spriteRenderer.sprite = spriteArray[2];
        yield return new WaitForSeconds(.2f);
        spriteRenderer.sprite = spriteArray[3];
        yield return new WaitForSeconds(.2f);
        spriteRenderer.sprite = spriteArray[4];
        yield return new WaitForSeconds(.2f);
        shootSound.Play();
        Instantiate(enemyShield, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        spriteRenderer.sprite = spriteArray[0];
    }

    IEnumerator Shoot()
    {
        if (shooting == false)
        {
            shooting = true;
            shootSound.Play();
            spriteRenderer.sprite = spriteArray[0];
            Instantiate(Shield, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
            spriteRenderer.sprite = spriteArray[1];
            yield return new WaitForSeconds(2.0f);
            spriteRenderer.sprite = spriteArray[2];
            yield return new WaitForSeconds(2.0f);
            spriteRenderer.sprite = spriteArray[3];
            yield return new WaitForSeconds(2.0f);
            spriteRenderer.sprite = spriteArray[4];
            shooting = false;
        }
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
