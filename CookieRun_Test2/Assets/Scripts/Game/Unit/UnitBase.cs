using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    [SerializeField]
    protected float hp;
    [SerializeField]
    protected float maxHp;

    [SerializeField]
    protected UnitType unitType = UnitType.None;

    public UnitType GetUnitType()
    {
        return unitType;
    }
}
