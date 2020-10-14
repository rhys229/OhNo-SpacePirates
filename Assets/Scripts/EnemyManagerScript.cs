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
    
    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        Level.gameObject.SetActive(false);
        LevelText.gameObject.SetActive(false);
        PirateText.gameObject.SetActive(false);
    }

    

    void Update()
    {
        LevelStarter();
        Score.text = score.ToString();

    }
    

    void LevelStarter()
    {
        if (level == 0)
        {
            Debug.Log("Starting level 1");
            level++;
            StartCoroutine("levelTransfer",level);
        }
        if (tally >= 24*(level+pirateLevel))
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
        Level.text = level.ToString();
        Level.gameObject.SetActive(true);
        LevelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        int[,] levelMatrix = levelHolder(level);
        enemySpawner(levelMatrix);
        Level.gameObject.SetActive(false);
        LevelText.gameObject.SetActive(false);
    }
    IEnumerator pirateTransfer(int PirateLevel)
    {
        Level.text = level.ToString();
        PirateText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        int[,] piratelevelMatrix = piratelevelHolder(PirateLevel);
        enemySpawner(piratelevelMatrix);
        PirateText.gameObject.SetActive(false);
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
                        Transform bb = Instantiate(Bomber);
                        Vector2 bbloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        bb.transform.position = bbloc;
                        break;
                    case 4:
                        Transform rb = Instantiate(RayBeamShip);
                        Vector2 rbloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        rb.transform.position = rbloc;
                        break;
                    case 5:
                        Transform st = Instantiate(StealthShip);
                        Vector2 stloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        st.transform.position = stloc;
                        break;
                    case 6:
                        Transform sk = Instantiate(SkullShip);
                        Vector2 skloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        sk.transform.position = skloc;
                        break;
                    case 7:
                        Transform ps = Instantiate(PirateSpear);
                        Vector2 psloc = new Vector2(xOrigin + (i * xSpacing), (yOrigin + 6) - (j * ySpacing));
                        ps.transform.position = psloc;
                        break;
                    case 8:
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
                return new int [,] {{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1}};
                break;
            case 2:
                return new int [,] {{1,1,2,2,2,2,1,1},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1}};
                break;
            case 3:
                return new int [,] {{2,2,2,1,1,2,2,2},{1,1,2,2,2,2,1,1},{1,1,1,1,1,1,1,1}}; 
                break;
            case 4:
                return new int [,] {{3,2,2,3,3,2,2,3},{1,1,2,2,2,2,1,1},{1,1,1,1,1,1,1,1}}; 
                break;
            case 5:
                return new int [,] {{3,2,2,3,3,2,2,3},{2,2,2,2,2,2,2,2},{2,1,1,1,1,1,1,2}}; 
                break;
            case 6:
                return new int [,] {{3,3,3,2,2,3,3,3},{2,2,2,2,2,2,2,2},{1,2,1,2,1,2,1,2}}; 
                break;
            case 7:
                return new int [,] {{3,3,3,4,4,3,3,3},{2,2,2,2,2,2,2,2},{1,2,1,2,1,2,1,2}}; 
                break;
            case 8:
                return new int [,] {{3,3,4,4,4,4,3,3},{2,2,2,2,2,2,2,2},{2,2,2,2,2,2,2,2}};
                break;
            case 9:
                return new int [,] {{3,3,4,5,5,4,3,3},{2,2,2,2,2,2,2,2},{2,2,2,2,2,2,2,2}};
                break;
            case 10:
                return new int [,] {{3,4,4,5,5,4,4,3},{2,2,3,3,3,3,2,2},{2,2,2,2,2,2,2,2}};
                break;
        }
        return new int[,]{{6,7,6,7,6,7,6,7},{0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0}};;
    }
    int[,] levelHolder(int level)
    {
        switch (level)
        {
            case 1:
                return new int [,] {{4,4,5,5,5,5,4,4},{3,3,3,2,2,3,3,3},{1,1,1,1,1,1,1,1}};
                break;
            case 2:
                return new int [,] {{1,1,2,2,2,2,1,1},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1}};
                break;
            case 3:
                return new int [,] {{2,2,2,1,1,2,2,2},{1,1,2,2,2,2,1,1},{1,1,1,1,1,1,1,1}}; 
                break;
            case 4:
                return new int [,] {{3,2,2,3,3,2,2,3},{1,1,2,2,2,2,1,1},{1,1,1,1,1,1,1,1}}; 
                break;
            case 5:
                return new int [,] {{3,2,2,3,3,2,2,3},{2,2,2,2,2,2,2,2},{2,1,1,1,1,1,1,2}}; 
                break;
            case 6:
                return new int [,] {{3,3,3,2,2,3,3,3},{2,2,2,2,2,2,2,2},{1,2,1,2,1,2,1,2}}; 
                break;
            case 7:
                return new int [,] {{3,3,3,4,4,3,3,3},{2,2,2,2,2,2,2,2},{1,2,1,2,1,2,1,2}}; 
                break;
            case 8:
                return new int [,] {{3,3,4,4,4,4,3,3},{2,2,2,2,2,2,2,2},{2,2,2,2,2,2,2,2}};
                break;
            case 9:
                return new int [,] {{3,3,4,5,5,4,3,3},{2,2,2,2,2,2,2,2},{2,2,2,2,2,2,2,2}};
                break;
            case 10:
                return new int [,] {{3,4,4,5,5,4,4,3},{2,2,3,3,3,3,2,2},{2,2,2,2,2,2,2,2}};
                break;
        }
        return new int[,]{{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1}};;
    }
        
}
    


