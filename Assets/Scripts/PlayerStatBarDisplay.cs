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

	private Vector2 pos;
	private Vector2 size;

	private PlayerStatBar statBar;

	public bool drawBar = true;

	public GUISkin customSkin;

	void OnGUI()
	{
		//draw the background:
		if (drawBar) {

			GUI.depth = 2;
			GUI.Box(new Rect (pos.x, pos.y, size.x, size.y), "");
			GUI.skin = customSkin;
			GUI.Box(new Rect(pos.x, pos.y, statBar.getStatPercentage() * size.x, size.y), "");
			GUI.Label(new Rect (pos.x, pos.y, size.x, size.y), statBar.label);
			GUI.skin = null;

		}
	}

	void Start() 
	{
		this.statBar = this.gameObject.GetComponent<PlayerStatBar>();
		this.pos = new Vector2(boxPosX,boxPosY);
		this.size = new Vector2(boxSizeX,boxSizeY);
	}
}