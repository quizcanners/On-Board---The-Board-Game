using System;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeckRenderer.OnBoard
{
    public class DoomsCardDesign : CardDesignGeneric<DoomsPrototype>
    {
        public Image manualDamage;
        public Image infectionDamage;
        public Image misfortuneDamage;
        public Image ocultDamage;
        public Image madnessDamage;
        
        public override void Fill(DoomsPrototype prot)
        {

            TrySetActive(manualDamage, prot.physical);
            TrySet(manualDamage, ImpostorPower.Physical.GetSprite());

            TrySetActive(madnessDamage, prot.madness);
            TrySet(madnessDamage, ImpostorPower.Madness.GetSprite());
            
            TrySetActive(infectionDamage, prot.infection);
            TrySet(infectionDamage, ImpostorPower.Infection.GetSprite());

            TrySetActive(misfortuneDamage, prot.misfortune);
            TrySet(misfortuneDamage, ImpostorPower.Misfortune.GetSprite());

            TrySetActive(ocultDamage, prot.ocult);
            TrySet(ocultDamage, ImpostorPower.Ocult.GetSprite());

        }
    }
}
