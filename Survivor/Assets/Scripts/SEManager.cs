using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    private static SEManager instance;
    private AudioSource source;
    [SerializeField] private AudioClip generatorShot;
    [SerializeField] private AudioClip generatorDamaged;
    [SerializeField] private AudioClip generatorDestroyed;
    [SerializeField] private AudioClip playerShot;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        this.source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void DamagedG()
    {
        instance.source.PlayOneShot(instance.generatorDamaged);
    }

    public static void DestroyG()
    {
        instance.source.PlayOneShot(instance.generatorDestroyed);
    }

    public static void ShotG()
    {
        instance.source.PlayOneShot(instance.generatorShot);
    }

    public static void ShotP()
    {
        instance.source.PlayOneShot(instance.playerShot);
    }

}
