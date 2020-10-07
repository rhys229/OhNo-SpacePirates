using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    private float     xPos;
    public float      speed = .05f;
    public float      leftWall, rightWall;
    public int middleShipID = 1;
    public GameObject leftAbductor;
    public GameObject midAbductor;
    public GameObject rightAbductor;
    public KeyCode leftAbduct;
    public KeyCode rightAbduct;
    public KeyCode midAbduct;

    public bool lockMovement;
  

    // Start is called before the first frame update
    void Start()
    {
        lockMovement = false;
    }
    

    // Update is called once per frame
    void Update() {
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
        }

        if (Input.GetKeyDown(leftAbduct))
        {
            StartCoroutine("leftAbduction");
        }
        if (Input.GetKeyDown(midAbduct))
        {
            StartCoroutine("midAbduction");
        }
        if (Input.GetKeyDown(rightAbduct))
        {
            StartCoroutine("rightAbduction");
        }

        transform.localPosition = new Vector3(xPos, transform.position.y, 0);
    }
    IEnumerator leftAbduction()
    {
        GameObject leftAbductorInstantiated = Instantiate(leftAbductor, new Vector2(transform.position.x-.7f, transform.position.y+ .7f), Quaternion.identity);
        lockMovement = true;
        yield return new WaitForSeconds(2);
        lockMovement = false;
        Destroy(leftAbductorInstantiated);
    }
    IEnumerator midAbduction()
    {
        GameObject midAbductorInstantiated = Instantiate(midAbductor, new Vector2(transform.position.x, transform.position.y+ 0.7f), Quaternion.identity);
        lockMovement = true;
        yield return new WaitForSeconds(2);
        lockMovement = false;
        Destroy(midAbductorInstantiated);
    }
    IEnumerator rightAbduction()
    {
        GameObject rightAbductorInstantiated = Instantiate(rightAbductor, new Vector2(transform.position.x+.7f, transform.position.y+ 0.7f), Quaternion.identity);
        lockMovement = true;
        yield return new WaitForSeconds(2);
        lockMovement = false;
        Destroy(rightAbductorInstantiated);
    }
    
}

