using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Enum for the current play state

public enum PlayState{
	idle, //The player is idle
	aiming, //The player has selected a cude and is currently aiming
	launch,
	placing,
	placed}; //The player has launched the cube, and the cube is in motion

public class Cube : MonoBehaviour {

	public PlayState playState;  //The current play state
	private Vector3 velocity; //The cube's velocity
	//private Rigidbody cubeBody;

	//private Vector3 mousePos2D;
	//private Vector3 mousePos3D;
	private UnitClass unit;
	private Vector3 launcherPos;
	private float launcherY = 0.0f;
	private Vector3 cubePos;
	private GameObject ground;
	private RaycastHit hit;
	private Ray ray;
	private Vector3 hitPos;
	private float maxHitPosY;
	private float minHitPosY;
	private float maxlauncherY;
	private float minlauncherY;
	private float maxPlaceY = 6.0f;
	private float minPlaceY = -1.0f;
	private Collider[] colliders;
	private bool changeToAiming = false;
	//private GameObject lineRendererObject;
	//private LineRenderer lineRenderer;
    private float bombDelay = 0;


	private bool flick;
	public bool stun;



	public GameObject launcher;
	public GameObject hitPosMarker;
	public float velocityMulti = 4f; //Multiplyer for velocity
	public float hitPosMod = 0.02f;
	public float maxMagnitude = 5.0f;

	// Use this for initialization
	void Start () {
		//playState = StateMachine.playState;  BRING BACK LATER
		unit = this.gameObject.GetComponent<UnitClass>();
		flick = false;
		stun = false;
		//cubePos = Camera.main.ScreenToWorldPoint (this.gameObject.transform.position);
		cubePos = this.gameObject.transform.position;
		//mousePos2D = Input.mousePosition;
		//mousePos3D = Vector3.zero;
		//maxMagnitude = this.gameObject.GetComponent<Collider> ().bounds.size.x * 3f;
		velocity = Vector3.zero; //The player only controls X and Z velocity
		unit.startDefense ();
		//cubeBody = this.gameObject.GetComponent<Rigidbody>();
		//StateMachine.battlePhase(); //For turning batttle on/off
	}

	void Update() {
		switch (playState) {
		case PlayState.idle:
			if (changeToAiming) {
				changeToAiming = false;
				playState = PlayState.aiming;
			}
			break;
		case PlayState.aiming:
			if (Input.GetAxis ("Cancel") == 1) {
				isKinematic = false;
				LaunchLine.launchLine.Enabled (false);
				GameDriver.selectLightOff ();
				GameDriver.gameDriver.isCubeSelected = false;
				playState = PlayState.idle;
                    GameDriver.selectLightOff();
				break;
			}
			//alter hit position
			if (Input.GetAxis ("TargetUp") == 1) {
				if (hitPos.y < maxHitPosY) {
					hitPos.y += hitPosMod;
					launcherY += hitPosMod;
					if (hitPos.y > maxHitPosY) {
						hitPos.y = maxHitPosY;
					}
					if (hitPos.y > maxlauncherY) {
						launcherY = maxlauncherY;
					}
				}
			}
			if (Input.GetAxis ("TargetDown") == 1) {
				if (hitPos.y > minHitPosY) {
					hitPos.y -= hitPosMod;
					launcherY -= hitPosMod;
					if (hitPos.y < minHitPosY) {
						hitPos.y = minHitPosY;
					}
					if (hitPos.y < minlauncherY) {
						launcherY = minlauncherY;
					}
				}
			}

			//alter launcher pos
			if (Input.GetAxis ("ForceUp") == 1) {
				if (launcherY < maxlauncherY) {
					launcherY += hitPosMod;
					if (hitPos.y > maxlauncherY) {
						launcherY = maxlauncherY;
					}
				}
			}
			if (Input.GetAxis ("ForceDown") == 1) {
				if (hitPos.y > minlauncherY) {
					launcherY -= hitPosMod;
					if (hitPos.y < minlauncherY) {
						launcherY = minlauncherY;
					}
				}
			}
			UpdatelaunchVelocity ();

                if (Input.GetMouseButtonDown(0))
                {
                    isKinematic = false;
                    flick = true;
                    unit.startAttack();
                    this.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-velocity *
                        this.gameObject.GetComponent<Rigidbody>().mass * velocityMulti, hitPos);
                    LaunchLine.launchLine.Enabled(false);
                    GameDriver.selectLightOff();
					GameDriver.gameDriver.isCubeSelected = false;
                    StateMachine.isCubeLaunched = true;
                    if (GetComponent<UnitClass>().unitClass == className.Bomb) bombDelay = Time.time;
                    playState = PlayState.launch;
                }

			break;
		case PlayState.launch:
			break;
		case PlayState.placing:
			if (Input.GetAxis ("Cancel") == 1) {
				GameDriver.cancelPlaceCube ();
			}
			if (Input.GetAxis("TargetUp") == 1) {
				cubePos.y += hitPosMod;
				if (cubePos.y > maxPlaceY) {
					cubePos.y = maxPlaceY;
				}
			}
			if (Input.GetAxis("TargetDown") == 1) {
				cubePos.y -= hitPosMod;
				if (cubePos.y < minPlaceY) {
					cubePos.y = minPlaceY;
				}
			}
			//UpdatePlacingPosition ();
			UpdatePlacingPosition ();

			if (Input.GetMouseButtonDown(0)) {
				colliders = Physics.OverlapBox(cubePos,this.gameObject.GetComponent<Renderer>().bounds.extents,Quaternion.identity);
				if (colliders.Length > 1) {
					GameDriver.popup ("You can't place a cube there", 1000);
				}
				else{
					isKinematic = false;
					this.gameObject.layer = LayerMask.NameToLayer ("cube");
					playState = PlayState.placed;
				}
			}
			break;
		case PlayState.placed:
			break;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
			
		//StateMachine.playState = playState; //TODO: have the state machine called later
		//TODO: change Kine different way


		switch (playState) {
		case PlayState.idle:
			break;
		case PlayState.aiming:
			break;
		case PlayState.launch:
			if (this.gameObject.GetComponent<Rigidbody> ().IsSleeping()) {

				//this.gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
				velocity = Vector3.zero;
				//cubePos = this.gameObject.transform.position;
				hitPos = this.gameObject.transform.position;
				launcherY = this.gameObject.transform.position.y;
				unit.startDefense ();
				playState = PlayState.idle;
				GameDriver.checkCubeMovement();
			}
			break;
		case PlayState.placing:
			break;
		case PlayState.placed:
			if (this.gameObject.GetComponent<Rigidbody> ().IsSleeping ()) {
				playState = PlayState.idle;
				GameDriver.placedCube ();
			}
			break;
		}
	}


	//TODO: Add messages for each condition., rearrange conditions
	void OnMouseDown(){
		if((StateMachine.turnState != Turn.pause) && (StateMachine.getPhase() == GamePhase.battle) &&
			(playState == PlayState.idle) && (!GameDriver.gameDriver.isCubeSelected) && 
			(!StateMachine.isCubeLaunched) && (StateMachine.currentTurn() == unit.owner)){
			//Check for special units
			if (unit.unitClass == className.Healer) {
				GameDriver.healerActivated ();
				GameDriver.removeCubeFromPlay (this.gameObject);
			}
			//Slect normal cube to flick
			else if ((flick == false) && (stun == false)) {
				hitPos = this.gameObject.transform.position;
				launcherY = this.gameObject.transform.position.y;
				maxHitPosY = hitPos.y + this.gameObject.GetComponent<Renderer> ().bounds.extents.y;
				minHitPosY = hitPos.y - this.gameObject.GetComponent<Renderer> ().bounds.extents.y;
				maxlauncherY = maxHitPosY + this.gameObject.GetComponent<Renderer> ().bounds.extents.y;
				minlauncherY = minHitPosY - this.gameObject.GetComponent<Renderer> ().bounds.extents.y;


				isKinematic = true;
				UpdatelaunchVelocity ();
				LaunchLine.launchLine.Enabled (true);
				GameDriver.selectLightOn (this.gameObject);
				GameDriver.gameDriver.isCubeSelected = true;
				changeToAiming = true;
				//playState = PlayState.aiming;
			} else {
				GameDriver.popup ("That cube is stunned!", 1000);
			}
		}

	}

	void OnCollisionEnter(Collision other){
		if (unit.unitClass == className.Paralyze) {
			if (other.gameObject.GetComponent<Cube>() != null){
				if ((other.gameObject.GetComponent<UnitClass> ().owningPlayer != unit.owningPlayer) 
					&& (other.gameObject.GetComponent<UnitClass> ().unitClass != className.Paralyze)) {
					other.gameObject.GetComponent<Cube>().stunned = true;
				}
			}
		}
        else if (unit.unitClass == className.Bomb && StateMachine.gamePhase == GamePhase.battle && other.collider.isTrigger == false && Time.time-bombDelay > 0.25)

        {
            Collider[] explosionHit = Physics.OverlapSphere(this.gameObject.transform.position, GetComponent<UnitClass>().attack);
            foreach(Collider hit in explosionHit)
            {
                if(hit.gameObject.GetComponent<Rigidbody>() != null)
                {
                    hit.gameObject.GetComponent<Rigidbody>().AddExplosionForce(GetComponent<UnitClass>().attack*215, this.gameObject.transform.position, GetComponent<UnitClass>().attack);
                }
            }
            if(playState == PlayState.launch) GameDriver.checkCubeMovement();
            GameDriver.removeCubeFromPlay(this.gameObject);
        }
	}

	void OnCollisionExit(Collision other){
		if (other.gameObject.GetComponent<Cube> () != null) {
			if (other.gameObject.GetComponent<UnitClass> ().unitClass == className.Paralyze && 
				other.gameObject.GetComponent<UnitClass> ().owningPlayer != unit.owningPlayer) {
				stunned = false;
			}
		}
	}


	public bool isKinematic{
		get{
			return this.gameObject.GetComponent<Rigidbody> ().isKinematic;
		} 
		set{ this.gameObject.GetComponent<Rigidbody> ().isKinematic = value;
		}
	}	

	public bool flicked{
		get{ return flick;}
		set{ flick = value; }
	}

	public bool stunned{
		get{ return stun;}
		set{ stun = value; }
	}

	private void UpdatelaunchVelocity(){
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if ( Physics.Raycast (ray,out hit,1000.0f)) {
			launcherPos = new Vector3 (hit.point.x, launcherY, 
				hit.point.z);
		}

		velocity = launcherPos - hitPos;
		if (velocity.magnitude > maxMagnitude) {
			velocity = Vector3.ClampMagnitude(velocity, maxMagnitude);
			launcherPos = velocity + hitPos;
		}
		LaunchLine.launchLine.SetLinePosition(0,hitPos);
		LaunchLine.launchLine.SetLinePosition(1,launcherPos);
		LaunchLine.launchLine.SetPointPosition (launcherPos);

		LaunchLine.launchLine.endColor = Color.Lerp (Color.green, Color.red, velocity.magnitude / maxMagnitude);
	}
		
	public void SetToPlacing(){
		isKinematic = true;
		playState = PlayState.placing;
		this.gameObject.layer = LayerMask.NameToLayer("placing");
	}

	private void UpdatePlacingPosition(){
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if ( Physics.Raycast (ray,out hit,1000.0f, (LayerMask)4096)) { //Only casts the ray against the RaycastGround layer.
			cubePos = new Vector3 (hit.point.x, cubePos.y, 
				hit.point.z);
		}

		this.gameObject.transform.position = cubePos;
	}





}
