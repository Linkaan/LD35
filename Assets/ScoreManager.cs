using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ScoreManager : MonoBehaviour {

    public GameObject loading;
    public GameObject scoreBoard;
    public Button button;
    public InputField input;

    private string value;
    private Dictionary<string, Userscore> userScores;
    private dreamloLeaderBoard dl;

    private float waitTime;

    enum leaderboardState
    {
        waiting,
        leaderboard,
        done
    };

    leaderboardState ls;

    public struct Userscore
    {
        public Dictionary<string, int> scores;
        public string name;
    }

	void Start () {
        this.dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
        this.ls = leaderboardState.waiting;
	}
	
	void Update () {
	    if (ls == leaderboardState.leaderboard && Time.time - waitTime > 2f)
        {
            List<dreamloLeaderBoard.Score> scoreList = dl.ToListHighToLow();
            if (scoreList != null)
            {
                int maxToDisplay = 20;
                int count = 0;
                foreach (dreamloLeaderBoard.Score currentScore in scoreList)
                {
                    count++;
                    SetScore(currentScore.playerName, currentScore.playerName, "score", currentScore.score);
                    SetScore(currentScore.playerName, currentScore.playerName, "time", currentScore.score);

                    if (count >= maxToDisplay) break;
                }
                button.transform.parent.gameObject.SetActive(false);
                loading.SetActive(false);
                scoreBoard.SetActive(true);
                scoreBoard.GetComponentInChildren<UserscoreList>().UpdateScoreboard();
                ls = leaderboardState.done;
            }
        }          
	}

    public int GetScore(string userid, string scoreType)
    {
        if (userScores == null) userScores = new Dictionary<string, Userscore>();
        
        if (!userScores.ContainsKey(userid) || !userScores[userid].scores.ContainsKey(scoreType))
        {
            return 0;
        }
        return userScores[userid].scores[scoreType];
    }

    private void SetScore(string userid, string username, string scoreType, int value)
    {
        if (userScores == null) userScores = new Dictionary<string, Userscore>();

        if (!userScores.ContainsKey(userid))
        {
            userScores[userid] = new Userscore() { name = username, scores = new Dictionary<string, int>() };
        }
        userScores[userid].scores[scoreType] = value;
    }

    public string GetUserName(string userid)
    {
        return userScores[userid].name;
    }

    public string[] GetUserIDs(string sortingType)
    {
        if (userScores == null) userScores = new Dictionary<string, Userscore>();

        return userScores.Keys.OrderByDescending(n => GetScore(n, sortingType)).ToArray();
    }

    public void ToggleButton()
    {
        value = input.text;
        Debug.Log("toggle button value: " + value);        
        if (string.IsNullOrEmpty(value))
        {
            Color c = button.GetComponentInChildren<Text>().color;
            c.a = 0.5f;
            button.GetComponentInChildren<Text>().color = c;
            button.interactable = false;
        }
        else
        {
            Color c = button.GetComponentInChildren<Text>().color;
            c.a = 1.0f;
            button.GetComponentInChildren<Text>().color = c;
            button.interactable = true;
        }
    }

    public void LoadScene(int scene)
    {
        Application.LoadLevel(scene);
    }

    public void LoadLeaderboard()
    {
        dl.LoadScores();
        ls = leaderboardState.leaderboard;
        waitTime = Time.time;
    }

    public void SubmitScore()
    {
        Debug.Log("SubmitScore() called");
        button.transform.parent.gameObject.SetActive(false);
        loading.SetActive(true);
        Player player = GameObject.FindObjectOfType<Player>();
        int score = (int) player.monkeysHit;
        int time = (int) player.time;
        dl.AddScore(value, score, time);        
        ls = leaderboardState.leaderboard;
        waitTime = Time.time;
    }
}
