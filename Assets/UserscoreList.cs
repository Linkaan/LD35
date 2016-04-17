using UnityEngine;
using UnityEngine.UI;

public class UserscoreList : MonoBehaviour {

    public GameObject userscoreEntryPrefab;
    public ScoreManager scoreManager;

    public void UpdateScoreboard()
    {
        Debug.Log("UpdateScoreboard() called");
        while (this.transform.childCount > 0)
        {
            Transform child = this.transform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }

        string[] userids = scoreManager.GetUserIDs("score");
        foreach (string userid in userids)
        {
            Transform newChild = Instantiate(userscoreEntryPrefab).transform;
            newChild.SetParent(transform);
            string username = scoreManager.GetUserName(userid);
            string usernameText = "ê\"" + username;
            for (int i = 0; i < (20 - username.Length); i++)
            {
                usernameText += "\"";
            }
            usernameText += "ë";
            newChild.Find("Username").GetComponent<Text>().text = usernameText;
            newChild.Find("Highscore").GetComponent<Text>().text = scoreManager.GetScore(userid, "score").ToString();
            newChild.Find("Time").GetComponent<Text>().text = scoreManager.GetScore(userid, "time").ToString();
        }
    }
}
