using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

    public string[] loadingTexts;
    public float speed;

    private float delta;
    private int index;

	void Start () {
        index = 0;
        GetComponentInChildren<Text>().text = loadingTexts[index];
	}
	
	// Update is called once per frame
	void Update () {
        if ((delta += Time.deltaTime) > speed)
        {
            delta = 0.0f;
            if (++index == loadingTexts.Length) index = 0;
            GetComponentInChildren<Text>().text = loadingTexts[index];
        }        
    }
}
