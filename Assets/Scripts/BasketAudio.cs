using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketAudio : MonoBehaviour
{
    private GameObject audioSource;

    private List<AudioClip> clips;
    private AudioClip defeatAudio;
    
    void Start()
    {
        audioSource = GameObject.Find("Basket Audio");
        LoadAudioClips();
    }

    private void LoadAudioClips()
    {
        var garrosh = Resources.Load("Sounds/garrosh") as AudioClip;
        var yourRights = Resources.Load("Sounds/yourRights") as AudioClip;
        var zabralo = Resources.Load("Sounds/zabralo") as AudioClip;
        var fbi = Resources.Load("Sounds/fbi") as AudioClip;

        clips = new List<AudioClip>();
        clips.Add(garrosh);
        clips.Add(yourRights);
        clips.Add(zabralo);
        clips.Add(fbi);
        
        defeatAudio = Resources.Load("Sounds/defeat") as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySuccess()
    {
        var randomIndex = Random.Range(0, clips.Count);
        audioSource.GetComponent<AudioSource>().PlayOneShot(clips[randomIndex]);
    }

    public bool isPlaying()
    {
        return audioSource.GetComponent<AudioSource>().isPlaying;
    }

    public void PlayDefeat()
    {
        audioSource.GetComponent<AudioSource>().Stop();
        audioSource.GetComponent<AudioSource>().PlayOneShot(defeatAudio);
    }
}
