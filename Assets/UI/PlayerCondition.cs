using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}


public class PlayerCondition : MonoBehaviour , IDamageable
{
    public UICondition uiconditionp;

    Condition health { get { return uiconditionp.health; } }
    Condition hunger { get { return uiconditionp.hunger; } }
    Condition stamina { get { return uiconditionp.stamina; } }

    public float noHungerhealthDecay;

    public event Action onTakeDamage;


    void Update()
    {
        hunger.Add(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0f)
        {
            health.Subtract(noHungerhealthDecay * Time.deltaTime);
        }
        if (health.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    private void Die()
    {
        Debug.Log("Die!");
    }

    public void TakePhysicalDamage(int damage)
    {
      
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}
