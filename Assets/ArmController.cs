using UnityEngine;
using System.Collections;

public class ArmController : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;
    public float leftArmSegmentCount;
    public float rightArmSegmentCount;

    // Update is called once per frame
    void Update () {
        if (!leftHand || !rightHand) return;

        float leftForce = Mathf.Max(12f * leftArmSegmentCount, 40f);
        float rightForce = Mathf.Max(12f * rightArmSegmentCount, 40f);
        if(transform.position.y < 0.75f)
        {
            leftHand.GetComponent<Rigidbody>().AddForce(Vector3.up * leftForce);
            rightHand.GetComponent<Rigidbody>().AddForce(Vector3.up * rightForce);
        }else
        {
            leftHand.GetComponent<Rigidbody>().AddForce(Vector3.up * leftForce * 0.3f);
            rightHand.GetComponent<Rigidbody>().AddForce(Vector3.up * rightForce * 0.3f);
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
