using UnityEngine;
using System.Collections;

public class ArmController : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;
    public float leftArmSegmentCount;
    public float rightArmSegmentCount;

    private float factor = 1.0f;

    // Update is called once per frame
    void Update () {
        if (!leftHand || !rightHand) return;

        float leftForce = Mathf.Max(10f * leftArmSegmentCount, 40f);
        float rightForce = Mathf.Max(10f * rightArmSegmentCount, 40f);
        if(transform.position.y < 10)
        {
            leftHand.GetComponent<Rigidbody>().AddForce(Vector3.up * leftForce * factor);
            rightHand.GetComponent<Rigidbody>().AddForce(Vector3.up * rightForce * factor);
        }else
        {
            factor /= 1.25f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            leftHand.GetComponent<Rigidbody>().AddForce(Vector3.right * leftForce);
        }

        if (Input.GetKey(KeyCode.D))
        {
            leftHand.GetComponent<Rigidbody>().AddForce(Vector3.left * leftForce);
        }

        if (Input.GetKey(KeyCode.W))
        {
            leftHand.GetComponent<Rigidbody>().AddForce(Vector3.forward * leftForce);
        }

        if (Input.GetKey(KeyCode.S))
        {
            leftHand.GetComponent<Rigidbody>().AddForce(Vector3.back * leftForce);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rightHand.GetComponent<Rigidbody>().AddForce(Vector3.right * rightForce);
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            rightHand.GetComponent<Rigidbody>().AddForce(Vector3.left * rightForce);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rightHand.GetComponent<Rigidbody>().AddForce(Vector3.forward * rightForce);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rightHand.GetComponent<Rigidbody>().AddForce(Vector3.back * rightForce);
        }
    }
}
