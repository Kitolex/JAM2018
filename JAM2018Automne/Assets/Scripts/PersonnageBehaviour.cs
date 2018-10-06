using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnageBehaviour : MonoBehaviour {

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
	}

	public void setPlayerID(string playerID) {
		this.playerID = playerID;
	}
	
	// Update is called once per frame
	void Update () {

		if(!sortieDeLaMap && Time.time >= dashAnimationLockActual) {
			float x = Input.GetAxis(playerID+"Horizontal");
			float z = Input.GetAxis(playerID+"Vertical");
			this.rb.velocity = new Vector3(x, 0.0f, z);
			this.rb.velocity.Normalize();
			this.rb.velocity *= speed;
		}

		if(Input.GetKeyDown(KeyCode.LeftShift)){
			dasherVers(new Vector3(-1.0f, 0.0f, 0.0f));
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
