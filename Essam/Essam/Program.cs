using System;
using Starcounter;

namespace Essam
{
    class Program
    {
        static void Main()
        {
            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());


            Handle.GET("/Essam", () =>
            {
                return Db.Scope(() =>
                {
                    var json = new StartJson();
                    QueryResultRows<Corporation> corps = Db.SQL<Corporation>("SELECT c FROM Essam.Corporation c");

                    json.RefreshCorporations(corps);

                    if (Session.Current == null)
                    {
                        Session.Current = new Session(SessionOptions.PatchVersioning);
                    }
                    json.Session = Session.Current;
                    return json;
                });
            });

            Handle.GET("/Essam/partials/corporation/{?}", (string id) =>
            {
                var json = new CorporationJson();
                json.Data = DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));
                return json;
            });
            Handle.GET("/Essam/partials/franchise_office/{?}", (string id) =>
            {
                return Db.Scope(() =>
                {
                    var json = new FranchiseOfficeJson();
                    json.Data = DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));

                    return json;
                });
            });

            Handle.GET("/Essam/franchise_office_details/{?}", (string id) =>
            {
                return Db.Scope(() =>
                {

                    FranchiseOffice office = (FranchiseOffice)DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));

                    var json = new FranchiseOfficeDetailsJson();
                    json.Data = DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));

                    json.RefreshSaleTransactions(office.SaleTransactions);

                    json.SaleTransactionNew = Db.Scope(() =>
                    {
                        SaleTransactionDetailsJson transactionJson = new SaleTransactionDetailsJson();
                        return transactionJson;
                    });


                    if (Session.Current == null)
                    {
                        Session.Current = new Session(SessionOptions.PatchVersioning);
                    }
                    json.Session = Session.Current;
                    return json;
                });
            });


        }
    }
}