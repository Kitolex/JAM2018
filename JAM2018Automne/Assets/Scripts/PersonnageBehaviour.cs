using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnageBehaviour : MonoBehaviour {

	private string AXIS_HORIZONTAL;
	private string AXIS_VERTICAL;
    private string BUTTON_DASH;

    private string playerID;
	private Rigidbody rb;
	private bool sortieDeLaMap;
	private float dashCooldownActual;
	private float dashAnimationLockActual;
	private float stunDurationActual;
	private Vector3 previousPosition;
	private Vector3 lastDir;
	private float previousInputMagnitude;
	private float vitesseResiduelle;
	private Vector3 previousDeplacement;

	public float speed = 8.0f;
	public float dashPropulsionForce = 30.0f;
	public float dashCooldown = 1.0f;
	public float dashAnimationLock = 0.2f;
	public float stunDuration = 0.1f;
	public float dashImpactForce = 30.0f;
	public bool commandeInversees;
	public bool commandeTournees;
	public bool solGlace;
	public float maxSpeedGlace;

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
		this.previousPosition = this.transform.position;
		this.lastDir = Vector3.zero;
		this.previousInputMagnitude = 0.0f;
		this.vitesseResiduelle = 0.0f;
		this.previousDeplacement = Vector3.zero;
	}

	public void setPlayerID(string playerID) {
		this.playerID = playerID;
		AXIS_HORIZONTAL = playerID + "Horizontal";
		AXIS_VERTICAL = playerID + "Vertical";
        BUTTON_DASH = playerID + "Dash";

    }
	
    public string getPlayerID()
    {
        return playerID;
    }

	// Update is called once per frame
	void Update () {

		float x = getXAxis();
		float z = getZAxis();

		Vector3 deplacement = Vector3.zero;

		if(this.peutAgir()) {

			Vector3 dir = new Vector3(x, 0.0f, z);

			if(Input.GetAxisRaw(BUTTON_DASH) != 0){
				dasherVers(dir.normalized);
			} else {
				deplacement = dir * speed;
			}
		}

		if(solGlace) {

			Vector3 newDeplacement = (this.transform.position - this.previousPosition) / Time.deltaTime;
			newDeplacement.y = 0.0f;

			if(newDeplacement.magnitude >= this.previousDeplacement.magnitude) {
				deplacement += newDeplacement;
			} else {
				deplacement += this.previousDeplacement;
			}

			if(deplacement.magnitude > this.previousDeplacement.magnitude && deplacement.magnitude >= this.maxSpeedGlace) {
				deplacement.Normalize();
				deplacement *= this.maxSpeedGlace;
			}
		}

		this.transform.position += deplacement * Time.deltaTime;

		this.previousPosition = this.transform.position;

		this.previousDeplacement = deplacement;
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

			this.rb.AddForce(direction * dashPropulsionForce, ForceMode.Impulse);
		}
	}

	void OnCollisionEnter(Collision collision) {

		if(collision.collider.tag.Equals("Player")) {

			PersonnageBehaviour pb = collision.collider.gameObject.GetComponent<PersonnageBehaviour>();

			if(pb.isDashing()) {
				this.stun(pb.stunDuration);
				this.rb.AddForce((this.transform.position - pb.transform.position).normalized * pb.dashImpactForce, ForceMode.Impulse);
			}
		}
	}

	private float getXAxis() {
		if(commandeInversees)
			return - Input.GetAxis(AXIS_HORIZONTAL);
		else if(commandeTournees)
			return - Input.GetAxis(AXIS_VERTICAL);
		else
			return Input.GetAxis(AXIS_HORIZONTAL);
	}

	private float getZAxis() {
		if(commandeInversees)
			return - Input.GetAxis(AXIS_VERTICAL);
		else if(commandeTournees)
			return Input.GetAxis(AXIS_HORIZONTAL);
		else
			return Input.GetAxis(AXIS_VERTICAL);
	}
}
