using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManage : MonoBehaviour
{
    public AudioSource bgmPlayer;
    public AudioSource[] sfxPlayer;
    public AudioClip[] sfxClip;
    int sfxCursor;
    public enum sfx { walksound, knockback }
    // Start is called before the first frame update
    void Start()
    {
        bgmPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SfxPlay(sfx type)
    {
        switch (type)
        {
            case sfx.walksound:
                sfxPlayer[sfxCursor].clip = sfxClip[0];
                break;
            case sfx.knockback:
                sfxPlayer[sfxCursor].clip = sfxClip[1];
                break;
        }
        sfxPlayer[sfxCursor].Play();
        sfxCursor = (sfxCursor + 1) % sfxPlayer.Length;
    }
}
