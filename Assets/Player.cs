using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {


    // UNITY WHY DID YOU DELETE ME!!! <------------------------------------------------------------

    public Arm arm;
    public Text scoreTextLabel;
    public GameObject inputScore;
    
    public int monkeysHit;
    public float time;
    private float lastMonkeyHit;

    public void Start()
    {
        monkeysHit = 0;
        scoreTextLabel.text = "Ú''''" + monkeysHit + "''''Û";
    }

	public void OnMonkeyHit()
    {
        monkeysHit++;
        scoreTextLabel.text = "Ú''''" + monkeysHit + "''''Û";

        lastMonkeyHit = Time.time;
        if (arm.controller.leftArmSegmentCount < arm.controller.rightArmSegmentCount)
        {
            arm.InsertLeftArmSegment();
        }else
        {
            arm.InsertRightArmSegment();
        }
    }

    void Update()
    {
        if (Time.time - lastMonkeyHit > 2f)
        {
            if (arm.controller.leftArmSegmentCount > arm.controller.rightArmSegmentCount)
            {
                if (arm.DeleteLeftArmSegment())
                {
                    OnLost();
                }
            } else
            {
                if (arm.DeleteRightArmSegment())
                {
                    OnLost();
                }
            }
            lastMonkeyHit = Time.time;
        }
    }

    void OnLost()
    {
        Destroy(arm.controller);
        time = Time.time;
        inputScore.SetActive(true);
    }
}
