using UnityEngine;
using System.Collections;

public class Epilogue : MonoBehaviour {

	public GUISkin skin;

	public float initialDelay = 3;

	public float lineDelay = 5;

	public bool firstEnabled;
	public bool secondEnabled;
	public bool thirdEnabled;
	private AudioSource letterSound;

	private string[] sourceLines = new string[3];
	private string[] lines = new string[3];

	private int currentIndex = -1;

	public float dt;

	public float textSpeed = 80.0f;

	private float timeStart;

	private StatsTracker stats;

	public int fontSize = 12;
	
	// Use this for initialization
	void Start () {
		
		firstEnabled = false;
		secondEnabled = false;
		thirdEnabled = false;

		letterSound = GameObject.FindWithTag("LetterSound").GetComponent<AudioSource>();

		stats = GameObject.FindGameObjectWithTag("StatsTracker").GetComponent<StatsTracker>();

		sourceLines[0] = getTimeText();
		sourceLines[1] = getMoodText();
		sourceLines[2] = getSobrietyText();

		lines[0] = "";
		lines[1] = "";
		lines[2] = "";

		timeStart = Time.time;

		StartCoroutine("triggerAnimation");

		
	}

	private string getTimeText() {

		if (stats.timeSpentAtParty < 3) {
			return "You didn't go to the party. Everyone asked where you were on Monday Morning.";
		} else if (stats.timeSpentAtParty < 120) {
			return "You barely spent any time at the party. People asked questions about it afterwards.";
		} else if (stats.timeSpentAtParty < 60 * 5) {
			return "You made an apperance at the party. You never got to see the boss.";
		} else if (stats.timeSpentAtParty < 60 * 15) {
			return "You stayed at the party a reasonable amount of time.";
		} else {
			return "You closed the party down. You didn't help clean up.";
		}

	}

	private string getMoodText() {

		if (stats.playerMood < 10) {
			return "You were in a foul mood when you left the party. You cursed under your breath while leaving. People heard you.";
		} else if (stats.playerMood < 30) {
			return "You left feeling down in the dumps. It's nothing saturday morning cartoons won't fix though.";
		} else if (stats.playerMood < 55) {
			return "A malaise overcame you as you walked to your car. You resolve to quit your job when you save enough money.";
		} else if (stats.playerMood < 71) {
			return "All in all, you didn't have a great time, but you survived.";
		}  else if (stats.playerMood < 81) {
			return "You left in pretty much the same mood you arrived in.";
		}else {
			return "You left happier than you came. A successful party.";
		}
	}
	
	private string getSobrietyText() {

		if (stats.playerSobriety < 30) {
			return "You passed out before making it to your car. Your friend took care of you and gave you a ride home.";
		} else if (stats.playerSobriety < 35) {
			return "You threw up on your car, and somehow managed to call a cab home. You swear off punch forever.";
		} else if (stats.playerSobriety < 51) {
			return "You don't have it in you to drive home, so you fall asleep in your back seat.";
		} else if (stats.playerSobriety < 66) {
			return "You are feeling tipsy as you leave. Your friend comes outside and you talk for a few hours before driving home.";
		} else if (stats.playerSobriety < 81) {
			return "You walk around in the field for a while to sober up after one drink of punch before driving away.";
		} else {
			return "You stayed stone cold sober, a wise choice.";
		}	}

	void Update() {

		if (currentIndex >= 0) {

			string text = sourceLines[currentIndex];
			int chars = (int) ((Time.time - timeStart) * textSpeed);
			if (lines[currentIndex].Length < text.Length) {
				lines[currentIndex] = text.Substring(0, Mathf.Clamp(chars, 0, text.Length));
				letterSound.Play();
			}

		}

	}

	
	IEnumerator triggerAnimation() {
		

		yield return new WaitForSeconds(initialDelay);

		timeStart = Time.time;
		currentIndex++;
		firstEnabled = true;

		yield return new WaitForSeconds(lineDelay);

		timeStart = Time.time;
		currentIndex++;
		secondEnabled = true;

		yield return new WaitForSeconds(lineDelay);

		timeStart = Time.time;
		currentIndex++;
		thirdEnabled = true;

	}
	
	
	void OnGUI() {

		
		if (this.skin) {
			GUI.skin = this.skin;
		}

		this.skin.label.fontSize = fontSize;
		
		GUI.Label(new Rect (0,  Screen.height/4, Screen.width, 50), lines[0]);
		GUI.Label(new Rect (0,  2 * Screen.height/4, Screen.width, 50), lines[1]);
		GUI.Label(new Rect (0,  3 * Screen.height/4, Screen.width, 50), lines[2]);

	}
}
