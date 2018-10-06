using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnageBehaviour : MonoBehaviour {

	private static string AXIS_HORIZONTAL;
	private static string AXIS_VERTICAL;

	private string playerID;
	private Rigidbody rb;
	private bool sortieDeLaMap;
	private float dashCooldownActual;
	private float dashAnimationLockActual;

	public float speed = 5.0f;
	public float dashForce = 10.0f;
	public float dashCooldown = 1.0f;
	public float dashAnimationLock = 0.2f;

	void Awake () {
		this.rb = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {
		this.initialise();
	}

	public void initialise() {
		this.sortieDeLaMap = false;
		this.dashCooldownActual = Time.time;
		this.dashAnimationLockActual = Time.time;
		setPlayerID("");
	}

	public void setPlayerID(string playerID) {
		this.playerID = playerID;
		AXIS_HORIZONTAL = playerID + "Horizontal";
		AXIS_VERTICAL = playerID + "Vertical";
	}
	
	// Update is called once per frame
	void Update () {

		float x = Input.GetAxis(AXIS_HORIZONTAL);
		float z = Input.GetAxis(AXIS_VERTICAL);

		if(!sortieDeLaMap && Time.time >= dashAnimationLockActual) {
			
			this.rb.velocity = new Vector3(x, 0.0f, z);
			this.rb.velocity.Normalize();
			this.rb.velocity *= speed;
		}

		if(Input.GetKeyDown(KeyCode.LeftShift)){
			dasherVers(new Vector3(x, 0.0f, z).normalized);
		}
	}

	public void sortDeLaMap() {
		this.sortieDeLaMap = true;
	}

	private void dasherVers(Vector3 direction) {

		if(Time.time >= dashCooldownActual) {
			dashCooldownActual = Time.time + dashCooldown;
			dashAnimationLockActual = Time.time + dashAnimationLock;

			this.rb.AddForce(direction * dashForce, ForceMode.Impulse);
		}
	}
}
