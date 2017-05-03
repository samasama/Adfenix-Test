using Starcounter;

namespace Essam
{
    
    partial class FranchiseOfficeJson : Json
    {
        public string FranchiseOfficeDetailsUrl => "/Essam/franchise_office_details/" + ((FranchiseOffice)Data).GetObjectID();

    }
}
