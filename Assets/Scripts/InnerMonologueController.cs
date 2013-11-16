using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InnerMonologueController : MonoBehaviour {


	public float mediocreThoughtThreshould = 50;
	public float sadThoughtThreshold = 30;

	public List<GameObject> happyThoughts = new List<GameObject>();
	public List<GameObject> mediocreThoughts = new List<GameObject>();
	public List<GameObject> sadThoughts = new List<GameObject>();
	
	private GameObject moodObject;
	private PlayerStatBar mood;

	public float intrusiveThoughtInterval;
	private int frame = 1;

	private bool inConversation = false;

	private MonoBehaviour currentDialog;

	private GameObject player;
	private ConversationStatus playerConversationStatus;

	// Use this for initialization
	void Start () {

		this.player = GameObject.FindWithTag("PlayerObject");
		this.playerConversationStatus = player.GetComponent<ConversationStatus>();

		this.moodObject = GameObject.FindWithTag("PlayerHealth");

		if (moodObject) {
			this.mood = moodObject.GetComponent<PlayerStatBar>();
		}
	}

	// Update is called once per frame
	void Update () {

		frame++;

		if (this.weShouldInitiateConversation()) {
			this.startConversation();
		}
		
		if (this.weShouldEndConversation()) {
			this.endConversation();
		} 

	}



	private bool weShouldInitiateConversation() {
		return 	this.inConversation == false &&
				playerConversationStatus.isReadyForNewConversation() && 
				(frame % (this.intrusiveThoughtInterval * 60) == 0);
	}

	private bool weShouldEndConversation() {
		return this.inConversation && !currentDialog.enabled;
	}
	

	private MonoBehaviour getNextDialogOption() {

		GameObject optionObject;

		if (mood.currentValue > mediocreThoughtThreshould) {

			int index = Random.Range(0, happyThoughts.Count - 1);

			optionObject = happyThoughts[index];
			happyThoughts.RemoveAt(index);

		} else if (mood.currentValue > sadThoughtThreshold) {
			
			int index = Random.Range(0, mediocreThoughts.Count - 1);
			
			optionObject = mediocreThoughts[index];
			mediocreThoughts.RemoveAt(index);

		} else {
			
			int index = Random.Range(0, sadThoughts.Count - 1);
			
			optionObject = sadThoughts[index];
			sadThoughts.RemoveAt(index);
		}

		MonoBehaviour dialogOption = optionObject.GetComponent<MonoBehaviour>();

		return dialogOption;
	}

	private void startConversation() {

		this.playerConversationStatus.setConversationReadiness(false);

		this.currentDialog = this.getNextDialogOption();

		this.currentDialog.enabled = true;
		 
		this.inConversation = true;

	}

	private void endConversation() {
		this.inConversation = false;
		this.playerConversationStatus.setConversationReadiness(true);

	}

}

	