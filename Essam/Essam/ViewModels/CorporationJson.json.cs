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
                FranchiseOfficeName = this.FranchiseOfficeName,
                ParentCorporation = (Corporation)this.Data
            };
            Transaction.Commit();
            RefreshOffices(((Corporation)this.Data).FranchiseOffices);
        }
        public void AddFranchiseOffice(FranchiseOffice office)
        {
            var officeJson = new FranchiseOfficeJson
            {
                Data = office
            };
            this.FranchiseOffices.Add(officeJson);
        }
        public void RefreshOffices(IEnumerable<FranchiseOffice> offices)
        {
            this.FranchiseOffices.Clear();
            IEnumerable<FranchiseOffice> sortedOffices;
            switch (SortedBy)
            {
                case "HomesTotal":
                    sortedOffices = offices.OrderByDescending(x => x.HomesTotal);
                    break;
                case "CommissionsTotal":
                    sortedOffices = offices.OrderByDescending(x => x.CommissionsTotal);
                    break;
                case "CommissionsAverage":
                    sortedOffices = offices.OrderByDescending(x => x.CommissionsAverage);
                    break;
                case "Trend":
                    sortedOffices = offices.OrderByDescending(x => x.Trend);
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
