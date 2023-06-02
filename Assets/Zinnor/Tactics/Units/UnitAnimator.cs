using UnityEngine;
using Zinnor.Tactics.Scriptables;

namespace Zinnor.Tactics.Units
{
    public class UnitAnimator : MonoBehaviour
    {
        public RuntimeAnimatorController[] Controllers;
        public ScriptableStringSet States;

        private Unit _unit;
        private Animator _Animator;

        public void Awake()
        {
            _unit = GetComponent<Unit>();
            _Animator = GetComponent<Animator>();
        }

        public void AfterControllerChanged()
        {
            Animator.runtimeAnimatorController = CurrentController;
        }

        public void AfterStateChanged()
        {
            var state = CurrentState;
            Animator.Play(state);
        }

        public void AfterPropertySet()
        {
            AfterControllerChanged();
            AfterStateChanged();
        }

        private Unit Unit => _unit;

        private Animator Animator => _Animator;

        private RuntimeAnimatorController CurrentController => Controllers[_unit.Faction];

        private string CurrentState => States[(int)Unit.State];
    }
}