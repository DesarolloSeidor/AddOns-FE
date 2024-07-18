using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddOnFE_Facturatech.Proveedor.Facturatech;
using AddOnFE_Facturatech.Resources;
using Application = SAPbouiCOM.Framework.Application;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AddOnFE_Facturatech.Proveedor;
using System.Net;

namespace AddOnFE_Facturatech.Methods
{
    class Send
    {
        public static DBResourceExtension dbRE = new DBResourceExtension();
        //validacion de proveedor para envio de informacion
        public static void SendFE(string docentry, string docNum, string prefijo, string codeLog, string typeDoc, Boolean reSend)
        {
            try
            {
                Variables.senalActiva = false;
                string filestr = "";
                string sNumSegui = "";
                string sRequest = "";
                Variables.responseStatus = true;


                if (Variables.proveedor == "FT")
                {
                    SAPbobsCOM.UserTables tbls = null;
                    SAPbobsCOM.UserTable tbl = null;
                    Log.EscribirLogFileTXT("SendFE: DocEntry: " + docentry + " TipoDoc: " + typeDoc);

                    Variables.oDocument = null;
                    dynamic resultDocument = "";
                    string oDocumento = "";

                    switch (typeDoc)
                    {
                        case "01":
                            {
                                Variables.oDocument = (Documents)Program.SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
                                oDocumento = StrXML(docentry, "01", Variables.oDocument);
                                if (oDocumento != "")
                                {
                                    resultDocument = MetodosFacturatech.EnviarDocumento(typeDoc, oDocumento);
                                }
                                break;
                            }
                        case "05":
                            {
                                Variables.oDocument = (Documents)Program.SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);
                                oDocumento = StrXML(docentry, "05", Variables.oDocument);

                                if (oDocumento != "")
                                {
                                    resultDocument = MetodosFacturatech.EnviarDocumento(typeDoc, oDocumento);
                                }
                                break;
                            }
                        case "91":
                            {
                                Variables.oDocument = (Documents)Program.SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oCreditNotes);
                                oDocumento = StrXML(docentry, "91", Variables.oDocument);
                                if (oDocumento != "")
                                {
                                    resultDocument = MetodosFacturatech.EnviarDocumento(typeDoc, oDocumento);
                                }
                                break;
                            }
                        default:
                            {
                                Application.SBO_Application.StatusBar.SetText($"FE: No tiene autorizado este documento para enviar a la DIAN", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                break;
                            }
                    }

                    if (resultDocument != null)
                    {
                        string code = resultDocument.code;
                        string error = resultDocument.error;
                        //string success = resultDocument.success;
                        if (typeDoc == "91")
                        {
                            Variables.transaccionID = resultDocument.transactionID;

                        }
                        else
                        {
                            Variables.transaccionID = resultDocument.transaccionID;

                        }
                        var resultlist = true;
                        Variables.responseStatus = resultlist;

                        if (Variables.banderaUpdateLog == true)
                        {
                            UpdateLogFacturatech(codeLog, resultDocument, oDocumento, reSend, filestr);
                        }

                        //if (resAPIDoc.codigoEstado == "VO")
                        //{
                        //    StatusCertiCam(codeLog);
                        //}
                        //else if (resAPIDoc.listaErrores != null && resAPIDoc.listaErrores[0].codigo == "EP:16101")
                        //{
                        //    StatusCertiCam(codeLog);
                        //}
                    }
                    Utilities.Release(tbl);
                    Utilities.Release(tbls);
                }

                Variables.senalActiva = true;
            }
            catch (Exception ex)
            {
                Variables.senalActiva = true;
                Log.EscribirLogFileTXT("SendFE: " + ex.Message);
            }
        }

        public static string StrXML(string docentry, string typeDocument, Documents oDocument)
        {
            Factura factura = new Factura();
            DocSoporte docSoporte = new DocSoporte();
            Nota nota = new Nota();


            if (oDocument.GetByKey(int.Parse(docentry)))
            {
                #region Encabezado ENC
                ENC enc = new ENC
                {
                    ENC1 = "INVOIC",
                    ENC2 = "901143311",
                    ENC3 = "1061710433",
                    ENC4 = "UBL 2.1",
                    ENC5 = "DIAN 2.1",
                    ENC6 = "TCFA82397",
                    ENC9 = "01",
                    ENC10 = "COP",
                    ENC15 = "3",
                    ENC20 = "2",
                    ENC21 = "10"
                };
                if (typeDocument == "01")
                {
                    factura.ENC = enc;
                }
                if (typeDocument == "05")
                {
                    docSoporte.ENC = enc;
                }
                if (typeDocument == "91")
                {
                    nota.ENC = enc;
                }

                #endregion

                #region Emisor EMI
                EMI emi = new EMI
                {
                    EMI1 = "1",
                    EMI2 = "901143311",
                    EMI3 = "31",
                    EMI6 = "FACTURATECH SA. DE CV",
                    EMI7 = "FACTURATECH SA. DE CV",
                    EMI10 = "Carrera 48",
                    EMI11 = "19",
                    EMI13 = "MEDELLIN",
                    EMI15 = "CO",
                    EMI19 = "Antioquia",
                    EMI22 = "8",
                    EMI23 = "19001",
                    EMI24 = "FACTURATECH SA. DE CV",
                    TAC = new TAC { TAC1 = "R-99-PN" },
                    DFE = new DFE
                    {
                        DFE1 = "19001",
                        DFE2 = "19",
                        DFE3 = "CO",
                        DFE4 = "190003",
                        DFE5 = "Colombia",
                        DFE6 = "CAUCA",
                        DFE7 = "POPAYÁN",
                        DFE8 = "CALLE 5 #38A-13"
                    },
                    ICC = new ICC { ICC1 = "125546877", ICC9 = "TCFA" },
                    CDE = new CDE { CDE1 = "1", CDE2 = "LUIS MIUGUEL GONZALEZ", CDE3 = "3185222474", CDE4 = "null" },
                    GTE = new GTE { GTE1 = "1", GTE2 = "IVA" }
                };
                if (typeDocument == "01")
                {
                    factura.EMI = emi;
                }
                if (typeDocument == "05")
                {
                    docSoporte.EMI = emi;
                }
                if (typeDocument == "91")
                {
                    nota.EMI = emi;
                }
                #endregion

                #region Adquiriente ADQ
                ADQ adq = new ADQ
                {
                    ADQ1 = "2",
                    ADQ2 = "1061710433",
                    ADQ3 = "13",
                    ADQ6 = "Mauricio",
                    ADQ7 = "Restrepo",
                    ADQ10 = "Carrera 48",
                    ADQ11 = "19",
                    ADQ13 = "MEDELLIN",
                    ADQ14 = "Antioquia",
                    ADQ15 = "CO",
                    ADQ19 = "MEDELLIN",
                    ADQ21 = "MEDELLIN",
                    ADQ22 = "1111",
                    ADQ23 = "110111",
                    TCR = new TCR { TCR1 = "R-99-PN" },
                    ILA = new ILA { ILA1 = "11001", ILA2 = "11001", ILA3 = "11001", ILA4 = "11001" },
                    DFA = new DFA { DFA1 = "SABANETA", DFA2 = "SABANETA", DFA3 = "SABANETA", DFA4 = "SABANETA", DFA5 = "SABANETA", DFA6 = "SABANETA", DFA7 = "SABANETA", DFA8 = "SABANETA" },
                    ICR = new ICR { ICR1 = "Gomez" },
                    CDA = new CDA { CDA1 = "1061710433", CDA2 = "1061710433", CDA3 = "1061710433", CDA4 = "1061710433" },
                    GTA = new GTA { GTA1 = "2", GTA2 = "IVA" }
                };
                if (typeDocument == "01")
                {
                    factura.ADQ = adq;
                }
                if (typeDocument == "05")
                {
                    docSoporte.ADQ = adq;
                }
                if (typeDocument == "91")
                {
                    nota.ADQ = adq;
                }
                #endregion

                #region Totales TOT
                TOT tot = new TOT
                {
                    TOT1 = Variables.oDocument.DocTotal.ToString(),
                    TOT2 = "COP",
                    TOT3 = Variables.oDocument.VatSum.ToString(),
                    TOT4 = "COP",
                    TOT5 = Variables.oDocument.WTAmount.ToString(),
                    TOT6 = "COP",
                    TOT7 = Variables.oDocument.TotalDiscount.ToString(),
                    TOT8 = "COP"
                };
                if (typeDocument == "01")
                {
                    factura.TOT = tot;
                }
                if (typeDocument == "05")
                {
                    docSoporte.TOT = tot;
                }
                if (typeDocument == "91")
                {
                    nota.TOT = tot;
                }
                #endregion

                #region TIM
                for (int i = 0; i <= 1; i++) // Cambiado a 1 para mostrar dos instancias de TIM
                {
                    TIM tim1 = new TIM
                    {
                        TIM1 = "false",
                        TIM2 = "313.50",
                        TIM3 = "COP"
                    };
                    tim1.IMPs.Add(new IMP
                    {
                        IMP1 = "01",
                        IMP2 = "1650.00",
                        IMP3 = "COP",
                        IMP4 = "19",
                        IMP5 = "313.50",
                        IMP6 = "COP"
                    });

                    if (typeDocument == "01")
                    {
                        factura.TIMs.Add(tim1);
                    }
                    if (typeDocument == "05")
                    {
                        docSoporte.TIMs.Add(tim1);
                    }
                    if (typeDocument == "91")
                    {
                        nota.TIMs.Add(tim1);
                    }
                }
                #endregion

                #region DRF
                DRF drf = new DRF
                {
                    DRF1 = "Fecha",
                    DRF2 = "yyyy-MM-dd",
                    DRF3 = "2022-02-21",
                    DRF4 = "2022-02-21",
                    DRF5 = "2022-02-21",
                    DRF6 = "2022-02-21"
                };
                //oDocumento.DRF = drf;
                if (typeDocument == "01")
                {
                    factura.DRF = drf;
                }
                if (typeDocument == "05")
                {
                    docSoporte.DRF = drf;
                }
                if (typeDocument == "91")
                {
                    nota.DRF = drf;
                }
                #endregion

                #region MEP
                MEP mep = new MEP
                {
                    MEP1 = "10",
                    MEP2 = "true",
                    MEP3 = "03"
                };
                //oDocumento.MEP = mep;
                if (typeDocument == "01")
                {
                    factura.MEP = mep;
                }
                if (typeDocument == "05")
                {
                    docSoporte.MEP = mep;
                }
                if (typeDocument == "91")
                {
                    nota.MEP = mep;
                }
                #endregion

                #region Items ITE
                for (int j = 0; j <= 1; j++) // Cambiado a 1 para mostrar dos instancias de TIM
                {
                    ITE ite = new ITE
                    {
                        ITE1 = "1",
                        ITE3 = "01",
                        ITE4 = "test",
                        ITE5 = "1",
                        ITE6 = "test",
                        ITE7 = "test",
                        ITE8 = "test",
                        ITE11 = "test",
                        ITE20 = "test",
                        ITE21 = "test",
                        ITE24 = "test",
                        ITE27 = "test",
                        ITE28 = "test",
                    };
                    ite.IAE = (new IAE
                    {
                        IAE1 = "test",
                        IAE2 = "test"
                    });
                    ite.IDE = (new IDE
                    {
                        IDE1 = "test",
                        IDE2 = "test",
                        IDE3 = "test",
                        IDE6 = "test",
                        IDE7 = "test",
                        IDE8 = "test",
                        IDE10 = "test"
                    });
                    for (int h = 0; h <= 1; h++)
                    {
                        ite.TIIs.Add(new TII
                        {
                            TII1 = "test",
                            TII2 = "test",
                            TII3 = "test",
                            IIM = new IIM
                            {
                                IIM1 = "test",
                                IIM2 = "test",
                                IIM3 = "test",
                                IIM4 = "test",
                                IIM5 = "test",
                                IIM6 = "test"
                            }
                        });
                    }
                    //oDocumento.ITEs.Add(ite);
                    if (typeDocument == "01")
                    {
                        factura.ITEs.Add(ite);
                    }
                    if (typeDocument == "05")
                    {
                        docSoporte.ITEs.Add(ite);
                    }
                    if (typeDocument == "91")
                    {
                        nota.ITEs.Add(ite);
                    }
                }
                #endregion

                string xmlString = "";
                // Serializar el objeto a una cadena XML
                if (typeDocument == "01")
                {
                    xmlString = General.SerializeObjectToXml(factura);
                }
                if (typeDocument == "05")
                {
                    xmlString = General.SerializeObjectToXml(docSoporte);
                }
                if (typeDocument == "91")
                {
                    xmlString = General.SerializeObjectToXml(nota);
                }
                //string xmlString = General.SerializeObjectToXml(oDocumento);
                // Convertir la cadena XML a una cadena Base64
                string base64EncodedXml = General.ConvertToBase64(xmlString);
                return base64EncodedXml;
            }

            Application.SBO_Application.StatusBar.SetText($"FE: El documento no existe", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

            return "";

        }

        //Actualizacion Log despues de envio a Facturatech
        public static void UpdateLogFacturatech(string codeline, dynamic response, string srequest, Boolean reSend, string textstr)
        {
            try
            {
                Variables.banderaUpdateLog = false;
                UserTables tbls = null;
                UserTable tbl = null;
                tbls = Program.SBO_Company.UserTables;
                tbl = tbls.Item("FEDIAN_MONITORLOG");

                tbl.GetByKey(codeline.ToString());

                int nReenvios = string.IsNullOrEmpty((string)tbl.UserFields.Fields.Item("U_NReenvios").Value) ? 0 : Convert.ToInt32(tbl.UserFields.Fields.Item("U_NReenvios").Value);
                nReenvios = reSend ? nReenvios + 1 : nReenvios;
                tbl.UserFields.Fields.Item("U_NReenvios").Value = nReenvios.ToString();

                if (response.code == "201")
                {
                    tbl.UserFields.Fields.Item("U_Status").Value = response.code;
                    tbl.UserFields.Fields.Item("U_Resultado").Value = response.success;
                    tbl.UserFields.Fields.Item("U_ID_Seguimiento").Value = Variables.transaccionID;
                }
                if (response.code == "200")
                {
                    tbl.UserFields.Fields.Item("U_Status").Value = response.code;
                    tbl.UserFields.Fields.Item("U_Resultado").Value = "Validacion OK";
                    tbl.UserFields.Fields.Item("U_ID_Seguimiento").Value = Variables.transaccionID;
                }
                else if (response.code == "400")
                {
                    tbl.UserFields.Fields.Item("U_Resultado").Value = response.error;
                    tbl.UserFields.Fields.Item("U_Status").Value = response.code;
                }
                else
                {
                    tbl.UserFields.Fields.Item("U_Resultado").Value = response.error;
                    tbl.UserFields.Fields.Item("U_Status").Value = response.code;
                }
                //if (srequest != "")
                //{
                //    XmlDocument doc = JsonConvert.DeserializeXmlNode(srequest, "root"); //JsonConvert.DeserializeXmlNode(srequest);

                tbl.UserFields.Fields.Item("U_Det_Peticion").Value = srequest;
                //}

                //if (responseStatus != "")
                //{
                //    XmlDocument docresponse = (XmlDocument)JsonConvert.DeserializeXmlNode(responseStatus, "root");
                tbl.UserFields.Fields.Item("U_Respuesta_Int").Value = Variables.responseStatus.ToString();
                //}



                Variables.lRetCode = tbl.Update();

                if (Variables.lRetCode != 0)
                {
                    Program.SBO_Company.GetLastError(out Variables.lRetCode, out Variables.sErrMsg);
                    Log.EscribirLogFileTXT("updateLog: " + Variables.sErrMsg);
                }
                Variables.banderaUpdateLog = true;
                Utilities.Release(tbl);
                Utilities.Release(tbls);
            }
            catch (Exception ex)
            {
                Variables.banderaUpdateLog = true;
                Log.EscribirLogFileTXT("updateLog: " + ex.Message);
            }
        }

        //public static string StrJson(string docEntry, string typeDoc)
        //{
        //    try
        //    {

        //        string eInvoiceJson = "", Inv = "", InvTax = "";
        //        Factura curInv = new Factura();
        //        //Documentos.InvoiceLine curInvLine = new Documentos.InvoiceLine();

        //        SAPbobsCOM.Recordset oRS_Inv = ((SAPbobsCOM.Recordset)(Program.SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));

        //        switch (typeDoc)
        //        {
        //            case "01":
        //                Inv = string.Format(dbRE.GetSQL("GetInvoice.sql"), docEntry);
        //                InvTax = string.Format(dbRE.GetSQL("InvoiceTaxesTotal.sql"), docEntry);
        //                break;
        //            case "02":
        //                Inv = string.Format(dbRE.GetSQL("GetInvoice.sql"), docEntry);
        //                InvTax = string.Format(dbRE.GetSQL("InvoiceTaxesTotal.sql"), docEntry);
        //                break;
        //            case "03":
        //                Inv = string.Format(dbRE.GetSQL("GetInvoice.sql"), docEntry);
        //                InvTax = string.Format(dbRE.GetSQL("InvoiceTaxesTotal.sql"), docEntry);
        //                break;
        //            case "05":
        //                Inv = string.Format(dbRE.GetSQL("GetPurchaseInvoice.sql"), docEntry);
        //                InvTax = string.Format(dbRE.GetSQL("PurchaseInvoiceTaxesTotal.sql"), docEntry);
        //                break;
        //            case "91":
        //                Inv = string.Format(dbRE.GetSQL("GetCreditNote.sql"), docEntry);
        //                InvTax = string.Format(dbRE.GetSQL("CreditNoteTaxesTotal.sql"), docEntry);
        //                break;
        //            case "92":
        //                Inv = string.Format(dbRE.GetSQL("GetInvoice.sql"), docEntry);
        //                InvTax = string.Format(dbRE.GetSQL("InvoiceTaxesTotal.sql"), docEntry);
        //                break;
        //        }
        //        oRS_Inv.DoQuery(Inv);

        //        if (oRS_Inv.RecordCount > 0)
        //        {
        //            #region Encabezado ENC
        //            ENC enc = new ENC
        //            {
        //                ENC1 = oRS_Inv.Fields.Item(0).Value.ToString(),
        //                ENC2 = "901143311",
        //                ENC3 = "1061710433",
        //                ENC4 = "UBL 2.1",
        //                ENC5 = "DIAN 2.1",
        //                ENC6 = "TCFA82397",
        //                ENC9 = "01",
        //                ENC10 = "COP",
        //                ENC15 = "3",
        //                ENC20 = "2",
        //                ENC21 = "10"
        //            };
        //            curInv.ENC = enc;
        //            #endregion

        //            curInv.tipoDocumento = oRS_Inv.Fields.Item(0).Value.ToString();
        //            curInv.versionDocumento = oRS_Inv.Fields.Item(1).Value.ToString();
        //            curInv.registrar = bool.Parse(oRS_Inv.Fields.Item(2).Value);
        //            curInv.control = oRS_Inv.Fields.Item(3).Value.ToString();
        //            curInv.codigoTipoDocumento = oRS_Inv.Fields.Item(4).Value.ToString();
        //            curInv.tipoOperacion = oRS_Inv.Fields.Item(5).Value.ToString();
        //            curInv.prefijoDocumento = oRS_Inv.Fields.Item(6).Value.ToString();
        //            curInv.numeroDocumento = Int32.Parse(oRS_Inv.Fields.Item(7).Value.ToString());
        //            curInv.fechaEmision = oRS_Inv.Fields.Item(8).Value.ToString();
        //            curInv.horaEmision = oRS_Inv.Fields.Item(9).Value.ToString();
        //            if (typeDoc == "91")
        //            {
        //                Documentos.periodoFacturacion operiodoFacturacion = new Documentos.periodoFacturacion();
        //                operiodoFacturacion.fechaInicio = oRS_Inv.Fields.Item(114).Value.ToString();
        //                operiodoFacturacion.fechaFin = oRS_Inv.Fields.Item(115).Value.ToString();
        //                curInv.periodoFacturacion = operiodoFacturacion;
        //            }
        //            curInv.numeroLineas = Int32.Parse(oRS_Inv.Fields.Item(10).Value.ToString());
        //            curInv.subtotal = decimal.Parse(oRS_Inv.Fields.Item(11).Value.ToString("0.0000"));
        //            curInv.totalBaseImponible = decimal.Parse(oRS_Inv.Fields.Item(12).Value.ToString("0.0000"));
        //            curInv.subtotalMasTributos = decimal.Parse(oRS_Inv.Fields.Item(13).Value.ToString("0.0000"));
        //            curInv.totalDescuentos = decimal.Parse(oRS_Inv.Fields.Item(14).Value.ToString("0.0000"));
        //            curInv.total = decimal.Parse(oRS_Inv.Fields.Item(15).Value.ToString("0.0000"));
        //            curInv.codigoMoneda = oRS_Inv.Fields.Item(16).Value.ToString();

        //            if (curInv.codigoMoneda != "COP")
        //            {
        //                Documentos.TasaCambio oTasaCambio = new Documentos.TasaCambio();

        //                oTasaCambio.fechaCambio = oRS_Inv.Fields.Item(17).Value.ToString();
        //                oTasaCambio.codigoMonedaFacturado = oRS_Inv.Fields.Item(18).Value.ToString();
        //                oTasaCambio.codigoMonedaCambio = oRS_Inv.Fields.Item(19).Value.ToString();
        //                oTasaCambio.baseCambioFacturado = decimal.Parse(oRS_Inv.Fields.Item(20).Value.ToString("0.0000"));
        //                oTasaCambio.baseCambio = decimal.Parse(oRS_Inv.Fields.Item(21).Value.ToString("0.0000"));
        //                oTasaCambio.trm = decimal.Parse(oRS_Inv.Fields.Item(22).Value.ToString("0.0000"));

        //                curInv.tasaCambio = oTasaCambio;
        //            }

        //            Documentos.Pago oPago = new Documentos.Pago();

        //            oPago.id = 1;
        //            oPago.codigoMedioPago = oRS_Inv.Fields.Item(23).Value.ToString();
        //            oPago.fechaVencimiento = oRS_Inv.Fields.Item(24).Value.ToString();

        //            curInv.pago = oPago;

        //            if (decimal.Parse(oRS_Inv.Fields.Item(49).Value.ToString("0.0000")) > 0)
        //            {
        //                curInv.listaCargosDescuentos = new List<Documentos.CargosDescuentos>();
        //                var oDescCab = new Documentos.CargosDescuentos();
        //                oDescCab.id = 1;
        //                oDescCab.esCargo = false;
        //                oDescCab.codigo = oRS_Inv.Fields.Item(46).Value.ToString();
        //                oDescCab.razon = oRS_Inv.Fields.Item(47).Value.ToString();
        //                oDescCab.@base = decimal.Parse(oRS_Inv.Fields.Item(48).Value.ToString("0.0000"));
        //                oDescCab.porcentaje = decimal.Parse(oRS_Inv.Fields.Item(49).Value.ToString("0.0000"));
        //                oDescCab.valor = decimal.Parse(oRS_Inv.Fields.Item(50).Value.ToString("0.0000"));

        //                curInv.listaCargosDescuentos.Add(oDescCab);
        //            }

        //            curInv.facturador = new Documentos.Facturador();
        //            var oFacturador = new Documentos.Facturador();
        //            oFacturador.razonSocial = oRS_Inv.Fields.Item(51).Value.ToString();
        //            oFacturador.nombreRegistrado = oRS_Inv.Fields.Item(52).Value.ToString();
        //            oFacturador.tipoIdentificacion = oRS_Inv.Fields.Item(53).Value.ToString();
        //            oFacturador.identificacion = oRS_Inv.Fields.Item(54).Value.ToString();
        //            oFacturador.digitoVerificacion = oRS_Inv.Fields.Item(55).Value.ToString();
        //            oFacturador.naturaleza = oRS_Inv.Fields.Item(56).Value.ToString();
        //            oFacturador.codigoRegimen = oRS_Inv.Fields.Item(57).Value.ToString();
        //            oFacturador.responsabilidadFiscal = oRS_Inv.Fields.Item(58).Value.ToString();
        //            oFacturador.codigoImpuesto = oRS_Inv.Fields.Item(59).Value.ToString();
        //            oFacturador.nombreImpuesto = oRS_Inv.Fields.Item(60).Value.ToString();
        //            oFacturador.telefono = oRS_Inv.Fields.Item(61).Value.ToString();
        //            oFacturador.email = oRS_Inv.Fields.Item(62).Value.ToString();

        //            CompanyService oCompanyService;
        //            AdminInfo oCompanyAdminInfo;
        //            oCompanyService = oCompany.GetCompanyService();
        //            oCompanyAdminInfo = oCompanyService.GetAdminInfo();

        //            Recordset RS_Tribu = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
        //            string sSQL = "";
        //            sSQL = "Select \"U_Codigo\", \"U_Desc\" From \"@FEDIAN_SNTRI\" Where \"Code\" = '" + oCompanyAdminInfo.FederalTaxID + "'" +
        //                   " And \"U_Codigo\" != '" + oFacturador.codigoImpuesto + "'";
        //            RS_Tribu.DoQuery(sSQL);

        //            if (RS_Tribu.RecordCount > 0)
        //            {
        //                oFacturador.listaResponsabilidadesTributarias = new List<Documentos.ResponTribu>();
        //                while (!RS_Tribu.EoF)
        //                {
        //                    var oTributos = new Documentos.ResponTribu();
        //                    oTributos.codigo = RS_Tribu.Fields.Item(0).Value.ToString();
        //                    oTributos.nombre = RS_Tribu.Fields.Item(1).Value.ToString();
        //                    oFacturador.listaResponsabilidadesTributarias.Add(oTributos);
        //                    RS_Tribu.MoveNext();
        //                }
        //            }
        //            Utilities.Release(RS_Tribu);
        //            Utilities.Release(oCompanyService);
        //            Utilities.Release(oCompanyAdminInfo);

        //            curInv.facturador.direccion = new Documentos.dirección();
        //            curInv.facturador.direccionFiscal = new Documentos.dirección();
        //            var oDireccion = new Documentos.dirección();
        //            oDireccion.codigoPais = oRS_Inv.Fields.Item(63).Value.ToString();
        //            oDireccion.nombrePais = oRS_Inv.Fields.Item(64).Value.ToString();
        //            oDireccion.codigoLenguajePais = oRS_Inv.Fields.Item(65).Value.ToString();
        //            oDireccion.codigoDepartamento = oRS_Inv.Fields.Item(66).Value.ToString();
        //            oDireccion.nombreDepartamento = oRS_Inv.Fields.Item(67).Value.ToString();
        //            oDireccion.codigoCiudad = oRS_Inv.Fields.Item(68).Value.ToString();
        //            oDireccion.nombreCiudad = oRS_Inv.Fields.Item(69).Value.ToString();
        //            oDireccion.direccionFisica = oRS_Inv.Fields.Item(70).Value.ToString();
        //            oDireccion.codigoPostal = oRS_Inv.Fields.Item(71).Value.ToString();

        //            oFacturador.direccion = oDireccion;
        //            oFacturador.direccionFiscal = oDireccion;

        //            curInv.facturador = oFacturador;

        //            curInv.adquiriente = new Documentos.Adquiriente();
        //            var oAdquirente = new Documentos.Adquiriente();
        //            oAdquirente.razonSocial = oRS_Inv.Fields.Item(72).Value.ToString();
        //            oAdquirente.nombreRegistrado = oRS_Inv.Fields.Item(73).Value.ToString();
        //            oAdquirente.tipoIdentificacion = oRS_Inv.Fields.Item(74).Value.ToString();
        //            oAdquirente.identificacion = oRS_Inv.Fields.Item(75).Value.ToString();
        //            oAdquirente.digitoVerificacion = oRS_Inv.Fields.Item(76).Value.ToString();
        //            oAdquirente.naturaleza = oRS_Inv.Fields.Item(77).Value.ToString();
        //            oAdquirente.codigoRegimen = oRS_Inv.Fields.Item(78).Value.ToString();
        //            oAdquirente.responsabilidadFiscal = oRS_Inv.Fields.Item(79).Value.ToString();
        //            oAdquirente.codigoImpuesto = oRS_Inv.Fields.Item(80).Value.ToString();
        //            oAdquirente.nombreImpuesto = oRS_Inv.Fields.Item(81).Value.ToString();
        //            oAdquirente.telefono = oRS_Inv.Fields.Item(82).Value.ToString();
        //            oAdquirente.email = oRS_Inv.Fields.Item(83).Value.ToString();


        //            Documents oInvoice;
        //            oInvoice = oCompany.GetBusinessObject(BoObjectTypes.oInvoices);
        //            oInvoice.GetByKey(Int32.Parse(docEntry));

        //            Recordset RS_TribAdq = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
        //            sSQL = "Select \"U_Codigo\", \"U_Desc\" From \"@FEDIAN_SNTRI\" Where \"Code\" = '" + oInvoice.CardCode + "'" +
        //                   " And \"U_Codigo\" != '" + oAdquirente.codigoImpuesto + "'";
        //            RS_TribAdq.DoQuery(sSQL);

        //            if (RS_TribAdq.RecordCount > 0)
        //            {
        //                oAdquirente.listaResponsabilidadesTributarias = new List<Documentos.ResponTribu>();
        //                while (!RS_TribAdq.EoF)
        //                {
        //                    var oTributosAdq = new Documentos.ResponTribu();
        //                    oTributosAdq.codigo = RS_TribAdq.Fields.Item(0).Value.ToString();
        //                    oTributosAdq.nombre = RS_TribAdq.Fields.Item(1).Value.ToString();
        //                    oAdquirente.listaResponsabilidadesTributarias.Add(oTributosAdq);
        //                    RS_TribAdq.MoveNext();
        //                }
        //            }
        //            Utilities.Release(RS_TribAdq);
        //            Utilities.Release(oInvoice);

        //            curInv.adquiriente.direccion = new Documentos.dirección();
        //            curInv.adquiriente.direccionFiscal = new Documentos.dirección();
        //            var oDirAdq = new Documentos.dirección();
        //            oDirAdq.codigoPais = oRS_Inv.Fields.Item(84).Value.ToString();
        //            oDirAdq.nombrePais = oRS_Inv.Fields.Item(85).Value.ToString();
        //            oDirAdq.codigoLenguajePais = oRS_Inv.Fields.Item(86).Value.ToString();
        //            oDirAdq.codigoDepartamento = oRS_Inv.Fields.Item(87).Value.ToString();
        //            oDirAdq.nombreDepartamento = oRS_Inv.Fields.Item(88).Value.ToString();
        //            oDirAdq.codigoCiudad = oRS_Inv.Fields.Item(89).Value.ToString();
        //            oDirAdq.nombreCiudad = oRS_Inv.Fields.Item(90).Value.ToString();
        //            oDirAdq.direccionFisica = oRS_Inv.Fields.Item(91).Value.ToString();
        //            oDirAdq.codigoPostal = oRS_Inv.Fields.Item(92).Value.ToString();

        //            oAdquirente.direccion = oDirAdq;
        //            oAdquirente.direccionFiscal = oDirAdq;

        //            curInv.adquiriente = oAdquirente;

        //            if (typeDoc == "01" || typeDoc == "05")
        //            {
        //                curInv.resolucion = new Documentos.Resolucion();
        //                var oResolucion = new Documentos.Resolucion();
        //                oResolucion.numero = oRS_Inv.Fields.Item(93).Value.ToString();
        //                oResolucion.fechaInicio = oRS_Inv.Fields.Item(94).Value.ToString();
        //                oResolucion.fechaFin = oRS_Inv.Fields.Item(95).Value.ToString();

        //                var oNumeracion = new Documentos.Numeracion();
        //                oNumeracion.prefijo = oRS_Inv.Fields.Item(96).Value.ToString();
        //                oNumeracion.desde = Int32.Parse(oRS_Inv.Fields.Item(97).Value.ToString());
        //                oNumeracion.hasta = Int32.Parse(oRS_Inv.Fields.Item(98).Value.ToString());
        //                oNumeracion.fechaInicio = oRS_Inv.Fields.Item(94).Value.ToString();
        //                oNumeracion.fechaFin = oRS_Inv.Fields.Item(95).Value.ToString();
        //                oResolucion.numeracion = oNumeracion;

        //                curInv.resolucion = oResolucion;
        //            }


        //            curInv.cvcc = oRS_Inv.Fields.Item(99).Value.ToString();

        //            curInv.posicionXCufe = oRS_Inv.Fields.Item(101).Value.ToString();
        //            curInv.posicionYCufe = oRS_Inv.Fields.Item(102).Value.ToString();
        //            curInv.rotacionCufe = oRS_Inv.Fields.Item(103).Value.ToString();
        //            curInv.fuenteCufe = oRS_Inv.Fields.Item(104).Value.ToString();
        //            curInv.posicionXQr = oRS_Inv.Fields.Item(105).Value.ToString();
        //            curInv.posicionYQr = oRS_Inv.Fields.Item(106).Value.ToString();

        //            if (typeDoc == "91" && !string.IsNullOrEmpty(oRS_Inv.Fields.Item(107).Value.ToString()))
        //            {
        //                curInv.listaDocumentosReferenciados = new List<Documentos.DocRef>();
        //                var documentRef = new Documentos.DocRef();
        //                documentRef.id = oRS_Inv.Fields.Item(107).Value.ToString();
        //                documentRef.tipo = oRS_Inv.Fields.Item(108).Value.ToString();
        //                documentRef.fecha = oRS_Inv.Fields.Item(109).Value.ToString();
        //                documentRef.algoritmo = oRS_Inv.Fields.Item(110).Value.ToString();
        //                documentRef.cufe = oRS_Inv.Fields.Item(111).Value.ToString();

        //                curInv.listaDocumentosReferenciados.Add(documentRef);
        //            }

        //            if (typeDoc == "91" && !string.IsNullOrEmpty(oRS_Inv.Fields.Item(112).Value.ToString()))
        //            {
        //                curInv.listaCorrecciones = new List<Documentos.listCorrec>();
        //                var listaCorrecciones = new Documentos.listCorrec();
        //                listaCorrecciones.id = 1;
        //                listaCorrecciones.codigo = oRS_Inv.Fields.Item(112).Value.ToString();
        //                listaCorrecciones.descripcion = oRS_Inv.Fields.Item(113).Value.ToString();

        //                curInv.listaCorrecciones.Add(listaCorrecciones);
        //            }

        //            if ((typeDoc == "01" || typeDoc == "05") && !string.IsNullOrEmpty(oRS_Inv.Fields.Item(107).Value.ToString()))
        //            {
        //                curInv.documentosAnexos = new List<Documentos.listaAnexos>();
        //                var oAnexos = new Documentos.listaAnexos();
        //                oAnexos.id = oRS_Inv.Fields.Item(107).Value.ToString();
        //                oAnexos.tipo = "OR";
        //                curInv.documentosAnexos.Add(oAnexos);
        //            }


        //            curInv.listaProductos = new List<Documentos.InvoiceLine>();
        //            while (!oRS_Inv.EoF)
        //            {
        //                curInvLine = new Documentos.InvoiceLine();

        //                curInvLine.numeroLinea = Int32.Parse(oRS_Inv.Fields.Item(25).Value.ToString());
        //                curInvLine.cantidad = decimal.Parse(oRS_Inv.Fields.Item(26).Value.ToString("0.0000"));
        //                curInvLine.valorTotal = decimal.Parse(oRS_Inv.Fields.Item(27).Value.ToString("0.0000"));
        //                curInvLine.idProducto = oRS_Inv.Fields.Item(28).Value.ToString();
        //                curInvLine.codigoPrecio = oRS_Inv.Fields.Item(29).Value.ToString();

        //                curInvLine.valorUnitario = decimal.Parse(oRS_Inv.Fields.Item(30).Value.ToString("0.0000"));
        //                curInvLine.cantidadReal = decimal.Parse(oRS_Inv.Fields.Item(31).Value.ToString("0.0000"));
        //                curInvLine.codigoUnidad = oRS_Inv.Fields.Item(32).Value.ToString();
        //                curInvLine.esMuestraComercial = bool.Parse(oRS_Inv.Fields.Item(33).Value.ToString());

        //                var oItemLin = new Documentos.InvoiceLine.Item();
        //                oItemLin.codigoEstandar = oRS_Inv.Fields.Item(100).Value.ToString();
        //                oItemLin.descripcion = oRS_Inv.Fields.Item(34).Value.ToString();
        //                curInvLine.item = oItemLin;

        //                if (decimal.Parse(oRS_Inv.Fields.Item(38).Value.ToString("0.0000")) > 0)
        //                {
        //                    curInvLine.listaCargosDescuentos = new List<Documentos.CargosDescuentos>();
        //                    var oDescLinea = new Documentos.CargosDescuentos();
        //                    oDescLinea.id = 1;
        //                    oDescLinea.esCargo = false;
        //                    oDescLinea.codigo = oRS_Inv.Fields.Item(35).Value.ToString();
        //                    oDescLinea.razon = oRS_Inv.Fields.Item(36).Value.ToString();
        //                    oDescLinea.@base = decimal.Parse(oRS_Inv.Fields.Item(37).Value.ToString("0.0000"));
        //                    oDescLinea.porcentaje = decimal.Parse(oRS_Inv.Fields.Item(38).Value.ToString("0.0000"));
        //                    oDescLinea.valor = decimal.Parse(oRS_Inv.Fields.Item(39).Value.ToString("0.0000"));

        //                    curInvLine.listaCargosDescuentos.Add(oDescLinea);
        //                }

        //                if (oRS_Inv.Fields.Item(40).Value.ToString() != "ZZ")
        //                {
        //                    curInvLine.listaImpuestos = new List<Documentos.InvoiceTax>();

        //                    var oTaxLine = new Documentos.InvoiceTax();
        //                    oTaxLine.codigo = oRS_Inv.Fields.Item(40).Value.ToString();
        //                    oTaxLine.nombre = oRS_Inv.Fields.Item(41).Value.ToString();
        //                    oTaxLine.baseGravable = decimal.Parse(oRS_Inv.Fields.Item(42).Value.ToString("0.0000"));
        //                    oTaxLine.porcentaje = decimal.Parse(oRS_Inv.Fields.Item(43).Value.ToString("0.0000"));
        //                    oTaxLine.valor = decimal.Parse(oRS_Inv.Fields.Item(44).Value.ToString("0.0000"));
        //                    oTaxLine.codigoUnidad = oRS_Inv.Fields.Item(45).Value.ToString();

        //                    curInvLine.listaImpuestos.Add(oTaxLine);
        //                }


        //                curInv.listaProductos.Add(curInvLine);
        //                oRS_Inv.MoveNext();
        //            }
        //        }

        //        Utilities.Release(oRS_Inv);

        //        Recordset oRS_InvTax = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
        //        oRS_InvTax.DoQuery(InvTax);

        //        if (oRS_InvTax.RecordCount > 0)
        //        {
        //            List<Documentos.GrupoImpuestos> oImpuesto = null;
        //            List<Documentos.GrupoDeducciones> oRetencion = null;
        //            curInv.gruposImpuestos = new List<Documentos.GrupoImpuestos>();
        //            if (typeDoc == "01" || typeDoc == "05")
        //            {
        //                curInv.gruposDeducciones = new List<Documentos.GrupoDeducciones>();
        //            }

        //            Documentos.GrupoImpuestos grupoImp = null;
        //            Documentos.GrupoDeducciones grupoRet = null;

        //            while (!oRS_InvTax.EoF)
        //            {
        //                if (oRS_InvTax.Fields.Item(0).Value.ToString() == "false")
        //                {
        //                    if (oImpuesto == null) oImpuesto = new List<Documentos.GrupoImpuestos>();

        //                    if (!oImpuesto.Any(imp => imp.codigo == oRS_InvTax.Fields.Item(1).Value.ToString()))
        //                    {
        //                        grupoImp = new Documentos.GrupoImpuestos();
        //                        if (grupoImp.listaImpuestos == null) grupoImp.listaImpuestos = new List<Documentos.InvoiceTax>();

        //                        grupoImp.codigo = oRS_InvTax.Fields.Item(1).Value.ToString();
        //                        grupoImp.total = decimal.Parse(oRS_InvTax.Fields.Item(2).Value.ToString("0.0000"));

        //                        var oListImp = new Documentos.InvoiceTax();
        //                        oListImp.codigo = oRS_InvTax.Fields.Item(3).Value.ToString();
        //                        oListImp.nombre = oRS_InvTax.Fields.Item(4).Value.ToString();
        //                        oListImp.baseGravable = decimal.Parse(oRS_InvTax.Fields.Item(5).Value.ToString("0.0000"));
        //                        oListImp.porcentaje = decimal.Parse(oRS_InvTax.Fields.Item(6).Value.ToString("0.0000"));
        //                        oListImp.valor = decimal.Parse(oRS_InvTax.Fields.Item(7).Value.ToString("0.0000"));
        //                        oListImp.codigoUnidad = oRS_InvTax.Fields.Item(8).Value.ToString();

        //                        grupoImp.listaImpuestos.Add(oListImp);

        //                        oImpuesto.Add(grupoImp);
        //                    }
        //                    else
        //                    {
        //                        var oListImp = new Documentos.InvoiceTax();
        //                        oListImp.codigo = oRS_InvTax.Fields.Item(3).Value.ToString();
        //                        oListImp.nombre = oRS_InvTax.Fields.Item(4).Value.ToString();
        //                        oListImp.baseGravable = decimal.Parse(oRS_InvTax.Fields.Item(5).Value.ToString("0.0000"));
        //                        oListImp.porcentaje = decimal.Parse(oRS_InvTax.Fields.Item(6).Value.ToString("0.0000"));
        //                        oListImp.valor = decimal.Parse(oRS_InvTax.Fields.Item(7).Value.ToString("0.0000"));
        //                        oListImp.codigoUnidad = oRS_InvTax.Fields.Item(8).Value.ToString();

        //                        Documentos.GrupoImpuestos product = oImpuesto.Where(p => p.codigo == oRS_InvTax.Fields.Item(3).Value.ToString()).FirstOrDefault();
        //                        product.listaImpuestos.Add(oListImp);
        //                    }
        //                }
        //                else if ((typeDoc == "01" || typeDoc == "05") && oRS_InvTax.Fields.Item(0).Value.ToString() == "true")
        //                {
        //                    if (oRetencion == null) oRetencion = new List<Documentos.GrupoDeducciones>();

        //                    if (!oRetencion.Any(imp => imp.codigo == oRS_InvTax.Fields.Item(1).Value.ToString()))
        //                    {
        //                        grupoRet = new Documentos.GrupoDeducciones();
        //                        if (grupoRet.listaDeducciones == null) grupoRet.listaDeducciones = new List<Documentos.InvoiceTax>();

        //                        grupoRet.codigo = oRS_InvTax.Fields.Item(1).Value.ToString();
        //                        grupoRet.total = decimal.Parse(oRS_InvTax.Fields.Item(2).Value.ToString("0.0000"));

        //                        var oListRet = new Documentos.InvoiceTax();
        //                        oListRet.codigo = oRS_InvTax.Fields.Item(3).Value.ToString();
        //                        oListRet.nombre = oRS_InvTax.Fields.Item(4).Value.ToString();
        //                        oListRet.baseGravable = decimal.Parse(oRS_InvTax.Fields.Item(5).Value.ToString("0.0000"));
        //                        oListRet.porcentaje = decimal.Parse(oRS_InvTax.Fields.Item(6).Value.ToString("0.0000"));
        //                        oListRet.valor = decimal.Parse(oRS_InvTax.Fields.Item(7).Value.ToString("0.0000"));

        //                        grupoRet.listaDeducciones.Add(oListRet);

        //                        oRetencion.Add(grupoRet);
        //                    }
        //                    else
        //                    {
        //                        var oListRet = new Documentos.InvoiceTax();
        //                        oListRet.codigo = oRS_InvTax.Fields.Item(3).Value.ToString();
        //                        oListRet.nombre = oRS_InvTax.Fields.Item(4).Value.ToString();
        //                        oListRet.baseGravable = decimal.Parse(oRS_InvTax.Fields.Item(5).Value.ToString("0.0000"));
        //                        oListRet.porcentaje = decimal.Parse(oRS_InvTax.Fields.Item(6).Value.ToString("0.0000"));
        //                        oListRet.valor = decimal.Parse(oRS_InvTax.Fields.Item(7).Value.ToString("0.0000"));

        //                        Documentos.GrupoDeducciones product = oRetencion.Where(p => p.codigo == oRS_InvTax.Fields.Item(3).Value.ToString()).FirstOrDefault();
        //                        product.listaDeducciones.Add(oListRet);
        //                    }
        //                }
        //                oRS_InvTax.MoveNext();
        //            }
        //            if (oImpuesto != null)
        //            {
        //                curInv.gruposImpuestos = oImpuesto;
        //            }
        //            if (oRetencion != null)
        //            {

        //                curInv.gruposDeducciones = oRetencion;
        //            }
        //        }

        //        Utilities.Release(oRS_InvTax);

        //        Variables.base64 = General.ExportarPDF(typeDoc, docEntry, "");

        //        eInvoiceJson = JsonConvert.SerializeObject(curInv, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        //        byte[] encodedBytes = Encoding.UTF8.GetBytes(eInvoiceJson);
        //        Encoding.Convert(Encoding.UTF8, Encoding.Unicode, encodedBytes);
        //        string utfString = Encoding.UTF8.GetString(encodedBytes, 0, encodedBytes.Length);

        //        return utfString;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.EscribirLogFileTXT("strJson: " + ex.Message);
        //        return "";
        //    }
        //}

        //Verificar estado archivos enviados (Timer)
        public static void Verifystatus()
        {
            try
            {
                Recordset oRS = null;
                if (oRS != null) // Not sure why this is needed as rs will always be null but leaving it in anyway
                {
                    Utilities.Release(oRS);
                    oRS = null;
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                oRS = (Recordset)Program.SBO_Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                string sSql = string.Format(Querys.Default.ProcessStatus, "'" + String.Join("'" + ",'", Constants.yellow.ToArray()) + "'");
                oRS.DoQuery(sSql);

                if (oRS.RecordCount > 0)
                {
                    Log.EscribirLogFileTXT("verifystatus: " + " Lineas a verificar: " + oRS.RecordCount);
                    System.Data.DataTable ResultQuery = new System.Data.DataTable();
                    ResultQuery = General.RecordSet_DataTable(oRS);

                    for (int i = 0; i < ResultQuery.Rows.Count; i++) //Looping through rows
                    {
                        string idLog;
                        string numSeg;
                        string strReq;
                        string docType;

                        if (Variables.proveedor == "FT")
                        {
                            idLog = Convert.ToString(ResultQuery.Rows[i]["Code"]); //Getting value CodeLog
                            numSeg = Convert.ToString(ResultQuery.Rows[i]["ID_Seguimiento"]); //Getting value IdProcess
                            strReq = Convert.ToString(ResultQuery.Rows[i]["Det_Peticion"]); //Getting value Request
                            docType = Convert.ToString(ResultQuery.Rows[i]["docType"]); //Getting value Request
                            MetodosFacturatech.StatusFacturatech(idLog, numSeg);
                        }
                    }
                }
                Utilities.Release(oRS);
                oRS = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                Log.EscribirLogFileTXT("verifystatus: " + ex.Message);
            }
        }

        public static void saveStatus(string codeLog, dynamic resultadoDoc, dynamic resultadoPDF, dynamic resultadoXML, dynamic resultadoCUFE)
        {
            Variables.banderaUpdateLog = false;
            SAPbobsCOM.UserTables tbls = null;
            SAPbobsCOM.UserTable tbl = null;
            tbls = Program.SBO_Company.UserTables;
            tbl = tbls.Item("FEDIAN_MONITORLOG");
            tbl.GetByKey(codeLog.ToString());


            if (!string.IsNullOrEmpty(resultadoPDF.resourceData) && !string.IsNullOrEmpty(resultadoXML.resourceData))
            {
                tbl.UserFields.Fields.Item("U_Archivo_PDF").Value = resultadoPDF.resourceData;
                tbl.UserFields.Fields.Item("U_Enlace_XML").Value = resultadoXML.resourceData;
                tbl.UserFields.Fields.Item("U_Status").Value = resultadoDoc.code;
                tbl.UserFields.Fields.Item("U_Resultado").Value = resultadoDoc.status;
                tbl.UserFields.Fields.Item("U_Respuesta_Int").Value = resultadoDoc;
                tbl.UserFields.Fields.Item("U_ProcessID").Value = resultadoCUFE.resourceData;
            }
            else
            {
                tbl.UserFields.Fields.Item("U_Archivo_PDF").Value = resultadoPDF.resourceData;
                tbl.UserFields.Fields.Item("U_Enlace_XML").Value = resultadoXML.resourceData;
                tbl.UserFields.Fields.Item("U_Status").Value = resultadoDoc.code;
                tbl.UserFields.Fields.Item("U_Resultado").Value = resultadoDoc.status;
                tbl.UserFields.Fields.Item("U_Respuesta_Int").Value = resultadoDoc;
                tbl.UserFields.Fields.Item("U_ID_Seguimiento").Value = resultadoCUFE.resourceData;
            }

            Variables.lRetCode = tbl.Update();
            if (Variables.lRetCode != 0)
            {
                Program.SBO_Company.GetLastError(out Variables.lRetCode, out Variables.sErrMsg);
                Log.EscribirLogFileTXT("updateLogDispapelesDocs: " + Variables.sErrMsg);
            }
            Variables.banderaUpdateLog = true;
            Utilities.Release(tbl);
            Utilities.Release(tbls);
        }
    }
}
