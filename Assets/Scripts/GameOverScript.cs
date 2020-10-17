using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public Text FinalScore;
    public GameObject ScoreManager;
    public ScoreManagerScript ScoreManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("gameover");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Intro");
        }
    }

    IEnumerator gameover()
    {
        yield return new WaitForSeconds(.1f);
        ScoreManager = GameObject.Find("ScoreManager");
        ScoreManagerScript = ScoreManager.GetComponent<ScoreManagerScript>();
        int finalscore = ScoreManagerScript.score;
        FinalScore.text = finalscore.ToString();
    }
}
