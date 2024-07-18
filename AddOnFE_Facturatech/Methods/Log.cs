using AddOnFE_Facturatech.Proveedor;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AddOnFE_Facturatech.Methods
{
    public partial class Log
    {
        public string Msg { get; set; }
        private static readonly string FileLog = Properties.Settings.Default.FileLog;
        //Funcion para escribir log txt
        public static void EscribirLogFileTXT(string cadenalog)
        {

            string ArchivoLog = FileLog + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00") + ".txt";
            string sPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\" + ArchivoLog;

            sPath = sPath.Substring(6, sPath.Length - 6);
            System.IO.StreamWriter file = new System.IO.StreamWriter(sPath, true);
            file.WriteLine(DateTime.Now + " : " + cadenalog);
            file.Close();
        }

        //Actualizacion Log despues de envio a Carvajal
        public static void UpdateLogCarvajal(string codeline, string codseg, FacturatechWS.response_xml response, string srequest, Boolean reSend, string textstr)
        {
            //try
            //{
            //    SAPbobsCOM.Documents oInvoice = (SAPbobsCOM.Documents)Program.SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
            //    SAPbobsCOM.Documents oCreditNote = (SAPbobsCOM.Documents)Program.SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oCreditNotes);
            //    SAPbobsCOM.CompanyService oCmpSrv;
            //    SAPbobsCOM.SeriesService oSeriesService;
            //    Series oSeries = null;
            //    SeriesParams oSeriesParams = null;
            //    // get company service
            //    oCmpSrv = Program.SBO_Company.GetCompanyService();
            //    // get series service
            //    oSeriesService = (SeriesService)oCmpSrv.GetBusinessService(ServiceTypes.SeriesService);
            //    // get series params
            //    oSeriesParams = (SeriesParams)oSeriesService.GetDataInterface(SeriesServiceDataInterfaces.ssdiSeriesParams);
            //    // set the number of an existing series

            //    SAPbobsCOM.UserTables tbls = null;
            //    SAPbobsCOM.UserTable tbl = null;
            //    string pdfResult = "";
            //    string xmlResult = "";

            //    tbls = Program.SBO_Company.UserTables;
            //    tbl = tbls.Item("FEDIAN_MONITORLOG");

            //    tbl.GetByKey(codeline.ToString());

            //    if (srequest != "")
            //    {
            //        XmlDocument doc = new XmlDocument();
            //        doc.LoadXml(srequest);
            //        XmlNodeList nodeList = null;
            //        nodeList = doc.GetElementsByTagName("fileData");
            //        foreach (XmlNode node in nodeList)
            //        {
            //            node.InnerText = textstr;
            //        }
            //        tbl.UserFields.Fields.Item("U_Det_Peticion").Value = doc.InnerXml;
            //    }

            //    tbl.UserFields.Fields.Item("U_Respuesta_Int").Value = Variables.responseStatus;

            //    if (response.code == "FAIL" || response.error == "REJECTED")
            //    {
            //        if (response.errorMessage.Contains("Ya existe un comprobante con ese mismo tipo y número"))
            //        {
            //            tbl.UserFields.Fields.Item("U_Resultado").Value = response.errorMessage;
            //            tbl.UserFields.Fields.Item("U_Status").Value = "2";

            //            string tipoDoc = (string)tbl.UserFields.Fields.Item("U_DocType").Value;
            //            string documentNumber = (string)tbl.UserFields.Fields.Item("U_Folio").Value;
            //            string documentType = "";
            //            switch (tipoDoc)
            //            {
            //                case "01":
            //                    documentType = "FV";
            //                    oInvoice.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                    oSeriesParams.Series = oInvoice.Series;
            //                    break;
            //                case "02":
            //                    documentType = "FC";
            //                    oInvoice.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                    oSeriesParams.Series = oInvoice.Series;
            //                    break;
            //                case "03":
            //                    documentType = "FE";
            //                    oInvoice.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                    oSeriesParams.Series = oInvoice.Series;
            //                    break;
            //                case "91":
            //                    documentType = "NC";
            //                    oCreditNote.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                    oSeriesParams.Series = oCreditNote.Series;
            //                    break;
            //                case "92":
            //                    documentType = "ND";
            //                    oInvoice.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                    oSeriesParams.Series = oInvoice.Series;
            //                    break;
            //                default:
            //                    break;
            //            }
            //            // get the series
            //            oSeries = oSeriesService.GetSeries(oSeriesParams);
            //            string prefijo = "";
            //            prefijo = oSeries.Prefix;
            //            //Procesos.EscribirLogFileTXT("FAIL: Descarga XML");
            //            xmlResult = MetodosCarvajal.DownloadDocFE(codeline, documentType, prefijo + documentNumber, "SIGNED_XML");


            //            if (xmlResult == "El recurso solicitado no ha sido encontrado.")
            //            {
            //                tbl.UserFields.Fields.Item("U_Status").Value = "2";
            //                tbl.UserFields.Fields.Item("U_Resultado").Value = xmlResult;
            //                tbl.UserFields.Fields.Item("U_Enlace_XML").Value = "";
            //            }
            //            else
            //            {
            //                tbl.UserFields.Fields.Item("U_Status").Value = "1";
            //                tbl.UserFields.Fields.Item("U_Resultado").Value = response.processName;
            //                tbl.UserFields.Fields.Item("U_Enlace_XML").Value = xmlResult;
            //                if (xmlResult.Length > 256000)
            //                {
            //                    tbl.UserFields.Fields.Item("U_Enlace_XML").Value = xmlResult.Substring(0, 256000);
            //                }
            //                else
            //                {
            //                    tbl.UserFields.Fields.Item("U_Enlace_XML").Value = xmlResult;
            //                }
            //            }
            //            System.Threading.Thread.Sleep(10000);
            //            //Procesos.EscribirLogFileTXT("FAIL: Descarga PDF");

            //            pdfResult = MetodosCarvajal.DownloadDocFE(codeline, documentType, prefijo + documentNumber, "PDF");
            //            if (pdfResult == "El recurso solicitado no ha sido encontrado.")
            //            {
            //                tbl.UserFields.Fields.Item("U_Status").Value = "2";
            //                tbl.UserFields.Fields.Item("U_Resultado").Value = pdfResult;
            //                tbl.UserFields.Fields.Item("U_Archivo_PDF").Value = "";
            //            }
            //            else
            //            {
            //                tbl.UserFields.Fields.Item("U_Status").Value = "1";
            //                tbl.UserFields.Fields.Item("U_Resultado").Value = response.processName;
            //                if (pdfResult.Length > 256000)
            //                {
            //                    tbl.UserFields.Fields.Item("U_Archivo_PDF").Value = pdfResult.Substring(0, 256000);
            //                }
            //                else
            //                {
            //                    tbl.UserFields.Fields.Item("U_Archivo_PDF").Value = pdfResult;
            //                }
            //            }
            //        }

            //        else
            //        {
            //            tbl.UserFields.Fields.Item("U_Resultado").Value = response.errorMessage;
            //            tbl.UserFields.Fields.Item("U_Status").Value = "3";
            //            tbl.UserFields.Fields.Item("U_Archivo_PDF").Value = "";
            //            tbl.UserFields.Fields.Item("U_Enlace_XML").Value = "";
            //        }
            //    }

            //    else if (response.processStatus == "PROCESSING")
            //    {
            //        tbl.UserFields.Fields.Item("U_Resultado").Value = response.processName;
            //        tbl.UserFields.Fields.Item("U_Status").Value = "2";

            //        string tipoDoc = (string)tbl.UserFields.Fields.Item("U_DocType").Value;
            //        string documentNumber = (string)tbl.UserFields.Fields.Item("U_Folio").Value;
            //        string documentType = "";
            //        switch (tipoDoc)
            //        {
            //            case "01":
            //                documentType = "FV";
            //                oInvoice.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                oSeriesParams.Series = oInvoice.Series;
            //                break;
            //            case "02":
            //                documentType = "FC";
            //                oInvoice.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                oSeriesParams.Series = oInvoice.Series;
            //                break;
            //            case "03":
            //                documentType = "FE";
            //                oInvoice.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                oSeriesParams.Series = oInvoice.Series;
            //                break;
            //            case "91":
            //                documentType = "NC";
            //                oCreditNote.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                oSeriesParams.Series = oCreditNote.Series;
            //                break;
            //            case "92":
            //                documentType = "ND";
            //                oInvoice.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                oSeriesParams.Series = oInvoice.Series;
            //                break;
            //            default:
            //                break;
            //        }
            //        // get the series
            //        oSeries = oSeriesService.GetSeries(oSeriesParams);
            //        string prefijo = "";
            //        prefijo = oSeries.Prefix;
            //        Log.EscribirLogFileTXT("FAIL: Descarga XML");
            //        xmlResult = MetodosCarvajal.DownloadDocFE(codeline, documentType, prefijo + documentNumber, "SIGNED_XML");
            //        if (xmlResult == "El recurso solicitado no ha sido encontrado.")
            //        {
            //            tbl.UserFields.Fields.Item("U_Status").Value = "2";
            //            tbl.UserFields.Fields.Item("U_Resultado").Value = xmlResult;
            //            tbl.UserFields.Fields.Item("U_Enlace_XML").Value = "";
            //        }
            //        else
            //        {
            //            tbl.UserFields.Fields.Item("U_Status").Value = "1";
            //            tbl.UserFields.Fields.Item("U_Resultado").Value = response.processName;
            //            if (xmlResult.Length > 256000)
            //            {
            //                tbl.UserFields.Fields.Item("U_Enlace_XML").Value = xmlResult.Substring(0, 256000);
            //            }
            //            else
            //            {
            //                tbl.UserFields.Fields.Item("U_Enlace_XML").Value = xmlResult;
            //            }
            //        }
            //        System.Threading.Thread.Sleep(10000);
            //        Log.EscribirLogFileTXT("FAIL: Descarga PDF");
            //        pdfResult = MetodosCarvajal.DownloadDocFE(codeline, documentType, prefijo + documentNumber, "PDF");
            //        if (pdfResult == "El recurso solicitado no ha sido encontrado.")
            //        {
            //            tbl.UserFields.Fields.Item("U_Status").Value = "2";
            //            tbl.UserFields.Fields.Item("U_Resultado").Value = pdfResult;
            //            tbl.UserFields.Fields.Item("U_Archivo_PDF").Value = "";
            //        }
            //        else
            //        {
            //            tbl.UserFields.Fields.Item("U_Status").Value = "1";
            //            tbl.UserFields.Fields.Item("U_Resultado").Value = response.processName;
            //            if (pdfResult.Length > 256000)
            //            {
            //                tbl.UserFields.Fields.Item("U_Archivo_PDF").Value = pdfResult.Substring(0, 256000);
            //            }
            //            else
            //            {
            //                tbl.UserFields.Fields.Item("U_Archivo_PDF").Value = pdfResult;
            //            }
            //        }
            //    }

            //    else if (response.processStatus == "OK" && response.legalStatus == "ACCEPTED")
            //    {
            //        string tipoDoc = (string)tbl.UserFields.Fields.Item("U_DocType").Value;
            //        string documentNumber = (string)tbl.UserFields.Fields.Item("U_Folio").Value;
            //        string documentType = "";
            //        switch (tipoDoc)
            //        {
            //            case "01":
            //                documentType = "FV";
            //                oInvoice.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                oSeriesParams.Series = oInvoice.Series;
            //                break;
            //            case "02":
            //                documentType = "FC";
            //                oInvoice.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                oSeriesParams.Series = oInvoice.Series;
            //                break;
            //            case "03":
            //                documentType = "FE";
            //                oInvoice.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                oSeriesParams.Series = oInvoice.Series;
            //                break;
            //            case "91":
            //                documentType = "NC";
            //                oCreditNote.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                oSeriesParams.Series = oCreditNote.Series;
            //                break;
            //            case "92":
            //                documentType = "ND";
            //                oInvoice.GetByKey((int)tbl.UserFields.Fields.Item("U_DocNum").Value);
            //                oSeriesParams.Series = oInvoice.Series;
            //                break;
            //            default:
            //                break;
            //        }
            //        // get the series
            //        oSeries = oSeriesService.GetSeries(oSeriesParams);
            //        string prefijo = "";
            //        prefijo = oSeries.Prefix;
            //        Log.EscribirLogFileTXT("FAIL: Descarga XML");
            //        xmlResult = MetodosCarvajal.DownloadDocFE(codeline, documentType, prefijo + documentNumber, "SIGNED_XML");
            //        if (xmlResult == "El recurso solicitado no ha sido encontrado.")
            //        {
            //            tbl.UserFields.Fields.Item("U_Status").Value = "2";
            //            tbl.UserFields.Fields.Item("U_Resultado").Value = xmlResult;
            //            tbl.UserFields.Fields.Item("U_Enlace_XML").Value = "";
            //        }
            //        else
            //        {
            //            tbl.UserFields.Fields.Item("U_Status").Value = "1";
            //            tbl.UserFields.Fields.Item("U_Resultado").Value = response.processName;
            //            if (xmlResult.Length > 256000)
            //            {
            //                tbl.UserFields.Fields.Item("U_Enlace_XML").Value = xmlResult.Substring(0, 256000);
            //            }
            //            else
            //            {
            //                tbl.UserFields.Fields.Item("U_Enlace_XML").Value = xmlResult;
            //            }
            //        }
            //        System.Threading.Thread.Sleep(10000);
            //        Log.EscribirLogFileTXT("FAIL: Descarga PDF");
            //        pdfResult = MetodosCarvajal.DownloadDocFE(codeline, documentType, prefijo + documentNumber, "PDF");
            //        if (pdfResult == "El recurso solicitado no ha sido encontrado.")
            //        {
            //            tbl.UserFields.Fields.Item("U_Status").Value = "2";
            //            tbl.UserFields.Fields.Item("U_Resultado").Value = pdfResult;
            //            tbl.UserFields.Fields.Item("U_Archivo_PDF").Value = "";
            //        }
            //        else
            //        {
            //            tbl.UserFields.Fields.Item("U_Status").Value = "1";
            //            tbl.UserFields.Fields.Item("U_Resultado").Value = response.processName;
            //            if (pdfResult.Length > 256000)
            //            {
            //                tbl.UserFields.Fields.Item("U_Archivo_PDF").Value = pdfResult.Substring(0, 256000);
            //            }
            //            else
            //            {
            //                tbl.UserFields.Fields.Item("U_Archivo_PDF").Value = pdfResult;
            //            }
            //        }
            //    }

            //    else
            //    {
            //        if (!string.IsNullOrEmpty(response.errorMessage))
            //        {
            //            tbl.UserFields.Fields.Item("U_Resultado").Value = response.errorMessage;
            //            tbl.UserFields.Fields.Item("U_Status").Value = "3";
            //        }
            //        else
            //        {
            //            tbl.UserFields.Fields.Item("U_Resultado").Value = response.processName;
            //            tbl.UserFields.Fields.Item("U_Status").Value = "2";
            //        }
            //    }

            //    tbl.UserFields.Fields.Item("U_ProcessID").Value = codseg;
            //    Log.EscribirLogFileTXT("CodigoSeguimiento: " + codseg);

            //    Log.EscribirLogFileTXT("Update log");
            //    Variables.lRetCode = tbl.Update();
            //    if (Variables.lRetCode != 0)
            //    {
            //        Program.SBO_Company.GetLastError(out Variables.lRetCode, out Variables.sErrMsg);
            //        Log.EscribirLogFileTXT("updateLog: " + Variables.sErrMsg);
            //    }

            //    Utilities.Release(tbls);
            //    tbls = null;
            //    Utilities.Release(tbl);
            //    tbl = null;
            //    Utilities.Release(oInvoice);
            //    oInvoice = null;
            //    Utilities.Release(oCreditNote);
            //    oCreditNote = null;
            //    Utilities.Release(oCmpSrv);
            //    oCmpSrv = null;
            //    Utilities.Release(oSeriesService);
            //    oSeriesService = null;
            //    Utilities.Release(oSeriesParams);
            //    oSeriesParams = null;
            //    GC.Collect();
            //}
            //catch (Exception ex)
            //{
            //    Log.EscribirLogFileTXT("updateLog: " + ex.Message);
            //}
        }

    }
}
