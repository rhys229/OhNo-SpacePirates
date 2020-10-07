using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TwinShooterScript : MonoBehaviour
{
    public GameObject twinProjectile;
    public GameObject enemyTwinProjectile;
    public int slot;
    public bool playerControlled = false;
    public GameObject playerObject;

    public float speed = 1f;
    public float yspeed = 2f;
    public float amplitude = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        float delay = Random.Range(2f, 10f);
        float rate = Random.Range(2f, 8f);
        InvokeRepeating("Fire", delay, rate);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControlled == true)
        {
            
            if (slot == 1)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Instantiate(twinProjectile, new Vector2(transform.position.x, transform.position.y+ 0.5f), Quaternion.identity);
                }
            }
            else if (slot == 2)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    Instantiate(twinProjectile, new Vector2(transform.position.x, transform.position.y+ 0.5f), Quaternion.identity);
                }
            }
            else if (slot == 3)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    Instantiate(twinProjectile, new Vector2(transform.position.x, transform.position.y+ 0.5f), Quaternion.identity);
                }
            }
        }
        else if (playerControlled == false)
        {
            float offsetx = Mathf.Sin(Time.time * speed) * amplitude / 20;
            float offsety = -.2f*(Time.time * yspeed);
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
            transform.position = new Vector2(playerObject.transform.position.x-.7f, playerObject.transform.position.y);
            transform.Rotate(180,0,0);
            transform.parent = playerObject.transform;
            slot = 1;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "midAbductor")
        {
            playerControlled = true;
            transform.gameObject.tag = "Ally";
            transform.position = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
            transform.Rotate(180,0,0);
            transform.parent = playerObject.transform;
            slot = 2;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "rightAbductor")
        {
            playerControlled = true;
            transform.gameObject.tag = "Ally";
            transform.position = new Vector2(playerObject.transform.position.x+.7f, playerObject.transform.position.y);
            transform.Rotate(180,0,0);
            transform.parent = playerObject.transform;
            slot = 3;
            Destroy(other.gameObject);
        }
    }

    private void Fire()
    {
        int i = Random.Range(0, 100);
        if (i > 80 && playerControlled == false) 
        {
            Instantiate(enemyTwinProjectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
    }

    public bool IsPlayerControlled()
    {
        return playerControlled;
    }
}
