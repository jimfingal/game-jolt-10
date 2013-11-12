using UnityEngine;
using System.Collections;

public class DialogueController : MonoBehaviour {

	public GameObject player;
	public GameObject playerConversationFlag;

	public float rotationSpeed = 0.01f;

	public bool loopLastDialogOption = true;

	public GameObject[] dialogOptions;

	public bool enableDialogKey = false;
	public bool inConversation = false;
	
	private Quaternion wantedRotation;
	private MonoBehaviour currentDialog;
	private MonoBehaviour[] playerMovementScripts;

	public int dialogIndex = 0;
	
	// Use this for initialization
	void Start () {
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
		Debug.Log ("Entered Trigger: " + other);
		enableDialogKey = true;
	}
	
	void OnTriggerExit (Collider other) {
		Debug.Log ("Exited Trigger: " + other);
		enableDialogKey = false;
	}


	private void turnPlayerTowardsMe() {
		player.transform.rotation = Quaternion.Lerp(player.transform.rotation, wantedRotation, Time.time * this.rotationSpeed);
	}

	private bool weShouldInitiateConversation() {
		return this.enableDialogKey && Input.GetButton("Talk") && !this.inConversation && !playerConversationFlag.activeSelf;
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
		this.playerConversationFlag.SetActive(true);

		this.currentDialog = this.getNextDialogOption();

		this.currentDialog.enabled = true;

		this.wantedRotation = Quaternion.LookRotation(transform.position - player.transform.position);

	}

	private void endConversation() {
		this.inConversation = false;
		this.playerConversationFlag.SetActive(false);
		this.togglePlayerMovementScripts(true);
		this.dialogIndex++;
	}

}

	