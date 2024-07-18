using SAPbouiCOM;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using Application = SAPbouiCOM.Framework.Application;
using AddOnFE_Facturatech.Methods;
using System.Threading.Tasks;
using System;

namespace AddOnFE_Facturatech.Interfaz
{
    [FormAttribute("AddOnFE_Facturatech.Interfaz.ParametrizacionFE", "Interfaz/ParametrizacionFE.b1f")]
    class ParametrizacionFE : UserFormBase
    {
        public ParametrizacionFE()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_0").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_1").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_2").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_3").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_4").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_5").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_6").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button0.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button0_ClickBefore);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_9").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_10").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_11").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_12").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_14").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.EditText EditText0;

        private void OnCustomInitialize()
        {
            this.oForm = this.UIAPIRawForm;
            Task.Run(() => General.CentralizeForm(this));
            SetCampos();
        }

        #region Propiedades
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.StaticText StaticText5;
        private IForm oForm;
        private Recordset oRecordset;
        public static SAPbobsCOM.Company oCompany = Program.SBO_Company;
        #endregion

        #region Variables
        public static int lRetCode;
        public static string sErrMsg;
        public static string sSql;
        #endregion

        #region Eventos
        private void Button0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            //Actualizacion o Creacion de registro - click boton
            try
            {
                string codigo = "";
                string prov = "";
                string msg = "";

                codigo = this.EditText0.Value;

                if (this.ComboBox0.Selected != null)
                {
                    prov = this.ComboBox0.Selected.Value;
                }

                //Procesos.SHA256Encrypt(clave);

                SAPbobsCOM.UserTables tbls = oCompany.UserTables;
                SAPbobsCOM.UserTable tbl = tbls.Item("FEDIAN_PARAMG");

                if (codigo == "" || codigo == "0")
                {
                    tbl.Code = "1";
                    tbl.Name = "1";
                }
                else
                {
                    tbl.GetByKey(codigo);
                }

                tbl.UserFields.Fields.Item("U_Proveedor").Value = this.ComboBox0.Selected.Value;
                tbl.UserFields.Fields.Item("U_NIT_Emisor").Value = this.EditText1.Value;
                tbl.UserFields.Fields.Item("U_Email_Usuario").Value = this.EditText2.Value;
                tbl.UserFields.Fields.Item("U_Clave_Usuario").Value = this.EditText3.Value;
                tbl.UserFields.Fields.Item("U_Token").Value = this.EditText3.Value;

                tbl.UserFields.Fields.Item("U_NReenvios").Value = this.EditText4.Value;
                tbl.UserFields.Fields.Item("U_IReenvios").Value = this.EditText5.Value;

                switch (oForm.Mode)
                {
                    case SAPbouiCOM.BoFormMode.fm_ADD_MODE:
                        lRetCode = tbl.Add();
                        msg = "Registro creado correctamente, ";
                        oForm.Refresh();
                        break;
                    case SAPbouiCOM.BoFormMode.fm_UPDATE_MODE:
                        lRetCode = tbl.Update();
                        msg = "Registro actualizado correctamente, ";
                        oForm.Refresh();
                        break;
                    default:
                        break;
                }

                //Validacion de registros añadidos o actualizados
                if (lRetCode != 0)
                {
                    oCompany.GetLastError(out lRetCode, out sErrMsg);
                    Application.SBO_Application.MessageBox(sErrMsg);
                }
                else
                {
                    Application.SBO_Application.StatusBar.SetText(msg + "Operación finalizada con éxito", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    oForm.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE;
                    //Funcion para recargar datos almacenados en cargue inicial
                    General.CargueInicial();
                }
                Utilities.Release(tbls);
                tbls = null;
                Utilities.Release(tbl);
                tbl = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.Message);
                Log.EscribirLogFileTXT("Grabar_FORM_FE_0001: " + ex.Message);
            }

        }

        #endregion

        #region Metodos
        private void SetCampos()
        {
            try
            {
                this.oForm.Freeze(true);

                this.oRecordset = ((Recordset)(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset)));
                sSql = Querys.Default.PARAMG;
                this.oRecordset.DoQuery(sSql);

                if (this.oRecordset.RecordCount > 0)
                {
                    this.EditText0.Value = this.oRecordset.Fields.Item("Code").Value.ToString();
                    this.ComboBox0.Select(this.oRecordset.Fields.Item("U_Proveedor").Value.ToString(), SAPbouiCOM.BoSearchKey.psk_ByValue);
                    this.EditText1.Value = this.oRecordset.Fields.Item("U_NIT_Emisor").Value.ToString();
                    this.EditText2.Value = this.oRecordset.Fields.Item("U_Email_Usuario").Value.ToString();
                    this.EditText3.Value = this.oRecordset.Fields.Item("U_Clave_Usuario").Value.ToString();
                    this.EditText4.Value = this.oRecordset.Fields.Item("U_NReenvios").Value.ToString();
                    this.EditText5.Value = this.oRecordset.Fields.Item("U_IReenvios").Value.ToString();
                    this.oForm.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE;
                }
                else
                {
                    string querys = string.Format(@"SELECT MAX(""Code"") +1 AS ""NextCode"" FROM ""@FEDIAN_PARAMG""  ");
                    this.oRecordset.DoQuery(querys);

                    this.EditText0.Value = this.oRecordset.Fields.Item("NextCode").Value.ToString();

                    this.oForm.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE;
                }
                Utilities.Release(this.oRecordset);
                this.oRecordset = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.Message);
                Log.EscribirLogFileTXT("Cargar_FORM_FE_0001: " + ex.Message);
            }
            this.oForm.Freeze(false);

        }
        #endregion
    }
}
