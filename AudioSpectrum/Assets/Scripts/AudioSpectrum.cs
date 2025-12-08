using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSpectrum : MonoBehaviour {
    public AudioSource source;

    private int numSamples = 512;
    private int numBands = 8;
    public static float[] samples;
    public static float[] bands;

    private void Start() {
        samples = new float[numSamples];
        bands = new float[numBands];
    }

    private void Update() {
        source.GetSpectrumData(samples, 0, FFTWindow.Hamming);
        GetFrequencyBands();
    }

    private void GetFrequencyBands() {
        int count = 0;
        
        for (int i = 0; i < numBands; i++) {
            float avg = 0;
            int sampleCount = (int)Mathf.Pow (2, i) * 2;

            for (int j = 0; j < count; j++) {
                avg += samples[sampleCount] * (sampleCount + 1);
                count++;
            }
            
            avg /= count;
            bands[i] = avg * 10;
        }
    }
}