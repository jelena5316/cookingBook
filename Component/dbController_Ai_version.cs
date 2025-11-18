/*********
 * AI versions, SqliteConnection using
 ********/

using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace MajPAbGr_project
{ /*
    public class dbController : DataBaseCreator
    {
        private string connectionString;
        private SqliteConnection connection;
        private SqliteDataReader reader;
        private SqliteCommand command;

        protected int error_code = 0;
        protected string error_message = "";
        protected AnswerInfo Info;

        public dbController()
        {
            Tables tbs = new Tables();
            setTablesColumnsNames(tbs);
            connectionString = Program.connectionStringPath;
            connection = new SqliteConnection(connectionString);
        }

        // Properties
        public SqliteConnection Connection => connection;
        public SqliteDataReader Reader { get => reader; set => reader = value; }
        public SqliteCommand Command { get => command; set => command = value; }
        public AnswerInfo getInfo() => Info;

        public string ConnectionString
        {
            get => connectionString;
            set => connectionString = value;
        }

        public void resetConnecting()
        {
            connection = new SqliteConnection(connectionString);
        }

        // --------------------------------------------
        // Test DB connection
        // --------------------------------------------
        public bool testConnection()
        {
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch (SqliteException ex)
            {
                if (ex.SqliteErrorCode == 14)
                {
                    string message = $"{DateTime.Now} {ex.Message} {connectionString}";
                    Program.cook_error(message);
                }
                return false;
            }
        }

        public override TablesCreator CreateDataBaseTables(dbController db)
        {
            TablesCreator tc = base.CreateDataBaseTables(this);
            int result = tc.createTable();
            Console.WriteLine($"Created tables number is {result}");
            return tc;
        }

        // --------------------------------------------
        // INSERT / UPDATE / DELETE
        // --------------------------------------------
        public int Edit(string query)
        {
            int ind = 0;

            try
            {
                connection.Open();
                using (command = new SqliteCommand(query, connection))
                {
                    ind = command.ExecuteNonQuery();
                }
            }
            catch (SqliteException ex)
            {
                Program.cook_error($"{DateTime.Now} {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return ind;
        }

        // --------------------------------------------
        // COUNT / SCALAR
        // --------------------------------------------
        public string Count(string query)
        {
            string count = "";

            try
            {
                connection.Open();
                using (command = new SqliteCommand(query, connection))
                {
                    var num = command.ExecuteScalar();
                    count = num?.ToString() ?? "0";
                }
            }
            catch (SqliteException ex)
            {
                Program.cook_error($"{DateTime.Now} {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return count;
        }

        // --------------------------------------------
        // CATALOG (id, name)
        // --------------------------------------------
        public List<Item> Catalog(string query)
        {
            List<Item> list = new List<Item>();

            try
            {
                connection.Open();
                using (command = new SqliteCommand(query, connection))
                {
                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item();
                            item.createItem(reader.GetInt32(0), reader.GetString(1));
                            list.Add(item);
                        }
                    }
                }

                Info = new AnswerInfo(0, "", query, connectionString);
            }
            catch (SqliteException ex)
            {
                error_code = ex.SqliteErrorCode;
                error_message = ex.Message;
                Program.cook_error($"{DateTime.Now} {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return list;
        }

        // --------------------------------------------
        // Example: Technology reader
        // --------------------------------------------
        public List<FormEF_test.Technology> DbReadTech(string query)
        {
            List<FormEF_test.Technology> techs = new List<FormEF_test.Technology>();

            try
            {
                connection.Open();
                using (command = new SqliteCommand(query, connection))
                {
                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tech = new FormEF_test.Technology
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Note = reader.GetString(2)
                            };
                            techs.Add(tech);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Program.cook_error($"{DateTime.Now} {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return techs;
        }

        // --------------------------------------------
        // Dispose the persistent connection
        // --------------------------------------------
        public void DisposeConnection()
        {
            if (connection != null)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                    connection.Close();
                connection.Dispose();
            }
        }
    }*/

//    using System;
//    using System.Collections.Generic;
//    using Microsoft.Data.Sqlite;

//    namespace MajPAbGr_project
//    {
//        public class dbController : IDisposable
//        {
//            protected SqliteConnection connection;
//            public ErrorInfo Info { get; private set; } = new ErrorInfo();

//            public dbController(string connectionString)
//            {
//                connection = new SqliteConnection(connectionString);
//            }

//            // --- Connection helpers ---
//            protected void EnsureOpen()
//            {
//                if (connection.State != System.Data.ConnectionState.Open)
//                    connection.Open();
//            }

//            protected void SafeClose()
//            {
//                if (connection.State == System.Data.ConnectionState.Open)
//                    connection.Close();
//            }

//            // --- Execute SELECT returning strings ---
//            protected List<string> dbReader(string query)
//            {
//                List<string> results = new List<string>();

//                try
//                {
//                    EnsureOpen();
//                    using var cmd = new SqliteCommand(query, connection);
//                    using var reader = cmd.ExecuteReader();

//                    while (reader.Read())
//                    {
//                        results.Add(reader[0].ToString());
//                    }

//                    Info.Clear();
//                }
//                catch (SqliteException ex)
//                {
//                    Info.SetError(ex.SqliteErrorCode, ex.Message, query, connection.DataSource);
//                }
//                finally
//                {
//                    SafeClose();
//                }

//                return results;
//            }

//            // --- Execute SELECT returning Item list ---
//            protected List<Item> Catalog(string query)
//            {
//                List<Item> catalog = new List<Item>();

//                try
//                {
//                    EnsureOpen();
//                    using var cmd = new SqliteCommand(query, connection);
//                    using var reader = cmd.ExecuteReader();

//                    while (reader.Read())
//                    {
//                        catalog.Add(new Item
//                        {
//                            id = reader.GetInt32(0),
//                            name = reader.GetString(1)
//                        });
//                    }

//                    Info.Clear();
//                }
//                catch (SqliteException ex)
//                {
//                    Info.SetError(ex.SqliteErrorCode, ex.Message, query, connection.DataSource);
//                }
//                finally
//                {
//                    SafeClose();
//                }

//                return catalog;
//            }

//            // --- Execute SELECT returning Element list ---
//            protected List<Element> dbReadElement(string query)
//            {
//                List<Element> elements = new List<Element>();

//                try
//                {
//                    EnsureOpen();
//                    using var cmd = new SqliteCommand(query, connection);
//                    using var reader = cmd.ExecuteReader();

//                    while (reader.Read())
//                    {
//                        elements.Add(new Element(
//                            reader.GetInt32(0),
//                            reader.GetString(1),
//                            reader.GetDouble(2)
//                        ));
//                    }

//                    Info.Clear();
//                }
//                catch (SqliteException ex)
//                {
//                    Info.SetError(ex.SqliteErrorCode, ex.Message, query, connection.DataSource);
//                }
//                finally
//                {
//                    SafeClose();
//                }

//                return elements;
//            }

//            // --- Execute INSERT, UPDATE, DELETE ---
//            protected int Edit(string query)
//            {
//                int rowsAffected = 0;

//                try
//                {
//                    EnsureOpen();
//                    using var cmd = new SqliteCommand(query, connection);
//                    rowsAffected = cmd.ExecuteNonQuery();
//                    Info.Clear();
//                }
//                catch (SqliteException ex)
//                {
//                    Info.SetError(ex.SqliteErrorCode, ex.Message, query, connection.DataSource);
//                }
//                finally
//                {
//                    SafeClose();
//                }

//                return rowsAffected;
//            }

//            // --- Dispose pattern ---
//            public void Dispose()
//            {
//                SafeClose();
//                connection?.Dispose();
//            }
//        }
//    }

}

