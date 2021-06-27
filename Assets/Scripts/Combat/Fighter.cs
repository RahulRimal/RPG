using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Health target;

        [SerializeField] float weaponRange = 5f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 30f;

        float timeSinceLastAttack = Mathf.Infinity;

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
            target.damage(weaponDamage);
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
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
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