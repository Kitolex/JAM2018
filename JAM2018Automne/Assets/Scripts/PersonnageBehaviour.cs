﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnageBehaviour : MonoBehaviour {

	private string AXIS_HORIZONTAL;
	private string AXIS_VERTICAL;
    private string BUTTON_DASH;

    public int playerID;
	private Rigidbody rb;
	private bool sortieDeLaMap;
	private float dashCooldownActual;
	private float dashAnimationLockActual;
	private float stunDurationActual;
	private Vector3 previousPosition;
	private Vector3 previousDeplacement;
    private Animator animator;
	private SpriteRenderer spriteRenderer;
    private int VieActuelle;
    private MultiplayerManager multiplayerManager;
    private bool DashPressed;

	public GameObject ombre;
	public float speed = 8.0f;
	public float dashPropulsionForce = 30.0f;
	public float dashCooldown = 1.0f;
	public float dashAnimationLock = 0.2f;
	public float stunDuration = 0.1f;
	public float dashImpactForce = 30.0f;
    public int VieMax;
    public bool commandeInversees;
	public bool commandeTournees;
	public bool solGlace;
	public float maxSpeedGlace = 12.0f;
	public bool chaleurIntense;
	public bool ejectionRenforcee;

	void Awake () {
		this.rb = GetComponent<Rigidbody>();
        this.animator = GetComponentInChildren<Animator>();
		this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        this.multiplayerManager = GameObject.FindGameObjectWithTag("MultiplayerManager").GetComponent<MultiplayerManager>();
	}

	// Use this for initialization
	void Start () {
		this.initialise();
	}

	public void initialise() {
        Respawn();
        this.VieMax = 3;
        this.VieActuelle = VieMax;
	}

    public void Respawn()
    {
		this.ombre.SetActive(true);
        this.sortieDeLaMap = false;
        this.dashCooldownActual = Time.time;
        this.dashAnimationLockActual = Time.time;
        this.stunDurationActual = Time.time;
        this.previousPosition = this.transform.position;
        this.previousDeplacement = Vector3.zero;
        this.DashPressed = false;
    }

	public void setPlayerID(int playerID) {
		this.playerID = playerID;
		AXIS_HORIZONTAL = "Player" + playerID + "Horizontal";
		AXIS_VERTICAL = "Player" + playerID + "Vertical";
        BUTTON_DASH = "Player" + playerID + "Dash";

    }
	
    public int getPlayerID()
    {
        return playerID;
    }

	// Update is called once per frame
	void Update () {

		float x = getXAxis();
		float z = getZAxis();

		Vector3 deplacement = Vector3.zero;

		Vector3 dir = new Vector3(x, 0.0f, z);

		if(dir.magnitude <0.05f) {
			dir = Vector3.zero;
		}
	
		//Update animator
		if(this.iStun()) {
			animator.SetBool("Walk", false);
		} else {
			animator.SetBool("Walk", dir.magnitude > 0.0f);
		}
		

		if(this.peutAgir()) {

			if(dir.x < 0.0f) {
				this.spriteRenderer.flipX = true;
			} else if(dir.x > 0.0f) {
				this.spriteRenderer.flipX = false;
			}

           
            if (Input.GetAxisRaw(BUTTON_DASH) != 0)
            {
                if (!DashPressed)
                {                    
                    dasherVers(dir.normalized);
                }
                else
                {
                    deplacement = dir * speed;
                }
                
                DashPressed = true;
            }
            else
            {

                deplacement = dir * speed;
                DashPressed = false;
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

		if(chaleurIntense) {
			deplacement *= 0.5f;
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
		this.ombre.SetActive(false);
        animator.SetBool("Drowned", true);
        StartCoroutine(DrownIntoDeath());
	}

    public IEnumerator DrownIntoDeath()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("Drowned", false);
        multiplayerManager.KillPlayer(playerID);

    }

    public void Kill()
    {
        VieActuelle--;
        if (VieActuelle < 0)
            VieActuelle = 0;
    }

    public bool IsDead()
    {
        return VieActuelle == 0;
    }

	public void stun(float stunDuration) {
		stunDurationActual = Time.time + stunDuration;
	}

	public bool iStun() {
		return Time.time < this.stunDurationActual;
	}

	private void dasherVers(Vector3 direction) {

		if(Time.time >= dashCooldownActual) {
			dashCooldownActual = Time.time + dashCooldown;
			dashAnimationLockActual = Time.time + dashAnimationLock;

			if(chaleurIntense) {

				this.rb.AddForce(direction * dashPropulsionForce * 0.5f, ForceMode.Impulse);
				this.stun(this.stunDuration);

			} else {

				this.rb.AddForce(direction * dashPropulsionForce, ForceMode.Impulse);
			}
			
            animator.SetTrigger("dash");
		}
	}

	void OnCollisionEnter(Collision collision) {

		if(collision.collider.tag.Equals("Player")) {

			PersonnageBehaviour pb = collision.collider.gameObject.GetComponent<PersonnageBehaviour>();

			if(pb.isDashing()) {
				this.stun(pb.stunDuration);

				Vector3 impact = (this.transform.position - pb.transform.position).normalized * pb.dashImpactForce;

				if(chaleurIntense) {
					impact *= 0.5f;
				}

				if(ejectionRenforcee) {
					impact *= 2.0f;
				}

				this.rb.AddForce(impact, ForceMode.Impulse);
			}
		}

		if(this.isDashing()){

			IDashable d = collision.collider.GetComponent<IDashable>();

			if(d != null) {
				d.subirDash(this.gameObject);
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
