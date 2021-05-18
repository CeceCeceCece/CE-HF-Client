using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float health;
    public float maxHealth;
    public int itemCount = 0;
    public MeshRenderer model;
    public HealthBar healthBar;
    public Text usernameText;
    public Text classText;
   

    private Color materialColor;

    public void Initialize(int _id, string _username, string _classText = "")
    {
        id = _id;
        username = _username;
        health = maxHealth;
        if (healthBar == null)
        {
            healthBar = GameObject.FindGameObjectWithTag("LocalHealthBar").GetComponent<HealthBar>(); 
        }
        if(usernameText == null)
            usernameText = GameObject.FindGameObjectWithTag("LocalUsernameField").GetComponent<Text>();

        if (classText == null)
            classText = GameObject.FindGameObjectWithTag("LocalClassTextField").GetComponent<Text>();


        healthBar.SetMaximumHealth(health);
        healthBar.SetHealth(health);
        usernameText.text = username;
        classText.text = _classText;


    }

    public void SetHealth(float _health)
    {
        Debug.Log(_health);
        health = _health;
        if(health <=0f)
        {
            Die();
        }
        if (health <= 0) health = 0;
        healthBar.SetHealth(health);

    }
    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        SetHealth(maxHealth);
    }

    public void IceBlockCasted()
    {
        materialColor = model.material.color;
        model.material.color = Color.yellow;
    }

    public void IceBlockEnded()
    {
        model.material.color = materialColor;
    }
}