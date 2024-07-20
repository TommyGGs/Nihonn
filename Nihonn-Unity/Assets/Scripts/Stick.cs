using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToObject : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("touch");
		if(other.gameObject.CompareTag("omochi")){
			Debug.Log("touch");
            FixedJoint2D fixedJoint = gameObject.AddComponent<FixedJoint2D>();
            fixedJoint.connectedBody = other.rigidbody;
            fixedJoint.breakForce = 80f;//大きくするほど離れない
            fixedJoint.breakTorque = 80f;//同上

            // SpringJoint2D springJoint = gameObject.AddComponent<SpringJoint2D>();
            // springJoint.connectedBody = other.rigidbody;

            // springJoint.dampingRatio = 0.5f;
            // springJoint.frequency = 1f;
            // springJoint.breakForce = 100f;
            // springJoint.breakTorque = 50f;
		}else if(other.gameObject.CompareTag("onigiri")){
            Debug.Log("touch");
            FixedJoint2D fixedJoint = gameObject.AddComponent<FixedJoint2D>();
            fixedJoint.connectedBody = other.rigidbody;
            fixedJoint.breakForce = 30f;//大きくするほど離れない
            fixedJoint.breakTorque = 50f;//同上

            // SpringJoint2D springJoint = gameObject.AddComponent<SpringJoint2D>();
            // springJoint.connectedBody = other.rigidbody;

            // springJoint.dampingRatio = 0.5f;
            // springJoint.frequency = 1f;
            // springJoint.breakForce = 100f;
            // springJoint.breakTorque = 50f;
        }else if(other.gameObject.CompareTag("natto")){
            Debug.Log("touch");
            FixedJoint2D fixedJoint = gameObject.AddComponent<FixedJoint2D>();
            fixedJoint.connectedBody = other.rigidbody;
            fixedJoint.breakForce = 70f;
            fixedJoint.breakTorque = 50f;

            SpringJoint2D springJoint = gameObject.AddComponent<SpringJoint2D>();
            // springJoint.connectedBody = other.rigidbody;

            // springJoint.dampingRatio = 0.5f;
            // springJoint.frequency = 1f;
            springJoint.breakForce = 80f;
            // springJoint.breakTorque = 50f;
        }
    }
}
