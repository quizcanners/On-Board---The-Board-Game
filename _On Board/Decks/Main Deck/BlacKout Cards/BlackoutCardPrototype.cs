using Newtonsoft.Json.Linq;
using PlayerAndEditorGUI;
using QuizCannersUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckRenderer.OnBoard
{
    [Serializable]
    public class BlackoutCardPrototype : CardPrototypeBase
    {
        public string type;
        public string sabotague;
        
        public override bool Inspect()
        {
            var changed = base.Inspect();

            "Description".editBig(ref description);
            
            return changed;
        } 

    }
}