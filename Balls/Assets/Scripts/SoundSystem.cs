using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public AudioSource ballPop;
    public AudioSource coin;
    public AudioSource weaponSet;
    public AudioSource click;
    public AudioSource whoosh;

    public void PlayBallPop()
    {
        ballPop.Play();
    }

    public void PlayCoin()
    {
        coin.Play();
    }

    public void PlayWeaponSet()
    {
        weaponSet.Play();
    }

    public void PlayClick()
    {
        click.Play();
    }

    public void PlayWhoosh()
    {
        whoosh.Play();
    }
}