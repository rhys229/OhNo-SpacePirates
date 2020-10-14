using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SplitShooterScript : MonoBehaviour
{
    public GameObject leftProjectile;
    public GameObject midProjectile;
    public GameObject rightProjectile;
    public GameObject enemyleftProjectile;
    public GameObject enemymidProjectile;
    public GameObject enemyrightProjectile;
    public int slot;
    public bool playerControlled = false;
    public GameObject playerObject;

    public float speed = 1f;
    public float yspeed = 2f;
    public float amplitude = 0.5f;

    public Quaternion leftRotate = Quaternion.Euler(0,30,0);
    public Quaternion rightRotate = Quaternion.Euler(0,-30,0);
    
    public GameObject manager;
    public EnemyManagerScript ManagerScript;

    public bool shooting = false;
    
    public PlayerScript playerScript;
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
            float offsetx = Mathf.Sin(Time.time * speed) * amplitude / 20;
            float offsety = -.2f*(yspeed);
            Vector3 move = new Vector3(offsetx, y: offsety,0);
            transform.position = transform.position+move;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
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
    

    private void Fire()
    {
        int i = Random.Range(0, 100);
        if (i > 80 && playerControlled == false)
        {
            StartCoroutine("EnemyShoot");
        }
    }

    public bool IsPlayerControlled()
    {
        return playerControlled;
    }

    IEnumerator Shoot()
    {
        if (shooting == false)
        {
            shooting = true;
            Instantiate(leftProjectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Instantiate(rightProjectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Instantiate(midProjectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            yield return new WaitForSeconds(.2f);
            shooting = false;
        }
    }

    IEnumerator EnemyShoot()
    {
        yield return new WaitForSeconds(.6f);
        Instantiate(enemyleftProjectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Instantiate(enemyrightProjectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Instantiate(enemymidProjectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    public void OnDestroy()
    {
        if (playerControlled)
        {
            playerScript.unloadShip(slot);
        }
    }
}
