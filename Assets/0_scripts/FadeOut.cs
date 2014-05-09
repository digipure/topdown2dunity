using UnityEngine;
using System.Collections;

/*
Fade in / out
*/

public class FadeOut : MonoBehaviour {

	public Texture2D fadeTexture;
	public float fadeSpeed = 0.2f;
	public int drawDepth = -1000;
	
	private float alpha = 1.0f; 
	private float fadeDir = -1;

	private bool end=true;
	private bool fading = false;

	public System.Action endDelegate;

	
	public void DoFadeOut(int speed, System.Action _endDelegate) {	
		fadeSpeed = 6f/(float)speed;
		endDelegate = _endDelegate;
		enabled = true;
		StartFadeOut();
	}
	public void DoFadeIn(int speed,System.Action _endDelegate) {
		fadeSpeed = 6f/(float)speed;
		enabled = true;
		endDelegate = _endDelegate;
		StartFadeIn();
	}
	
	public bool isFading() { return !end; }
	
	public void ClearFade() {
		fading = false; 
	}
	
	
	
	
	
	
	void StartFadeOut() {
		fadeDir = 1;
		alpha = 0.0f; 
		fading = true;
		enabled = true;
		end = false;
	}
	void StartFadeIn() {
		fadeDir = -1;
		alpha = 1.0f; 
		fading = true;
		enabled = true;
		end = false;
	}
	


	
	void OnGUI(){
		if (fading) {
		    alpha += fadeDir * fadeSpeed * Time.deltaTime;  
		    alpha = Mathf.Clamp01(alpha);   
		 
			Color c = GUI.color;
			c.a = alpha;
			
		    GUI.color = c;
		 
		    GUI.depth = drawDepth;
		 
		    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
			
			if (alpha>=1 && fadeDir==1) {
				if (!end && endDelegate!=null) {
					end = true;
					endDelegate();
				}
				
			}
			
			if (alpha<=0 && fadeDir==-1) {							
				if (!end && endDelegate!=null) {
					end = true;					
					endDelegate();				
				}
				
			}
		}
			
		
		
	}	
}
