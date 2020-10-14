using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagerScript : MonoBehaviour
{
    public GameObject manager;
    public EnemyManagerScript ManagerScript;
    public bool active;

    public int score;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("EnemyManager");
        ManagerScript = manager.GetComponent<EnemyManagerScript>();
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            score = ManagerScript.score;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void kill()
    {
        active = false;
    }
}
