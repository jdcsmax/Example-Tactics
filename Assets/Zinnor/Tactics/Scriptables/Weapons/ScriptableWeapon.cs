using System.Collections.Generic;
using UnityEngine;
using Zinnor.Tactics.Scriptables.Effects;
using Zinnor.Tactics.Scriptables.Elements;
using Zinnor.Tactics.Scriptables.Genders;

namespace Zinnor.Tactics.Scriptables.Weapons
{
    [CreateAssetMenu(fileName = "ScriptableWeapon", menuName = "ScriptableObjects/ScriptableWeapon")]
    public class ScriptableWeapon : ScriptableObject
    {
        public string Name;

        public ScriptableWeaponType Type;

        public ScriptableElement Element;

        public ScriptableGender Gender;

        public List<ScriptableEffect> Effects;

        public string Rank;

        public int MinRange;

        public int MaxRange;

        public int Uses;

        public int Might;

        public int Hit;

        public int Critical;

        public int WeaponExp;

        public int Exp;

        public int Price;
    }
}