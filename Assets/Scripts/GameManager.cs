using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance != null)
            {
                return _instance;
            }
            else
            {
                GameObject gameManager = new GameObject("GameManager");
                _instance = gameManager.AddComponent<GameManager>();
                return _instance;
            }
        }
    }
     
    public float pointsPerUnitTraveled = 1.0f;
    public float gameSpeed = 10.0f;
    public string titleScreenName = "TitleScreen";

    [HideInInspector]
    public int previousScore = 0;

    private float score = 0.0f;
    private static float highScore = 0.0f;
    private bool gameOver = false;
    private bool hasSaved = false;

    // Use this for initialization
    void Start()
    {
        if(_instance != this)
        {
            if(_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        LoadHighScore();
        DontDestroyOnLoad(gameObject);  //by default, everything is destroyed between scenes
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Application.loadedLevelName != titleScreenName)
        {
            if(GameObject.FindGameObjectWithTag("Player") == null)
            {
                gameOver = true;
                if(gameOver)
                {
                    if(!hasSaved)
                    {
                        SaveHighScore();
                        previousScore = (int)score;
                        hasSaved = true;
                    }
                    if(Input.anyKeyDown)
                    {
                        Application.LoadLevel(titleScreenName);
                    }
                }
            }

            if(!gameOver)
            {
                score += pointsPerUnitTraveled * gameSpeed * Time.deltaTime;
                if(score > highScore)
                {
                    highScore = score;
                }
            }
        }
        else
        {
            //Reset stuff for next game
            ResetGame();
        }
	}

    void ResetGame()
    {
        score = 0.0f;
        gameOver = false;
        hasSaved = false;
    }

    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("Highscore");
    }
    void SaveHighScore()
    {
        PlayerPrefs.SetInt("Highscore", (int)highScore);
        PlayerPrefs.Save();
    }

    void OnGUI()
    {
        if(Application.loadedLevelName != titleScreenName)
        {
            int currentScore = (int)score;
            int currentHighScore = (int)highScore;
            GUILayout.Label("Score: " + currentScore.ToString());
            GUILayout.Label("High Score: " + currentHighScore.ToString());
            if(gameOver == true)
            {
                GUILayout.Label("Game Over! Press any key to quit!");
            }
        }
    }
}
