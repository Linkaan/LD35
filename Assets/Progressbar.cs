using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Progressbar : MonoBehaviour {

    public ArmController controller;
    public GameObject segmentPart;
    public GameObject endPart;    

    private float lastValue;
    private List<GameObject> segments;

    void Start ()
    {
        segments = new List<GameObject>();
    }
	
	void Update () {
        float value = Mathf.Max(controller.leftArmSegmentCount, controller.rightArmSegmentCount);
        if (value != lastValue)
        {
            while (segments.Count > 0)
            {
                DestroyImmediate(segments[0]);
                segments.RemoveAt(0);
            }
            if (value <= 0) return;
            float posX = 0;
            for (int i = 0; i < value-1; i++)
            {
                GameObject segment = Instantiate(segmentPart);
                segment.GetComponent<Image>().rectTransform.position = new Vector2(posX, 0f);
                segment.transform.SetParent(transform);
                segments.Add(segment);
                posX += 32;
            }
            GameObject end = Instantiate(endPart);
            end.GetComponent<Image>().rectTransform.position = new Vector2(posX, 0f);
            end.transform.SetParent(transform);
            segments.Add(end);
        }
        lastValue = value;
    }
}
