Simply drag "Ghost" script onto GameObject you want to have a ghost associated with.

StopRecordingGhost()
Call to Begin the recording

StartRecordingGhost()
Call to End Ghost Recording

playGhostRecording()
Call to play previously recorded ghost



Saving the file
---------------
The last recording will be saved in a variable "lastReplayList" which can be serialized into a binary file and stored on the disk.
Call the function SaveGhostToFile() to save the recording.
You can call this function after checking your PlayerPrefs or Database if a record has been broken.
This will overwrite the current filename "Ghost" so be sure to add a dynamic filename should you need to keep multiple different recordings.


Recovering a previous saved GhostShot
-------------------------------------
You can deserialize a file into a "List<GhostShot>" variable and play it through the MoveGhost function.
This function uses the "lastReplayList" variable name so load your file into that variable or create another move function with your new variable name.


Expanding functionality
-----------------------
You can add as many variables to the GhostShot class as you like.
Example:
public class GhostShot {
   ....
   //posMark
   //rotMark
   ...

   public float carSoundRPM; //New Sound Tracking Variable
}

private void RecordFrame() {
	recordTime += Time.smoothDeltaTime * 1000;
    GhostShot newFrame = new GhostShot()
    {
        timeMark = recordTime,
		posMark = car.transform.position,
		rotMark = car.transform.rotation,
		carSoundRPM = realisticEngineSound.getRPM()
    };
    framesList.Add(newFrame);
}

This adds the car RPM to the recording so we can use it on playback to play the cars engine sound.
You can use this to store any additional information you need just save it with the other data each frame.


Uploading
---------
You can upload the file to your server like this:
You will need to save the ghost to disk first and upload that file.
Please consider writing your own php upload file with some security measures.


public IEnumerator UploadAFile() {
	byte[] bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/Ghost"); //Read saved file from disk
    var form = new WWWForm();
    form.AddBinaryData("theFile", bytes, "Ghost");

	//POST the file to DB
    WWW w = new WWW("https://www.mywebsite.com/UploadGhost.php", form);

    yield return w;

    if (w.error != null) { Debug.Log("UploadAFile Log !Null: "+w.error); }
    else { Debug.Log("UploadAFile Log Null: "+w.text); }
}


//UploadGhost.php
<?php
if(isset($_FILES['theFile'])) { move_uploaded_file($_FILES['theFile']['tmp_name'], "/" . $_FILES['theFile']['name']); }
else { print("Failed!"); }
?>


Assistance - tombye_07@hotmail.com
