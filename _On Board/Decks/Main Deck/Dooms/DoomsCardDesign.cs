using System;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using TMPro;
using UnityEngine;

namespace DeckRenderer.OnBoard
{
    public class DoomsCardDesign : CardDesignBase
    {
        public RectTransform manualDamage;
        public RectTransform infectionDamage;
        public RectTransform misfortuneDamage;
        public RectTransform ocultDamage;
        public RectTransform madnessDamage;


        public override void Fill()
        {
            base.Fill();

            var doomProt = activePrototype as DoomsPrototype;

            if (doomProt != null)
            {
                TrySetEnable(madnessDamage, doomProt.madness);
                TrySetEnable(infectionDamage, doomProt.infection);
                TrySetEnable(misfortuneDamage, doomProt.misfortune);
                TrySetEnable(ocultDamage, doomProt.ocult);
                TrySetEnable(madnessDamage, doomProt.madness);
            }
        }



    }
}
