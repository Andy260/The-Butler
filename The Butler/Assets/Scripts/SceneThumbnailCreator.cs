#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class SceneThumbnailCreator : MonoBehaviour 
{
    bool _takenScreenshot = false;

	void Start() 
    {
        
	}
	
	void Update() 
    {
	    if (!_takenScreenshot)
        {
            string filePath = Application.dataPath + "/Textures/Level Thumbnails/";
            Application.CaptureScreenshot(filePath + Application.loadedLevelName + "_thumbnail.png");

            _takenScreenshot = true;
        }
	}
}
#endif
