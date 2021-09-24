using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFinishLine()
    {
        UI.SetActive(true);
        Debug.Log("pause menu active: " + gameObject.activeSelf.ToString());
    }

    public void OnPlayAgain()
    {
        UI.SetActive(false);
    }
}
