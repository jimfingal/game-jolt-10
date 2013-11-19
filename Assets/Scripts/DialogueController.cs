using UnityEngine;
using System.Collections;

public class DialogueController : MonoBehaviour {


	public float rotationSpeed = 0.01f;
	public bool loopLastDialogOption = true;
	public bool triggerOnlyOnce = false;
	private bool triggered = false;

	public bool useDefaults = true;

	public GameObject[] dialogOptions;
	public int[] dialogImpact;
	public GameObject barToImpact = null;

	public Vector3 lookOffset = new Vector3(0, 0, 0);

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

	private DefaultDialogue defaultDialogue;

	private	GameObject innerMonologue;

	public static bool dialogUnlockingCursor = false;

	
	// Use this for initialization
	void Start () {

		this.player = GameObject.FindWithTag("PlayerObject");
		this.playerConversationStatus = player.GetComponent<ConversationStatus>();
		this.playerMovementScripts = player.GetComponents<MonoBehaviour>();

		if (this.barToImpact == null) {
			this.barToImpact = GameObject.FindWithTag("PlayerHealth");
		}

		if (barToImpact) {
			this.impactedStatBar = barToImpact.GetComponent<PlayerStatBar>();
		}

		if (GameObject.FindWithTag("DefaultDialogue")) {
			defaultDialogue =  GameObject.FindWithTag("DefaultDialogue").GetComponent<DefaultDialogue>();
		}

		if (GameObject.FindGameObjectWithTag("InnerMonologue")) {
			innerMonologue = GameObject.FindGameObjectWithTag("InnerMonologue");
		}
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
				!(triggerOnlyOnce && triggered);
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

		MonoBehaviour dialogOption = null;

		if (useDefaults && defaultDialogue) {
			dialogOption = defaultDialogue.getDefaultOption();
		}

		if (!dialogOption) {

			GameObject optionObject = dialogOptions[dialogIndex];
			dialogOption = optionObject.GetComponent<MonoBehaviour>();

		}

		return dialogOption;
	}

	private void startConversation() {

		CursorLocker.unlockCursor = true;
		triggered = true;

		Debug.Log("Pressed 'Talk' Button");

		if (this.paralyzesPlayerDuringDialogue) {
			this.togglePlayerMovementScripts(false);
		}
		this.inConversation = true;
		this.playerConversationStatus.setConversationReadiness(false);

		this.currentDialog = this.getNextDialogOption();

		this.currentDialog.enabled = true;

		Vector3 myPosition = gameObject.transform.position;
		Vector3 playerPosition = player.transform.position;

		this.wantedRotation = Quaternion.LookRotation((myPosition + lookOffset) - playerPosition);
				
	}

	private void endConversation() {

		CursorLocker.unlockCursor = false;

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

		if (dialogIndex >= dialogOptions.Length) {
			if (loopLastDialogOption) {
				dialogIndex = dialogOptions.Length - 1;
			} else {
				dialogIndex = 0;
			}
		}

		// Haxy way to reset counter
		if (innerMonologue) {
			innerMonologue.SetActive(false);
			innerMonologue.SetActive(true);
		}

	}

}

	