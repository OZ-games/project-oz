using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitHandler : MonoBehaviour
{
    public void Quit()
    {
        AndroidJavaObject activity = new AndroidJavaClass("com.metalense.ProjectOZ").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack", true);
    }
}
