using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SheatheState : State
    {
        private bool draw;
        private bool unequipMelee; 

        private int sheatheParam = Animator.StringToHash("SheathMelee");
        private int meleeParam = Animator.StringToHash("IsMelee");

        public SheatheState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            character.TriggerAnimation(sheatheParam);
            draw = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            draw = Input.GetKeyDown(KeyCode.E);
            unequipMelee = Input.GetKeyDown(KeyCode.X);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (draw)
            {
                stateMachine.ChangeState(character.drawing);
                Debug.Log("Drawing weapon");
            }
            else if (unequipMelee) //Unequip melee is available only when it is in sheathe state
            {
                character.SetAnimationBool(meleeParam, false);
                Debug.Log("Melee unequipped");

                stateMachine.ChangeState(character.melee);
            }
        }
    }
}