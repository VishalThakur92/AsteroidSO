using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontageManager : MonoBehaviour
{
    #region Parameters

    //Singleton Instance
    public static MontageManager Instance { get; private set; }
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


    public float PlayGameStartCutScene()
    {
        Animator playerSpaceshipAnimator = GameManager.Instance.playerSpaceShip.GetComponent<Animator>();
        playerSpaceshipAnimator.enabled = true;
        return playerSpaceshipAnimator.GetCurrentAnimatorStateInfo(0).length;
    }

    public void EndGameStartCutScene()
    {
        Animator playerSpaceshipAnimator = GameManager.Instance.playerSpaceShip.GetComponent<Animator>();
        Destroy(playerSpaceshipAnimator);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.K)){
            EndGameStartCutScene();
        }

    }

    #endregion
}
