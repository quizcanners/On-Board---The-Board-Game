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
    public class TricketsCardPrototype : CardPrototypeBase
    {
        public string type;
        public string sabotague;

        #region Decoding
        public override void Decode(string key, CfgData token)
        {
            switch (key) { 
                default: base.Decode(key, token); break;
            }
        }

        #endregion


        public override bool Inspect()
        {
            var changed = base.Inspect();

            "Description".editBig(ref description);
            
            return changed;
        } 

    }
}