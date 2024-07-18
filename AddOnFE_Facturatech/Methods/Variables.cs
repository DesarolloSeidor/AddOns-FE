using AddOnFE_Facturatech.Proveedor.Facturatech;
using System;

namespace AddOnFE_Facturatech.Methods
{
    public static class Variables
    {
        public static string proveedor { get; set; }
        public static string nit { get; set; }
        public static string username { get; set; }
        public static string password { get; set; }
        public static string token { get; set; }
        public static string LogCode { get; set; }
        public static string requestSend { get; set; }
        public static bool responseStatus { get; set; }
        public static System.Timers.Timer bTimer { get; set; }
        public static System.Timers.Timer aTimer { get; set; }
        public static string transaccionID { get; set; }
        public static string user { get; set; }
        public static bool senalActiva = true;
        public static bool banderaReenviar = true;
        public static bool banderaVerificaEstados = true;
        public static bool banderaAgregarDoc = true;
        public static bool banderaUpdateLog = true;
        public static DateTime dateSend { get; set; }
        public static int lRetCode;
        public static string sErrMsg;
        public static SAPbobsCOM.Recordset oRS = null;
        public static SAPbobsCOM.Recordset oRs = null;
        public static SAPbobsCOM.Documents oDocument = null;
        public static SAPbobsCOM.Company oCompany = Program.SBO_Company;
        public static dynamic Documento = null;
        public static string urlAnexos { get; set; }
        public static string base64 { get; set; }
        public static string posicionXCufe { get; set; }
        public static string posicionYCufe { get; set; }
        public static string rotacionCufe { get; set; }
        public static string fuenteCufe { get; set; }
        public static string posicionXQr { get; set; }
        public static string posicionYQr { get; set; }
        public static string descripcionGeneral { get; set; }
        public static string cvcc { get; set; }
        public static string formato { get; set; }
        public static string fechaHoraRecepcion { get; set; }
        public static string cufe { get; set; }
        public static string qr { get; set; }
    }
}
