using UnityEngine;
using System.Collections;

public class PlayerStatBarDisplay : MonoBehaviour
{

	//current progress
	public float barDisplay;

	public int boxPosX = 10;
	public int boxPosY = 500;

	public int boxSizeX = 250;
	public int boxSizeY = 50;


	//public Texture2D progressForeground;

	public Vector2 posOffset = new Vector2(25, 130);
	public Vector2 size = new Vector2(250, 500);

	private PlayerStatBar statBar;

	public bool drawBar = true;

	public GUISkin customSkin;

	void OnGUI()
	{
		//draw the background:
		if (drawBar) {

			GUI.depth = 2;
			GUI.Box(new Rect (Screen.width - posOffset.x, Screen.height - posOffset.y, size.x, size.y), "");
			GUI.skin = customSkin;
			GUI.Box(new Rect(Screen.width - posOffset.x, Screen.height - posOffset.y, statBar.getStatPercentage() * size.x, size.y), "");
			GUI.Label(new Rect (Screen.width - posOffset.x, Screen.height - posOffset.y, size.x, size.y), statBar.label);
			GUI.skin = null;

		}
	}

	void Start() 
	{
		this.statBar = this.gameObject.GetComponent<PlayerStatBar>();
	}
}