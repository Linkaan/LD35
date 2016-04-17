using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Arm : MonoBehaviour {

    public GameObject armPrefab;
    public GameObject handPrefab;
    public GameObject leftArmBase;
    public GameObject rightArmBase;

    public ArmController controller;

    private List<Rigidbody> leftArm;
    private List<Rigidbody> rightArm;

    void Start () {
        leftArm = new List<Rigidbody>();
        rightArm = new List<Rigidbody>();

        Rigidbody lastRigidbody;
        Vector3 pos = leftArmBase.transform.position;
        pos.y -= 0.5f;
        GameObject leftArmSegment = Instantiate(armPrefab, pos, Quaternion.identity) as GameObject;
        leftArmSegment.transform.parent = leftArmBase.transform;
        Rigidbody leftRigidbody = leftArmSegment.AddComponent<Rigidbody>();
        HingeJoint leftJoint = leftArmSegment.AddComponent<HingeJoint>();
        leftJoint.connectedBody = leftArmBase.GetComponent<Rigidbody>();
        lastRigidbody = leftRigidbody;
        leftArm.Add(lastRigidbody);

        for (int i = 0; i < 5; i++)
        {
            pos.y -= 1.0f;
            GameObject leftArmSegmenti = Instantiate(armPrefab, pos, Quaternion.identity) as GameObject;
            leftArmSegmenti.tag = "Player";
            leftArmSegmenti.transform.parent = leftArmBase.transform;
            Rigidbody leftRigidi = leftArmSegmenti.AddComponent<Rigidbody>();
            HingeJoint leftJointi = leftArmSegmenti.AddComponent<HingeJoint>();
            leftJointi.connectedBody = lastRigidbody;
            lastRigidbody = leftRigidi;
            leftArm.Add(lastRigidbody);
        }

        pos.y -= 1.0f;
        GameObject leftHand = Instantiate(armPrefab, pos, Quaternion.identity) as GameObject;
        leftHand.tag = "Player";
        leftHand.transform.parent = leftArmBase.transform;
        Rigidbody leftHandRigidbody = leftHand.AddComponent<Rigidbody>();
        HingeJoint leftHandJoint = leftHand.AddComponent<HingeJoint>();
        leftHandJoint.connectedBody = lastRigidbody;
        leftArm.Add(leftHandRigidbody);

        pos = rightArmBase.transform.position;
        pos.y -= 0.5f;
        GameObject rightArmSegment = Instantiate(armPrefab, pos, Quaternion.identity) as GameObject;
        rightArmSegment.transform.parent = rightArmBase.transform;
        Rigidbody rightRigidbody = rightArmSegment.AddComponent<Rigidbody>();
        HingeJoint rightJoint = rightArmSegment.AddComponent<HingeJoint>();
        rightJoint.connectedBody = rightArmBase.GetComponent<Rigidbody>();    
        lastRigidbody = rightRigidbody;
        rightArm.Add(lastRigidbody);

        for (int i = 0; i < 5; i++)
        {
            pos.y -= 1.0f;
            GameObject rightArmSegmenti = Instantiate(armPrefab, pos, Quaternion.identity) as GameObject;
            rightArmSegmenti.tag = "Player";
            rightArmSegmenti.transform.parent = rightArmBase.transform;
            Rigidbody rightRigidi = rightArmSegmenti.AddComponent<Rigidbody>();
            HingeJoint rightJointi = rightArmSegmenti.AddComponent<HingeJoint>();
            rightJointi.connectedBody = lastRigidbody;
            lastRigidbody = rightRigidi;
            rightArm.Add(lastRigidbody);
        }

        pos.y -= 1.0f;
        GameObject rightHand = Instantiate(armPrefab, pos, Quaternion.identity) as GameObject;
        rightHand.tag = "Player";
        rightHand.transform.parent = rightArmBase.transform;
        Rigidbody rightHandRigidbody = rightHand.AddComponent<Rigidbody>();
        HingeJoint rightHandJoint = rightHand.AddComponent<HingeJoint>();
        rightHandJoint.connectedBody = lastRigidbody;
        rightArm.Add(rightHandRigidbody);

        controller.leftHand = leftHand;
        controller.rightHand = rightHand;
        controller.leftArmSegmentCount = 5;
        controller.rightArmSegmentCount = 5;

        leftHand.GetComponent<Rigidbody>().AddForce(Vector3.up * 40.0f);
        rightHand.GetComponent<Rigidbody>().AddForce(Vector3.up * 40.0f);
        leftHand.GetComponent<Rigidbody>().AddForce(Vector3.forward * 40.0f);
        rightHand.GetComponent<Rigidbody>().AddForce(Vector3.forward * 40.0f);
    }
	
	public void InsertLeftArmSegment()
    {
        int index = leftArm.Count - 1;
        while (index > 0 && leftArm[index] == null)
        {
            index--;
        }
        if (index < 0)
        {
            Debug.Log("WHAT IN THE WORLD!!??");
            return;
        }
        Vector3 pos = leftArm[index].transform.position + -leftArm[index].transform.up * 1f;
        GameObject leftHand = Instantiate(armPrefab, pos, leftArm[index].transform.rotation) as GameObject;
        leftHand.tag = "Player";
        leftHand.transform.parent = leftArmBase.transform;
        Rigidbody leftHandRigidbody = leftHand.AddComponent<Rigidbody>();
        HingeJoint leftHandJoint = leftHand.AddComponent<HingeJoint>();
        leftHandJoint.connectedBody = leftArm[index];
        leftArm.Add(leftHandRigidbody);
        controller.leftHand = leftHand;
        controller.leftArmSegmentCount++;
    }

    public bool DeleteLeftArmSegment()
    {
        int index = leftArm.Count - 1;
        while (index > 0 && leftArm[index] == null)
        {
            index--;
        }
        if (index < 0)
        {
            Debug.Log("WHAT IN THE WORLD!!??");
            return false;
        }
        Rigidbody bodyToRemove = leftArm[index];
        DestroyImmediate(bodyToRemove.gameObject);
        leftArm = leftArm.Where(item => item != null).ToList();
        if(index == 0)
        {
            return true;
        }
        controller.leftHand = leftArm[index - 1].gameObject;
        controller.leftArmSegmentCount--;
        return false;
    }

    public void InsertRightArmSegment()
    {
        int index = rightArm.Count - 1;
        while (index > 0 && rightArm[index] == null)
        {
            index--;
        }
        if (index < 0)
        {
            Debug.Log("WHAT IN THE WORLD!!??");
            return;
        }
        Vector3 pos = rightArm[index].transform.position + -rightArm[index].transform.up * 1f;
        GameObject rightHand = Instantiate(armPrefab, pos, rightArm[index].transform.rotation) as GameObject;
        rightHand.tag = "Player";
        rightHand.transform.parent = rightArmBase.transform;
        Rigidbody rightHandRigidbody = rightHand.AddComponent<Rigidbody>();
        HingeJoint rightHandJoint = rightHand.AddComponent<HingeJoint>();
        rightHandJoint.connectedBody = rightArm[index];
        rightArm.Add(rightHandRigidbody);
        controller.rightHand = rightHand;
        controller.rightArmSegmentCount++;
    }

    public bool DeleteRightArmSegment()
    {
        int index = rightArm.Count - 1;
        while (index > 0 && rightArm[index] == null)
        {
            index--;
        }
        if (index < 0) {
            Debug.Log("WHAT IN THE WORLD!!??");
            return false;
        }
        Rigidbody bodyToRemove = rightArm[index];
        DestroyImmediate(bodyToRemove.gameObject);
        rightArm = rightArm.Where(item => item != null).ToList();
        if (index == 0)
        {
            return true;
        }
        controller.rightHand = rightArm[index - 1].gameObject;
        controller.rightArmSegmentCount--;
        return false;
    }
}
