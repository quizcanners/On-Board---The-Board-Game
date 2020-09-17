using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using PlayerAndEditorGUI;
using QuizCannersUtilities;
using UnityEngine;

namespace DeckRenderer
{
    [Serializable]
    public class CardPrototypeBase : IPEGI, IPEGI_ListInspect, IGotName, IGotIndex, IJObjectCustom
    {
        protected CardsRenderer CardRederer => CardsRenderer.instance;

        [SerializeField]
        protected string name;
        [SerializeField]
        protected int index;

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

        public int IndexForPEGI
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }

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
            if (CardsRepository.inspected.cardDesignPrefab && icon.Play.Click())
                CardRederer.ShowCard(CardsRepository.inspected, this);
               // Rederer.cardDesign.ActivePrototype = this;
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
