using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Aggressive Weapon Data", menuName = "Data/Weapon Data/Aggressive Weapon")]
public class SO_AggressiveWeaponData : SO_WeaponData
{
    [SerializeField] private WeaponAttackDetails[] attackDetails;

    public WeaponAttackDetails[] AttackDetails { get => attackDetails; private set => attackDetails = value; }

    private void OnEnable()
    {
        AmountOfAttacks = attackDetails.Length;
        MovementSpeed = new float[AmountOfAttacks];
        for(int i = 0; i < AmountOfAttacks; i++)
        {
            MovementSpeed[i] = attackDetails[i].MovementSpeed;
        }
    }
}
