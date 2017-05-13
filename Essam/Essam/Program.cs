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
                    var startJson = new StartJson();
                    QueryResultRows<Corporation> corps = Corporation.Corporations;

                    startJson.RefreshCorporations(corps);

                    if (Session.Current == null)
                    {
                        Session.Current = new Session(SessionOptions.PatchVersioning);
                    }
                    startJson.Session = Session.Current;
                    return startJson;
                });
            });


            Handle.GET("/Essam/franchise_office_details/{?}", (string id) =>
            {
                return Db.Scope(() =>
                {

                    FranchiseOffice office = (FranchiseOffice)DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));

                    var franchiseOfficeDetailsJson = new FranchiseOfficeDetailsJson
                    {
                        Data = office,
                        Address = (AddressDetailsJson)Self.GET("/Essam/partials/address_details/" + office.GetObjectID()),
                        SaleTransactionNew = Db.Scope(() =>
                        {
                            SaleTransactionDetailsJson transactionJson = new SaleTransactionDetailsJson
                            {
                                Address = new AddressDetailsJson()
                            };

                            if (Session.Current == null)
                            {
                                Session.Current = new Session(SessionOptions.PatchVersioning);
                            }
                            transactionJson.Session = Session.Current;
                            return transactionJson;
                        })
                    };

                    franchiseOfficeDetailsJson.RefreshSaleTransactions(office.SaleTransactions);

                    if (Session.Current == null)
                    {
                        Session.Current = new Session(SessionOptions.PatchVersioning);
                    }
                    franchiseOfficeDetailsJson.Session = Session.Current;

                    return franchiseOfficeDetailsJson;
                });
            });


            Handle.GET("/Essam/partials/corporation/{?}", (string id) =>
            {
                Corporation corp = (Corporation)DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));
                var corporationJson = new CorporationJson
                {
                    Data = corp
                };
                corporationJson.RefreshOffices(corp.FranchiseOffices);

                return corporationJson;
            });

            Handle.GET("/Essam/partials/franchise_office/{?}", (string id) =>
            {
                var franchiseOfficeJson = new FranchiseOfficeJson
                {
                    Data = DbHelper.FromID(DbHelper.Base64DecodeObjectID(id))
                };
                return franchiseOfficeJson;

            });

            Handle.GET("/Essam/partials/address_details/{?}", (string id) =>
            {
                var addressJson = new AddressDetailsJson
                {
                    Data = DbHelper.FromID(DbHelper.Base64DecodeObjectID(id))
                };
                return addressJson;

            });

            Handle.GET("/Essam/partials/address/{?}", (string id) =>
            {
                var addressJson = new AddressJson
                {
                    Data = DbHelper.FromID(DbHelper.Base64DecodeObjectID(id))
                };
                return addressJson;

            });

            Handle.GET("/Essam/partials/transaction/{?}", (string id) =>
            {
                SaleTransaction transaction = (SaleTransaction)DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));
                var saleTransactionJson = new SaleTransactionJson
                {
                    Data = transaction,
                    Address = (AddressJson)Self.GET("/Essam/partials/address/" + transaction.GetObjectID())
                };
                return saleTransactionJson;

            });
        }
    }
}