using QuizCannersUtilities;
using PlayerAndEditorGUI;
using UnityEngine;
using System;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DeckRenderer.DnD
{
    [CreateAssetMenu(fileName = "DnD Spell Deck", menuName = "DeckRenderer/D&D/Deck/Spells")]
    public class DnDSpellDeck : DeckGeneric<DnDSpellPrototype> {  }

#if UNITY_EDITOR
    [CustomEditor(typeof(DnDSpellDeck))]
    public class DnDSpellDeckDrawer : PEGI_Inspector_SO<DnDSpellDeck> { }
#endif


    public enum DamageType
    {
       None =0, Acid =1, Necrotic =2, Force = 3, Fire =4, Poison =5, Cold =6, Lightning =7, Piercing=8, Psychic=9,  Thunder, Radiant, Slashing, Bludgeoning, Custom //FireAndCold, BludgeoningAndCold,  SeeDetails 
    }

    public enum School
    {
        Abjuration = 0, Conjuration =1, Divination = 2, Enchantment = 3, Evocation = 4, Illusion = 5, Necromancy = 6, Transmutation = 7,  
    }

    public enum SourceBook
    {
        PHB = 0, 
    }

    [Serializable]
    public class DnDSpellPrototype : CardPrototypeBase, IPEGI_Searchable
    {
        public School school;
        public int level = 1;
        public bool ritual;
        public string castingTime;
        public string range;
        public string targetArea;
        public bool verbal;
        public bool somatic;
        public bool material;
        public string components;
        public string Cost;
        public bool concentration;
        public string duration;
        public string AttackSaving;
        public DamageType damageType;
        public string customDamageType;
        public string dice;
        public SourceBook sourceBook;
        public int page;
        public string perHigherSpellLevel;

        public string Bard, Cleric, Druid, Paladin, Ranger, Sorceror, Warlock, Wizard;

        public bool String_SearchMatch(string searchString)
        {
            if (Bard.Contains(searchString) ||
                Cleric.Contains(searchString) ||
                Druid.Contains(searchString) ||
                Paladin.Contains(searchString) ||
                Ranger.Contains(searchString) ||
                Sorceror.Contains(searchString) ||
                Warlock.Contains(searchString) ||
                Wizard.Contains(searchString))
                return true;

            if (school.ToString().Contains(searchString))
                return true;

            return false;
        }

    #region Decoding
    public override void Decode(string key, CfgData token)
        {
            switch (key)
            {
                case "School": school = token.ToEnum(defaultValue: School.Abjuration); break;
                case "Level": level = token.ToInt(); break;
                case "Ritual": ritual = token.ToBool("Ritual"); break;
                case "Casting Time": castingTime = token.ToString(); break;
                case "Range": range = token.ToString(); break;
                case "Target/Area": targetArea = token.ToString(); break;
                case "V": verbal = token.ToBool("V"); break;
                case "S": somatic = token.ToBool("S"); break;
                case "M": material = token.ToBool("M"); break;
                case "Component(s)": components = token.ToString(); break;
                case "Cost": Cost = token.ToString(); break;
                case "Concentration": concentration = token.ToBool("Concentration"); break;
                case "Duration": duration = token.ToString(); break;
                case "Attack/Saving Throw (Effect)": AttackSaving = token.ToString(); break;
                case "Damage Type":
                    string txt = token.ToString();
                    if (txt.IsNullOrEmpty())
                    {
                        damageType = DamageType.None;
                    }
                    else
                    {
                        damageType = token.ToEnum(defaultValue: DamageType.Custom);
                        customDamageType = token.ToString();
                    }
                    break;
                case "Damage/Heal": dice = token.ToString(); break;
                case "Sourcebook": sourceBook = SourceBook.PHB; break;   
                case "Page #": page = token.ToInt(); break;
                case "Per Higher Spell Level": perHigherSpellLevel = token.ToString(); break;
                case "Bard": Bard = token.ToString(); break;
                case "Cleric": Cleric = token.ToString(); break;
                case "Druid": Druid = token.ToString(); break;
                case "Paladin": Paladin = token.ToString(); break;
                case "Ranger": Ranger = token.ToString(); break;
                case "Sorceror": Sorceror = token.ToString(); break;
                case "Warlock": Warlock = token.ToString(); break;
                case "Wizard": Wizard = token.ToString(); break;

               // case "Name":
               // case "Id": 
                default: base.Decode(key, token); break;
            }
        }
        #endregion
        
        public override bool Inspect()
        {
            var changed = base.Inspect();

            "School".editEnum(ref school).nl();

            "Level Up".editBig(ref perHigherSpellLevel).nl();

            if ("Classes".foldout())
            {
                InspectClass(ref Bard, "Bard");
                InspectClass(ref Cleric, "Cleric");
                InspectClass(ref Druid, "Druid");
                InspectClass(ref Paladin, "Paladin");
                InspectClass(ref Ranger, "Ranger");
                InspectClass(ref Sorceror, "Sorceror");
                InspectClass(ref Warlock, "Warlock");
                InspectClass(ref Wizard, "Wizard");
            }
            return changed;
        }

        private bool InspectClass(ref string className, string DefaultName)
        {
            string tmp = className;
            var changed = false;
            var available = !className.IsNullOrEmpty();

            if (DefaultName.toggleIcon(available, val => { tmp = val ? DefaultName : ""; }, hideTextWhenTrue: true))
            {
                className = tmp;
            }

            if (available)
            {
                pegi.edit(ref className);
            }

            pegi.nl();

            return changed;
        }

    }

}