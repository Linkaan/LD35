using UnityEngine;
using System.Collections;

public class Ragdoll : MonoBehaviour {

    public float mass = 0.01f;
    public Ragdoll[] children;

	public void InstantiateRagdoll(Rigidbody parent)
    {
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>() as Rigidbody;
        rigidbody.mass = mass;
        if (parent != null)
        {
            (gameObject.AddComponent<CharacterJoint>() as CharacterJoint).connectedBody = parent;
        }

        if (children.Length > 0)
        {
            foreach (Ragdoll ragdoll in children)
            {
                if (ragdoll != null)
                {
                    ragdoll.InstantiateRagdoll(rigidbody);
                }
            }
        }
    }
}
