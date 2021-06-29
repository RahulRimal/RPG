using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Health target;

        
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform lefttHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] string defaultWeaponName = "Unarmed";
        

        float timeSinceLastAttack = Mathf.Infinity;

        Weapon currentWeapon = null;


        void Start()
        {
            Weapon weapon = Resources.Load<Weapon>(defaultWeaponName);
            equipWeapon(weapon);
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if(target == null) return;
            if(target.IsDead()) return;

            if(!getIsInRange())
            {
                GetComponent<Mover>().moveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().cancel();
                attackBehaviour();
            }
        }

        public void equipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.spawn(rightHandTransform,lefttHandTransform,  animator);

        }

        void attackBehaviour()
        {
            transform.LookAt(target.transform);

            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                triggerAttack();
                hit();
                timeSinceLastAttack = 0f;
            }

        }

        void triggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
                GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event
        void hit()
        {
            if(target == null) return;

            if(currentWeapon.hasProjectile())
                currentWeapon.launchProjectile(rightHandTransform, lefttHandTransform, target);
            else
                target.damage(currentWeapon.getDamage());

            target.damage(currentWeapon.getDamage());
        }

        void shoot()
        {
            hit();
        }

        public bool canAttack(GameObject combatTarget)
        {
            if(combatTarget == null)
                return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        bool getIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.getRange();
        }

        public void attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().startAction(this);
            target = combatTarget.GetComponent<Health>();
            // print("Attacking !!");
        }

        public void cancel()
        {
            stopAttack();
            target = null;
            GetComponent<Mover>().cancel();
        }

        void stopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }









    }
}