using System.Collections.Generic;
using UnityEngine;
using Zinnor.Tactics.Scriptables.Genders;

namespace Zinnor.Tactics.Scriptables.Weapons
{
    /**
     * ����
     */
    [CreateAssetMenu(fileName = "ScriptableWeapon", menuName = "ScriptableObjects/ScriptableWeapon")]
    public class ScriptableWeapon : ScriptableObject
    {
        /**
         * ����
         */
        public string Name;

        /**
         * ����
         */
        public ScriptableWeaponType Type;


        /**
         * Ԫ��
         */
        public ScriptableElement Element;

        /**
         * �Ա�
         */
        public ScriptableGender Gender;

        /**
         * Ч��
         */
        public List<ScriptableEffect> Effects;

        /**
         * �ȼ�
         */
        public string Rank;

        /**
         * ��С��Χ
         */
        public int MinRange;

        /**
         * ���Χ
         */
        public int MaxRange;

        /**
         * �;�
         */
        public int Uses;

        /**
         * ����
         */
        public int Might;

        /**
         * ����
         */
        public int Hit;

        /**
         * ����
         */
        public int Critical;

        /**
         * ��������
         */
        public int WeaponExp;

        /**
         * ��ɫ����
         */
        public int Exp;

        /**
         * �۸�
         */
        public int Price;
    }
}