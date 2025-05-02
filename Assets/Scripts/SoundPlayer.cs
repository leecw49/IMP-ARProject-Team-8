using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource cardDraw, cardShuffle, explosion, hitFlesh, punch;
    
    void Start()
    {
        var audioArray = GetComponents<AudioSource>();
        cardDraw = audioArray[0];
        cardShuffle = audioArray[1];
        explosion = audioArray[2];
        hitFlesh = audioArray[3];
        punch = audioArray[4];
    }
}