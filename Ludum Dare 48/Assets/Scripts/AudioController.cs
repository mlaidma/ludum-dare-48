using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] AudioClip menuTheme;

    private AudioSource audioSource;

    private Level level;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = menuTheme;
        audioSource.Play();

        StartCoroutine(PlayMenuTheme());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayMenuTheme()
    {
        while(true)
        {
            if(!audioSource.isPlaying) audioSource.Play();

            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }
}
