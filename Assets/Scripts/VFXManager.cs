using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{

    //hit fx
    [SerializeField]
    ParticleSystem explosionEffect;

    //Singelton Instance
    public static VFXManager Instance { get; private set;}

    private void Awake()
    {
        //Singelton Logic
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }


    //Show the specified explosion Effect at a position
    public void ShowExplosionFX(Vector3 position)
    {
        explosionEffect.transform.position = position;
        explosionEffect.Play();
    }
}
