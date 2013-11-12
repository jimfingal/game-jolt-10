using UnityEngine;
using System.Collections;

public class ConversationStatus : MonoBehaviour {

	public bool readyForConversation = true;

	public void setConversationReadiness(bool value) {
		this.readyForConversation = value;
	}

	public bool isReadyForNewConversation() {
		return readyForConversation;
	}
}
