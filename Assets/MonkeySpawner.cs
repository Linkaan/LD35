using UnityEngine;
using System.Collections;

public class MonkeySpawner : MonoBehaviour {

    public new Camera camera;
    public GameObject monkeyPrefab;
    public float maxMonkeyCount;

    private float monkeyCount;
    private float lastCountUpdate;
	
	void Update()
    {
        if (Time.time - lastCountUpdate > 1.0f)
        {
            monkeyCount = GameObject.FindGameObjectsWithTag("Monkey").Length;
            lastCountUpdate = Time.time;
            Debug.Log(monkeyCount);
        }

        if (monkeyCount < maxMonkeyCount)
        {
            float randX = Random.Range(250f / 2f - 122.9f / 2f, 250f / 2f - 132.9f / 2f + 130f);
            float randZ = Random.Range(250f / 2f - 115.5f / 2f, 250f / 2f - 125.5f / 2f + 140f);        
            Vector3 randPos = new Vector3(randX, 3f, randZ);
            Bounds randBounds = new Bounds(randPos, monkeyPrefab.GetComponent<BoxCollider>().bounds.size);
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            if (!GeometryUtility.TestPlanesAABB(planes, randBounds))
            {
                Instantiate(monkeyPrefab, randPos, monkeyPrefab.transform.rotation);
                monkeyCount++;
            }            
        }
    }
}
