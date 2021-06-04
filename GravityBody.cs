using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
	
	GravityAttractor planet;
    private new Rigidbody rigidbody;

    public Rigidbody Rigidbody { get => rigidbody; set => rigidbody = value; }

    void Awake()
    {
		planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
		Rigidbody = GetComponent<Rigidbody> ();

		// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
		Rigidbody.useGravity = false;
		Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	void FixedUpdate () {
		// Allow this body to be influenced by planet's gravity
		planet.Attract(Rigidbody);
	}
}