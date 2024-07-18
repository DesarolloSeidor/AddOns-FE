using SAPbouiCOM.Framework;
using System.Threading.Tasks;
using AddOnFE_Facturatech.Methods;
//using AddOnFE_Facturatech.Documentos;
using AddOnFE_Facturatech.Proveedor.Facturatech;
using Application = SAPbouiCOM.Framework.Application;
using System.Linq;
using System.Drawing;
using System;
using System.Xml;
using Newtonsoft.Json;
using SAPbobsCOM;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using AddOnFE_Facturatech.Proveedor;

namespace AddOnFE_Facturatech.Interfaz
{
    [FormAttribute("AddOnFE_Facturatech.Interfaz.MonitorFE", "Interfaz/MonitorFE.b1f")]
    class MonitorFE : UserFormBase
    {
        public MonitorFE()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_2").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_3").Specific));
            this.ComboBox1 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_4").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_7").Specific));
            this.Button1.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button1_ClickBefore);
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("Item_8").Specific));
            this.Button2.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button2_ClickBefore);
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_9").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_10").Specific));
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_11").Specific));
            this.Grid0.LinkPressedBefore += new SAPbouiCOM._IGridEvents_LinkPressedBeforeEventHandler(this.Grid0_LinkPressedBefore);
            this.Grid0.DoubleClickBefore += new SAPbouiCOM._IGridEvents_DoubleClickBeforeEventHandler(this.Grid0_DoubleClickBefore);
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_12").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button Button0;

        private void OnCustomInitialize()
        {
            Task.Run(() => General.AddDTEMonitor());
            this.oForm = this.UIAPIRawForm;
            Task.Run(() => General.CentralizeForm(this));
            setCombos();
        }

        #region Propiedades
        private SAPbouiCOM.IForm oForm;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.ComboBox ComboBox1;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.Button Button2;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.StaticText StaticText4;
        private Recordset oRecordset;



        #endregion

        #region Variables
        public static SAPbouiCOM.EditTextColumn oCol;
        public static bool senalActiva = true;
        public static bool banderaReenviar = true;
        public static bool banderaVerificaEstados = true;
        public static bool banderaAgregarDoc = true;
        public static bool banderaUpdateLog = true;
        #endregion

        #region Metodos

        public void setCombos()
        {
            oRecordset = ((SAPbobsCOM.Recordset)(Program.SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
            oRecordset.DoQuery(string.Format(Querys.Default.cmbTipodoc));
            //Elimina los valores del combobox
            //for (int i = 0; i <= this.ComboBox0.ValidValues.Count - 1; i++)
            //{
            //    this.ComboBox0.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
            //}
            while (oRecordset.EoF == false)
            {
                this.ComboBox0.ValidValues.Add((string)oRecordset.Fields.Item(0).Value, (string)oRecordset.Fields.Item(1).Value);
                oRecordset.MoveNext();
            }
            oRecordset = null;
            oRecordset = ((SAPbobsCOM.Recordset)(Program.SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)));
            oRecordset.DoQuery(string.Format(Querys.Default.cmbEstado));
            //Elimina los valores del combobox
            //for (int i = 0; i <= this.ComboBox1.ValidValues.Count - 1; i++)
            //{
            //    this.ComboBox1.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
            //}
            while (oRecordset.EoF == false)
            {
                this.ComboBox1.ValidValues.Add((string)oRecordset.Fields.Item(0).Value, (string)oRecordset.Fields.Item(1).Value);
                oRecordset.MoveNext();
            }
        }
        private void LoadGridLog(string sSQL)
        {
            try
            {
                if (sSQL != "")
                {
                    this.oForm.DataSources.DataTables.Item(0).ExecuteQuery(sSQL);
                    Grid0.DataTable = oForm.DataSources.DataTables.Item("DT_0");
                    Grid0.Columns.Item("Descripcion Estado").Width = 300;
                    Grid0.Columns.Item("Detalle Peticion").Width = 100;
                    Grid0.Columns.Item("Respuesta Integracion").Width = 100;
                    Grid0.Columns.Item("Archivo PDF").Width = 100;
                    Grid0.Columns.Item("Archivo XML").Width = 100;
                    Grid0.Item.Enabled = false;
                }
                else if (sSQL == "")
                {

                    if (Grid0.Rows.Count > 0)
                    {
                        Grid0.DataTable.Clear();
                    }
                }

                //oCol = (SAPbouiCOM.EditTextColumn)Grid0.Columns.Item("U_DocNum");
                //oCol.LinkedObjectType = "13";

                SAPbouiCOM.CommonSetting settingGrid = Grid0.CommonSetting;

                int redBackColor = Color.Tomato.R | (Color.Tomato.G << 8) | (Color.Tomato.B << 16);
                int greenBackColor = Color.PaleGreen.R | (Color.PaleGreen.G << 8) | (Color.PaleGreen.B << 16);
                int yellowBackColor = Color.Gold.R | (Color.Gold.G << 8) | (Color.Gold.B << 16);

                // Set background color in row
                //settingGrid.SetRowBackColor(1, redBackColor);
                //settingGrid.SetRowBackColor(2, yellowBackColor);
                //settingGrid.SetRowBackColor(3, greenBackColor);

                int row = 0;
                int rowcolor = 1;

                while (row < Grid0.Rows.Count)
                {
                    oCol = (SAPbouiCOM.EditTextColumn)Grid0.Columns.Item("Numero Interno");
                    oCol.LinkedObjectType = (string)Grid0.DataTable.Columns.Item("Tipo Objeto").Cells.Item(row).Value;

                    settingGrid.SetRowBackColor(rowcolor, -1);
                    string estado = (string)Grid0.DataTable.Columns.Item("Codigo Estado").Cells.Item(row).Value;
                    if (Constants.red.Contains(estado))
                    {
                        settingGrid.SetRowBackColor(rowcolor, redBackColor);
                    }
                    else if (Constants.green.Contains(estado))
                    {
                        settingGrid.SetCellBackColor(rowcolor, 8, greenBackColor);
                    }
                    else if (Constants.yellow.Contains(estado))
                    {
                        settingGrid.SetRowBackColor(rowcolor, yellowBackColor);
                    }
                    row++;
                    rowcolor++;
                }
            }
            catch (System.Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.Message);
                Log.EscribirLogFileTXT("LoadGridLog: " + ex.Message);
            }
        }
       

        #endregion


        #region Eventos
        //Evento boton Buscar
        private void Button1_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                string fechaini = "";
                string fechafin = "";
                string tipodoc = "";
                string estado = "";

                fechaini = this.EditText0.Value;
                fechafin = this.EditText1.Value;

                if (this.ComboBox0.Selected != null)
                {
                    tipodoc = this.ComboBox0.Selected.Value;
                }

                if (this.ComboBox1.Selected != null)
                {
                    estado = this.ComboBox1.Selected.Value;
                }

                if (fechaini != "" & fechafin != "")
                {
                    this.oForm.Freeze(true);
                    LoadGridLog(string.Format(Querys.Default.CargueMonitor, fechaini, fechafin, tipodoc, estado));
                    this.oForm.Freeze(false);
                }
                else
                {
                    Application.SBO_Application.StatusBar.SetText($"FE: Debe ingresar parametros de fecha", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                }
            }
            catch (System.Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText($"FE Error: {ex}: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                Log.EscribirLogFileTXT("Monitor: " + ex.Message);
            }

        }
        //Evento boton Reenviar
        private void Button2_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            BubbleEvent = true;
            try
            {
                senalActiva = false;
                SAPbouiCOM.DataTable oDT = this.Grid0.DataTable;
                if (this.Grid0.Rows.SelectedRows.Count > 0)
                {

                    for (int i = 0; (i <= (this.Grid0.Rows.SelectedRows.Count - 1)); i++)
                    {
                        string sCodeLog = (string)oDT.GetValue("Code", this.Grid0.GetDataTableRowIndex(this.Grid0.Rows.SelectedRows.Item(i, SAPbouiCOM.BoOrderType.ot_RowOrder)));
                        string sDocentry = (string)oDT.GetValue("Numero Interno", this.Grid0.GetDataTableRowIndex(this.Grid0.Rows.SelectedRows.Item(i, SAPbouiCOM.BoOrderType.ot_RowOrder)));
                        string sDocnum = (string)oDT.GetValue("Numero Documento", this.Grid0.GetDataTableRowIndex(this.Grid0.Rows.SelectedRows.Item(i, SAPbouiCOM.BoOrderType.ot_RowOrder)));
                        string sPrefijo = (string)oDT.GetValue("Prefijo", this.Grid0.GetDataTableRowIndex(this.Grid0.Rows.SelectedRows.Item(i, SAPbouiCOM.BoOrderType.ot_RowOrder)));
                        string sStatus = (string)oDT.GetValue("Codigo Estado", this.Grid0.GetDataTableRowIndex(this.Grid0.Rows.SelectedRows.Item(i, SAPbouiCOM.BoOrderType.ot_RowOrder)));
                        string sTipoDoc = (string)oDT.GetValue("Tipo Documento", this.Grid0.GetDataTableRowIndex(this.Grid0.Rows.SelectedRows.Item(i, SAPbouiCOM.BoOrderType.ot_RowOrder)));
                        string mensajeBarra = $"FE: Reenviando... :  {sDocnum}  ( {i + 1}  de  {this.Grid0.Rows.SelectedRows.Count} )";
                        Application.SBO_Application.StatusBar.SetText(mensajeBarra, SAPbouiCOM.BoMessageTime.bmt_Long, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                        //string sObject = oDT.GetValue("Tipo Objeto", oGrid.GetDataTableRowIndex(oGrid.Rows.SelectedRows.Item(i, SAPbouiCOM.BoOrderType.ot_RowOrder)));
                        //if (!Constants.green.Contains(sStatus))
                        //{
                        Send.SendFE(sDocentry, sDocnum, sPrefijo, sCodeLog, sTipoDoc, true);
                        //}
                        //progressBar.Value += AvanceBar;
                    }
                    //progressBar.Value = LimiteBar;
                    string fechaini = "";
                    string fechafin = "";
                    string tipodoc = "";
                    string estado = "";

                    fechaini = this.EditText0.Value;
                    fechafin = this.EditText1.Value;

                    if (this.ComboBox0.Selected != null)
                    {
                        tipodoc = this.ComboBox0.Selected.Value;
                    }

                    if (this.ComboBox1.Selected != null)
                    {
                        estado = this.ComboBox1.Selected.Value;
                    }

                    if (fechaini != "" & fechafin != "")
                    {
                        oForm.Freeze(true);
                        LoadGridLog(string.Format(Querys.Default.CargueMonitor, fechaini, fechafin, tipodoc, estado));
                        oForm.Freeze(false);
                    }
                    else
                    {
                        Application.SBO_Application.StatusBar.SetText($"FE: Debe ingresar parametros de fecha", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    }
                    //progressBar.Stop();
                    //Utilities.Release(progressBar);
                    Application.SBO_Application.StatusBar.SetText($"FE: Reenvio finalizado", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    System.GC.Collect();
                    senalActiva = true;
                }
                else
                {

                }
            }
            catch (System.Exception ex)
            {
                //progressBar.Stop();
                //Utilities.Release(progressBar);
                System.GC.Collect();
                Application.SBO_Application.StatusBar.SetText($"FE Error: {ex}: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                Log.EscribirLogFileTXT("Monitor: " + ex.Message);
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                senalActiva = true;
            }

        }

        //Evento dobleclick tabla
        private void Grid0_DoubleClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            string tempDirectory = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xml";
            try
            {
                if (pVal.ColUID == "Detalle Peticion")
                {
                    if (Variables.proveedor == "FT")
                    {
                        int index = this.Grid0.GetDataTableRowIndex(pVal.Row);
                        SAPbouiCOM.DataTable myDataTable = this.Grid0.DataTable;
                        string valuexml = (string)myDataTable.GetValue(pVal.ColUID, index);
                        // Create the XmlDocument.
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(General.DecodeTo64(valuexml));
                        doc.Save(tempDirectory);
                        System.Diagnostics.Process.Start("iexplore.exe", tempDirectory);
                    }
                }

                else if (pVal.ColUID == "Respuesta Integracion")
                {
                    if (Variables.proveedor == "FT")
                    {
                        int index = this.Grid0.GetDataTableRowIndex(pVal.Row);
                        SAPbouiCOM.DataTable myDataTable = this.Grid0.DataTable;
                        string valuexml = (string)myDataTable.GetValue(pVal.ColUID, index);
                        // Create the XmlDocument.
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(General.DecodeTo64(valuexml));
                        doc.Save(tempDirectory);
                        System.Diagnostics.Process.Start("iexplore.exe", tempDirectory);
                    }
                }

                else if (pVal.ColUID == "Archivo XML")
                {

                    if (Variables.proveedor == "C")
                    {
                        Documents oInvoice = (Documents)Program.SBO_Company.GetBusinessObject(BoObjectTypes.oInvoices);
                        Documents oCreditNote = (Documents)Program.SBO_Company.GetBusinessObject(BoObjectTypes.oCreditNotes);
                        CompanyService oCmpSrv;
                        SeriesService oSeriesService;
                        Series oSeries = null;
                        SeriesParams oSeriesParams = null;
                        // get company service
                        oCmpSrv = Program.SBO_Company.GetCompanyService();
                        // get series service
                        oSeriesService = (SeriesService)oCmpSrv.GetBusinessService(ServiceTypes.SeriesService);
                        // get series params
                        oSeriesParams = (SeriesParams)oSeriesService.GetDataInterface(SeriesServiceDataInterfaces.ssdiSeriesParams);
                        // set the number of an existing series

                        string xmlResult = "";
                        int index = this.Grid0.GetDataTableRowIndex(pVal.Row);
                        SAPbouiCOM.DataTable myDataTable = this.Grid0.DataTable;

                        string codeline = (string)myDataTable.GetValue("Code", index);
                        string tipoDoc = (string)myDataTable.GetValue("Tipo Documento", index);
                        string documentNumber = (string)myDataTable.GetValue("Numero Documento", index);
                        int NumberInterno = Convert.ToInt32(myDataTable.GetValue("Numero Interno", index));
                        string documentType = "";
                        switch (tipoDoc)
                        {
                            case "01":
                                documentType = "FV";
                                oInvoice.GetByKey(NumberInterno);
                                oSeriesParams.Series = oInvoice.Series;
                                break;
                            case "02":
                                documentType = "FC";
                                oInvoice.GetByKey(NumberInterno);
                                oSeriesParams.Series = oInvoice.Series;
                                break;
                            case "03":
                                documentType = "FE";
                                oInvoice.GetByKey(NumberInterno);
                                oSeriesParams.Series = oInvoice.Series;
                                break;
                            case "91":
                                documentType = "NC";
                                oCreditNote.GetByKey(NumberInterno);
                                oSeriesParams.Series = oCreditNote.Series;
                                break;
                            case "92":
                                documentType = "ND";
                                oInvoice.GetByKey(NumberInterno);
                                oSeriesParams.Series = oInvoice.Series;
                                break;
                            default:
                                break;
                        }
                        // get the series
                        oSeries = oSeriesService.GetSeries(oSeriesParams);
                        string prefijo = "";
                        prefijo = oSeries.Prefix;

                        //carvajal
                        //xmlResult = MetodosCarvajal.DownloadDocFE(codeline, documentType, prefijo + documentNumber, "SIGNED_XML");
                        if (xmlResult == "El recurso solicitado no ha sido encontrado.")
                        {
                            Application.SBO_Application.MessageBox("XML: " + xmlResult);
                        }
                        else
                        {
                            string valuexml = xmlResult;
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(General.DecodeTo64(valuexml));
                            doc.Save(tempDirectory);
                            if (valuexml != "")
                            {
                                System.Diagnostics.Process.Start(tempDirectory);
                                //System.Diagnostics.Process.Start("iexplore.exe", tempDirectory);
                            }
                        }
                        Utilities.Release(oInvoice);
                        oInvoice = null;
                        Utilities.Release(oCreditNote);
                        oCreditNote = null;
                        Utilities.Release(oCmpSrv);
                        oCmpSrv = null;
                        Utilities.Release(oSeriesService);
                        oSeriesService = null;
                        Utilities.Release(oSeriesParams);
                        oSeriesParams = null;
                        GC.Collect();
                    }

                    else if (Variables.proveedor == "CC")
                    {
                        int index = this.Grid0.GetDataTableRowIndex(pVal.Row);
                        SAPbouiCOM.DataTable myDataTable = this.Grid0.DataTable;

                        string tipoDoc = (string)myDataTable.GetValue("Tipo Documento", index);
                        string prefijo = (string)myDataTable.GetValue("Prefijo", index);
                        string folio = (string)myDataTable.GetValue("Numero Documento", index);

                        switch (tipoDoc)
                        {
                            case "01":
                            case "02":
                            case "03":
                                tipoDoc = "1";
                                break;
                            case "91":
                                tipoDoc = "2";
                                break;
                            case "92":
                                tipoDoc = "3";
                                break;
                            default:
                                break;
                        }

                        Documentos.consultaDoc oConsultapdf = new Documentos.consultaDoc();
                        oConsultapdf.tipoDocumento = tipoDoc;
                        oConsultapdf.numeroDocumento = prefijo + folio;
                        oConsultapdf.tipoRespuesta = "xml";
                        //oConsultapdf.versionDocumento = "1.0";

                        string urlstatus = "";
                        SAPbobsCOM.UserTables tbls = null;
                        SAPbobsCOM.UserTable tbl = null;

                        tbls = Program.SBO_Company.UserTables;
                        tbl = tbls.Item("FEDIAN_INTERF_CFG");
                        tbl.GetByKey("7");

                        urlstatus = (string)tbl.UserFields.Fields.Item("U_URL").Value;

                        string eInvoiceJson = JsonConvert.SerializeObject(oConsultapdf, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        byte[] encodedBytes = Encoding.UTF8.GetBytes(eInvoiceJson);
                        Encoding.Convert(Encoding.UTF8, Encoding.Unicode, encodedBytes);
                        string utfString = Encoding.UTF8.GetString(encodedBytes, 0, encodedBytes.Length);

                        var resultDocument = "";// Certifactura.Servicios.ConsultaDoc(urlstatus, "POST", utfString, Procesos.token, false);
                                                // var resultlist = resultDocument[true];
                                                //var res = System.Net.WebRequest.Equals(System.Net.HttpStatusCode.OK, resultlist);
                                                //var objAPIDoc = JsonConvert.DeserializeObject<dynamic>(resultlist.ToString());
                                                //Certifactura.respConsulta 
                        var resAPIDoc = "";
                        //resAPIDoc = ((JObject)objAPIDoc).ToObject<Certifactura.respConsulta>();

                        Utilities.Release(tbl);
                        Utilities.Release(tbls);

                        if (resAPIDoc != null)
                        {
                            string valuexml = resAPIDoc;//.documento;
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(General.DecodeTo64(valuexml));
                            doc.Save(tempDirectory);
                            if (valuexml != "")
                            {
                                System.Diagnostics.Process.Start(tempDirectory);
                                //System.Diagnostics.Process.Start("iexplore.exe", tempDirectory);
                            }
                        }
                        else
                        {
                            Application.SBO_Application.MessageBox("Archivo XML no encontrado");
                        }
                    }


                    else if (Variables.proveedor == "D")
                    {
                        int index = this.Grid0.GetDataTableRowIndex(pVal.Row);
                        SAPbouiCOM.DataTable myDataTable = this.Grid0.DataTable;
                        string valuexml = (string)myDataTable.GetValue(pVal.ColUID, index);
                        // Create the XmlDocument.
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(General.DecodeTo64(valuexml));
                        doc.Save(tempDirectory);
                        if (valuexml != "")
                        {
                            System.Diagnostics.Process.Start(tempDirectory);
                            //System.Diagnostics.Process.Start("iexplore.exe", tempDirectory);
                        }
                    }

                    else if (Variables.proveedor == "FT")
                    {

                    }
                }

                else if (pVal.ColUID == "Archivo PDF")
                {

                    //if (Variables.proveedor == "C")
                    //{
                    //    SAPbobsCOM.Documents oInvoiceXML = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoiceXMLs);
                    //    SAPbobsCOM.Documents oCreditNote = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oCreditNotes);
                    //    SAPbobsCOM.CompanyService oCmpSrv;
                    //    SAPbobsCOM.SeriesService oSeriesService;
                    //    Series oSeries = null;
                    //    SeriesParams oSeriesParams = null;
                    //    // get company service
                    //    oCmpSrv = oCompany.GetCompanyService();
                    //    // get series service
                    //    oSeriesService = oCmpSrv.GetBusinessService(ServiceTypes.SeriesService);
                    //    // get series params
                    //    oSeriesParams = oSeriesService.GetDataInterface(SeriesServiceDataInterfaces.ssdiSeriesParams);
                    //    // set the number of an existing series

                    //    string pdfResult = "";
                    //    SAPbouiCOM.Grid grd = SBO_Application.Forms.ActiveForm.Items.Item("Grid").Specific;
                    //    int index = grd.GetDataTableRowIndex(pVal.Row);
                    //    SAPbouiCOM.DataTable myDataTable = oGrid.DataTable;

                    //    string codeline = myDataTable.GetValue("Code", index);
                    //    string tipoDoc = myDataTable.GetValue("Tipo Documento", index);
                    //    string documentNumber = myDataTable.GetValue("Numero Documento", index);
                    //    int NumberInterno = Convert.ToInt32(myDataTable.GetValue("Numero Interno", index));
                    //    string documentType = "";
                    //    switch (tipoDoc)
                    //    {
                    //        case "01":
                    //            documentType = "FV";
                    //            oInvoiceXML.GetByKey(NumberInterno);
                    //            oSeriesParams.Series = oInvoiceXML.Series;
                    //            break;
                    //        case "02":
                    //            documentType = "FC";
                    //            oInvoiceXML.GetByKey(NumberInterno);
                    //            oSeriesParams.Series = oInvoiceXML.Series;
                    //            break;
                    //        case "03":
                    //            documentType = "FE";
                    //            oInvoiceXML.GetByKey(NumberInterno);
                    //            oSeriesParams.Series = oInvoiceXML.Series;
                    //            break;
                    //        case "91":
                    //            documentType = "NC";
                    //            oCreditNote.GetByKey(NumberInterno);
                    //            oSeriesParams.Series = oCreditNote.Series;
                    //            break;
                    //        case "92":
                    //            documentType = "ND";
                    //            oInvoiceXML.GetByKey(NumberInterno);
                    //            oSeriesParams.Series = oInvoiceXML.Series;
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //    // get the series
                    //    oSeries = oSeriesService.GetSeries(oSeriesParams);
                    //    string prefijo = "";
                    //    prefijo = oSeries.Prefix;

                    //    pdfResult = MetodosCarvajal.DownloadDocFE(codeline, documentType, prefijo + documentNumber, "PDF");
                    //    if (pdfResult == "El recurso solicitado no ha sido encontrado.")
                    //    {
                    //        SBO_Application.MessageBox("PDF: " + pdfResult);
                    //    }
                    //    else
                    //    {
                    //        string valuepdf = pdfResult;
                    //        tempDirectory = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                    //        byte[] bytes = Convert.FromBase64String(valuepdf);
                    //        System.IO.FileStream stream = new FileStream(tempDirectory, FileMode.CreateNew);
                    //        System.IO.BinaryWriter writer = new BinaryWriter(stream);
                    //        writer.Write(bytes, 0, bytes.Length);
                    //        writer.Close();
                    //        if (valuepdf != "")
                    //        {
                    //            System.Diagnostics.Process.Start(tempDirectory);
                    //        }
                    //    }

                    //    Utilities.Release(oInvoiceXML);
                    //    oInvoiceXML = null;
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

                    //else if (Variables.proveedor == "CC")
                    //{

                    //    SAPbouiCOM.Grid grd = SBO_Application.Forms.ActiveForm.Items.Item("Grid").Specific;
                    //    int index = grd.GetDataTableRowIndex(pVal.Row);
                    //    SAPbouiCOM.DataTable myDataTable = oGrid.DataTable;

                    //    string tipoDoc = myDataTable.GetValue("Tipo Documento", index);
                    //    string prefijo = myDataTable.GetValue("Prefijo", index);
                    //    string folio = myDataTable.GetValue("Numero Documento", index);

                    //    switch (tipoDoc)
                    //    {
                    //        case "01":
                    //        case "02":
                    //        case "03":
                    //            tipoDoc = "1";
                    //            break;
                    //        case "91":
                    //            tipoDoc = "2";
                    //            break;
                    //        case "92":
                    //            tipoDoc = "3";
                    //            break;
                    //        default:
                    //            break;
                    //    }

                    //    Documentos.consultaDoc oConsultapdf = new Documentos.consultaDoc();
                    //    oConsultapdf.tipoDocumento = tipoDoc;
                    //    oConsultapdf.numeroDocumento = prefijo + folio;
                    //    oConsultapdf.tipoRespuesta = "pdf";
                    //    //oConsultapdf.versionDocumento = "1.0";

                    //    string urlstatus = "";
                    //    SAPbobsCOM.UserTables tbls = null;
                    //    SAPbobsCOM.UserTable tbl = null;

                    //    tbls = oCompany.UserTables;
                    //    tbl = tbls.Item("FEDIAN_INTERF_CFG");
                    //    tbl.GetByKey("7");

                    //    urlstatus = tbl.UserFields.Fields.Item("U_URL").Value;

                    //    string eInvoiceJson = JsonConvert.SerializeObject(oConsultapdf, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    //    byte[] encodedBytes = Encoding.UTF8.GetBytes(eInvoiceJson);
                    //    Encoding.Convert(Encoding.UTF8, Encoding.Unicode, encodedBytes);
                    //    string utfString = Encoding.UTF8.GetString(encodedBytes, 0, encodedBytes.Length);

                    //    var resultDocument = Certifactura.Servicios.ConsultaDoc(urlstatus, "POST", utfString, Procesos.token, false);
                    //    var resultlist = resultDocument[true];
                    //    var res = System.Net.WebRequest.Equals(System.Net.HttpStatusCode.OK, resultlist);
                    //    var objAPIDoc = JsonConvert.DeserializeObject<dynamic>(resultlist.ToString());
                    //    Certifactura.respConsulta resAPIDoc = null;
                    //    resAPIDoc = ((JObject)objAPIDoc).ToObject<Certifactura.respConsulta>();

                    //    Utilities.Release(tbl);
                    //    Utilities.Release(tbls);

                    //    if (resAPIDoc.documento != null)
                    //    {
                    //        string valuepdf = resAPIDoc.documento;
                    //        tempDirectory = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                    //        byte[] bytes = Convert.FromBase64String(valuepdf);
                    //        System.IO.FileStream stream = new FileStream(tempDirectory, FileMode.CreateNew);
                    //        System.IO.BinaryWriter writer = new BinaryWriter(stream);
                    //        writer.Write(bytes, 0, bytes.Length);
                    //        writer.Close();
                    //        if (valuepdf != "")
                    //        {
                    //            System.Diagnostics.Process.Start(tempDirectory);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        SBO_Application.MessageBox("Archivo PDF no encontrado");
                    //    }
                    //}

                    //else if (Variables.proveedor == "F")
                    //{
                    //    SAPbouiCOM.Grid grd = SBO_Application.Forms.ActiveForm.Items.Item("Grid").Specific;
                    //    int index = grd.GetDataTableRowIndex(pVal.Row);
                    //    SAPbouiCOM.DataTable myDataTable = oGrid.DataTable;
                    //    string febosID = myDataTable.GetValue("FebosID", index);

                    //    string urlstatus = "";
                    //    SAPbobsCOM.UserTables tbls = null;
                    //    SAPbobsCOM.UserTable tbl = null;

                    //    tbls = oCompany.UserTables;
                    //    tbl = tbls.Item("FEDIAN_INTERF_CFG");
                    //    tbl.GetByKey("6");

                    //    urlstatus = string.Format((string)tbl.UserFields.Fields.Item("U_URL").Value, febosID);
                    //    var resultstatus = ServiceFebos.Febos_StatusDoc(urlstatus, "GET", febosID, Procesos.token, false);
                    //    var resultliststatus = resultstatus[true];
                    //    Procesos.responseStatus = resultliststatus;
                    //    var objAPIDocstatu = JsonConvert.DeserializeObject<dynamic>(resultliststatus.ToString());
                    //    ResultAPI resAPIstatusDoc = null;
                    //    resAPIstatusDoc = ((JObject)objAPIDocstatu).ToObject<ResultAPI>();

                    //    Utilities.Release(tbl);
                    //    tbl = null;
                    //    Utilities.Release(tbls);
                    //    tbls = null;
                    //    System.GC.Collect();

                    //    if (resAPIstatusDoc.imagenLink != null)
                    //    {
                    //        System.Diagnostics.Process.Start("iexplore.exe", resAPIstatusDoc.imagenLink);
                    //    }
                    //    else
                    //    {
                    //        SBO_Application.MessageBox("Archivo PDF no encontrado");
                    //    }
                    //}

                    //else if (Variables.proveedor == "D")
                    //{
                    //    consultarArchivosDispape.felRepuestaDescargaDocumentos respuestaPDF;
                    //    SAPbouiCOM.Grid grd = SBO_Application.Forms.ActiveForm.Items.Item("Grid").Specific;
                    //    int index = grd.GetDataTableRowIndex(pVal.Row);
                    //    SAPbouiCOM.DataTable myDataTable = oGrid.DataTable;
                    //    string folio = "", prefijo = "", tipoDoc = "";

                    //    folio = myDataTable.GetValue("Numero Documento", index);
                    //    prefijo = myDataTable.GetValue("Prefijo", index);
                    //    tipoDoc = myDataTable.GetValue("Tipo Documento", index);

                    //    SAPbobsCOM.UserTables tblscnf = null;
                    //    SAPbobsCOM.UserTable tblcnf = null;
                    //    string urlWS = "";

                    //    tblscnf = oCompany.UserTables;
                    //    tblcnf = tblscnf.Item("FEDIAN_INTERF_CFG");
                    //    tblcnf.GetByKey(tipoDoc);
                    //    urlWS = tblcnf.UserFields.Fields.Item("U_URL").Value;

                    //    respuestaPDF = Controllers.WebServiceDispapelesController.consultaArchivos(folio, prefijo, tipoDoc, urlWS);

                    //    if (respuestaPDF != null && respuestaPDF.listaArchivos != null)
                    //    {
                    //        for (int i = 0; i < respuestaPDF.listaArchivos.Length; i++)
                    //        {
                    //            string tipoArchivo = "";
                    //            tipoArchivo = respuestaPDF.listaArchivos[i].formato;
                    //            if (tipoArchivo == ".pdf")
                    //            {
                    //                string valuepdf = Convert.ToBase64String(respuestaPDF.listaArchivos[i].streamFile);
                    //                tempDirectory = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                    //                byte[] bytes = Convert.FromBase64String(valuepdf);
                    //                System.IO.FileStream stream = new FileStream(tempDirectory, FileMode.CreateNew);
                    //                System.IO.BinaryWriter writer = new BinaryWriter(stream);
                    //                writer.Write(bytes, 0, bytes.Length);
                    //                writer.Close();
                    //                if (valuepdf != "")
                    //                {
                    //                    System.Diagnostics.Process.Start(tempDirectory);
                    //                }
                    //            }
                    //        }
                    //    }

                    //    Utilities.Release(tblscnf);
                    //    tblscnf = null;
                    //    Utilities.Release(tblcnf);
                    //    tblcnf = null;
                    //    System.GC.Collect();
                    //}

                }
            }
            catch (System.Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.Message);
                Log.EscribirLogFileTXT("DobleClick_FORM_FE_0008: " + ex.Message);
            }

        }
        private void Grid0_LinkPressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = false;
            string ObjectLinkType = Convert.ToString(this.Grid0.DataTable.Columns.Item("Tipo Objeto").Cells.Item(pVal.Row).Value);
            SAPbouiCOM.EditTextColumn col = (SAPbouiCOM.EditTextColumn)this.Grid0.Columns.Item("Numero Interno");

            switch (ObjectLinkType)
            {
                case "13":
                    {
                        col.LinkedObjectType = "13";
                        break;
                    }
                case "14":
                    {
                        col.LinkedObjectType = "14";
                        break;
                    }
                case "18":
                    {
                        col.LinkedObjectType = "18";
                        break;
                    }

            }
            BubbleEvent = true;

        }

        #endregion


    }
}
