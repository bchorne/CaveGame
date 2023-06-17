using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=f1xtE_Jkf7o

public class footsteps : MonoBehaviour
{
    public Movement movement;

    public AudioClip[] steps;
    public List<AudioClip> randomList;
    AudioSource src;

    [SerializeField] float walkingSpeed = 0.5f;
    [SerializeField] float pitchMin = 0.95f;
    [SerializeField] float pitchMax = 1.05f;
    [SerializeField] float volMin = 0.95f;
    [SerializeField] float volMax = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        randomList = new List<AudioClip>(new AudioClip[steps.Length]);
        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
        src = gameObject.AddComponent<AudioSource>();

        for (int i = 0; i < steps.Length; i++)
        {
            randomList[i] = steps[i];
        }
    }

    public void Reset()
    {
        for (int i = 0; i < steps.Length; i++)
        {
            randomList.Add(steps[i]);
        }
    }

    private void CallFootsteps()
    {
        if (movement.isMoving)
        {
            PlayRandomSound();
        }
    }

    private void PlayRandomSound()
    {
        int i = Random.Range(0, randomList.Count);
        src.pitch = Random.Range(pitchMin, pitchMax);
        src.volume = Random.Range(volMin, volMax);
        src.PlayOneShot(randomList[i]);
        randomList.RemoveAt(i);

        if (randomList.Count == 0)
        {
            Reset();
        }
    }
}
