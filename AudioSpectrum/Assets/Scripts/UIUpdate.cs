using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIUpdate : MonoBehaviour {
    [SerializeField] public Text songName;
    [SerializeField] public Text danceSpeed;
    [SerializeField] public Text volume;
    
    public void UpdateSongName(string name, string artist) {
        songName.text = "NP: " + name + " - " + artist;
    }

    public void UpdateDanceSpeed(float speed) {
        danceSpeed.text = speed.ToString("0.00");
    }

    public void UpdateVolume(float value) {
        volume.text = value.ToString("0.00");
    }
}
