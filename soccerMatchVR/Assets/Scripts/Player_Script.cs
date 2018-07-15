using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.VR.WSA.Input;

public class Player_Script : MonoBehaviour {

	// player name
	public string Name;
	public TypePlayer type = TypePlayer.DEFENDER;
	public float Speed = 1.0f;
	public float Strong = 1.0f;
	public float Control = 1.0f;
		

	private const float STAMINA_DIVIDER = 64.0f;
	private const float STAMINA_MIN = 0.5f;
	private const float STAMINA_MAX = 1.0f;	
		
		
	public enum TypePlayer {
			DEFENDER,
			MIDDLER,
			ATTACKER
		};
		
	public Vector3 actualVelocityPlayer;
	private Vector3 oldVelocityPlayer;
	//public Sphere sphere;
	private GameObject[] players;
	private GameObject[] oponents;
	public Vector3 resetPosition;
	public Vector3 initialPosition;
	private float inputSteer;
    private const float initialDisplacement = 20.0f;
	public Transform goalPosition;
	public Transform headTransform;	
	[HideInInspector]	
	public bool temporallyUnselectable = true;
	[HideInInspector]	
	public float timeToBeSelectable = 1.0f;	
	public float maxDistanceFromPosition = 20.0f;
//    GestureRecognizer recognizer;

    public Transform[] waypoints;
    public int rate = 20;
    private int currentWaypoint = 0;

    public enum Player_State { 
		   PREPARE_TO_KICK_OFF,
		   KICK_OFFER,
		   RESTING,
		   GO_ORIGIN,
		   CONTROLLING,
		   PASSING,
		   SHOOTING,
		   MOVE_AUTOMATIC,
		   ONE_STEP_BACK,
		   STOLE_BALL,
		   OPONENT_ATTACK,
		   PICK_BALL,
		   CHANGE_DIRECTION,
		   THROW_IN,
		   CORNER_KICK,
		   TACKLE,
           DEFENDING
		  };
	   
	public Player_State state;
    //ScenesScript scenescript;

	private float timeToRemove = 3.0f;	
	private float timeToPass = 1.0f;
		
	// hand of player in squeleton hierarchy
	public Transform hand_bone;
    GameObject attackerselected;
    GameObject goalieref;
    Vector3 shooting_spawn_init;
    private Vector3 initial_pos;
    private Vector3 initial_rot;

    private bool throwin = false;
    private bool shotcomplete = false;
    private bool start_scene_goalie_kick = false;

    //public InGameState_Script inGame;
		
	public Texture barTexture;
	public Texture barStaminaTexture;
	private int barPosition=0;
	private Quaternion initialRotation;

    public GameObject player_component;
		
	public float stamina = 64.0f;
    private float delay = 0.15f;
    private bool run_delay = false;
    private bool isFoul = false;

    public bool animation_paused = false;
    private bool goal_kick_played = false;
    private bool stop_watching_goalie = false;
    private bool second_corner_kick = false;
    private bool block_reset = false;

    void  Awake () {

		GetComponent<Animation>().Stop();
		state = Player_State.PREPARE_TO_KICK_OFF;
        
	}
    

    void  Start (){

        initial_pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        initial_rot = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        GameObject shooting = GameObject.Find("shooting_spawn");

        
        GetComponent<Animation>()["jump_backwards_bucle"].speed = 1.5f;
        GetComponent<Animation>()["starting"].speed = 1.0f;
        GetComponent<Animation>()["starting_ball"].speed = 1.0f;
        GetComponent<Animation>()["running"].speed = 1.2f;
        GetComponent<Animation>()["running_ball"].speed = 1.0f;
        GetComponent<Animation>()["pass"].speed = 1.8f;
        GetComponent<Animation>()["rest"].speed = 1.0f;
        GetComponent<Animation>()["turn"].speed = 1.3f;
        GetComponent<Animation>()["tackle"].speed = 1.0f;

        GetComponent<Animation>()["fight"].speed = 1.2f;
        GetComponent<Animation>().Play("rest");
        attackerselected = GameObject.Find("Attacker_Calvo1");

        
        //initialRotation = transform.rotation * headTransform.rotation;
	}


    void Update()
    {

        stamina += 2.0f * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 1, 64);



        switch (state)
        {
            case Player_State.STOLE_BALL:

                //attackerselected.transform.LookAt(goalieref.transform.position);

                Vector3 relPos = transform.InverseTransformPoint(0,0,0);
                inputSteer = relPos.x / relPos.magnitude;
                transform.Rotate(0, inputSteer * 20.0f, 0);
                
                GetComponent<Animation>().Play("running");
                float staminaTemp3 = Mathf.Clamp((stamina / STAMINA_DIVIDER), STAMINA_MIN, STAMINA_MAX);
                transform.position += transform.forward * 4.5f * Time.deltaTime * staminaTemp3 * Speed;



            break;
            case Player_State.RESTING:

                GetComponent<Animation>().Play("rest");

                break;
        }

        

    }
	
}
	
