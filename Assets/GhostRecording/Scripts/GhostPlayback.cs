using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GhostPlayback : MonoBehaviour
{

    private GameObject theGhost;

    private List<GhostShot> bestReplay = null;

    private int replayIndex = 0;

    private float replayTimescale = 1;

    private float replayTime = 0.0f;

    private bool playRecording = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bestReplay != null && playRecording) {
            MoveGhost();
        }
    }

    public void StartGhostPlayback() {
		CreateGhost();
		replayIndex = 0;
		playRecording = true;
	}

    public void StopGhostPlayback() {
        replayIndex = 0;
        playRecording = false;
    }

    public void MoveGhost()
    {
        replayIndex++;

        if (replayIndex < bestReplay.Count)
        {
            GhostShot frame = bestReplay[replayIndex];
            DoLerp(bestReplay[replayIndex - 1], frame);
            replayTime += Time.smoothDeltaTime * 1000 * replayTimescale;
        }
    }

    private void DoLerp(GhostShot a, GhostShot b)
    {
		if(GameObject.FindWithTag("Ghost") != null) {
	        theGhost.transform.position = Vector3.Slerp(a.posMark, b.posMark, Mathf.Clamp(replayTime, a.timeMark, b.timeMark));
	        theGhost.transform.rotation = Quaternion.Slerp(a.rotMark, b.rotMark, Mathf.Clamp(replayTime, a.timeMark, b.timeMark));
		}
    }

    public void CreateGhost()
    {
		//Check if ghost exists or not, no reason to destroy and create it everytime.
		if(GameObject.FindWithTag("Ghost") == null) {
	        theGhost = Instantiate(Resources.Load("GhostPrefab", typeof(GameObject))) as GameObject;
	        theGhost.gameObject.tag = "Ghost";

	        //Disable RigidBody
	        // theGhost.GetComponent<Rigidbody>().isKinematic = true;

            MeshRenderer mr = theGhost.gameObject.GetComponent<MeshRenderer>();
            mr.material = Resources.Load("Ghost_Shader", typeof(Material)) as Material;
		}
    }

    public void SaveGhostToFile()
    {
        // Prepare to write
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Ghost");
        Debug.Log("File Location: " + Application.persistentDataPath + "/Ghost");
        // Write data to disk
        bf.Serialize(file, bestReplay);
        file.Close();
    }

    public void loadFromFile() {
		//Check if Ghost file exists. If it does load it
		if(File.Exists(Application.persistentDataPath + "/Ghost")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Ghost", FileMode.Open);
			bestReplay = (List<GhostShot>)bf.Deserialize(file);
			file.Close();
		} else {
			Debug.Log("No Ghost Found");
		}
	}

    public void setBestReplay(List<GhostShot> replayList) {
        bestReplay = new List<GhostShot>(replayList);
    }
}
