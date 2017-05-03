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
                    var start = new Start();
                    var json = new StartJson()
                    {
                        Data = start
                    };
                    if (start.Corporations != null)
                    {
                        json.RefreshCorporations(start.Corporations);
                    }
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
        }
    }
}