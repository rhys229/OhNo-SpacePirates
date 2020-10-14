using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagerScript : MonoBehaviour
{
    public GameObject manager;
    public EnemyManagerScript ManagerScript;

    public int score;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("EnemyManager");
        ManagerScript = manager.GetComponent<EnemyManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        score = ManagerScript.score;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
