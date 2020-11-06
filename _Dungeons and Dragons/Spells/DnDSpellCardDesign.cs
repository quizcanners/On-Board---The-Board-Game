using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PlayerAndEditorGUI;
using QuizCannersUtilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        public TextMeshProUGUI damageInfo;

        public TextMeshProUGUI level;

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
                TrySet(school, prot.school.ToString());
                TrySet(Description, prot.description);

                bool showRollsInfo = !prot.AttackSaving.IsNullOrEmpty() || prot.damageType != DamageType.None;

                TrySetActive(damageInfo, showRollsInfo);

                if (showRollsInfo) {
                    StringBuilder diceInfo = new StringBuilder(128);

                    diceInfo.Append(prot.AttackSaving);

                    if (prot.damageType != DamageType.None)
                    {
                        if (prot.AttackSaving.IsNullOrEmpty() == false)
                            diceInfo.Append(" | ");

                        if (prot.damageType == DamageType.Custom)
                        {
                            diceInfo.Append(prot.customDamageType);
                        }
                        else
                            diceInfo.Append(prot.damageType.ToString());

                        diceInfo.Append(" ").Append(prot.dice).Append(" ");
                    }

                    TrySet(damageInfo, diceInfo.ToString());

                 }
                 
                   
                TrySetActive(spellScalingContent, prot.perHigherSpellLevel.IsNullOrEmpty() == false);
                TrySet(spellScaling, prot.perHigherSpellLevel);

                TrySet(level, (prot.level > 0 ? "Lvl. " + prot.level.ToString() : "Cantrip"));// +" "+ prot.school.ToString());

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

     
    }
}
