using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProfilingMapping.Repository
{
    public class Data
    {
        /// <summary>
        /// Save data to the database
        /// </summary>
        /// <typeparam name="T">Any Class Model for the database</typeparam>
        /// <param name="Class">Class Model</param>
        /// <param name="filters">Filters that must be included</param>
        /// <returns>String "Success" id successfull and error message if not.</returns>
        public string Save<T>(T Class, List<string> filter, string PrimaryKey)
        {
            string tableName = Class.GetType().Name;
            string[] except = { "createddate", "updateddate", "updatedby" };

            except = except.Concat(filter.ToArray()).ToArray();

            var Properties = Class
                .GetType()
                .GetProperties()
                .Where(
                    x => !except.Contains(x.Name.ToLower()) &&
                    getValue(x.Name, Class) != null &&
                    getValue(x.Name, Class).ToString() != "00000000-0000-0000-0000-000000000000" &&
                    (x.PropertyType == typeof(System.Guid) || x.PropertyType == typeof(System.String) || x.PropertyType == typeof(System.Int16) || x.PropertyType == typeof(System.Int32) || x.PropertyType == typeof(System.Int64) || x.PropertyType == typeof(System.DateTime) || x.PropertyType == typeof(System.Boolean) || x.PropertyType == typeof(Nullable<Guid>) || x.PropertyType == typeof(Nullable<Int16>) || x.PropertyType == typeof(Nullable<Int32>) || x.PropertyType == typeof(Nullable<Int64>) || x.PropertyType == typeof(Nullable<Boolean>))
                    )
                .Select(x => x.Name);

            string strCommand = "INSERT INTO " + tableName + "([";
            string strParam = "output INSERTED." + PrimaryKey + " VALUES(@";

            strCommand += String.Join("],[", Properties);
            strCommand += "]) ";

            strParam += String.Join(",@", Properties);
            strParam += ")";

            strCommand += strParam;
            Guid output = new Guid();
            string connstring = ConfigurationManager.ConnectionStrings["PROFILINGMAPPINGDBEntities"].ConnectionString;
            string providerString = new EntityConnectionStringBuilder(connstring).ProviderConnectionString;
            SqlConnection conn = new SqlConnection(providerString);
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (var command = new SqlCommand(strCommand, conn))
                    {
                        foreach (string prop in Properties)
                        {
                            command.Parameters.AddWithValue("@" + prop, getValue(prop, Class).ToString());
                        }
                        output = (Guid)command.ExecuteScalar();
                        //var r = command.ExecuteNonQuery();
                    }
                    conn.Close();
                }

                return output.ToString();
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Update data in the database
        /// </summary>
        /// <typeparam name="T">Any class method</typeparam>
        /// <param name="Class">The new data</param>
        /// <param name="FilterClass">The filter for the update</param>
        /// <param name="updatedBy">The user who updated it</param>
        /// <returns>String "Success" id successfull and error message if not.</returns>
        public string Update<T>(T Class, T FilterClass, Guid updatedBy)
        {
            string tableName = Class.GetType().Name;
            string[] except = { "createddate", "createdby" };
            string[] filterexcept = except;
            var FilterProperties1 = FilterClass
                .GetType()
                .GetFields();

            // Get all filter. retrieved values will be added at "where" statement
            var FilterProperties = FilterClass
                .GetType()
                .GetProperties()
                .Where(
                    x => !filterexcept.Contains(x.Name.ToLower()) &&
                    getValue(x.Name, FilterClass) != null &&
                    getValue(x.Name, FilterClass).ToString() != "00000000-0000-0000-0000-000000000000" &&
                    (x.PropertyType == typeof(System.Guid) || x.PropertyType == typeof(System.String) || x.PropertyType == typeof(System.Int16) || x.PropertyType == typeof(System.Int64) || x.PropertyType == typeof(Nullable<Guid>) || x.PropertyType == typeof(Nullable<Int16>) || x.PropertyType == typeof(Nullable<Int64>))
                    )
                .Select(x => x.Name);

            except = except.Concat(FilterProperties.ToArray()).ToArray();

            var Properties = Class
                .GetType()
                .GetProperties()
                .Where(
                    x => !except.Contains(x.Name.ToLower()) &&
                    getValue(x.Name, Class) != null &&
                    getValue(x.Name, Class).ToString() != "00000000-0000-0000-0000-000000000000" &&
                    (x.PropertyType == typeof(System.Guid) || x.PropertyType == typeof(System.String) || x.PropertyType == typeof(System.Int16) || x.PropertyType == typeof(System.Int32) || x.PropertyType == typeof(System.Int64) || x.PropertyType == typeof(System.DateTime) || x.PropertyType == typeof(System.Boolean) || x.PropertyType == typeof(Nullable<Guid>) || x.PropertyType == typeof(Nullable<Int16>) || x.PropertyType == typeof(Nullable<Int32>) || x.PropertyType == typeof(Nullable<Int64>) || x.PropertyType == typeof(Nullable<Boolean>))
                    )
                .Select(x => x.Name);

            string strCommand = "UPDATE " + tableName + " SET [";

            strCommand += String.Join(", [", Properties.Select(x => x + "]=@" + x));

            strCommand += ", UpdatedDate=getdate(), UpdatedBy='" + updatedBy.ToString() + "' ";

            if (filterexcept.Any() && FilterProperties.Count() > 0)
            {
                strCommand += " Where ";

                strCommand += string.Join(" and ", FilterProperties.Select(x => x + "=@" + x));
            }

            try
            {
                string connstring = ConfigurationManager.ConnectionStrings["PROFILINGMAPPINGDBEntities"].ConnectionString;
                string providerString = new EntityConnectionStringBuilder(connstring).ProviderConnectionString;
                SqlConnection conn = new SqlConnection(providerString);
                using (conn)
                {
                    conn.Open();
                    using (var command = new SqlCommand(strCommand, conn))
                    {
                        foreach (string prop in Properties)
                        {
                            command.Parameters.AddWithValue("@" + prop, getValue(prop, Class).ToString());
                        }
                        foreach (string prop in FilterProperties)
                        {
                            command.Parameters.AddWithValue("@" + prop, getValue(prop, FilterClass).ToString());
                        }
                        var r = command.ExecuteNonQuery();
                    }
                    conn.Close();
                }

                return "Success";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Delete data in the database using list of string
        /// </summary>
        /// <typeparam name="T">Any Class Model</typeparam>
        /// <param name="Class">Class in database</param>
        /// <param name="toDeleteItems">List of items to delete</param>
        /// <param name="toDeleteColumn">Filter column for data to be deleted</param>
        /// <returns>String "Success" id successfull and error message if not.</returns>
        public string Delete<T>(T Class, List<string> toDeleteItems, string toDeleteColumn)
        {
            if (toDeleteItems == null)
            {
                return "Please select item to delete.";
            }

            string tableName = Class.GetType().Name;
            string[] except = { "createddate", "createdby", "updatedby", "updateddate", "isactive" };

            var Properties = Class
                .GetType()
                .GetProperties()
                .Where(
                    x => !except.Contains(x.Name.ToLower()) &&
                    getValue(x.Name, Class) != null &&
                    getValue(x.Name, Class).ToString() != "00000000-0000-0000-0000-000000000000" &&
                    (x.PropertyType == typeof(System.Guid) || x.PropertyType == typeof(System.String) || x.PropertyType == typeof(System.Int16) || x.PropertyType == typeof(System.Int32) || x.PropertyType == typeof(System.Int64) || x.PropertyType == typeof(System.DateTime) || x.PropertyType == typeof(System.Boolean) || x.PropertyType == typeof(Nullable<Guid>) || x.PropertyType == typeof(Nullable<Int16>) || x.PropertyType == typeof(Nullable<Int32>) || x.PropertyType == typeof(Nullable<Int64>) || x.PropertyType == typeof(Nullable<Boolean>))
                    )
                .Select(x => x.Name);

            string strCommand = "DELETE " + tableName + " ";

            strCommand += " Where " + toDeleteColumn + " in ('" + String.Join("','", toDeleteItems) + "') ";
            //if (Properties.ToArray().Any())
            //{
            //    strCommand += "and ";
            //    strCommand += string.Join(" and ", Properties.Select(x => x + "=@" + x));
            //}

            try
            {
                string connstring = ConfigurationManager.ConnectionStrings["PROFILINGMAPPINGDBEntities"].ConnectionString;
                string providerString = new EntityConnectionStringBuilder(connstring).ProviderConnectionString;
                SqlConnection conn = new SqlConnection(providerString);
                using (conn)
                {
                    conn.Open();
                    using (var command = new SqlCommand(strCommand, conn))
                    {
                        if (Properties.ToArray().Any())
                        {
                            foreach (string prop in Properties)
                            {
                                command.Parameters.AddWithValue("@" + prop, getValue(prop, Class).ToString());
                            }
                        }
                        var r = command.ExecuteNonQuery();
                    }
                    conn.Close();
                }

                return "Success";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }


        public object getValue<T>(string name, T Class)
        {
            return Class.GetType().GetProperty(name).GetValue(Class, null);
        }
    }
}