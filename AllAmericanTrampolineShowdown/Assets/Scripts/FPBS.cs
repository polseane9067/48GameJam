﻿using UnityEngine;
using System.Collections;

public class FPBS : MonoBehaviour {

    public Rigidbody Burger2Throw;

    public Rigidbody datBurger;
    public Rigidbody demFries;
    public Rigidbody datDrink;
    public Rigidbody FlameBurg;
    public Rigidbody TripBurg;

    public bool hasTripBurg = false;
    public bool hasFlameBurg = false;
    float powerUpTimer = 5.0f;

    public int BurgerSpeed = 20;
    public bool CanFire = true;
    public int IntBurg;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0) || Input.GetButtonUp("Fire1"))
        {
            if(CanFire)
            {
                CanFire = false;
                IntBurg = Random.Range(0, 3);
                if(IntBurg >= 2)
                {
                    Burger2Throw = demFries;
                }
                else if(IntBurg >= 1)
                {
                    Burger2Throw = datDrink;
                }
                else
                {
                    Burger2Throw = datBurger;
                }

                if (hasFlameBurg)
                {
                    Burger2Throw = FlameBurg;
                }
                else if (hasTripBurg)
                {
                    Burger2Throw = TripBurg;
                }
                Rigidbody Burger = Instantiate(Burger2Throw, this.transform.position, this.transform.rotation) as Rigidbody;
                if (this.gameObject.tag == "Player")
                {
                    Burger.gameObject.tag = "Player1Bullet";
                }
                else if (this.gameObject.tag == "Player02")
                {
                    Burger.gameObject.tag = "Player2Bullet";
                }
                Burger.velocity = transform.TransformDirection(Vector3.forward * BurgerSpeed);
                //Burger.AddForce(Vector3.forward * BurgerSpeed);
                Destroy(Burger.gameObject, 2);
                audio.Play();
                Invoke("ResetFire", 1);
            }
        }
        if (hasFlameBurg || hasTripBurg)
        {
            powerUpTimer -= Time.deltaTime;
        }
        if (powerUpTimer <= 0)
        {
            hasTripBurg = false;
            hasFlameBurg = false;
        }
	}

    void ResetFire()
    {
        CanFire = true;
    }
}
