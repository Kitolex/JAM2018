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
	private float stunDurationActual;

	public float speed = 8.0f;
	public float dashForce = 70.0f;
	public float dashCooldown = 1.0f;
	public float dashAnimationLock = 0.2f;
	public float stunDuration = 0.1f;

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
		this.stunDurationActual = Time.time;
		setPlayerID("");	// A SUPPRIMER
	}

	public void setPlayerID(string playerID) {
		this.playerID = playerID;
		AXIS_HORIZONTAL = playerID + "Horizontal";
		AXIS_VERTICAL = playerID + "Vertical";
	}
	
	// Update is called once per frame
	void Update () {

		if(this.peutAgir()) {

			float x = Input.GetAxis(AXIS_HORIZONTAL);
			float z = Input.GetAxis(AXIS_VERTICAL);

			if(x != 0.0f || z != 0.0f) {
				this.rb.velocity = new Vector3(x, 0.0f, z);
				this.rb.velocity.Normalize();
				this.rb.velocity *= speed;

				if(Input.GetKeyDown(KeyCode.LeftShift)){
					dasherVers(new Vector3(x, 0.0f, z).normalized);
				}
			}
		}
	}

	private bool peutAgir() {
		return !sortieDeLaMap && Time.time >= dashAnimationLockActual && Time.time >= stunDurationActual;
	}

	public bool isDashing() {
		return Time.time < this.dashAnimationLockActual;
	}

	public void sortDeLaMap() {
		this.sortieDeLaMap = true;
	}

	public void stun(float stunDuration) {
		stunDurationActual = Time.time + stunDuration;
	}

	private void dasherVers(Vector3 direction) {

		if(Time.time >= dashCooldownActual) {
			dashCooldownActual = Time.time + dashCooldown;
			dashAnimationLockActual = Time.time + dashAnimationLock;

			this.rb.AddForce(direction * dashForce, ForceMode.Impulse);
		}
	}

	void OnCollisionEnter(Collision collision) {

		if(collision.collider.tag.Equals("Player")) {

			PersonnageBehaviour pb = collision.collider.gameObject.GetComponent<PersonnageBehaviour>();

			if(pb.isDashing()) {
				this.stun(pb.stunDuration);
				this.rb.AddForce((this.transform.position - pb.transform.position).normalized * pb.dashForce, ForceMode.Impulse);
			}
		}
	}
}
