using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerControlier controlier;
    public PlayerCondition condition;


    private void Awake()
    {
        CharacterManager.Instance.player = this;
        controlier = this.GetComponent<PlayerControlier>();
        condition = this.GetComponent<PlayerCondition>();
    }


}
