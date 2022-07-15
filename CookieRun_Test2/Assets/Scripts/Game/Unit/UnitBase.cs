using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    [SerializeField]
    public float hp = 0f;
    [SerializeField]
    public float maxHp = 20f;   

    [SerializeField]
    protected UnitType unitType = UnitType.None;

    public UnitType GetUnitType()
    {
        return unitType;
    }

}
