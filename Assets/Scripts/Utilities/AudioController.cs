using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

	public SoundSet[] soundSet;

	private AudioSource audioSource;

	void Awake () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	public void SetRepeating (bool enabled) {
		audioSource.loop = enabled;
	}

    // Play sound randomly from the selected sound set
	public void PlaySoundType (string soundType) {
		for(int i = 0; i < soundSet.Length; i++) {
			if (soundType == soundSet [i].soundType) {
				audioSource.PlayOneShot (Randomizer (i));
				return;
			}
		}

		Debug.Log (gameObject.name + ": No sound set [" + soundType + "] found!");
	}

    // Play music randomly from the selected sound set
    public void PlayMusicType (string soundType) {
        for (int i = 0; i < soundSet.Length; i++) {
            if (soundType == soundSet[i].soundType) {
                audioSource.clip = Randomizer (i);
                audioSource.Play ();
                return;
            }
        }

        Debug.Log (gameObject.name + ": No sound set [" + soundType + "] found!");
    }

    AudioClip Randomizer (int index) {
		int size = soundSet [index].audios.Length;
		System.Random r = new System.Random();
		int rIndex = r.Next(0, size);
		return soundSet [index].audios [rIndex];
	}

	[Serializable]
	public struct SoundSet {
		public string soundType;
		public AudioClip[] audios;
	}
}

