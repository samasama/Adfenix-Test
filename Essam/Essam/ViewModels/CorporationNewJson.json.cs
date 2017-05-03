using Starcounter;

namespace Essam
{
    partial class CorporationNewJson : Json
    {
        void Handle(Input.SaveTrigger action)
        {
            Transaction.Commit();
        }
       
    }
}
