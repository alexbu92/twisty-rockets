using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class WB_Vector3 {

	private float x;
	private float y;
	private float z;

	public WB_Vector3() { }
	public WB_Vector3(Vector3 vec3) {
		this.x = vec3.x;
		this.y = vec3.y;
		this.z = vec3.z;
	}

	public static implicit operator WB_Vector3(Vector3 vec3) {
		return new WB_Vector3(vec3);
	}
	public static explicit operator Vector3(WB_Vector3 wb_vec3) {
		return new Vector3(wb_vec3.x, wb_vec3.y, wb_vec3.z);
	}
}

[System.Serializable]
public class WB_Quaternion {

    private float w;
	private float x;
	private float y;
	private float z;

	public WB_Quaternion() { }
	public WB_Quaternion(Quaternion quat3) {
		this.x = quat3.x;
		this.y = quat3.y;
		this.z = quat3.z;
        this.w = quat3.w;
	}

	public static implicit operator WB_Quaternion(Quaternion quat3) {
		return new WB_Quaternion(quat3);
	}
	public static explicit operator Quaternion(WB_Quaternion wb_quat3) {
		return new Quaternion(wb_quat3.x, wb_quat3.y, wb_quat3.z, wb_quat3.w);
	}
}

[System.Serializable]
public class GhostShot
{
    public float timeMark = 0.0f;       // mark at which the position and rotation are of af a given shot

    private WB_Vector3 _posMark;
    public Vector3 posMark {
		get {
			if (_posMark == null) {
				return Vector3.zero;
			} else {
				return (Vector3)_posMark;
			}
		}
		set {
			_posMark = (WB_Vector3)value;
		}
	}

    private WB_Quaternion _rotMark;
    public Quaternion rotMark {
		get {
			if (_rotMark == null) {
				return Quaternion.identity;
			} else {
				return (Quaternion)_rotMark;
			}
		}
		set {
			_rotMark = (WB_Quaternion)value;
		}
	}

}

public class Ghost : MonoBehaviour {

    private List<GhostShot> framesList;
    private List<GhostShot> lastReplayList = null;

    
    
    private float recordTime = 0.0f;
    

    //Check whether we should be recording or not
    private bool startRecording = false, recordingFrame = false;

	

	void FixedUpdate () {
        if (startRecording) {
            startRecording = false;
			Debug.Log("Recording Started");
            StartRecording();
        } else if (recordingFrame) {
		    RecordFrame();
        }
	}

	private void RecordFrame() {
		recordTime += Time.smoothDeltaTime * 1000;
        GhostShot newFrame = new GhostShot()
        {
            timeMark = recordTime,
			posMark = this.transform.position,
			rotMark = this.transform.rotation
        };

        framesList.Add(newFrame);
	}

	private void StartRecording() {
        framesList = new List<GhostShot>();
        recordTime = Time.time * 1000;
        recordingFrame = true;
    }

	public void StopRecordingGhost() {
		recordingFrame = false;
		if (framesList != null && framesList.Count > 0)
		{
			lastReplayList = new List<GhostShot>(framesList);
		}
	}

    public void StartRecordingGhost() {
        startRecording = true;
    }  

	public List<GhostShot> getCurrentReplay() {
		return lastReplayList;
	}
}
