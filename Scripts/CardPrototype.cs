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

        public virtual bool Inspect()
        {
            var changed = false;

            "Name".edit(60, ref name).nl();

            "Description".editBig(ref description);

            return changed;
        }

        public virtual bool InspectInList(IList list, int ind, ref int edited)
        {
            var changed = false;

            IndexForPEGI.ToString().write(35);

            this.inspect_Name();



            if (icon.Enter.Click())
                edited = ind;

            return changed;
        }

        #endregion

    }
}
