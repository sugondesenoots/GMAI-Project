using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class DrawState : State
    {
        private bool drawn; 
        private int drawParam = Animator.StringToHash("DrawMelee");
        private int sheatheParam = Animator.StringToHash("SheathMelee");

        public DrawState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(sheatheParam, false);
            character.TriggerAnimation(drawParam);
            drawn = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (drawn)
            {
                stateMachine.ChangeState(character.drawn);
                Debug.Log("Weapon is drawn");
            }
        }
    }
}
