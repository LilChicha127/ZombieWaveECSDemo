using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    public abstract class InventoryBehaviour : MonoBehaviour
    {
        #region GETTERS

        public abstract WeaponBehaviour GetEquipped();

        public abstract int GetEquippedIndex();
        
        #endregion
        
        #region METHODS

        public abstract void Init(int equippedAtStart = 0);
        
        public abstract WeaponBehaviour Equip(int index);

        #endregion
    }
}