using UnityEngine;
using System.Collections;

public class Monkey : MonoBehaviour {

    public float speed;
    public float smoothTime;
    public Ragdoll root;

    public AudioClip[] monkeysfx;

    private Player player;
    private Vector3 velocity, direction, lastDirection;
    private bool triggered;
    private float lastRotTime;
    private float yPosition;

	void Start () {
        velocity = Vector3.zero;
        direction = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up) * Vector3.right;
        direction *= speed;
        lastDirection = direction;
        triggered = false;
        player = GameObject.FindObjectOfType<Player>();
        yPosition = this.transform.position.y;
	}
	
	void Update () {
	    if (triggered) return;
        if ((Time.time - lastRotTime) >= 5 && Random.value >= 0.5f) {
            direction = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up) * Vector3.right;
            direction *= speed;
            lastRotTime = Time.time;
        }

        lastDirection = Vector3.SmoothDamp(lastDirection, direction, ref velocity, smoothTime);
        transform.position += lastDirection;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lastDirection, Vector3.up), 10.0f * Time.deltaTime);
        transform.eulerAngles = new Vector3(90, transform.eulerAngles.y + 45, 0);
	}

    void FixedUpdate()
    {
        float limitX1 = 250f / 2f - 122.9f / 2f;
        float limitX2 = 250f / 2f - 122.9f / 2f + 130f;
        float limitZ1 = 250f / 2f - 115.5f / 2f;
        float limitZ2 = 250f / 2f - 115.5f / 2f + 140f;

        Vector3 forwardPos = transform.position + direction * 10;
        if (forwardPos.x < limitX1 || forwardPos.x > limitX2 || forwardPos.z < limitZ1 || forwardPos.z > limitZ2)
        {
            direction = -direction;
            lastRotTime = Time.time;
        }
    }

    void LateUpdate()
    {
        this.transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        player.OnMonkeyHit();
        AudioClip audiosfx = monkeysfx[Random.Range(0, monkeysfx.Length)];
        AudioSource source = gameObject.GetComponent<AudioSource>();
        source.clip = audiosfx;
        source.Play();
        triggered = true;
        gameObject.tag = "Hit";
        Destroy(gameObject, 15);
        Destroy(GetComponent<Animator>());
        root.InstantiateRagdoll(null);
        root.GetComponent<Rigidbody>().AddExplosionForce(40.0f, other.transform.position, 10.0f);
    }
}
