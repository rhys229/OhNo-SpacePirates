using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyManagerScript : MonoBehaviour
{
    public Transform SingleShooter;
    public Transform TwinShooter;
    public Transform Bomber;
    public Transform SkullShip;
    public Transform PirateSpear;
    public Transform RayBeamShip;
    public Transform SplitShooter;
    public Transform StealthShip;
    public Transform BurstShooter;

    public int score = 0;
    public int tally = 0;

    public int level = 0;
    public int pirateLevel = 0;
    
    public float xSpacing, ySpacing;
    public float xOrigin, yOrigin;
    public int numRows, numColumns;

    public Text Score;
    public Text Level;
    public Text LevelText;
    public Text PirateText;

    public AudioSource MainMusic;
    public AudioSource PirateMusic;
    
    public GameObject scoreManager;
    public ScoreManagerScript scoreManagerScript;

    public bool levelInProgress;
    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        pirateLevel = 0;
        Level.gameObject.SetActive(false);
        LevelText.gameObject.SetActive(false);
        PirateText.gameObject.SetActive(false);
        
        scoreManager = GameObject.Find("ScoreManager");
        scoreManagerScript = scoreManager.GetComponent<ScoreManagerScript>();
    }

    

    void Update()
    {
        LevelStarter();
        Score.text = score.ToString();

    }
    

    void LevelStarter()
    {
        GameObject[] enemyObjects;
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if (level == 0)
        {
            level++;
            Debug.Log("Starting level 1");
            StartCoroutine("levelTransfer",level);
        }

        if (level > 0 && enemyObjects.Length == 0 && levelInProgress == true)
        {
            int i = Random.Range(0, 100);
            if (i < 25 && level > 3)
            {
                MainMusic.Pause();
                PirateMusic.Play();
                pirateLevel++;
                StartCoroutine("pirateTransfer", pirateLevel);
            }
            else
            {
                if (MainMusic.isPlaying == false)
                {
                    MainMusic.Play();
                    PirateMusic.Pause();
                }
                level++;
                StartCoroutine("levelTransfer", level);
            }
        }
        
    }

    IEnumerator levelTransfer(int level)
    {
        levelInProgress = false;
        tally = 0;
        Level.text = level.ToString();
        Level.gameObject.SetActive(true);
        LevelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        int[,] levelMatrix = levelHolder(level);
        enemySpawner(levelMatrix);
        Level.gameObject.SetActive(false);
        LevelText.gameObject.SetActive(false);
        levelInProgress = true;
    }
    IEnumerator pirateTransfer(int PirateLevel)
    {
        levelInProgress = false;
        tally = 0;
        Level.text = level.ToString();
        PirateText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        int[,] piratelevelMatrix = piratelevelHolder(PirateLevel);
        enemySpawner(piratelevelMatrix);
        PirateText.gameObject.SetActive(false);
        levelInProgress = true;
    }

    void enemySpawner(int [,] levelMatrix)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                switch (levelMatrix[j,i])
                {
                    case 1: 
                        Transform ss = Instantiate(SingleShooter);
                        Vector2 ssloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        ss.transform.position = ssloc;
                        break;
                    case 2:
                        Transform ts = Instantiate(TwinShooter);
                        Vector2 tsloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        ts.transform.position = tsloc;
                        break;
                    case 3:
                        Transform bs = Instantiate(BurstShooter);
                        Vector2 bsloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        bs.transform.position = bsloc;
                        break;
                    case 4:
                        Transform bb = Instantiate(Bomber);
                        Vector2 bbloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        bb.transform.position = bbloc;
                        break;
                    case 5:
                        Transform rb = Instantiate(RayBeamShip);
                        Vector2 rbloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        rb.transform.position = rbloc;
                        break;
                    case 6:
                        Transform st = Instantiate(StealthShip);
                        Vector2 stloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        st.transform.position = stloc;
                        break;
                    case 7:
                        Transform sk = Instantiate(SkullShip);
                        Vector2 skloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        sk.transform.position = skloc;
                        break;
                    case 8:
                        Transform ps = Instantiate(PirateSpear);
                        Vector2 psloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        ps.transform.position = psloc;
                        break;
                    case 9:
                        Transform sp = Instantiate(SplitShooter);
                        Vector2 sploc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        sp.transform.position = sploc;
                        break;
                    
                }
            }
        }
        

    }
    
    int[,] piratelevelHolder(int pirateLevel)
    {
        switch (pirateLevel)
        {
            case 1:
                tally = tally + 16;
                return new int [,] {{8,7,8,7,8,7,8,7},{0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0}};
                break;
            case 2:
                tally = tally + 12;
                return new int [,] {{8,8,8,8,8,8,8,8},{0,0,7,7,7,7,0,0},{0,0,0,0,0,0,0,0}};
                break;
            case 3:
                tally = tally + 8;
                return new int [,] {{8,7,8,7,8,7,8,7},{7,7,7,7,7,7,7,7},{0,0,0,0,0,0,0,0}};
                break;
            case 4:
                tally = tally + 8;
                return new int [,] {{8,7,8,7,8,7,8,7},{8,9,8,9,8,9,8,9},{0,0,0,0,0,0,0,0}}; 
                break;
            case 5:
                return new int [,] {{8,7,8,7,8,7,8,7},{8,9,8,9,8,9,8,9},{7,7,7,7,7,7,7,7}};
                break;
        }
        
        return new int[,]{{8,7,8,7,8,7,8,7},{8,9,8,9,8,9,8,9},{7,7,7,7,7,7,7,7}};
    }
    int[,] levelHolder(int level)
    {
        switch (level)
        {
            case 1:
                tally = tally + 16;
                return new int [,] {{1,1,1,1,1,1,1,1},{0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0}};
                break;
            case 2:
                return new int [,] {{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1}};
                break;
            case 3:
                return new int [,] {{1,1,2,2,2,2,1,1},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1}};
                break;
            case 4:
                return new int [,] {{2,2,2,2,2,2,2,2},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1}};
                break;
            case 5:
                return new int [,] {{2,2,2,3,3,2,2,2},{1,1,2,2,2,2,1,1},{1,1,1,1,1,1,1,1}}; 
                break;
            case 6:
                return new int [,] {{3,2,2,3,3,2,2,3},{1,1,2,2,2,2,1,1},{1,1,1,1,1,1,1,1}}; 
                break;
            case 7:
                return new int [,] {{3,2,2,4,4,2,2,3},{2,2,2,2,2,2,2,2},{2,1,1,1,1,1,1,2}}; 
                break;
            case 8:
                return new int [,] {{3,3,3,4,4,3,3,3},{2,2,2,2,2,2,2,2},{1,2,1,2,1,2,1,2}}; 
                break;
            case 9:
                return new int [,] {{3,3,4,4,4,4,3,3},{2,2,3,3,3,3,2,2},{1,2,1,2,1,2,1,2}}; 
                break;
            case 10:
                return new int [,] {{3,3,4,4,4,4,3,3},{2,2,3,3,2,3,2,2},{2,2,2,2,2,2,2,2}};
                break;
            case 11:
                return new int [,] {{3,3,4,5,5,4,3,3},{2,2,2,2,2,2,2,2},{2,2,2,2,2,2,2,2}};
                break;
            case 12:
                return new int [,] {{3,4,4,5,5,4,4,3},{2,2,3,3,3,3,2,2},{2,2,2,2,2,2,2,2}};
                break;
            case 13:
                return new int [,] {{4,4,4,5,5,4,4,4},{3,3,3,3,3,3,3,3},{2,2,2,2,2,2,2,2}};
                break;
            case 14:
                return new int [,] {{4,4,4,5,5,4,4,4},{3,3,3,3,3,3,3,3},{2,2,2,2,2,2,2,2}};
                break;
            case 16:
                return new int [,] {{4,4,5,6,6,5,4,4},{4,4,3,3,3,3,4,4},{2,2,2,2,2,2,2,2}};
                break;
            case 17:
                return new int [,] {{4,4,5,6,6,5,4,4},{4,4,3,3,3,3,4,4},{3,2,3,2,3,2,3,2}};
                break;
            case 18:
                return new int [,] {{4,4,5,6,6,5,4,4},{4,4,3,3,3,3,4,4},{3,3,3,3,3,3,3,3}};
                break;
            case 19:
                return new int [,] {{5,5,6,6,6,6,5,5},{5,5,4,4,4,4,5,5},{3,3,3,3,3,3,3,3}};
                break;
            case 20:
                return new int [,] {{6,5,6,6,6,6,6,5},{5,5,4,4,4,4,5,5},{3,3,3,4,4,3,3,3}};
                break;
        }
        return new int [,] {{6,5,6,6,6,6,6,5},{5,5,4,4,4,4,5,5},{3,3,3,4,4,3,3,3}};
    }

    private void OnDestroy()
    {
        Debug.Log("killing score script");
        scoreManagerScript.kill();
    }
}
    


