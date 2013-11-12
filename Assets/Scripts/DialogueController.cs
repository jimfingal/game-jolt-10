using UnityEngine;
using System.Collections;

public class DialogueController : MonoBehaviour {


	public float rotationSpeed = 0.01f;
	public bool loopLastDialogOption = true;
	public GameObject[] dialogOptions;


	private GameObject player;
	private ConversationStatus playerConversationStatus;

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

	}

	// Update is called once per frame
	void Update () {
		
		if (this.inConversation) {
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
		return this.enableDialogKey && Input.GetButton("Talk") && !this.inConversation && playerConversationStatus.isReadyForNewConversation();
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
		
		this.togglePlayerMovementScripts(false);
		this.inConversation = true;
		this.playerConversationStatus.setConversationReadiness(false);

		this.currentDialog = this.getNextDialogOption();

		this.currentDialog.enabled = true;

		this.wantedRotation = Quaternion.LookRotation(transform.position - player.transform.position);

	}

	private void endConversation() {
		this.inConversation = false;
		this.playerConversationStatus.setConversationReadiness(true);
		this.togglePlayerMovementScripts(true);
		this.dialogIndex++;
	}

}

	