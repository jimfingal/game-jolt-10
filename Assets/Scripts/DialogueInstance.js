// Copyright 2010 Royce Kimmons | royce@kimmonsdesign.com
// Released under Creative Commons Attribution 3.0 License
// http://creativecommons.org/licenses/by/3.0/

var dialogueSkin :GUISkin;
var startOn :
int = 0;
var defaultImg : Texture2D;
var defaultAudio :AudioClip;
var dialogue : DialogueEntry[] = [new DialogueEntry()]; 
private var display : DialogueEntry = new DialogueEntry(); 
private var waitTime :float = 0.2; 
private var lineCount : int = 0; 
private var timeStart : float = 0.0; 
private var textSpeed : float = 80.0; 

private var parsedText : String[];
private var pwEntry : String = ""; 
private var toLoad : int = -1; 
private var curItem : int = 0; 
private var history : Array = new Array(); 

private var phase :int = 0; 
private var curContent : GUIContent = GUIContent(""); 
private var glow : float = 0; 
private var aud :AudioSource;
var bools : boolean[] = [false, false, false, false];
var ints : int[] = [0, 0, 0, 0];
var strings : String[] = ["", "", "", ""]; 
private var jumpto :int;
private var s : Vector2 = Vector2.zero;
private var movement : GameObject;
var endOn: int = 50; 

private var letterSound : Component; 

private var displayChoice : boolean = false;


private var TOP : int = 0;
private var MIDDLE : int = 1;
private var BOTTOM : int = 2;

private var CONTINUE : int = 0;
private var CHOICE : int = 1;
private var PASSWORD : int = 2;
private var SCRIPT : int = 3;
private var END : int = 4;
private var AUTOEND : int = 5;
private var AUTOCONTINUE : int = 6;


private var inCoroutine : boolean = false;

class DialogueEntry {
	var name :String;
	var longText :String;
	var img :Texture2D;
	var choices : DialogueChoice[];
	var choiceMode : int = 1;
	var links : Link[];
	var next :int;
	var editor :boolean;
	var pos :
	int = 0;
	// 0 - top, 1 - bottom, 2 - middle
	var passwords : DialogueChoice[];
	var incorrect :int;
	var exit :DialogueChoice;
	var align :int;
	var mode :int;
	var narration : AudioClip[];
	var script :String;
	var wait : int = 5;
	var scriptRunning : boolean = false;
		

	// 0 - next, 1 - choice, 2 - password, 3 - event, 4 - end
	function DialogueEntry() {
		name = "Name";
		longText = "New entry";
		next = 0;
		editor = false;
		incorrect = 0;
		align = 0;
		exit = new DialogueChoice("Exit");
		mode = 0;

	}
}


class DialogueChoice {
	var shortText :String;
	var next :int;
	var editor :boolean;
	function DialogueChoice() {
		shortText = "New Choice";
		editor = false;
	}

	function DialogueChoice (s:String) {
		shortText = s;
		editor = false;
	}
}

class Link {
	var shortText :String;
	var url :String;
	function Link() {
		shortText = "URL Description";
		url = "http://";
	}

}

function Start() {
// Hax
	var tmp : GameObject;
	tmp = GameObject.FindWithTag("LetterSound");
	letterSound = tmp.GetComponent(AudioSource);
	Restart();
}

function OnEnable() {
	Restart();
}

function OnDisable() {
	if(aud) {
		if(aud.clip) {
			if(aud.isPlaying)
				aud.Stop();
		}
	}
}

function Update() {
	var text = parsedText[lineCount];
	var chars = (Time.time - timeStart) * textSpeed;
	if(chars < text.length) {
		text = text.Substring(0, chars);
		// if (!letterSound.isPlaying) {
			letterSound.Play();
		//}
		displayChoice = false;
	} else {
		displayChoice = true;
	}
	var img :Texture2D;
	if(defaultImg)
		img = defaultImg;
	if(display.img)
		img = display.img;
	curContent = GUIContent(text);
	switch (display.mode) {
		case 2:
			if(Input.GetKeyDown("return"))
				EvaluatePassword();
			break;
	}
	glow = Mathf.PingPong(Time.time / 4, .4) + .6;
}

function OnGUI() {
	if(dialogue) {
		if(dialogue.length > 0) {
			if(display) {
				GUI.skin = dialogueSkin;
				if(curContent) {
					switch (display.pos) {
						case 0:
							GUILayout.BeginHorizontal("textbox", GUILayout.Width(Screen.width));
							if(display.align == 0 && curContent.image)
								GUILayout.Label(curContent.image, "img");
							GUILayout.Label(curContent.text, "text");
							if(display.align == 1 && curContent.image)
								GUILayout.Label(curContent.image, GUILayout.Width(256));
							GUILayout.EndHorizontal();
							break;
						case 1:
							
							var p = 0;
							if(display.align == 1)
								p = Screen.width - 400;
							var p2 = 20;
							if(display.align == 1)
								p2 = Screen.width - 220;
							GUI.Label(Rect(p, Screen.height - 517, 400, 400), display.img);
							GUI.Box(Rect(p2, Screen.height - 41 - 117, 200, 41), display.name, "namebar");
							GUI.Box(Rect(0, Screen.height - 120, Screen.width, 120), curContent, "textboxplayer");
							break;
						case 2:
						/*
							GUI.Box(Rect(30, Screen.height / 2 - 60, Screen.width - 60, 120), curContent, "textboxmiddle");
							break;
							*/
							GUILayout.BeginHorizontal("textboxinternal", GUILayout.Width(Screen.width));
							if(display.align == 0 && curContent.image)
								GUILayout.Label(curContent.image, "img");
							GUILayout.Label(curContent.text, "text");
							if(display.align == 1 && curContent.image)
								GUILayout.Label(curContent.image, GUILayout.Width(256));
							GUILayout.EndHorizontal();
					}
					/*	for (var l in display.links) {
					 GUILayout.Button(l.shortText);
					 }*/
					DoNextButton();
				}
				if(display.choices && displayChoice) {
					if(display.choices.length > 0 && lineCount >= parsedText.length - 1 && display.mode == 1) {
						if(display.choiceMode == 1) {
							ShowWheel();
						} else {
							ShowList();
						}
					}
				}
			}
		}
	}
}

function DoNextButton() {

	var nextHeight : int = 0;

		if (displayChoice) {
		switch(display.mode) {
			case 0:
				if(lineCount < parsedText.length - 1) {
					if(GUI.Button(Rect(Screen.width - 84, nextHeight, 64, 64), "Next", "arrow"))
						ProgressLineCount();
				} else {
					if(GUI.Button(Rect(Screen.width - 84, nextHeight, 64, 64), "Next", "arrow"))
						LoadDialogue(display.next);
				}
				break;
			case 1:
				if(lineCount < parsedText.length - 1) {
					if(GUI.Button(Rect(Screen.width - 84, nextHeight, 64, 64), "Next", "arrow"))
						ProgressLineCount();
				}
				break;
			case 2:
				if(lineCount < parsedText.length - 1) {
					if(GUI.Button(Rect(Screen.width - 84, nextHeight, 64, 64), "Next", "arrow"))
						ProgressLineCount();
				} else {
					ShowPassword();
				}
				break;
			case 3:
				if(lineCount < parsedText.length - 1) {
					if(GUI.Button(Rect(Screen.width - 84, nextHeight, 64, 64), "Next", "arrow"))
						ProgressLineCount();
				} else {
				
					if (!display.scriptRunning) {
						if(GUI.Button(Rect(Screen.width - 84, nextHeight, 64, 64), "Next", "arrow")) {
							// var d = gameObject.GetComponent("DialogueInstance");
							display.scriptRunning = true;
							eval(display.script);
						}
					}
				}
				break;
			case 4:
				if(lineCount < parsedText.length - 1) {
					if(GUI.Button(Rect(Screen.width - 84, nextHeight, 64, 64), "Next", "arrow"))
						ProgressLineCount();
				} else {
					if(GUI.Button(Rect(Screen.width - 84, nextHeight, 64, 64), "Next", "arrow"))
						EndDialogue();
				}
				break;
			case 5:
				if(lineCount < parsedText.length - 1) {
					if(GUI.Button(Rect(Screen.width - 84, nextHeight, 64, 64), "Next", "arrow"))
						ProgressLineCount();
				} else if (!inCoroutine) {
					WaitAndEnd(display.wait);
				} else if (Input.GetButtonDown("Talk")) {
					AbruptEnd();
				}
				break;
			case 6:
				if(lineCount < parsedText.length - 1) {
					if(GUI.Button(Rect(Screen.width - 84, nextHeight, 64, 64), "Next", "arrow"))
						ProgressLineCount();
				} else if (!inCoroutine) {
					WaitAndContinue(display.wait, display.next);
				} else if (Input.GetButtonDown("Talk")) {
					CancelAndContinue(display.next);
				}
				break;
		}
	}
}


function AbruptEnd() {
	StopCoroutine("WaitAndEnd");
	End();
}

function WaitAndEnd (waitTime : float) {
	inCoroutine = true;
	// suspend execution for waitTime seconds
	yield WaitForSeconds (waitTime);
	End();
}

function End() {
	inCoroutine = false;
	parsedText = new Array();
	curContent = GUIContent("");
	EndDialogue();
}


function CancelAndContinue(next : int) {
	StopCoroutine("WaitAndContinue");
	inCoroutine = false;
	LoadDialogue(next);
}

function WaitAndContinue (waitTime : float, next : int) {
		// suspend execution for waitTime seconds
		inCoroutine = true;
		yield WaitForSeconds (waitTime);
		inCoroutine = false;
		LoadDialogue(next);
}

function Restart() {
	LoadDialogue(startOn);
	timeStart = Time.time;
	lineCount = 0;
}

function LoadDialogue (i:int) {
	displayChoice = false;
	curItem = i;
	curContent = GUIContent("");
	timeStart = Time.time;
	display = new DialogueEntry();
	display = dialogue[i];
	lineCount = 0;
	ParseText(display.longText);
	if(!aud) {
		gameObject.AddComponent(AudioSource);
		aud = gameObject.GetComponent(AudioSource);
	}
	display.scriptRunning = false;
	PlayClip(i);
}

function ParseText (s:String) {
	parsedText = s.Split("|"[0]);
}

function EndDialogue() {
	this.enabled = false;
}

function ShowPassword() {
	pwEntry = GUI.TextField(Rect(Screen.width / 2 - 200, Screen.height / 2 + 30, 380, 60), pwEntry, "inputbox");
	if(pwEntry != "") {
		StartGlow();
		if(GUI.Button(Rect(Screen.width / 2 + 170, Screen.height / 2 + 30, 64, 64), "", "inputboxbutton"))
			EvaluatePassword();
		EndGlow();
	}
	if(GUI.Button(Rect(20, Screen.height - 84, Screen.width - 40, 64), display.exit.shortText, "exit"))
		LoadDialogue(display.exit.next);
}

function EvaluatePassword() {
	if(pwEntry != "") {
		var validate :
		boolean = false;
		for(var p in display.passwords) {
			if(pwEntry == p.shortText && validate == false) {
				LoadDialogue(p.next);
				validate = true;
			}
		}
		if(validate == false) {
			LoadDialogue(display.incorrect);
		}
		pwEntry = "";
	}
}

function ShowWheel() {
	GUI.Label(Rect(Screen.width / 2 - 140, Screen.height - 163, 280, 133), "", "wheelbase");
	if(display.choices.length > 0) {
		if(GUI.Button(Rect(0, Screen.height - 163, Screen.width / 2, 43), display.choices[0].shortText, "r1c1active"))
			toLoad = display.choices[0].next;
	} else {
		GUI.Label(Rect(0, Screen.height - 163, Screen.width / 2, 43), "");
	}
	if(display.choices.length > 1) {
		if(GUI.Button(Rect(Screen.width / 2, Screen.height - 163, Screen.width / 2, 43), display.choices[1].shortText, "r1c2active"))
			toLoad = display.choices[1].next;
	} else {
		GUI.Label(Rect(Screen.width / 2, Screen.height - 163, Screen.width / 2, 43), "");
	}
	if(display.choices.length > 2) {
		if(GUI.Button(Rect(0, Screen.height - 120, Screen.width / 2, 40), display.choices[2].shortText, "r2c1active"))
			toLoad = display.choices[2].next;
	} else {
		GUI.Label(Rect(0, Screen.height - 120, Screen.width / 2, 40), "");
	}
	if(display.choices.length > 3) {
		if(GUI.Button(Rect(Screen.width / 2, Screen.height - 120, Screen.width / 2, 40), display.choices[3].shortText, "r2c2active"))
			toLoad = display.choices[3].next;
	} else {
		GUI.Label(Rect(Screen.width / 2, Screen.height - 120, Screen.width / 2, 40), "");
	}

	if(display.choices.length > 4) {
		if(GUI.Button(Rect(0, Screen.height - 80, Screen.width / 2, 51), display.choices[4].shortText, "r3c1active"))
			toLoad = display.choices[4].next;
	} else {
		GUI.Label(Rect(0, Screen.height - 80, Screen.width / 2, 51), "");
	}
	if(display.choices.length > 5) {
		if(GUI.Button(Rect(Screen.width / 2, Screen.height - 80, Screen.width / 2, 51), display.choices[5].shortText, "r3c2active"))
			toLoad = display.choices[5].next;
	} else {
		GUI.Label(Rect(Screen.width / 2, Screen.height - 80, Screen.width / 2, 59), "");
	}

}

function ShowList() {
	GUILayout.BeginArea(Rect(Screen.width / 4, Screen.height / 2, Screen.width / 2, display.choices.length * 34 + 12), "", "box");
	s = GUILayout.BeginScrollView(s);
	for(var c in display.choices) {
		if(GUILayout.Button(c.shortText, GUILayout.Height(30)))
			toLoad = c.next;
	}
	GUILayout.EndScrollView();
	GUILayout.EndArea();
}

function ProgressLineCount() {
	timeStart = Time.time;
	lineCount++;
	PlayClip(curItem);
}

function FixedUpdate() {
	if(toLoad != -1) {
		LoadDialogue(toLoad);
		s = Vector2.zero;
	}
	toLoad = -1;
}

function StartGlow() {
	GUI.color.a = glow;
}

function EndGlow() {
	GUI.color.a = 1.0;
}

function PlayClip (i:int) {
	if(aud.clip) {
		if(aud.isPlaying)
			aud.Stop();
	}
	if(dialogue[i].narration) {
		if(dialogue[i].narration.length > lineCount) {
			if(dialogue[i].narration[lineCount]) {
				aud.clip = dialogue[i].narration[lineCount];
				aud.loop = false;
				aud.Play();
			}
		}
	}
}