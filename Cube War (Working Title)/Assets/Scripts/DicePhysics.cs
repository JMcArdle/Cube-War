using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePhysics : MonoBehaviour {
    public Vector3 incomingForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<Cube>().playState == PlayState.idle && collision.gameObject.CompareTag("Cube"))
        {


            float incDown = collision.gameObject.GetComponent<Rigidbody>().velocity.y;
            incomingForce = collision.impulse;
            incomingForce.x += -collision.gameObject.GetComponent<Rigidbody>().velocity.x;
            incomingForce.z += -collision.gameObject.GetComponent<Rigidbody>().velocity.z;
            //incomingForce *= Mathf.Pow(2, collision.gameObject.GetComponent<Rigidbody>().mass);
            incomingForce *= Mathf.Log(collision.gameObject.GetComponent<Rigidbody>().mass);
            print(incomingForce);
            incomingForce.x *= Mathf.Max(1, -incDown);
            incomingForce.z *= Mathf.Max(1, -incDown);
            incomingForce.y /= 50;
            GetComponent<Rigidbody>().AddForce(-incomingForce * GetComponent<Rigidbody>().mass);
            print("Hit, adding force of: " + -incomingForce * GetComponent<Rigidbody>().mass);


            //print(name + " - Hit, adding force of: " + incomingForce * GetComponent<Rigidbody>().mass + " from: " + GetComponent<Rigidbody>().mass);
        }
        else if (GetComponent<Cube>().playState == PlayState.launch && collision.gameObject.CompareTag("Cube"))
        {
            float incDown = GetComponent<Rigidbody>().velocity.y;
            incomingForce = collision.impulse;
            incomingForce.x += -GetComponent<Rigidbody>().velocity.x;
            incomingForce.z += -GetComponent<Rigidbody>().velocity.z;
            incomingForce.x *= Mathf.Max(1, -incDown);
            incomingForce.z *= Mathf.Max(1, -incDown);
            incomingForce.y /= 50;
            GetComponent<Rigidbody>().AddForce(incomingForce * GetComponent<Rigidbody>().mass * 5);
        }
    }
}
