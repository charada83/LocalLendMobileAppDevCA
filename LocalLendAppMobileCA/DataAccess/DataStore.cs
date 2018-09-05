using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace LocalLendAppMobileCA.DataAccess
{
    class DataStore
    {
        public static string DBLocation { get; }

        static DataStore()
        {
            if (string.IsNullOrEmpty(DBLocation))
            {
                DBLocation = Path.Combine(Environment.GetFolderPath

                    (Environment.SpecialFolder.Personal), "LocalLendApp.db3");

                InitialiseDB();
            }
        }

        private static void InitialiseDB()
        {
            try
            {
                using (SQLiteConnection cxn = new SQLiteConnection(DBLocation))
                {
                    cxn.DropTable<Item>();

                    cxn.CreateTable<Item>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void InsertIntoTableItem(Item item)
        {
            try
            {
                using (SQLiteConnection cxn = new SQLiteConnection(DBLocation))
                {
                    TableQuery<Item> query = cxn.Table<Item>();
                    //if (query.Count() == 0)
                    //{
                        cxn.Insert(item);
                    //}
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public IEnumerable<Item> GetItems()
        {
            try
            {
                using (SQLiteConnection cxn = new SQLiteConnection(DBLocation))
                {
                    IEnumerable<Item> items = cxn.Query<Item>("SELECT * FROM Item");

                    return items;
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}