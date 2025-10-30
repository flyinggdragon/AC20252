using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Spectrum : MonoBehaviour
{
    AudioSource audioSource;
    public float[] spectrum = new float[512];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumData();
    }

    private void GetSpectrumData() {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
    }
}
