using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using PlayerAndEditorGUI;
using QuizCannersUtilities;
using UnityEngine;

namespace DeckRenderer
{
    [Serializable]
    public class CardPrototypeBase : IPEGI, IPEGI_ListInspect, IGotName, IGotIndex, ICfgDecode
    {
        protected CardsRenderer CardRederer => CardsRenderer.instance;

        [SerializeField]
        protected bool selectedForRender;

        [SerializeField]
        protected string name;
        [SerializeField]
        protected int index;
        [SerializeField]
        public int count = 1;
        [SerializeField]
        public string description;

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
        
        #region Decoding
        public virtual void Decode(string key, CfgData token)
        {
            switch (key)
            {
                case "Id": token.ToInt(ref index); break;
                case "Name": NameForPEGI = token.ToString(); break;
                case "Count": count = token.ToInt(1); break;
                case "Description": description = token.ToString(); break;
            }
        }

        #endregion
        
        #region Inspector

        protected void FillInspect()
        {
            if (DeckBase.inspected && DeckBase.inspected.cardDesignPrefab && icon.Play.Click())
                CardRederer.ShowCard(DeckBase.inspected, this);
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

            pegi.toggle(ref selectedForRender);

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
