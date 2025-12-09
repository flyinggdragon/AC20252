using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSpectrum : MonoBehaviour {
    private AudioSource source;
    public List<GameObject> objs;

    private int numSamples = 512;
    private int numBands = 8;
    public float[] samples;
    public float[] bands;

    private void Start() {
        source = GetComponent<AudioSource>();

        samples = new float[numSamples];
        bands = new float[numBands];
    }

    private void Update() {
        source.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        GetFrequencyBands();

        ResizeObjs();
    }

    private void GetFrequencyBands() {
        int count = 0;

        for (int i = 0; i < numBands; i++) {
            float average = 0f;

            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == numBands - 1) sampleCount += 2;

            for (int j = 0; j < sampleCount; j++) {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= sampleCount;
            bands[i] = average * 10f;
        }
    }

    private void ResizeObjs() {
        for (int i = 0; i < numBands; i++) {
            objs[i].transform.localScale = new(bands[i], bands[i], bands[i]);
        }
    }
}

