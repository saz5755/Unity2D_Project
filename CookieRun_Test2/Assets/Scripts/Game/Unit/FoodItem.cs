using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : UnitBase
{
    CapsuleCollider2D capsule;

    private void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
    }
}
