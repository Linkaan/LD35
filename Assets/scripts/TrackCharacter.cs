using UnityEngine;
using System.Collections;

public class TrackCharacter : MonoBehaviour {

    public Transform target;
    public float distanceUp;
    public float distanceBack;
    public float lookForward;
    public float minimumHieght;

    private Vector3 posVelocity;
    private Vector3 lookAtVelocity;
    private Vector3 oldVelocity;
	
	void FixedUpdate () {
        if (target == null) return;

        Vector3 newPos = target.position - MaxAbs(oldVelocity, target.GetComponent<Rigidbody>().velocity);
        //newPos.z -= distanceBack;
        newPos.y = Mathf.Max(newPos.y + distanceUp, minimumHieght);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref posVelocity, 0.20f);

        Vector3 focalPoint = target.position + (target.GetComponent<Rigidbody>().velocity * lookForward);
        Quaternion targetRotation = Quaternion.LookRotation(focalPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f);
        //Debug.Log(target.GetComponent<Rigidbody>().velocity.magnitude);
        if (target.GetComponent<Rigidbody>().velocity.magnitude > 2.5)
        {            
            oldVelocity = target.GetComponent<Rigidbody>().velocity;
        }
	}

    Vector3 MaxAbs(Vector3 lhs, Vector3 rhs)
    {
        Vector3 abslhs = new Vector3();
        abslhs.x = Mathf.Abs(lhs.x);
        abslhs.y = Mathf.Abs(lhs.y);
        abslhs.z = Mathf.Abs(lhs.z);

        Vector3 absrhs = new Vector3();
        absrhs.x = Mathf.Abs(rhs.x);
        absrhs.y = Mathf.Abs(rhs.y);
        absrhs.z = Mathf.Abs(rhs.z);

        if(Vector3.Max(abslhs, absrhs) == abslhs)
        {
            return lhs;
        } else
        {
            return rhs;
        }
    }
}
