using UnityEngine;
using System.Collections;

public class LevelChangingDialogue : MonoBehaviour {


	public float rotationSpeed = 0.01f;

	public GameObject dialogOption;

	public bool paralyzesPlayerDuringDialogue = true;
	public bool orientsPlayerTowardsSelf = true;
	
	private GameObject player;
	private ConversationStatus playerConversationStatus;

	private bool enableDialogKey = false;
	private bool inConversation = false;
	private Quaternion wantedRotation;
	private MonoBehaviour currentDialog;
	private MonoBehaviour[] playerMovementScripts;

	public Vector3 pushBack;

	// Use this for initialization
	void Start () {

		this.player = GameObject.FindWithTag("PlayerObject");
		this.playerConversationStatus = player.GetComponent<ConversationStatus>();
		this.playerMovementScripts = player.GetComponents<MonoBehaviour>();

		this.currentDialog = dialogOption.GetComponent<MonoBehaviour>();


	}

	// Update is called once per frame
	void Update () {
		
		if (this.inConversation && orientsPlayerTowardsSelf) {
			this.turnPlayerTowardsMe();
		}
		
		if (this.weShouldInitiateConversation()) {
			this.startConversation();
		}
		
		if (this.weShouldEndConversation()) {
			this.endConversation();
		} 
		
	}
	
	void OnTriggerEnter (Collider other) {
		Debug.Log ("Entered Trigger ( " + gameObject.transform.parent + ") : " + other);
		enableDialogKey = true;
	}
	
	void OnTriggerExit (Collider other) {
		Debug.Log ("Exited Trigger ( " +  gameObject.transform.parent + ") : " + other);
		enableDialogKey = false;
	}


	private void turnPlayerTowardsMe() {
		player.transform.rotation = Quaternion.Lerp(player.transform.rotation, wantedRotation, Time.time * this.rotationSpeed);
	}

	private bool weShouldInitiateConversation() {
		return this.enableDialogKey && 
				!this.inConversation && playerConversationStatus.isReadyForNewConversation();
	}

	private bool weShouldEndConversation() {
		return this.inConversation && !currentDialog.enabled;
	}

	private void togglePlayerMovementScripts(bool value) {
		foreach (MonoBehaviour script in this.playerMovementScripts) {
			script.enabled = value;
		}

	}
	
	private void startConversation() {

		Debug.Log("Pressed 'Talk' Button");

		if (this.paralyzesPlayerDuringDialogue) {
			this.togglePlayerMovementScripts(false);
		}
		this.inConversation = true;
		this.playerConversationStatus.setConversationReadiness(false);

		this.currentDialog.enabled = true;

		Vector3 myPosition = gameObject.transform.parent.transform.position;
		Vector3 playerPosition = player.transform.position;
		this.wantedRotation = Quaternion.LookRotation(myPosition - playerPosition);


	}

	private void endConversation() {

		this.inConversation = false;
		this.playerConversationStatus.setConversationReadiness(true);
		if (this.paralyzesPlayerDuringDialogue) {
			this.togglePlayerMovementScripts(true);
		}

		player.transform.Translate(pushBack, Space.World);

		enableDialogKey = false;


	}

}

	