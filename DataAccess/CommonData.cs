using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace WMS.DataAccess
{
    public class CommonData
    {
        public static string connectionString = GetReleaseConnectString();

        private static string GetReleaseConnectString()
        {
            SqlConnectionStringBuilder sqlBulider = new SqlConnectionStringBuilder();
            sqlBulider.DataSource = "database.antufengda.jingjiajie.com,3433";
            sqlBulider.UserID = "wms";
            sqlBulider.Password = "Wms666666";
            sqlBulider.InitialCatalog = "wms";
            sqlBulider.IntegratedSecurity = false;
            sqlBulider.PersistSecurityInfo = true;
            sqlBulider.MultipleActiveResultSets = true;

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = sqlBulider.ToString();
            entityBuilder.Metadata = @"res://*/WMSModel.csdl|res://*/WMSModel.ssdl|res://*/WMSModel.msl";
            return entityBuilder.ToString();
        }

        private static string GetDebugConnectString()
        {
            SqlConnectionStringBuilder sqlBulider = new SqlConnectionStringBuilder();
            sqlBulider.DataSource = "wms.jingjiajie.com,3433";
            sqlBulider.UserID = "wms";
            sqlBulider.Password = "Wms666666";
            sqlBulider.InitialCatalog = "wms";
            sqlBulider.IntegratedSecurity = false;
            sqlBulider.PersistSecurityInfo = true;
            sqlBulider.MultipleActiveResultSets = true;

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = sqlBulider.ToString();
            entityBuilder.Metadata = @"res://*/WMSModel.csdl|res://*/WMSModel.ssdl|res://*/WMSModel.msl";
            return entityBuilder.ToString();
        }

    }
}
