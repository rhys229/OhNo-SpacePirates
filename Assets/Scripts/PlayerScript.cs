using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private float xPos;

    //private float yPos;
    public float speed = 10f;
    public float yspeed = 10f;
    public float leftWall, rightWall, topwall, bottomwall;
    public int middleShipID = 1;
    public GameObject leftAbductor;
    public GameObject midAbductor;
    public GameObject rightAbductor;
    public GameObject slotCleaner;
    public KeyCode leftAbduct;
    public KeyCode rightAbduct;
    public KeyCode midAbduct;
    public bool leftSlotFull = false;
    public bool midSlotFull = false;
    public bool rightSlotFull = false;

    public bool lockMovement;

    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;

    public GameObject explosion;

    public bool godMode;
    public bool vulnerable;
    public bool abducting;
    
    public AudioSource Abduction;
    // Start is called before the first frame update
    void Start()
    {
        leftSlotFull = false;
        midSlotFull = false;
        rightSlotFull = false;
        lockMovement = false;
        abducting = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }


    // Update is called once per frame
    void Update()
    {
        updateShip();
        if (lockMovement == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (xPos > leftWall)
                {
                    xPos -= speed * Time.deltaTime;
                }
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (xPos < rightWall)
                {
                    xPos += speed * Time.deltaTime;
                }
            }
            /*
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (yPos < topwall)
                {
                    yPos += yspeed*Time.deltaTime;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (yPos > bottomwall)
                {
                    yPos -= yspeed*Time.deltaTime;
                }
            }
            */

            Vector3 moveVector = new Vector3(xPos, -4.3f, 0);
            transform.localPosition = moveVector;
        }

        if (Input.GetKeyDown(leftAbduct))
        {
            if (leftSlotFull == false)
            {
                if (!abducting)
                {
                    StartCoroutine("leftAbduction");
                }
            }
        }

        if (Input.GetKeyDown(midAbduct))
        {
            if (midSlotFull == false)
            {
                if (!abducting)
                {
                    StartCoroutine("midAbduction");
                }
            }
        }

        if (Input.GetKeyDown(rightAbduct))
        {
            if (rightSlotFull == false)
            {
                if (!abducting)
                {
                    StartCoroutine("rightAbduction");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (leftSlotFull == true)
            {
                Instantiate(slotCleaner, new Vector2(transform.position.x - .7f, transform.position.y + .5f),
                    Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (midSlotFull == true)
            {
                Instantiate(slotCleaner, new Vector2(transform.position.x, transform.position.y + .5f),
                    Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (rightSlotFull == true)
            {
                Instantiate(slotCleaner, new Vector2(transform.position.x + .7f, transform.position.y + .5f),
                    Quaternion.identity);
            }
        }


    }

    IEnumerator leftAbduction()
    {
        Abduction.Play();
        abducting = true;
        GameObject leftAbductorInstantiated = Instantiate(leftAbductor,
            new Vector2(transform.position.x - .7f, transform.position.y + .9f), Quaternion.identity);
        lockMovement = true;
        yield return new WaitForSeconds(1.5f);
        lockMovement = false;
        Destroy(leftAbductorInstantiated);
        abducting = false;
    }

    IEnumerator midAbduction()
    {
        Abduction.Play();
        abducting = true;
        GameObject midAbductorInstantiated = Instantiate(midAbductor,
            new Vector2(transform.position.x, transform.position.y + 0.9f), Quaternion.identity);
        lockMovement = true;
        yield return new WaitForSeconds(1.5f);
        lockMovement = false;
        Destroy(midAbductorInstantiated);
        abducting = false;
    }

    IEnumerator rightAbduction()
    {
        Abduction.Play();
        abducting = true;
        GameObject rightAbductorInstantiated = Instantiate(rightAbductor,
            new Vector2(transform.position.x + .7f, transform.position.y + 0.9f), Quaternion.identity);
        lockMovement = true;
        yield return new WaitForSeconds(1.5f);
        lockMovement = false;
        Destroy(rightAbductorInstantiated);
        abducting = false;
    }
    

    public void loadShip(int slot)
    {
        if (slot == 1)
        {
            leftSlotFull = true;
            lockMovement = false;
        }

        if (slot == 2)
        {
            midSlotFull = true;
            lockMovement = false;
        }

        if (slot == 3)
        {
            rightSlotFull = true;
            lockMovement = false;
        }
    }

    public void unloadShip(int slot)
    {
        if (slot == 1)
        {
            leftSlotFull = false;
        }

        if (slot == 2)
        {
            midSlotFull = false;
        }

        if (slot == 3)
        {
            rightSlotFull = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy Projectile")
        {
            if (vulnerable)
            {
                StartCoroutine("gameover");
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (vulnerable)
            {
                StartCoroutine("gameover");
            }
        }
    }

    public void gameOver() 
    { 
        StartCoroutine("gameover");
    }

    public void updateShip() 
    {
        if ((leftSlotFull == true) && (midSlotFull == true) && (rightSlotFull == true))
        {
            spriteRenderer.sprite = spriteArray[3];
            vulnerable = false;
        }  
        if ((leftSlotFull == true) && (midSlotFull == false) && (rightSlotFull == true))
        {
            spriteRenderer.sprite = spriteArray[2];
            vulnerable = false;
        }  
        if ((leftSlotFull == false) && (midSlotFull == true) && (rightSlotFull == true))
        {
            spriteRenderer.sprite = spriteArray[2];
            vulnerable = false;
        }  
        if ((leftSlotFull == true) && (midSlotFull == true) && (rightSlotFull == false))
        {
            spriteRenderer.sprite = spriteArray[2];
            vulnerable = false;
        }  
        if ((leftSlotFull == true) && (midSlotFull == false) && (rightSlotFull == false))
        {
            spriteRenderer.sprite = spriteArray[1];
            vulnerable = false;
        } 
        if ((leftSlotFull == false) && (midSlotFull == true) && (rightSlotFull == false))
        {
            spriteRenderer.sprite = spriteArray[1];
            vulnerable = false;
        } 
        if ((leftSlotFull == false) && (midSlotFull == false) && (rightSlotFull == true))
        {
            spriteRenderer.sprite = spriteArray[1];
            vulnerable = false;
        }

        if ((leftSlotFull == false) && (midSlotFull == false) && (rightSlotFull == false) && (godMode == false))
        {
            spriteRenderer.sprite = spriteArray[0];
            vulnerable = true;
        }
    }

    IEnumerator gameover()
    {
        Instantiate(explosion, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(2f);
        GameObject[] enemyObjects;
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyObjects)
        {
            GameObject.Destroy(enemy);
        }
        GameObject[] allyObjects;
        allyObjects = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject ally in allyObjects)
        {
            GameObject.Destroy(ally);
        }
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene("GameOver");
    }
    
}

