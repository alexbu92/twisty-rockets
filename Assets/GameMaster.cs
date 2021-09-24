using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    public Timer timer;
    public Ghost ghost;
    public GhostPlayback ghostPlayback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLevelReset()
    {
        ghost.StopRecordingGhost();
        ghostPlayback.StopGhostPlayback();
        ghost.StartRecordingGhost();
        ghostPlayback.StartGhostPlayback();
    }

    public void OnFinishLine(float time)
    {
        ghost.StopRecordingGhost();
        if (time <= timer.bestTime)
        {
            Debug.Log("New best time: " + time);
            ghostPlayback.setBestReplay(ghost.getCurrentReplay());
        }
    }
}
