using Starcounter;
using System.Collections.Generic;

namespace Essam
{
    partial class StartJson : Json
    {

        void Handle(Input.SaveTrigger action)
        {
            var corp = new Corporation()
            {
                Name = CorporationName
            };
            Transaction.Commit();
            AddCorporation(corp);
        }
        public void AddCorporation(Corporation corp)
        {
            var corpJson = (CorporationJson)Self.GET("/Essam/partials/corporation/" + corp.GetObjectID());
            this.Corporations.Add(corpJson);
        }
        public void RefreshCorporations(IEnumerable<Corporation> corps)
        {
            this.Corporations.Clear();
            foreach (Corporation corp in corps)
            {
                AddCorporation(corp);
            }
        }
    }
}
