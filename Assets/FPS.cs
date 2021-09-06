using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour {

    public int targetFrameRate = 60;
    string label = "";
	float count;

	
	IEnumerator Start ()
	{
        Application.targetFrameRate = 60;
		GUI.depth = 2;
		while (true) {
			if (Time.timeScale == 1) {
				yield return new WaitForSeconds (0.1f);
				count = (1 / Time.deltaTime);
				label = "FPS :" + (Mathf.Round (count));
			} else {
				label = "Pause";
			}
			yield return new WaitForSeconds (0.5f);
		}
	}
	
	void OnGUI ()
	{
        Debug.Log(label);
		GUI.Label (new Rect (5, 40, 100, 25), label);
	}
}