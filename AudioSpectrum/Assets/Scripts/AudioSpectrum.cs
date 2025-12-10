using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSpectrum : MonoBehaviour {
    private AudioSource _source;
    public List<Transform> objs;
    public Transform volumeBar;
    public bool isMusicPlaying = true;

    private int _numSamples = 512;
    private int _numBands = 8;
    public float[] samples;
    public float[] bands;

    [SerializeField] public UIUpdate uiUpdate;

    private void Awake() {
        _source = GetComponent<AudioSource>();
    }

    private void Start() {
        samples = new float[_numSamples];
        bands = new float[_numBands];
    }

    private void Update() {
        _source.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        GetFrequencyBands();

        ResizeObjs();
    }

    private void GetFrequencyBands() {
        int count = 0;

        for (int i = 0; i < _numBands; i++) {
            float average = 0f;

            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == _numBands - 1) sampleCount += 2;

            for (int j = 0; j < sampleCount; j++) {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= sampleCount;
            bands[i] = average * 10f;
        }
    }

    private void ResizeObjs() {
        for (int i = 0; i < _numBands; i++) {
            objs[i].localScale = new(bands[i], bands[i], bands[i]);
        }

        float volume = SumOfAllBands();

        // Dividido por 2f só para caber na tela.
        volumeBar.localScale = new(volumeBar.localScale.x, volume / 2f, volumeBar.localScale.z);
        uiUpdate.UpdateVolume(volume);
    }

    public float SumOfAllBands() {
        float sum = 0f;

        foreach (float b in bands) {
            sum += b;
        }

        return sum;
    }
}

