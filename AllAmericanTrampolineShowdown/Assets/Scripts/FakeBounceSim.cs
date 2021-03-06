﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FakeBounceSim : MonoBehaviour {

    public GameObject Player;
    public GameObject OtherPlayer;
    public int PlayerBounceSpeed = 14;
    public int OtherPlayerBounceSpeed = 14;

    public int Player1MaxHeight = 15;
    public int Player1MinHeight = 8;
    public int Player2MaxHeight = 15;
    public int Player2MinHeight = 8;

    public bool isPlayerFattist = false;
    public bool isOtherFattist = false;
    public bool canPlayMusic = true;

    public Button mobileMoveUp;
    public Button mobileMoveDown;
    public EventSystem eventSys;

    public GameObject fallSoundController; 

    public GameObject PlayerTrampoline;
    public GameObject OtherTrampoline; 


    // Use this for initialization
	void Start () {
        fallSoundController = GameObject.Find("LosingSoundController");
	}

    public void SetBouncyFalse()
    {
        Player.GetComponentInChildren<PlayerAnimationControllerV2>().AnimObj.SetBool("Bouncy", false);
    }

    public void SetLowBounceFalse()
    {
        Player.GetComponentInChildren<PlayerAnimationControllerV2>().AnimObj.SetBool("lowBounce", false);
    }

    public void SetHighBounceFalse()
    {
        Player.GetComponentInChildren<PlayerAnimationControllerV2>().AnimObj.SetBool("highBounce", false);
    }

    public void SetEnemyBouncyFalse()
    {
        OtherPlayer.GetComponentInChildren<EnemyAI>().EnemyAnim.SetBool("Bouncy", false);
    }
	// Update is called once per frame
	void Update () {
	    if(Player.transform.position.y <= 1.5)
        {
            //print("Player UP");
            //Player.GetComponent<CharacterController>().velocity.Equals(Vector3.up * BounceSpeed);
            if (!isPlayerFattist)
            {
                if (Input.GetAxis("Vertical") == 1 || eventSys.currentSelectedGameObject == mobileMoveUp.gameObject)
                {
                    if (PlayerBounceSpeed < Player1MaxHeight)
                    {
                        PlayerBounceSpeed++;
                    }
                }
                else if (Input.GetAxis("Vertical") == -1 || eventSys.currentSelectedGameObject == mobileMoveDown.gameObject)
                {
                    if (PlayerBounceSpeed > Player1MinHeight)
                    {
                        PlayerBounceSpeed--;
                    }
                }
                PlayerTrampoline.GetComponent<TestTramp>().TrampAnim.SetBool("CanBounce", true);
                if (Input.GetKey(KeyCode.W) || eventSys.currentSelectedGameObject == mobileMoveUp.gameObject)
                {
                    Player.GetComponentInChildren<PlayerAnimationControllerV2>().AnimObj.SetBool("highBounce", true);
                    Invoke("SetHighBounceFalse", 1);
                }
                else if (Input.GetKey(KeyCode.S) || eventSys.currentSelectedGameObject == mobileMoveDown.gameObject)
                {
                    Player.GetComponentInChildren<PlayerAnimationControllerV2>().AnimObj.SetBool("lowBounce", true);
                    Invoke("SetLowBounceFalse", 1);
                }
                else
                {
                    Player.GetComponentInChildren<PlayerAnimationControllerV2>().AnimObj.SetBool("Bouncy", true);
                    Invoke("SetBouncyFalse", 2);
                }
                
                Player.rigidbody.velocity = transform.TransformDirection(Vector3.up * PlayerBounceSpeed);
                PlayerTrampoline.audio.Play();
                
                
            }
            else
            {
                //Player.GetComponent<Collider>().enabled = false;
                if (canPlayMusic)
                {
                    canPlayMusic = false;
                    fallSoundController.audio.Play();
                }
            }
        }
        if(OtherPlayer.transform.position.y <= 1.5)
        {
            //print("Other Player UP");
            //OtherPlayer.GetComponent<CharacterController>().velocity.Equals(Vector3.up * BounceSpeed);
            if (!isOtherFattist)
            {
                if (Input.GetAxis("Vertical2") == 1)
                {
                    if (OtherPlayerBounceSpeed < Player2MaxHeight)
                    {
                        OtherPlayerBounceSpeed++;
                    }
                }
                else if (Input.GetAxis("Vertical2") == -1)
                {
                    if (OtherPlayerBounceSpeed > Player2MinHeight)
                    {
                        OtherPlayerBounceSpeed--;
                    }
                }
                OtherTrampoline.GetComponent<TestTramp>().TrampAnim.SetBool("CanBounce", true);
                OtherPlayer.GetComponentInChildren<EnemyAI>().EnemyAnim.SetBool("Bouncy", true);
                Invoke("SetEnemyBouncyFalse", 2);
                OtherPlayer.rigidbody.velocity = transform.TransformDirection(Vector3.up * OtherPlayerBounceSpeed);
                OtherTrampoline.audio.Play();
            }
            else
            {
                //OtherPlayer.GetComponent<Collider>().enabled = false; 
                if(canPlayMusic)
                {
                    canPlayMusic = false; 
                    fallSoundController.audio.Play();
                }
            }
        }
	}
}
