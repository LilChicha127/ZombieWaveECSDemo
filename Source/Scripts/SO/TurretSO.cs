using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Turret", menuName = "New turret")]
public class TurretSO : ScriptableObject
{
    [field:SerializeField] public string Name {  get; set; }
    [field:SerializeField] public List<UpgradesTurret> Upgrades { get; set; }
}
