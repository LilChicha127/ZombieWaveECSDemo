using System;
using UnityEngine;

[Serializable]
public class UpgradesTurret
{
    [field: SerializeField] public int Price {  get; set; }
    [field: SerializeField] public int Damage { get; set; }
}
