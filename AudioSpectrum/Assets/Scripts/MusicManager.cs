using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    
    [SerializeField] public AudioSource _source;
    private int songIndex = 1; // Index começa em 0 para seguir o Dicionário
    public List<AudioClip> audioClips;
    private SongData currentSong;
    private Dictionary<int, SongData> songs;

    [SerializeField] public UIUpdate uiUpdate;

    private void Start() {
        GenerateDictionary();

        currentSong = songs[1];
        uiUpdate.UpdateSongName(currentSong.songName, currentSong.artistName);
        PlayNextMusic(currentSong);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) ScrollMusic(1);
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) ScrollMusic(-1);
    }

    private void GenerateDictionary() {
        songs = new Dictionary<int, SongData>();

        SongData s1 = new SongData(1, "Monkeys Spinning Monkeys", "Kevin MacLeod", audioClips[0], 1.0f);
        songs.Add(s1.id, s1);

        SongData s2 = new SongData(2, "百鬼夜行-Pandemonic Night Parade-", "Imperial Circus Dead Decadence", audioClips[1], .333f);
        songs.Add(s2.id, s2);

        SongData s3 = new SongData(3, "Hyper Sonic", "Blood Stain Child", audioClips[2], .375f);
        songs.Add(s3.id, s3);

        SongData s4 = new SongData(4, "Switched-on Lotus", "Susumu Hirasawa", audioClips[3], .426f);
        songs.Add(s4.id, s4);

        SongData s5 = new SongData(5, "IMAGE -MATERIAL- (Version 0)", "Tatsh", audioClips[4], .296f);
        songs.Add(s5.id, s5);

        SongData s6 = new SongData(6, "Água de Beber", "Tom Jobim", audioClips[5], .655f);
        songs.Add(s6.id, s6);
    }

    private void ScrollMusic(int increment) {
        if (!_source.isPlaying) return;
        
        int nextIndex = songIndex;
        nextIndex += increment;
        if (nextIndex > audioClips.Count || nextIndex < 1) return;

        songIndex = nextIndex;
        SongData newSong = songs[songIndex];

        Debug.Log("id agora: " + songIndex);

        PlayNextMusic(newSong);
    }

    private void PlayNextMusic(SongData newSong) {
        _source.Stop();
        _source.clip = newSong.clip;
        _source.volume = newSong.volume;
        _source.Play();

        uiUpdate.UpdateSongName(newSong.songName, newSong.artistName);
    }

    [System.Serializable]
    public class SongData {
        public int id;
        public string songName;
        public string artistName;
        public AudioClip clip;
        public float volume;
    
        public SongData(int id, string songName, string artistName, AudioClip clip, float volume) {
            this.id = id;
            this.songName = songName;
            this.artistName = artistName;
            this.clip = clip;
            this.volume = volume;
        }
    }
}

