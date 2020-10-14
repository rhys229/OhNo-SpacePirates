using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
    private float     xPos;
    private float yPos;
    public float      speed = .05f;
    public float      leftWall, rightWall, topwall, bottomwall;
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

    public GameObject leftStarterShip;
    public GameObject rightStarterShip;

    public bool godMode;
    // Start is called before the first frame update
    void Start()
    {
        leftSlotFull = true;
        midSlotFull = false;
        rightSlotFull = true;
        Instantiate(leftStarterShip, new Vector2(transform.position.x - .7f, transform.position.y+.4f), Quaternion.identity);
        Instantiate(rightStarterShip, new Vector2(transform.position.x + .7f, transform.position.y+.4f), Quaternion.identity);
        lockMovement = false;
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
                    xPos -= speed;
                }
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (xPos < rightWall)
                {
                    xPos += speed;
                }
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (yPos < topwall)
                {
                    yPos += speed;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (yPos > bottomwall)
                {
                    yPos -= speed;
                }
            }
        }

        if (Input.GetKeyDown(leftAbduct))
        {
            if (leftSlotFull == false)
            {
                StartCoroutine("leftAbduction");
            }
        }
        if (Input.GetKeyDown(midAbduct))
        {
            if (midSlotFull == false)
            {
                StartCoroutine("midAbduction");
            }
        }
        if (Input.GetKeyDown(rightAbduct))
        {
            if (rightSlotFull == false)
            {
                StartCoroutine("rightAbduction");
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (leftSlotFull == true)
            {
                Instantiate(slotCleaner, new Vector2(transform.position.x-.7f, transform.position.y+.5f), Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (midSlotFull == true)
            {
                Instantiate(slotCleaner, new Vector2(transform.position.x, transform.position.y+.5f), Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (rightSlotFull == true)
            {
                Instantiate(slotCleaner, new Vector2(transform.position.x+.7f, transform.position.y+.5f), Quaternion.identity);
            }
        }

            transform.localPosition = new Vector3(xPos, yPos, 0);
    }
    IEnumerator leftAbduction()
    {
        GameObject leftAbductorInstantiated = Instantiate(leftAbductor, new Vector2(transform.position.x-.7f, transform.position.y+ .7f), Quaternion.identity);
        lockMovement = true;
        yield return new WaitForSeconds(1.5f);
        lockMovement = false;
        Destroy(leftAbductorInstantiated);
    }
    IEnumerator midAbduction()
    {
        GameObject midAbductorInstantiated = Instantiate(midAbductor, new Vector2(transform.position.x, transform.position.y+ 0.7f), Quaternion.identity);
        lockMovement = true;
        yield return new WaitForSeconds(1.5f);
        lockMovement = false;
        Destroy(midAbductorInstantiated);
    }
    IEnumerator rightAbduction()
    {
        GameObject rightAbductorInstantiated = Instantiate(rightAbductor, new Vector2(transform.position.x+.7f, transform.position.y+ 0.7f), Quaternion.identity);
        lockMovement = true;
        yield return new WaitForSeconds(1.5f);
        lockMovement = false;
        Destroy(rightAbductorInstantiated);
    }

    public void loadShip(int slot)
    {
        if (slot == 1)
        {
            leftSlotFull = true;
        }
        if (slot == 2)
        {
            midSlotFull = true;
        }
        if (slot == 3)
        {
            rightSlotFull = true;
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
    public void updateShip() 
    {
        if ((leftSlotFull == true) && (midSlotFull == true) && (rightSlotFull == true))
        {
            spriteRenderer.sprite = spriteArray[2];
        }  
        if ((leftSlotFull == true) && (midSlotFull == false) && (rightSlotFull == true))
        {
            spriteRenderer.sprite = spriteArray[1];
        }  
        if ((leftSlotFull == false) && (midSlotFull == true) && (rightSlotFull == true))
        {
            spriteRenderer.sprite = spriteArray[1];
        }  
        if ((leftSlotFull == true) && (midSlotFull == true) && (rightSlotFull == false))
        {
            spriteRenderer.sprite = spriteArray[1];
        }  
        if ((leftSlotFull == true) && (midSlotFull == false) && (rightSlotFull == false))
        {
            spriteRenderer.sprite = spriteArray[0];
        } 
        if ((leftSlotFull == false) && (midSlotFull == true) && (rightSlotFull == false))
        {
            spriteRenderer.sprite = spriteArray[0];
        } 
        if ((leftSlotFull == false) && (midSlotFull == false) && (rightSlotFull == true))
        {
            spriteRenderer.sprite = spriteArray[0];
        }

        if ((leftSlotFull == false) && (midSlotFull == false) && (rightSlotFull == false) && (godMode == false))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    
}

