using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public Text FinalScore;
    public Text HighScore;
    public Text NewHighScore;
    public GameObject ScoreManager;
    public ScoreManagerScript ScoreManagerScript;
    public InputField nameInput;

    public bool scoreAdded;

    public int finalScore;
    // Start is called before the first frame update
    void Start()
    {
        scoreAdded = false;
        nameInput.onEndEdit.AddListener(delegate {addScore(nameInput); });
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

    void addScore(InputField input)
    {
        if (scoreAdded == false)
        {
            string name;
            if (input.text.Length > 7)
            {
                name = input.text.Substring(0, 7);
            }
            else
            {
                name = input.text;
            }
            Highscores.AddNewHighscore(name, finalScore);
            scoreAdded = true;
        }
    }

    IEnumerator gameover()
    {
        yield return new WaitForSeconds(.1f);
        ScoreManager = GameObject.Find("ScoreManager");
        ScoreManagerScript = ScoreManager.GetComponent<ScoreManagerScript>();
        finalScore = ScoreManagerScript.score;
        FinalScore.text = finalScore.ToString();
    }
}
