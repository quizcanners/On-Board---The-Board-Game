using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using PlayerAndEditorGUI;
using QuizCannersUtilities;
using UnityEngine;

namespace DeckRenderer
{
    [Serializable]
    public class CardPrototype : IPEGI, IPEGI_ListInspect, IGotName, IGotIndex, IJObjectCustom
    {
        protected CardsRenderer Rederer => CardsRenderer.instance;

        [SerializeField]
        protected string name;

        public string NameForPEGI
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public int IndexForPEGI { get; set; }

        public string description;
        public string type;
        public string sabotague;

        #region Decoding

        public void Decode(string key, JToken token)
        {
            switch (key)
            {
                case "Id": IndexForPEGI = (int)token; break;
                case "Name": NameForPEGI = (string)token; break;
                case "Description": description = (string)token; break;

            }
        }

        #endregion



        #region Inspector

        private void FillInspect()
        {
            if (Rederer.cardDesign.ActivePrototype != this && icon.Play.Click())
                Rederer.cardDesign.ActivePrototype = this;
        }

        public virtual bool Inspect()
        {
            var changed = false;

            FillInspect();

            "Name".edit(60, ref name).nl();

            "Description".editBig(ref description);

            return changed;
        }

        public virtual bool InspectInList(IList list, int ind, ref int edited)
        {
            var changed = false;

            IndexForPEGI.ToString().write(35);

            this.inspect_Name();

            FillInspect();

            if (icon.Enter.Click())
                edited = ind;

            return changed;
        }

        #endregion

    }
}
