using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/PlayerData", order = 1)]
public class PlayerDataScriptable : ScriptableObject
{
    [Header("Speed info")]

    [SerializeField]
    public float MaxSpeed;

    [SerializeField]
    public float acceleration;

    [SerializeField]
    public float deceleration;

    [Header("Jump info")]
    [SerializeField]
    public float jumpForce;
}
