using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPS : MonoBehaviour {

  	public int count;
	public int samples = 100;
	public float totalTime;

	public Text fpsText;
	
	public void Start(){
		Application.targetFrameRate = 60;
		count = samples;
		totalTime = 0f;
	}
 
	public void Update(){
		count -= 1;
		totalTime += Time.deltaTime;
	
		if (count <= 0) {
			float fps = samples / totalTime;
			displayFPS(fps); // your way of displaying number. Log it, put it to text object…
			totalTime = 0f;
			count = samples;
		}
	}

	private void displayFPS(float fps) {
		this.fpsText.text = ((int)fps).ToString();
	}
}