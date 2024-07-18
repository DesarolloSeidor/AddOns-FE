using System.IO;
using SAPbobsCOM;
using System.Reflection;

namespace AddOnFE_Facturatech.Resources
{
    class DBResourceExtension
    {
        public Company oCompany;
        private static string dbType = null;

        public DBResourceExtension()
        {


        }

        public string GetSQL(string resource)
        {
            var ns = typeof(Program).Namespace;
            if (dbType == null)
                dbType = (oCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB) ? "hana" : "sql";

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ns + ".Resources." + dbType + "." + resource))
            {
                if (stream != null)
                {
                    using (var streamReader = new StreamReader(stream, System.Text.Encoding.UTF8))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
            return string.Empty;
        }

        public string GetJson(string resource)
        {
            var ns = typeof(Program).Namespace;

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ns + ".Resources." + "json" + "." + resource))
            {
                if (stream != null)
                {
                    using (var streamReader = new StreamReader(stream, System.Text.Encoding.UTF8))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
            return string.Empty;
        }
    }

}
