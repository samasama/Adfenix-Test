using Starcounter;
using System.Collections.Generic;
using System.Linq;
namespace Essam
{
    partial class CorporationJson : Json
    {
        void Handle(Input.SaveTrigger action)
        {
            var office = new FranchiseOffice()
            {
                Name = this.FranchiseOfficeName,
                ParentCorporation = (Corporation)this.Data
            };
            Transaction.Commit();
            RefreshOffices(((Corporation)this.Data).FranchiseOffices);
        }
        public void AddFranchiseOffice(FranchiseOffice office)
        {
            var officeJson = (FranchiseOfficeJson)Self.GET("/Essam/partials/franchise_office/" + office.GetObjectID());
            this.FranchiseOffices.Add(officeJson);
        }
        public void RefreshOffices(IEnumerable<FranchiseOffice> offices)
        {
            this.FranchiseOffices.Clear();
            IEnumerable<FranchiseOffice> sortedOffices;
            switch (SortedBy)
            {
                case "HomesTotal":
                    sortedOffices = offices.OrderByDescending(x => x.HomesTotal).ThenByDescending(x=>x.CommissionsTotal).ThenByDescending(x=>x.CommissionsAverage).ThenByDescending(x=>x.Trend);
                    break;
                case "CommissionsTotal":
                    sortedOffices = offices.OrderByDescending(x => x.CommissionsTotal).ThenByDescending(x => x.HomesTotal).ThenByDescending(x => x.CommissionsAverage).ThenByDescending(x => x.Trend);
                    break;
                case "CommissionsAverage":
                    sortedOffices = offices.OrderByDescending(x => x.CommissionsAverage).ThenByDescending(x => x.HomesTotal).ThenByDescending(x => x.CommissionsTotal).ThenByDescending(x => x.Trend);
                    break;
                case "Trend":
                    sortedOffices = offices.OrderByDescending(x => x.Trend).ThenByDescending(x => x.HomesTotal).ThenByDescending(x => x.CommissionsTotal).ThenByDescending(x => x.CommissionsAverage);
                    break;
                default:
                    sortedOffices = offices;
                    break;
            }
            foreach (FranchiseOffice office in sortedOffices)
            {
                AddFranchiseOffice(office);
            }
        }
        void Handle(Input.SortByHomeCountTrigger action)
        {
            SortedBy = "HomesTotal";
            RefreshOffices(((Corporation)Data).FranchiseOffices);
        }

        void Handle(Input.SortByTotalCommissionTrigger action)
        {
            SortedBy = "CommissionsTotal";
            RefreshOffices(((Corporation)Data).FranchiseOffices);
        }

        void Handle(Input.SortByAverageCommissionTrigger action)
        {
            SortedBy = "CommissionsAverage";
            RefreshOffices(((Corporation)Data).FranchiseOffices);
        }

        void Handle(Input.SortByPositiveTrendTrigger action)
        {
            SortedBy = "Trend";
            RefreshOffices(((Corporation)Data).FranchiseOffices);
        }
    }
   
}
