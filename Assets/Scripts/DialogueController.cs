using UnityEngine;
using System.Collections;

public class DialogueController : MonoBehaviour {


	public float rotationSpeed = 0.01f;
	public bool loopLastDialogOption = true;
	public bool triggerOnlyOnce = false;

	public GameObject[] dialogOptions;
	public int[] dialogImpact;
	public GameObject barToImpact = null;

	public bool paralyzesPlayerDuringDialogue = true;
	public bool orientsPlayerTowardsSelf = true;

	public bool triggerOnCollision = false;

	private GameObject player;
	private ConversationStatus playerConversationStatus;
	private PlayerStatBar impactedStatBar;

	private bool enableDialogKey = false;
	private bool inConversation = false;
	private Quaternion wantedRotation;
	private MonoBehaviour currentDialog;
	private MonoBehaviour[] playerMovementScripts;
	private int dialogIndex = 0;
	
	// Use this for initialization
	void Start () {

		this.player = GameObject.FindWithTag("PlayerObject");
		this.playerConversationStatus = player.GetComponent<ConversationStatus>();
		this.playerMovementScripts = player.GetComponents<MonoBehaviour>();

		if (this.barToImpact == null) {
			this.barToImpact = GameObject.FindWithTag("PlayerHealth");
		}
		this.impactedStatBar = barToImpact.GetComponent<PlayerStatBar>();

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
				(Input.GetButtonDown("Talk") || this.triggerOnCollision) 
				&& !this.inConversation && playerConversationStatus.isReadyForNewConversation() &&
				!(triggerOnlyOnce && dialogIndex > 0);
	}

	private bool weShouldEndConversation() {
		return this.inConversation && !currentDialog.enabled;
	}

	private void togglePlayerMovementScripts(bool value) {
		foreach (MonoBehaviour script in this.playerMovementScripts) {
			script.enabled = value;
		}

	}

	private MonoBehaviour getNextDialogOption() {

		if (dialogIndex >= dialogOptions.Length) {
			if (loopLastDialogOption) {
				dialogIndex = dialogOptions.Length - 1;
			} else {
				dialogIndex = 0;
			}
		}

		GameObject optionObject = dialogOptions[dialogIndex];
		MonoBehaviour dialogOption = optionObject.GetComponent<MonoBehaviour>();

		return dialogOption;
	}

	private void startConversation() {

		Debug.Log("Pressed 'Talk' Button");

		if (this.paralyzesPlayerDuringDialogue) {
			this.togglePlayerMovementScripts(false);
		}
		this.inConversation = true;
		this.playerConversationStatus.setConversationReadiness(false);

		this.currentDialog = this.getNextDialogOption();

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

		
		if (dialogImpact.Length == dialogOptions.Length) {
			int barImpact = this.dialogImpact[dialogIndex];
			if (barImpact != 0) {
				impactedStatBar.fadeBar(barImpact);
			}

		}

		this.dialogIndex++;

	}

}

	