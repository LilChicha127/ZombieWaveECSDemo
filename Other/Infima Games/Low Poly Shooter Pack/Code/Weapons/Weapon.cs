﻿using InfimaGames.LowPolyShooterPack.Legacy;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    public class Weapon : WeaponBehaviour
    {
        #region FIELDS SERIALIZED
        
        [Title(label: "Settings")]
        
        [Tooltip("Weapon Name. Currently not used for anything, but in the future, we will use this for pickups!")]
        [SerializeField] 
        private string weaponName;

        [SerializeField]
        private BulletPool pool;

        [SerializeField]
        private Transform dulo;

        [SerializeField]
        private int damage = 30;

        [Tooltip("How much the character's movement speed is multiplied by when wielding this weapon.")]
        [SerializeField]
        private float multiplierMovementSpeed = 1.0f;
        
        [Title(label: "Firing")]

        [Tooltip("Is this weapon automatic? If yes, then holding down the firing button will continuously fire.")]
        [SerializeField] 
        private bool automatic;
        
        [Tooltip("Is this weapon bolt-action? If yes, then a bolt-action animation will play after every shot.")]
        [SerializeField]
        private bool boltAction;

        [Tooltip("Amount of shots fired at once. Helpful for things like shotguns, where there are multiple projectiles fired at once.")]
        [SerializeField]
        private int shotCount = 1;
        
        [Tooltip("How far the weapon can fire from the center of the screen.")]
        [SerializeField]
        private float spread = 0.25f;
        
        [Tooltip("How fast the projectiles are.")]
        [SerializeField]
        private float projectileImpulse = 400.0f;

        [Tooltip("Amount of shots this weapon can shoot in a minute. It determines how fast the weapon shoots.")]
        [SerializeField] 
        private int roundsPerMinutes = 200;

        [Title(label: "Reloading")]
        
        [Tooltip("Determines if this weapon reloads in cycles, meaning that it inserts one bullet at a time, or not.")]
        [SerializeField]
        private bool cycledReload;
        
        [Tooltip("Determines if the player can reload this weapon when it is full of ammunition.")]
        [SerializeField]
        private bool canReloadWhenFull = true;

        [Tooltip("Should this weapon be reloaded automatically after firing its last shot?")]
        [SerializeField]
        private bool automaticReloadOnEmpty;

        [Tooltip("Time after the last shot at which a reload will automatically start.")]
        [SerializeField]
        private float automaticReloadOnEmptyDelay = 0.25f;

        [Title(label: "Animation")]

        [Tooltip("Transform that represents the weapon's ejection port, meaning the part of the weapon that casings shoot from.")]
        [SerializeField]
        private Transform socketEjection;

        [Tooltip("Settings this to false will stop the weapon from being reloaded while the character is aiming it.")]
        [SerializeField]
        private bool canReloadAimed = true;

        [Title(label: "Resources")]

        [Tooltip("Casing Prefab.")]
        [SerializeField]
        private GameObject prefabCasing;
        
        [Tooltip("Projectile Prefab. This is the prefab spawned when the weapon shoots.")]
        [SerializeField]
        private Projectile prefabProjectile;
        
        [Tooltip("The AnimatorController a player character needs to use while wielding this weapon.")]
        [SerializeField] 
        public RuntimeAnimatorController controller;

        [Tooltip("Weapon Body Texture.")]
        [SerializeField]
        private Sprite spriteBody;
        
        [Title(label: "Audio Clips Holster")]

        [Tooltip("Holster Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipHolster;

        [Tooltip("Unholster Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipUnholster;
        
        [Title(label: "Audio Clips Reloads")]

        [Tooltip("Reload Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReload;
        
        [Tooltip("Reload Empty Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadEmpty;
        
        [Title(label: "Audio Clips Reloads Cycled")]
        
        [Tooltip("Reload Open Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadOpen;
        
        [Tooltip("Reload Insert Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadInsert;
        
        [Tooltip("Reload Close Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadClose;
        
        [Title(label: "Audio Clips Other")]

        [Tooltip("AudioClip played when this weapon is fired without any ammunition.")]
        [SerializeField]
        private AudioClip audioClipFireEmpty;
        
        [Tooltip("")]
        [SerializeField]
        private AudioClip audioClipBoltAction;

        #endregion

        #region FIELDS

        private Animator animator;
        private WeaponAttachmentManagerBehaviour attachmentManager;

        private int ammunitionCurrent;

        #region Attachment Behaviours
        
        private ScopeBehaviour scopeBehaviour;
        
        private MagazineBehaviour magazineBehaviour;
        private MuzzleBehaviour muzzleBehaviour;

        private LaserBehaviour laserBehaviour;
        private GripBehaviour gripBehaviour;

        #endregion

        private IGameModeService gameModeService;
        private CharacterBehaviour characterBehaviour;

        private Transform playerCamera;
        
        #endregion

        #region UNITY
        
        protected override void Awake()
        {
            animator = GetComponent<Animator>();
            attachmentManager = GetComponent<WeaponAttachmentManagerBehaviour>();

            gameModeService = ServiceLocator.Current.Get<IGameModeService>();
            characterBehaviour = gameModeService.GetPlayerCharacter();
            playerCamera = characterBehaviour.GetCameraWorld().transform;
        }
        protected override void Start()
        {
            #region Cache Attachment References

            scopeBehaviour = attachmentManager.GetEquippedScope();
            
            magazineBehaviour = attachmentManager.GetEquippedMagazine();
            muzzleBehaviour = attachmentManager.GetEquippedMuzzle();

            laserBehaviour = attachmentManager.GetEquippedLaser();
            gripBehaviour = attachmentManager.GetEquippedGrip();

            #endregion

            ammunitionCurrent = magazineBehaviour.GetAmmunitionTotal();

            prefabProjectile.SetProjectile(pool, damage);
            pool.Init(prefabProjectile.gameObject);
        }

        #endregion

        #region GETTERS

        public override float GetFieldOfViewMultiplierAim()
        {
            if (scopeBehaviour != null) 
                return scopeBehaviour.GetFieldOfViewMultiplierAim();
            
            Debug.LogError("Weapon has no scope equipped!");
  
            return 1.0f;
        }
        public override float GetFieldOfViewMultiplierAimWeapon()
        {
            if (scopeBehaviour != null) 
                return scopeBehaviour.GetFieldOfViewMultiplierAimWeapon();
            
            Debug.LogError("Weapon has no scope equipped!");
  
            return 1.0f;
        }
        
        public override Animator GetAnimator() => animator;
        public override bool CanReloadAimed() => canReloadAimed;

        public override Sprite GetSpriteBody() => spriteBody;
        public override float GetMultiplierMovementSpeed() => multiplierMovementSpeed;

        public override AudioClip GetAudioClipHolster() => audioClipHolster;
        public override AudioClip GetAudioClipUnholster() => audioClipUnholster;

        public override AudioClip GetAudioClipReload() => audioClipReload;
        public override AudioClip GetAudioClipReloadEmpty() => audioClipReloadEmpty;
        
        public override AudioClip GetAudioClipReloadOpen() => audioClipReloadOpen;
        public override AudioClip GetAudioClipReloadInsert() => audioClipReloadInsert;
        public override AudioClip GetAudioClipReloadClose() => audioClipReloadClose;

        public override AudioClip GetAudioClipFireEmpty() => audioClipFireEmpty;
        public override AudioClip GetAudioClipBoltAction() => audioClipBoltAction;
        
        public override AudioClip GetAudioClipFire() => muzzleBehaviour.GetAudioClipFire();
        public override int GetAmmunitionCurrent() => ammunitionCurrent;

        public override int GetAmmunitionTotal() => magazineBehaviour.GetAmmunitionTotal();
        public override bool HasCycledReload() => cycledReload;

        public override bool IsAutomatic() => automatic;
        public override bool IsBoltAction() => boltAction;

        public override bool GetAutomaticallyReloadOnEmpty() => automaticReloadOnEmpty;
        public override float GetAutomaticallyReloadOnEmptyDelay() => automaticReloadOnEmptyDelay;

        public override bool CanReloadWhenFull() => canReloadWhenFull;
        public override float GetRateOfFire() => roundsPerMinutes;
        
        public override bool IsFull() => ammunitionCurrent == magazineBehaviour.GetAmmunitionTotal();
        public override bool HasAmmunition() => ammunitionCurrent > 0;

        public override RuntimeAnimatorController GetAnimatorController() => controller;
        public override WeaponAttachmentManagerBehaviour GetAttachmentManager() => attachmentManager;

        #endregion

        #region METHODS

        public override void Reload()
        {
            const string boolName = "Reloading";
            animator.SetBool(boolName, true);
            
            ServiceLocator.Current.Get<IAudioManagerService>().PlayOneShot(HasAmmunition() ? audioClipReload : audioClipReloadEmpty, new AudioSettings(1.0f, 0.0f, false));
            
            animator.Play(cycledReload ? "Reload Open" : (HasAmmunition() ? "Reload" : "Reload Empty"), 0, 0.0f);
        }
        public override void Fire(float spreadMultiplier = 1.0f)
        {
            if (muzzleBehaviour == null)
                return;
            
            if (playerCamera == null)
                return;

            const string stateName = "Fire";
            animator.Play(stateName, 0, 0.0f);
            ammunitionCurrent = Mathf.Clamp(ammunitionCurrent - 1, 0, magazineBehaviour.GetAmmunitionTotal());

            if (ammunitionCurrent == 0)
                SetSlideBack(1);
            
            muzzleBehaviour.Effect();

            for (var i = 0; i < shotCount; i++)
            {
                Vector3 spreadValue = Random.insideUnitSphere * (spread * spreadMultiplier);
                spreadValue.z = 0;
                spreadValue = playerCamera.TransformDirection(spreadValue);

                GameObject bullet = pool.GetBullet();
                Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
                rbBullet.linearVelocity = Vector3.zero;
                bullet.transform.position = dulo.position;
                bullet.transform.rotation = Quaternion.Euler(playerCamera.eulerAngles + spreadValue);
                rbBullet.linearVelocity = bullet.transform.forward * projectileImpulse;
                

            }
        }

        public override void FillAmmunition(int amount)
        {
            ammunitionCurrent = amount != 0 ? Mathf.Clamp(ammunitionCurrent + amount, 
                0, GetAmmunitionTotal()) : magazineBehaviour.GetAmmunitionTotal();
        }
        public override void SetSlideBack(int back)
        {
            const string boolName = "Slide Back";
            animator.SetBool(boolName, back != 0);
        }

        public override void EjectCasing()
        {
            if(prefabCasing != null && socketEjection != null)
                Instantiate(prefabCasing, socketEjection.position, socketEjection.rotation);
        }

        #endregion
    }
}