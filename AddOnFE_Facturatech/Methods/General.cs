using SAPbouiCOM.Framework;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace AddOnFE_Facturatech.Methods
{
    class General
    {
        public static int lRetCode;
        public static string sErrMsg;
        public static void CentralizeForm(UserFormBase oForm)
        {
            oForm.UIAPIRawForm.Top = ((Application.SBO_Application.Desktop.Height - oForm.UIAPIRawForm.ClientHeight) / 2) - 75;
            oForm.UIAPIRawForm.Left = (Application.SBO_Application.Desktop.Width - oForm.UIAPIRawForm.ClientWidth) / 2;
        }
        public static void CargueInicial()
        {
            try
            {
                SAPbobsCOM.Recordset oRecordset = ((SAPbobsCOM.Recordset)(Program.SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
                string sSql = Querys.Default.PARAMG;
                oRecordset.DoQuery(sSql);

                if (oRecordset.RecordCount > 0)
                {
                    Variables.proveedor = oRecordset.Fields.Item("U_Proveedor").Value.ToString();
                    Variables.nit = oRecordset.Fields.Item("U_NIT_Emisor").Value.ToString();
                    Variables.username = oRecordset.Fields.Item("U_Email_Usuario").Value.ToString();
                    Variables.password = oRecordset.Fields.Item("U_Clave_Usuario").Value.ToString();
                    Variables.token = oRecordset.Fields.Item("U_Token").Value.ToString();
                }
                Utilities.Release(oRecordset);
                oRecordset = null;
                GC.Collect();
            }
            catch (SystemException ex)
            {
                Application.SBO_Application.MessageBox(ex.Message);
                Log.EscribirLogFileTXT("CargueInicial: " + ex.Message);
            }
        }

        public static bool version(SAPbobsCOM.Company oCmpn)
        {
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                bool resultado;

                SAPbobsCOM.UserTables tbls = null;
                SAPbobsCOM.UserTable tbl = null;

                tbls = oCmpn.UserTables;
                tbl = tbls.Item("FEDIAN_VERSION");

                if (tbl.GetByKey("1") == true & tbl.Name == version)
                {
                    resultado = false;
                }
                else if (tbl.GetByKey("1") == true & tbl.Name != version)
                {
                    resultado = true;
                    tbl.Name = version;

                    lRetCode = tbl.Update();

                    if (lRetCode != 0)
                    {
                        if (lRetCode == -1 || lRetCode == -2035 || lRetCode == -5002)
                        { }
                        else
                        {
                            oCmpn.GetLastError(out lRetCode, out sErrMsg);
                            Log.EscribirLogFileTXT("PreCarga: " + lRetCode + " > " + sErrMsg);
                        }
                    }
                }
                else
                {
                    resultado = true;
                    tbl.Code = "1";
                    tbl.Name = version;

                    lRetCode = tbl.Add();

                    if (lRetCode != 0)
                    {
                        if (lRetCode == -1 || lRetCode == -2035 || lRetCode == -5002)
                        { }
                        else
                        {
                            oCmpn.GetLastError(out lRetCode, out sErrMsg);
                            Log.EscribirLogFileTXT("PreCarga: " + lRetCode + " > " + sErrMsg);
                        }
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Log.EscribirLogFileTXT("Version: " + ex.Message);
                return true;
            }
        }

        public static string DecodeTo64(string toDecode)
        {
            byte[] data = Convert.FromBase64String(toDecode);
            string decodedString = Encoding.UTF8.GetString(data);
            return decodedString;
        }
        public static string SerializeObjectToXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }

        public static string ConvertToBase64(string xmlString)
        {
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlString);
            return Convert.ToBase64String(xmlBytes);
        }
        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir la contraseña en una matriz de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir los bytes en una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        //Add DTE al monitor (Timer)
        public static void AddDTEMonitor()
        {
            try
            {
                Variables.oRS = null;
                if (Variables.oRS != null) // Not sure why this is needed as rs will always be null but leaving it in anyway
                {
                    Utilities.Release(Variables.oRS);
                    Variables.oRS = null;
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                Variables.oRS = (SAPbobsCOM.Recordset)Program.SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                string sSql = "";

                if (Program.SBO_Company.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    sSql = "Select A3.\"U_DocDIAN\", A0.\"DocNum\", A1.\"BeginStr\", A0.\"ObjType\", A0.\"DocEntry\", A2.\"USER_CODE\", A0.\"DocDate\", A0.\"DocTime\" " +
                            "From OINV A0 " +
                            "Inner Join NNM1 A1 On A0.\"Series\" = A1.\"Series\" And A0.\"ObjType\" = A1.\"ObjectCode\" " +
                            "Inner Join OUSR A2 On A0.\"UserSign\" = A2.\"USERID\" " +
                            "Inner Join \"@FEDIAN_NUMAUTORI\" A3 On A1.\"Series\" = A3.\"Code\" " +
                            "Where A0.\"DocEntry\" Not In (Select \"U_DocNum\" From \"@FEDIAN_MONITORLOG\" Where \"U_ObjType\" = '13') And A0.\"DocDate\" Between ADD_DAYS(CURRENT_DATE, -150) and To_Date(Current_Date) " +
                            "Union All " +
                            "Select A3.\"U_DocDIAN\", A0.\"DocNum\", A1.\"BeginStr\", A0.\"ObjType\", A0.\"DocEntry\", A2.\"USER_CODE\", A0.\"DocDate\", A0.\"DocTime\" " +
                            "From ORIN A0 " +
                            "Inner Join NNM1 A1 On A0.\"Series\" = A1.\"Series\" And A0.\"ObjType\" = A1.\"ObjectCode\" " +
                            "Inner Join OUSR A2 On A0.\"UserSign\" = A2.\"USERID\" " +
                            "Inner Join \"@FEDIAN_NUMAUTORI\" A3 On A1.\"Series\" = A3.\"Code\" " +
                            "Where A0.\"DocEntry\" Not In (Select \"U_DocNum\" From \"@FEDIAN_MONITORLOG\" Where \"U_ObjType\" = '14') And A0.\"DocDate\" Between ADD_DAYS(CURRENT_DATE, -1) and To_Date(Current_Date) ";
                }

                else
                {
                    sSql = "Select A3.U_DocDIAN, A0.DocNum, A1.BeginStr, A0.ObjType, A0.DocEntry, A2.USER_CODE, A0.DocDate, A0.DocTime " +
                            "From OINV A0 " +
                            "Inner Join NNM1 A1 On A0.Series = A1.Series And A0.ObjType = A1.ObjectCode " +
                            "Inner Join OUSR A2 On A0.UserSign = A2.USERID " +
                            "Inner Join \"@FEDIAN_NUMAUTORI\" A3 On A1.Series = A3.Code " +
                            "Where A0.DocEntry Not In(Select U_DocNum From \"@FEDIAN_MONITORLOG\" Where U_ObjType = '13') And CONVERT(char(10), A0.DocDate,126) Between CONVERT(char(10), GetDate() - 1,126) and CONVERT(char(10), GetDate(),126) " +
                            "Union All " +
                            "Select A3.U_DocDIAN, A0.DocNum, A1.BeginStr, A0.ObjType, A0.DocEntry, A2.USER_CODE, A0.DocDate, A0.DocTime " +
                            "From ORIN A0 " +
                            "Inner Join NNM1 A1 On A0.Series = A1.Series And A0.ObjType = A1.ObjectCode " +
                            "Inner Join OUSR A2 On A0.UserSign = A2.USERID " +
                            "Inner Join \"@FEDIAN_NUMAUTORI\" A3 On A1.Series = A3.Code " +
                            "Where A0.DocEntry Not In(Select U_DocNum From \"@FEDIAN_MONITORLOG\" Where U_ObjType = '14') And CONVERT(char(10), A0.DocDate,126) Between CONVERT(char(10), GetDate() - 1,126) and CONVERT(char(10), GetDate(),126) ";
                }

                Variables.oRS.DoQuery(sSql);

                if (Variables.oRS.RecordCount > 0)
                {
                    System.Data.DataTable ResultQuery = null;
                    ResultQuery = new System.Data.DataTable();

                    ResultQuery = RecordSet_DataTable(Variables.oRS);

                    for (int i = 0; i < ResultQuery.Rows.Count; i++) //Looping through rows
                    {
                        SAPbobsCOM.UserTables tablas = null;
                        SAPbobsCOM.UserTable tabla = null;

                        tablas = Program.SBO_Company.UserTables;
                        tabla = tablas.Item("FEDIAN_MONITORLOG");

                        Variables.oRs = null;
                        Variables.oRs = (SAPbobsCOM.Recordset)Program.SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        Variables.oRs.DoQuery(string.Format(Querys.Default.MaxLog));

                        int newCode;
                        newCode = (int)Variables.oRs.Fields.Item("NextCode").Value;

                        tabla.Code = Convert.ToString(newCode);
                        tabla.Name = Convert.ToString(newCode);
                        tabla.UserFields.Fields.Item("U_DocType").Value = Convert.ToString(ResultQuery.Rows[i]["U_DocDIAN"]);
                        tabla.UserFields.Fields.Item("U_Folio").Value = Convert.ToString(ResultQuery.Rows[i]["DocNum"]);
                        tabla.UserFields.Fields.Item("U_Prefijo").Value = Convert.ToString(ResultQuery.Rows[i]["BeginStr"]);
                        tabla.UserFields.Fields.Item("U_ObjType").Value = Convert.ToString(ResultQuery.Rows[i]["ObjType"]);
                        tabla.UserFields.Fields.Item("U_DocNum").Value = Convert.ToString(ResultQuery.Rows[i]["DocEntry"]);
                        tabla.UserFields.Fields.Item("U_Usuario_Envio").Value = Convert.ToString(ResultQuery.Rows[i]["USER_CODE"]);
                        tabla.UserFields.Fields.Item("U_Fecha_Envio").Value = Convert.ToString(ResultQuery.Rows[i]["DocDate"]);
                        tabla.UserFields.Fields.Item("U_Hora_Envio").Value = Convert.ToString(ResultQuery.Rows[i]["DocTime"]);
                        tabla.UserFields.Fields.Item("U_Resultado").Value = string.Empty;
                        tabla.UserFields.Fields.Item("U_Status").Value = string.Empty;
                        tabla.UserFields.Fields.Item("U_ProcessID").Value = string.Empty;
                        tabla.UserFields.Fields.Item("U_Fecha_ReEnvio").Value = string.Empty;
                        tabla.UserFields.Fields.Item("U_Hora_ReEnvio").Value = string.Empty;
                        tabla.UserFields.Fields.Item("U_Det_Peticion").Value = string.Empty;
                        tabla.UserFields.Fields.Item("U_Respuesta_Int").Value = string.Empty;
                        tabla.UserFields.Fields.Item("U_Archivo_PDF").Value = string.Empty;
                        tabla.UserFields.Fields.Item("U_Enlace_XML").Value = string.Empty;
                        tabla.UserFields.Fields.Item("U_ID_Seguimiento").Value = string.Empty;

                        lRetCode = tabla.Add();

                        if (lRetCode != 0)
                        {
                            Program.SBO_Company.GetLastError(out lRetCode, out sErrMsg);
                            Log.EscribirLogFileTXT("FE: AddDTEMonitor: " + sErrMsg);
                        }
                        else
                        {
                            string sCodeLog = Convert.ToString(newCode);
                            string sDocentry = Convert.ToString(ResultQuery.Rows[i]["DocEntry"]);
                            string sDocnum = Convert.ToString(ResultQuery.Rows[i]["DocNum"]);
                            string sPrefijo = Convert.ToString(ResultQuery.Rows[i]["BeginStr"]);
                            string sStatus = string.Empty;
                            string sTipoDoc = Convert.ToString(ResultQuery.Rows[i]["U_DocDIAN"]);
                            Send.SendFE(sDocentry, sDocnum, sPrefijo, sCodeLog, sTipoDoc, true);
                            Log.EscribirLogFileTXT("FE: AddDTEMonitor: Se agrego registro: " + newCode + " DocNum: " + Convert.ToString(ResultQuery.Rows[i]["DocNum"]) + " DocEntry: " + Convert.ToString(ResultQuery.Rows[i]["DocEntry"]));
                        }
                        Utilities.Release(Variables.oRs);
                        Variables.oRs = null;
                        GC.Collect();
                        Utilities.Release(tablas);
                        tablas = null;
                        GC.Collect();
                        Utilities.Release(tabla);
                        tabla = null;
                        GC.Collect();
                    }
                }
                Utilities.Release(Variables.oRS);
                Variables.oRS = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                Log.EscribirLogFileTXT("FE: AddDTEMonitor: " + ex.Message);
            }
        }
        public static System.Data.DataTable RecordSet_DataTable(SAPbobsCOM.Recordset RS)
        {

            System.Data.DataTable dtTable = new System.Data.DataTable();
            System.Data.DataColumn NewCol = default(System.Data.DataColumn);
            DataRow NewRow = default(DataRow);
            int ColCount = 0;

            //try
            //{

            while (ColCount < RS.Fields.Count)
            {
                string dataType = "System.";
                switch (RS.Fields.Item(ColCount).Type)
                {
                    case SAPbobsCOM.BoFieldTypes.db_Alpha:
                        dataType = dataType + "String";
                        break;
                    case SAPbobsCOM.BoFieldTypes.db_Date:
                        dataType = dataType + "DateTime";
                        break;
                    case SAPbobsCOM.BoFieldTypes.db_Float:
                        dataType = dataType + "Double";
                        break;
                    case SAPbobsCOM.BoFieldTypes.db_Memo:
                        dataType = dataType + "String";
                        break;
                    case SAPbobsCOM.BoFieldTypes.db_Numeric:
                        dataType = dataType + "Decimal";
                        break;
                    default:
                        dataType = dataType + "String";
                        break;
                }

                NewCol = new System.Data.DataColumn(RS.Fields.Item(ColCount).Name, System.Type.GetType(dataType));
                dtTable.Columns.Add(NewCol);
                ColCount++;
            }
            int iCol = 0;
            while (!(RS.EoF))
            {
                NewRow = dtTable.NewRow();

                dtTable.Rows.Add(NewRow);

                iCol = 0;
                ColCount = 0;
                while (ColCount < RS.Fields.Count)
                {
                    //NewRow.Item(RS.Fields.Item(ColCount).Name) = RS.Fields.Item(ColCount).Value;
                    NewRow[iCol] = RS.Fields.Item(ColCount).Value;
                    iCol++;
                    ColCount++;
                }
                RS.MoveNext();
            }
            return dtTable;
        }

    }
}
