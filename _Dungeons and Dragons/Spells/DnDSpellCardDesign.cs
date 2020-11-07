using System.Collections.Generic;
using System.Text;
using PlayerAndEditorGUI;
using QuizCannersUtilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using UnityEditor;

namespace DeckRenderer.DnD
{
    public class DnDSpellCardDesign : CardDesignGeneric<DnDSpellPrototype>
    {

        public TextMeshProUGUI school;
        public TextMeshProUGUI castingTime;
        public TextMeshProUGUI range;
        public Graphic verbal, somatic, material;
        public TextMeshProUGUI component;
        public TextMeshProUGUI duration;
        public RectTransform concentration;
        public RectTransform ritual;

        public RectTransform spellScalingContent;
        public TextMeshProUGUI spellScaling;

        public RectTransform damageContent;
        public TextMeshProUGUI damageInfo;

        public List<RectTransform> subclassContent;
        public TextMeshProUGUI subclass;

        public Image Bard, Cleric, Druid, Paladin, Ranger, Sorceror, Warlock, Wizard;

        public override void Fill(DnDSpellPrototype prot)
        {
            if (prot != null)
            {
                TrySet(castingTime, prot.castingTime);
                TrySet(range, prot.range);

                TrySetEnabled(verbal, prot.verbal);
                TrySetEnabled(somatic, prot.somatic);
                TrySetEnabled(material, prot.material);
                TrySet(component, prot.components + prot.Cost);

                TrySet(duration, prot.duration);

                TrySetActive(concentration, prot.concentration);
                TrySetActive(ritual, prot.ritual);

                TrySet(Name, prot.NameForPEGI);

                StringBuilder lvl = new StringBuilder(64);

                if (prot.level == 0)
                {
                    lvl.Append(prot.school.ToString()).Append(" Cantrip");
                } else
                {
                    switch (prot.level)
                    {
                        case 1: lvl.Append("1st"); break;
                        case 2: lvl.Append("2nd"); break;
                        case 3: lvl.Append("3rd"); break;
                        default: lvl.Append(prot.level.ToString()+"th"); break;
                    }

                    lvl.Append("-level ").Append(prot.school.ToString());
                }

                TrySet(school, lvl.ToString());
                
                TrySet(Description, prot.description);

                bool showRollsInfo = !prot.attackRoll.IsNullOrEmpty() || !prot.savingThrow.IsNullOrEmpty() || prot.damageType != DamageType.None;

                TrySetActive(damageContent, showRollsInfo);

                if (showRollsInfo) {
                    StringBuilder diceInfo = new StringBuilder(128);

                    diceInfo.Append(prot.savingThrow);
                    if (!prot.savingEffect.IsNullOrEmpty())
                    {
                        diceInfo.Append(" ({0}) ".F(prot.savingEffect));
                    }

                    diceInfo.Append(prot.attackRoll);

                    if (prot.damageType != DamageType.None)
                    {
                        if (prot.savingThrow.IsNullOrEmpty() == false)
                            diceInfo.Append(" | ");

                        if (prot.damageType == DamageType.Custom)
                        {
                            diceInfo.Append(prot.customDamageType);
                        }
                        else
                        {
                            //TrySet(damageIcon, );
                            diceInfo.Append("<sprite name=\"{0}\">".F(prot.damageType.ToString()));
                        }
                        diceInfo.Append(" ").Append(prot.dice).Append(" ");
                    }
                    TrySet(damageInfo, diceInfo.ToString());
                 }
                 
                TrySetActive(spellScalingContent, prot.perHigherSpellLevel.IsNullOrEmpty() == false);
                TrySet(spellScaling, prot.perHigherSpellLevel);

                string specialClass = prot.GetSpecialClass();

                subclassContent.SetActive_List(specialClass.IsNullOrEmpty() == false);
                TrySet(subclass, specialClass);

                TrySetEnabled(Bard, !prot.Bard.IsNullOrEmpty());
                TrySetEnabled(Cleric, !prot.Cleric.IsNullOrEmpty());
                TrySetEnabled(Druid, !prot.Druid.IsNullOrEmpty());
                TrySetEnabled(Paladin, !prot.Paladin.IsNullOrEmpty());
                TrySetEnabled(Ranger, !prot.Ranger.IsNullOrEmpty());
                TrySetEnabled(Sorceror, !prot.Sorceror.IsNullOrEmpty());
                TrySetEnabled(Warlock, !prot.Warlock.IsNullOrEmpty());
                TrySetEnabled(Wizard, !prot.Wizard.IsNullOrEmpty());

            }
            else
            {
                TrySet(Name, "No Prototype");
                TrySet(Description, "No Prototype");
            }
        }

        public override bool Inspect()
        {
            var changed = pegi.toggleDefaultInspector(this); 
                
            base.Inspect().nl(ref changed);



            return changed;
        }

    }


    [CustomEditor(typeof(DnDSpellCardDesign))]
    public class DnDSpellCardDesignDrawer : PEGI_Inspector_Mono<DnDSpellCardDesign>
    {    }
}
