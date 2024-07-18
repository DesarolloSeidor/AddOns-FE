using AddOnFE_Facturatech.Proveedor.Facturatech;
using SAPbobsCOM;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Security.Cryptography;
using AddOnFE_Facturatech.Methods;
using Application = SAPbouiCOM.Framework.Application;

namespace AddOnFE_Facturatech.Proveedor
{
    class MetodosFacturatech
    {
        public static dynamic EnviarDocumento(String typeDoc, string base64EncodedXml)
        {

            // Encriptar contraseña
            string hashedPassword = General.EncryptPassword(Variables.password);

            dynamic result = null;

            switch (typeDoc)
            {
                case "01":
                    {
                        FacturatechWS.response_ws resultado;
                        var client = new FacturatechWS.SERVICESFACTURATECH();
                        resultado = client.FtechActionuploadInvoiceFile(Variables.username, hashedPassword, base64EncodedXml);
                        //resultado = result;
                        return resultado;
                    }
                case "05":
                    {
                        FacturatechWSdse.uploadResponse resultado;
                        var client = new FacturatechWSdse.DOCUMENTOSOPORTEFACTURATECH();
                        resultado = client.uploadDocument(Variables.username, hashedPassword, base64EncodedXml);
                        //resultado = result;
                        return resultado;
                    }
                case "91":
                    {
                        FacturatechWSNC.uploadResponse resultado;
                        var client = new FacturatechWSNC.SERVICESNOMINAFACTURATECH();
                        resultado = client.FtechActionuploadDocument(Variables.username, hashedPassword, base64EncodedXml);
                        //resultado = result;
                        return resultado;
                    }
                default:
                    {
                        Application.SBO_Application.StatusBar.SetText($"FE: No tiene autorizado este documento para enviar a la DIAN", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        return result;
                        //throw new InvalidOperationException($"Tipo no reconocido: {prueba}");
                    }
            }

            //var serializer = new XmlSerializer(typeof(Nota));
            //using (var writer = new StreamWriter("factura.xml"))
            //{
            //    serializer.Serialize(writer, oDocumento);
            //}

        }

        public static dynamic StatusFacturatech(string codeLog, string transID)
        {
            dynamic result = null;
            try
            {
                Variables.banderaUpdateLog = false;
                SAPbobsCOM.UserTables tbls = null;
                SAPbobsCOM.UserTable tbl = null;
                tbls = Program.SBO_Company.UserTables;
                tbl = tbls.Item("FEDIAN_MONITORLOG");
                tbl.GetByKey(codeLog.ToString());
                string tipoDoc = "", folio = "", prefijo = "", docEntry = "";

                tipoDoc = (string)tbl.UserFields.Fields.Item("U_DocType").Value;
                folio = (string)tbl.UserFields.Fields.Item("U_Folio").Value;
                prefijo = (string)tbl.UserFields.Fields.Item("U_Prefijo").Value;
                docEntry = (string)tbl.UserFields.Fields.Item("U_DocNum").Value;

                // Encriptar contraseña
                string hashedPassword = General.EncryptPassword(Variables.password);

                

                switch (tipoDoc)
                {
                    case "01":
                        {
                            //Consultar estado documento
                            FacturatechWS.response_docs resultadoDoc;
                            var client = new FacturatechWS.SERVICESFACTURATECH();
                            resultadoDoc = client.FtechActiondocumentStatusFile(Variables.username, hashedPassword, transID);
                            //Consultar PDF
                            FacturatechWS.response_pdf resultadoPDF;
                            var clientPDF = new FacturatechWS.SERVICESFACTURATECH();
                            resultadoPDF = clientPDF.FtechActiondownloadPDFFile(Variables.username, hashedPassword, prefijo, folio);
                            //Consultar XML
                            FacturatechWS.response_xml resultadoXML;
                            var clientXML = new FacturatechWS.SERVICESFACTURATECH();
                            resultadoXML = clientXML.FtechActiondownloadXMLFile(Variables.username, hashedPassword, prefijo, folio);
                            Send.saveStatus(codeLog, transID, resultadoDoc, resultadoPDF, resultadoXML);
                            return resultadoDoc;
                        }
                    case "05":
                        {
                            //Consultar estado documento
                            FacturatechWSdse.documentStatusResponse resultadoDoc;
                            var client = new FacturatechWSdse.DOCUMENTOSOPORTEFACTURATECH();
                            resultadoDoc = client.documentStatus(Variables.username, hashedPassword, transID);
                            //Consultar PDF
                            FacturatechWSdse.downloadPDFResponse resultadoPDF;
                            var clientPDF = new FacturatechWSdse.DOCUMENTOSOPORTEFACTURATECH();
                            resultadoPDF = clientPDF.downloadPDF(Variables.username, hashedPassword, prefijo, folio);
                            //Consultar XML
                            FacturatechWSdse.downloadXMLResponse resultadoXML;
                            var clientXML = new FacturatechWSdse.DOCUMENTOSOPORTEFACTURATECH();
                            resultadoXML = clientXML.downloadXML(Variables.username, hashedPassword, prefijo, folio);
                            Send.saveStatus(codeLog, transID, resultadoDoc, resultadoPDF, resultadoXML);
                            return resultadoDoc;
                        }
                    case "91":
                        {
                            //Consultar estado documento
                            FacturatechWSNC.documentStatusResponse resultadoDoc;
                            var client = new FacturatechWSNC.SERVICESNOMINAFACTURATECH();
                            resultadoDoc = client.FtechActiondocumentStatus(Variables.username, hashedPassword, transID);
                            //Consultar PDF
                            FacturatechWSNC.downloadPDFResponse resultadoPDF;
                            var clientPDF = new FacturatechWSNC.SERVICESNOMINAFACTURATECH();
                            resultadoPDF = clientPDF.FtechActiondownloadPDF(Variables.username, hashedPassword, prefijo, folio);
                            //Consultar XML
                            FacturatechWSNC.downloadXMLResponse resultadoXML;
                            var clientXML = new FacturatechWSNC.SERVICESNOMINAFACTURATECH();
                            resultadoXML = clientXML.FtechActiondownloadXML(Variables.username, hashedPassword, prefijo, folio);
                            Send.saveStatus(codeLog, transID, resultadoDoc, resultadoPDF, resultadoXML);
                            return resultadoDoc;
                        }
                    default:
                        {
                            Application.SBO_Application.StatusBar.SetText($"FE: No tiene autorizado este documento para enviar a la DIAN", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            return result;
                            //throw new InvalidOperationException($"Tipo no reconocido: {prueba}");
                        }
                }

            }
            catch (Exception ex)
            {
                Variables.banderaUpdateLog = true;
                Log.EscribirLogFileTXT("StatusCertiCam: " + ex.Message);
                return result;
            }
        }
    }
}

