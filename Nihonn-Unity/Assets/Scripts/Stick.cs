using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToObject : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
		if(other.gameObject.CompareTag("omochi")){
			Debug.Log("touch");
            FixedJoint2D fixedJoint = gameObject.AddComponent<FixedJoint2D>();
            fixedJoint.connectedBody = other.rigidbody;
            fixedJoint.breakForce = 80f;//大きくするほど離れない
            fixedJoint.breakTorque = 50f;//同上

            // SpringJoint2D springJoint = gameObject.AddComponent<SpringJoint2D>();
            // springJoint.connectedBody = other.rigidbody;

            // springJoint.dampingRatio = 0.5f;
            // springJoint.frequency = 1f;
            // springJoint.breakForce = 100f;
            // springJoint.breakTorque = 50f;
		}
    }
}
