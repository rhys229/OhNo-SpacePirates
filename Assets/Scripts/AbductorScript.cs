using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbductorScript : MonoBehaviour
{
    public BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        StartCoroutine("Abduct");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Abduct()
    {
        collider.size = new Vector2(.4f,.2f);
        yield return new WaitForSeconds(.2f);
        collider.size = new Vector2(.4f,.4f);
        yield return new WaitForSeconds(.2f);
        collider.size = new Vector2(.4f,.6f);
        yield return new WaitForSeconds(.2f);
        collider.size = new Vector2(.4f,.8f);
        yield return new WaitForSeconds(.1f);
        collider.size = new Vector2(.4f,1f);
    }
}
