using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontageManager : MonoBehaviour
{
    #region Parameters

    //Singleton Instance
    public static MontageManager Instance { get; private set; }


    //if active means a sequence is in progress
    public bool sequenceInProgress = false;


    Animator playerSpaceshipAnimator;
    #endregion

    #region Methods
    private void Awake()
    {
        //Singleton Logic
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        playerSpaceshipAnimator = GameManager.Instance.playerSpaceShip.GetComponent<Animator>();
        playerSpaceshipAnimator.enabled = false;
    }

    public float PlayGameStartCutScene() {
        playerSpaceshipAnimator.enabled = true;
        return playerSpaceshipAnimator.GetCurrentAnimatorStateInfo(0).length;
    }

    public void EndGameStartCutScene() {
        Destroy(playerSpaceshipAnimator.GetComponent<Animator>());
    }

    #endregion
}
