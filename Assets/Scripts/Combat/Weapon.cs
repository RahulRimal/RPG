using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponRange = 5f;
        [SerializeField] float weaponDamage = 30f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";


        public float getRange()
        {
            return weaponRange;
        }

        public float getDamage()
        {
            return weaponDamage;
        }


        public void spawn(Transform rightHand, Transform lefttHand, Animator animator)
        {
            destroyOldWeapon(rightHand, lefttHand);

            if(equippedPrefab != null)
            {
                GameObject weapon =  Instantiate(equippedPrefab, getTransform(rightHand, lefttHand).transform);
                weapon.name = weaponName;
            }
            
            if(animatorOverride != null)
                animator.runtimeAnimatorController = animatorOverride;
        }

        void destroyOldWeapon(Transform rightHand, Transform lefttHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null)
                oldWeapon = lefttHand.Find(weaponName);
            if(oldWeapon == null)
                return;
            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);

        }


        Transform getTransform(Transform rightHand, Transform lefttHand)
        {
            Transform handTransform;
            if(isRightHanded)
            handTransform = rightHand;
            else
                handTransform = lefttHand;
            return handTransform;    
        }

        public bool hasProjectile()
        {
            return projectile != null;
        }

        public void launchProjectile(Transform rightHand, Transform lefttHand,Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, getTransform(rightHand, lefttHand).position, Quaternion.identity);
            projectileInstance.setTarget(target, weaponDamage);
        }






    }    
}