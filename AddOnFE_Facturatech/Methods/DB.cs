using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Application = SAPbouiCOM.Framework.Application;

namespace AddOnFE_Facturatech.Methods
{
    public abstract class DB
    {
        public static SAPbobsCOM.Company SBO_Company = Program.SBO_Company;
        public static Log oLog;
        public static List<Log> lLog;

        public enum TypeMess
        {
            Atencion,
            Error,
            Exito
        }
        public enum Lang
        {
            ES
        }
        public DB()
        {
            lLog = new List<Log>();
        }
        #region Metodos

        public static void AddLog(string Log, TypeMess type, Lang lang)
        {
            oLog = new Log();

            switch (type)
            {
                case TypeMess.Atencion:
                    switch (lang)
                    {
                        //case Lang.PTBR:
                        //    oLog.Msg = $"[DATA:{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}][ATENÇÃO] : {Log}";

                        //    break;
                        case Lang.ES:
                            oLog.Msg = $"[DATA:{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}][ADVERTENCIA] : {Log}";
                            break;
                    }
                    Application.SBO_Application.StatusBar.SetText(oLog.Msg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                    break;


                case TypeMess.Error:
                    switch (lang)
                    {
                        case Lang.ES:
                            oLog.Msg = $"[DATA:{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}][ErrorR] : {Log}";
                            break;
                    }
                    Application.SBO_Application.StatusBar.SetText(oLog.Msg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    break;
                case TypeMess.Exito:
                    switch (lang)
                    {
                        case Lang.ES:
                            oLog.Msg = $"[DATA:{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}][ÉXITO] : {Log}";
                            break;
                    }
                    Application.SBO_Application.StatusBar.SetText(oLog.Msg, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    break;
                default:
                    break;
            }

            lLog.Add(oLog);
        }

        /// <summary>
        /// Verifica se a tabela já existe na base de dados do B1
        /// </summary>
        /// <param name="TBName">Nome da Tabela</param>
        /// <returns>bool - true/false indicando se a tabela existe ou não</returns>

        public bool ExisteTB(string TBName, Lang lang)
        {

            UserTablesMD oUserTable;
            oUserTable = (UserTablesMD)SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
            //UserTablesMD oUserTable = new UserTablesMD(ref oDiCompany);            
            bool ret = oUserTable.GetByKey(TBName);
            int errCode; string errMsg;
            SBO_Company.GetLastError(out errCode, out errMsg);

            if (errCode != 0)
            {
                AddLog(errMsg, TypeMess.Error, lang);
            }
            else
            {
                switch (lang)
                {
                    case Lang.ES:
                        AddLog($"Tabla de datos existente: {TBName}", TypeMess.Exito, lang);
                        break;
                }

            }

            TBName = null;
            errMsg = null;

            if (oUserTable != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTable);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                oUserTable = null;
            }


            return (ret);
        }

        public static object ExecuteSqlScalar(string query)
        {
            object objRet = null;
            SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            try
            {


                businessObject.DoQuery(query);
                if (!businessObject.EoF)
                {
                    objRet = businessObject.Fields.Item(0).Value;
                }

                if (businessObject != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(businessObject);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    businessObject = null;
                }



            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (businessObject != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(businessObject);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    businessObject = null;
                }
                GC.Collect();
            }
            return objRet;
        }

        public void AddUDO(string sUDO, string sTable, string sDescricaoUDO, SAPbobsCOM.BoUDOObjType oBoUDOObjType, string[] childTableName = null, string[] childObjectName = null, string[] formColumns = null, string[] formColumnsDsc = null,

            SAPbobsCOM.BoYesNoEnum CanCancel = SAPbobsCOM.BoYesNoEnum.tYES,
            SAPbobsCOM.BoYesNoEnum CanClose = SAPbobsCOM.BoYesNoEnum.tNO,
            SAPbobsCOM.BoYesNoEnum CanDelete = SAPbobsCOM.BoYesNoEnum.tNO,
            SAPbobsCOM.BoYesNoEnum CanFind = SAPbobsCOM.BoYesNoEnum.tYES,
            SAPbobsCOM.BoYesNoEnum CanYearTransfer = SAPbobsCOM.BoYesNoEnum.tNO,
            SAPbobsCOM.BoYesNoEnum eCanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tYES,
            SAPbobsCOM.BoYesNoEnum ManageSeries = SAPbobsCOM.BoYesNoEnum.tNO,
            SAPbobsCOM.BoYesNoEnum eEnableEnhancedForm = SAPbobsCOM.BoYesNoEnum.tNO,
            SAPbobsCOM.BoYesNoEnum eMenuItem = SAPbobsCOM.BoYesNoEnum.tNO,
            int? FatherMenuID = null,
            string Position = null,
            string MenuCaption = null,
            string MenuUID = null,
            bool bUpdate = false,
            Lang lang = Lang.ES,
            string[] findFields = null
            )
        {
            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;

            oUserObjectMD = (SAPbobsCOM.UserObjectsMD)SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);

            try
            {

                switch (lang)
                {
                    case Lang.ES:
                        AddLog($"Registrando objeto - {sUDO} - {sTable} - {sDescricaoUDO}", TypeMess.Atencion, lang);
                        break;

                }

                int lRetCode = 0;
                int iTabelasFilhas = 0;
                string sErrMsg = "";
                string sQuery = "";

                bool bExisteColuna = false;
                bool bExisteTabelaFilha = false;


                System.Data.DataTable tb = new System.Data.DataTable();


                if (oUserObjectMD.GetByKey(sUDO))
                {
                    switch (lang)
                    {
                        case Lang.ES:
                            AddLog($"Objeto ya existente - {sUDO} - {sTable} - {sDescricaoUDO}", TypeMess.Atencion, lang);
                            break;

                    }

                    if (bUpdate)
                    {
                        goto _continue;
                    }

                    if (oUserObjectMD != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserObjectMD);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                        oUserObjectMD = null;
                        return;
                    }



                }
                else
                {
                    oUserObjectMD.ManageSeries = ManageSeries;
                    oUserObjectMD.CanYearTransfer = CanYearTransfer;
                    oUserObjectMD.Code = sUDO;
                    oUserObjectMD.Name = sDescricaoUDO;
                    oUserObjectMD.ObjectType = oBoUDOObjType;
                    oUserObjectMD.TableName = sTable;
                    bUpdate = false;
                }
            _continue:

                oUserObjectMD.CanCancel = CanCancel;
                oUserObjectMD.CanClose = CanClose;
                oUserObjectMD.CanDelete = CanDelete;
                oUserObjectMD.CanFind = CanFind;

                if (CanFind == BoYesNoEnum.tYES)
                {
                    if (findFields != null)
                    {
                        for (var i = 0; i <= findFields.Length - 1; i++)
                        {
                            oUserObjectMD.FindColumns.Add();
                            oUserObjectMD.FindColumns.ColumnAlias = findFields[i];
                        }
                    }
                }

                if (eCanCreateDefaultForm == SAPbobsCOM.BoYesNoEnum.tYES)
                {
                    oUserObjectMD.CanCreateDefaultForm = eCanCreateDefaultForm;
                    oUserObjectMD.EnableEnhancedForm = eEnableEnhancedForm;
                    if (formColumns != null)
                    {
                        for (var i = 0; i <= formColumns.Length - 1; i++)
                        {
                            oUserObjectMD.FormColumns.Add();
                            oUserObjectMD.FormColumns.FormColumnAlias = formColumns[i];
                            oUserObjectMD.FormColumns.FormColumnDescription = formColumnsDsc[i];
                        }
                    }
                    if (eMenuItem == SAPbobsCOM.BoYesNoEnum.tYES)
                    {
                        oUserObjectMD.MenuItem = eMenuItem;
                        if (FatherMenuID != null)
                            oUserObjectMD.FatherMenuID = Convert.ToInt32(FatherMenuID);
                        oUserObjectMD.Position = Convert.ToInt32(Position);
                        oUserObjectMD.MenuCaption = MenuCaption;
                        oUserObjectMD.MenuUID = MenuUID;
                    }
                }
                //Adicionar tabelas filhas
                if (childObjectName != null)
                {
                    for (int x = 0; x < childObjectName.Length; x++)
                    {

                        iTabelasFilhas = oUserObjectMD.ChildTables.Count;
                        bExisteTabelaFilha = false;
                        for (int y = 0; y < iTabelasFilhas; y++)
                        {
                            oUserObjectMD.ChildTables.SetCurrentLine(y);
                            if (oUserObjectMD.ChildTables.TableName == childTableName[x])
                            {
                                bExisteTabelaFilha = true;
                                break;
                            }
                        }

                        if (bExisteTabelaFilha == false)
                        {
                            if (x > 0) oUserObjectMD.ChildTables.Add();
                            if (childObjectName[x] != "" && childTableName[x] != "")
                            {
                                oUserObjectMD.ChildTables.TableName = childTableName[x];
                                oUserObjectMD.ChildTables.ObjectName = childObjectName[x];
                            }
                        }

                    }

                }

                if (bUpdate)
                    lRetCode = oUserObjectMD.Update();
                else
                    lRetCode = oUserObjectMD.Add();

                // check for Errorrs in the process
                if (lRetCode != 0)
                {
                    SBO_Company.GetLastError(out lRetCode, out sErrMsg);

                    switch (lang)
                    {
                        case Lang.ES:
                            AddLog($"Errorr al registrar el objeto - {sUDO} - {sTable} - {sDescricaoUDO} : {sErrMsg}", TypeMess.Error, lang);
                            break;

                    }
                }
                else
                {
                    switch (lang)
                    {
                        case Lang.ES:
                            AddLog($"¡Objeto registrado correctamente! - {sUDO} - {sTable} - {sDescricaoUDO}", TypeMess.Exito, lang);
                            break;

                    }
                }


            }
            catch (Exception e)
            {

                switch (lang)
                {
                    case Lang.ES:
                        AddLog($"Errorr al registrar el objeto - {sUDO} - {sTable} - {sDescricaoUDO} : {e.Message}", TypeMess.Error, lang);
                        break;

                }
            }
            finally
            {
                if (oUserObjectMD != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserObjectMD);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    oUserObjectMD = null;
                }

            }

        }


        /// <summary>
        /// Criação dos campos de usuários
        /// </summary>
        /// <param name="NomeTabela">Tabela</param>
        /// <param name="NomeCampo">Come do campo</param>
        /// <param name="DescCampo">Descrição do campo</param>
        /// <param name="Tipo">Tipo do Campo</param>
        /// <param name="SubTipo">Sub Tipo</param>
        /// <param name="Tamanho">Tamanho</param>
        /// <returns>bool - true/false indicando se o campo foi criado</returns>
        //public void AddUserField(string NomeTabela, string NomeCampo, string DescCampo, SAPbobsCOM.BoFieldTypes Tipo, SAPbobsCOM.BoFldSubTypes SubTipo, Int16 Tamanho, string[,] valoresValidos, string valorDefault)
        public void AddUserField(string NomeTabela, string NomeCampo, string DescCampo, BoFieldTypes Tipo, BoFldSubTypes SubTipo, Int16 Tamanho, string[,] valoresValidos, string valorDefault, string linkedTable, UDFLinkedSystemObjectTypesEnum linkObj = 0, Lang lang = Lang.ES, bool updateField = false)
        {
            int lErrCode;
            string sErrMsg = "";

            switch (lang)
            {
                case Lang.ES:
                    AddLog($"Agregar el campo - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo}", TypeMess.Atencion, lang);
                    break;

            }


            string strSql = String.Empty;

            if (NomeTabela.Length > 4)
                if (NomeTabela.Contains("TAX4"))
                    if (!NomeTabela.Contains("@"))
                        NomeTabela = "@" + NomeTabela;


            strSql = string.Format(@"select ""FieldID""
                                                from CUFD 
                                                where ""TableID"" = '{0}' 
                                                    and ""AliasID"" = '{1}'", NomeTabela, NomeCampo);




            //0 - Campo Não exite
            //1 - Campos Existe
            dynamic resultado = ExecuteSqlScalar(strSql);

            UserFieldsMD oUserField;
            oUserField = (UserFieldsMD)SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);

            if (resultado != null && updateField)
            {
                oUserField.GetByKey(NomeTabela, Convert.ToInt32(resultado));
            }
            if (resultado != null && !updateField)
            {
                switch (lang)
                {
                    case Lang.ES:
                        AddLog($"Advertencia al agregar el campo - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo} :  ya existe en la base de datos", TypeMess.Atencion, lang);
                        break;

                }

                if (oUserField != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserField);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    oUserField = null;
                }

                return;
            }
            try
            {


                oUserField.TableName = NomeTabela.Replace("@", "").Replace("[", "").Replace("]", "").Trim();

                if (NomeCampo.Length > 25)
                {
                    switch (lang)
                    {
                        case Lang.ES:
                            throw new Exception("¡El tamaño del nombre de campo es mayor de lo permitido!");
                            break;

                    }

                }

                oUserField.Name = NomeCampo;
                if (Tamanho != 0)
                    oUserField.EditSize = Tamanho;

                if (DescCampo.Length > 30)
                    DescCampo = DescCampo.Substring(0, 30);

                oUserField.Description = DescCampo;
                oUserField.Type = Tipo;
                oUserField.SubType = SubTipo;
                oUserField.DefaultValue = valorDefault;
                if (linkObj != 0)
                    oUserField.LinkedSystemObject = linkObj;
                if (!string.IsNullOrEmpty(linkedTable))
                    oUserField.LinkedTable = linkedTable;

                //adicionar valores válidos
                if (valoresValidos != null)
                {
                    Int32 qtd = valoresValidos.GetLength(0);
                    if (qtd > 0)
                    {
                        for (int i = 0; i < qtd; i++)
                        {
                            oUserField.ValidValues.Value = valoresValidos[i, 0];
                            oUserField.ValidValues.Description = valoresValidos[i, 1];
                            oUserField.ValidValues.Add();
                        }
                    }
                }

                if (resultado != null && updateField)
                {
                    oUserField.Update();
                }
                else
                {
                    oUserField.Add();
                }

                SBO_Company.GetLastError(out lErrCode, out sErrMsg);
                if (lErrCode != 0)
                {

                    switch (lang)
                    {
                        case Lang.ES:
                            if (sErrMsg.Contains("(ODBC -2035)"))
                                AddLog($"Advertencia al agregar el campo - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo} : {sErrMsg}", TypeMess.Atencion, lang);
                            else
                                AddLog($"Errorr al agregar el campo - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo} : {sErrMsg}", TypeMess.Error, lang);
                            break;

                    }

                    //throw new Exception(sErrMsg);
                }
                else
                {
                    switch (lang)
                    {
                        case Lang.ES:
                            AddLog($"Éxito al agregar el campo - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo}", TypeMess.Exito, lang);
                            break;

                    }

                }

            }
            catch (Exception e)
            {
                switch (lang)
                {
                    case Lang.ES:
                        AddLog($"Errorr al agregar el campo - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo} : {e.Message}", TypeMess.Error, lang);
                        break;

                }


            }
            finally
            {
                if (oUserField != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserField);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    oUserField = null;
                }

            }
            if (resultado != null)
            {

                switch (lang)
                {
                    case Lang.ES:
                        AddLog($"Campo existente - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo}", TypeMess.Atencion, lang);
                        break;

                }

                if (oUserField != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserField);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    oUserField = null;
                }

            }

            if (oUserField != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserField);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                oUserField = null;
            }


        }
        public void AddUserFieldBR(string NomeTabela, string NomeCampo, string DescCampo, BoFieldTypes Tipo, BoFldSubTypes SubTipo, Int16 Tamanho, string[,] valoresValidos, string valorDefault, string linkedTable, UDFLinkedSystemObjectTypesEnum linkObj = 0, Lang lang = Lang.ES, bool UpdateFiled = false, bool DeleteField = false)
        {
            int lErrCode;
            string sErrMsg = "";

            switch (lang)
            {
                case Lang.ES:
                    AddLog($"Agregar el campo - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo}", TypeMess.Atencion, lang);
                    break;

            }


            string strSql = String.Empty;

            if (NomeTabela.Length > 4)
                if (NomeTabela.Contains("TAX4"))
                    if (!NomeTabela.Contains("@"))
                        NomeTabela = "@" + NomeTabela;


            strSql = string.Format(@"select ""FieldID""
                                                from CUFD 
                                                where ""TableID"" = '{0}' 
                                                    and ""AliasID"" = '{1}'", NomeTabela, NomeCampo);




            //0 - Campo Não exite
            //1 - Campos Existe
            dynamic resultado = ExecuteSqlScalar(strSql);

            UserFieldsMD oUserField;
            oUserField = (UserFieldsMD)SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);

            if (resultado != null && UpdateFiled)
            {
                oUserField.GetByKey(NomeTabela, Convert.ToInt32(resultado));
            }
            if (resultado != null && !UpdateFiled)
            {
                switch (lang)
                {
                    case Lang.ES:
                        AddLog($"Advertencia al agregar el campo - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo} :  ya existe en la base de datos", TypeMess.Atencion, lang);
                        break;

                }

                if (oUserField != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserField);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    oUserField = null;
                }

                return;
            }
            try
            {


                oUserField.TableName = NomeTabela.Replace("@", "").Replace("[", "").Replace("]", "").Trim();

                if (NomeCampo.Length > 25)
                {
                    switch (lang)
                    {
                        case Lang.ES:
                            throw new Exception("¡El tamaño del nombre de campo es mayor de lo permitido!");
                            break;

                    }

                }

                oUserField.Name = NomeCampo;
                if (Tamanho != 0)
                    oUserField.EditSize = Tamanho;

                if (DescCampo.Length > 30)
                    DescCampo = DescCampo.Substring(0, 30);

                oUserField.Description = DescCampo;
                oUserField.Type = Tipo;
                oUserField.SubType = SubTipo;
                oUserField.DefaultValue = valorDefault;
                if (linkObj != 0)
                    oUserField.LinkedSystemObject = linkObj;
                if (!string.IsNullOrEmpty(linkedTable))
                    oUserField.LinkedTable = linkedTable;

                //adicionar valores válidos
                if (valoresValidos != null)
                {
                    Int32 qtd = valoresValidos.GetLength(0);
                    if (qtd > 0)
                    {
                        for (int i = 0; i < qtd; i++)
                        {
                            oUserField.ValidValues.Value = valoresValidos[i, 0];
                            oUserField.ValidValues.Description = valoresValidos[i, 1];
                            oUserField.ValidValues.Add();
                        }
                    }
                }

                if (resultado != null && UpdateFiled)
                {
                    oUserField.Update();
                }
                else
                {
                    oUserField.Add();
                }

                SBO_Company.GetLastError(out lErrCode, out sErrMsg);
                if (lErrCode != 0)
                {

                    switch (lang)
                    {
                        case Lang.ES:
                            if (sErrMsg.Contains("(ODBC -2035)"))
                                AddLog($"Advertencia al agregar el campo - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo} : {sErrMsg}", TypeMess.Atencion, lang);
                            else
                                AddLog($"Errorr al agregar el campo - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo} : {sErrMsg}", TypeMess.Error, lang);
                            break;

                    }

                    //throw new Exception(sErrMsg);
                }
                else
                {
                    switch (lang)
                    {
                        case Lang.ES:
                            AddLog($"Éxito al agregar el campo - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo}", TypeMess.Exito, lang);
                            break;

                    }

                }

            }
            catch (Exception e)
            {
                switch (lang)
                {
                    case Lang.ES:
                        AddLog($"Errorr al agregar el campo - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo} : {e.Message}", TypeMess.Error, lang);
                        break;

                }


            }
            finally
            {
                if (oUserField != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserField);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    oUserField = null;
                }

            }
            if (resultado != null)
            {

                switch (lang)
                {
                    case Lang.ES:
                        AddLog($"Campo existente - Tabla: {NomeTabela} - Campo: {NomeCampo} - {DescCampo}", TypeMess.Atencion, lang);
                        break;

                }

                if (oUserField != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserField);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    oUserField = null;
                }

            }

            if (oUserField != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserField);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                oUserField = null;
            }
        }

        /// <summary>
        /// Adiciona tabela de usuário à Base do B1
        /// </summary>
        /// <param name="NomeTB">Nome da tabela a ser criada</param>
        /// <param name="Desc">Descrição da tabela a ser criada</param>
        public void AddUserTable(string NomeTB, string Desc, BoUTBTableType oTableType, Lang lang, bool update = false)
        {
            int lErrCode;
            string sErrMsg = "";

            switch (lang)
            {
                case Lang.ES:
                    AddLog($"Agregar tabla de usuario - Tabla: {NomeTB} - Escribe: {oTableType}", TypeMess.Atencion, lang);
                    break;

            }

            UserTablesMD oUserTable = (UserTablesMD)SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);


            try
            {
                if (!oUserTable.GetByKey(NomeTB))
                {
                    oUserTable.TableName = NomeTB.Replace("@", "").Replace("[", "").Replace("]", "").Trim();
                    oUserTable.TableDescription = Desc;
                    oUserTable.TableType = oTableType;

                    try
                    {
                        if (oUserTable.Add() != 0)
                        {
                            SBO_Company.GetLastError(out lErrCode, out sErrMsg);
                            switch (lang)
                            {
                                case Lang.ES:
                                    if (sErrMsg.Contains("(ODBC -2035)"))
                                        AddLog($"Advertencia al agregar el tabla: {NomeTB.Replace("@", "").Replace("[", "").Replace("]", "").Trim()} - Mensage : {sErrMsg}", TypeMess.Atencion, lang);
                                    else
                                        AddLog($"Errorr al agregar el tabla: {NomeTB.Replace("@", "").Replace("[", "").Replace("]", "").Trim()} - Errorr : {sErrMsg}", TypeMess.Error, lang);
                                    break;

                            }

                        }
                        else
                        {
                            switch (lang)
                            {
                                case Lang.ES:
                                    AddLog($"Éxito al crear tabla de usuario - Tabla: {NomeTB} - Escribe: {oTableType} : {sErrMsg}", TypeMess.Exito, lang);
                                    break;

                            }

                        }
                    }
                    catch (Exception e)
                    {
                        switch (lang)
                        {
                            case Lang.ES:
                                AddLog($"Errorr al crear tabla de usuario - Tabla: {NomeTB} - Escribe: {oTableType} : {e.Message}", TypeMess.Error, lang);
                                break;

                        }
                    }
                    finally
                    {
                        if (oUserTable != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTable);
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            GC.Collect();
                            oUserTable = null;
                        }
                    }
                }
                else if (update && oUserTable.GetByKey(NomeTB))
                {

                    oUserTable.TableType = oTableType;

                    if (oUserTable.Update() != 0)
                    {
                        SBO_Company.GetLastError(out lErrCode, out sErrMsg);
                        switch (lang)
                        {
                            case Lang.ES:
                                AddLog($"Errorr al actualizar tabla de usuario - Tabla: {NomeTB} - Escribe: {oTableType} : {sErrMsg}", TypeMess.Error, lang);
                                break;

                        }
                    }
                    else
                    {
                        switch (lang)
                        {
                            case Lang.ES:
                                AddLog($"Éxito al actualizar tabla de usuario - Tabla: {NomeTB} - Escribe: {oTableType} : {sErrMsg}", TypeMess.Exito, lang);
                                break;

                        }

                    }

                    if (oUserTable != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTable);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                        oUserTable = null;
                    }
                }
                else if (!update && oUserTable.GetByKey(NomeTB))
                {
                    switch (lang)
                    {
                        case Lang.ES:
                            AddLog($"¡Tabla de datos existente! - Tabla: {NomeTB} - Escribe: {oTableType} ", TypeMess.Atencion, lang);
                            break;

                    }
                    if (oUserTable != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTable);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                        oUserTable = null;
                    }
                }
            }
            catch (Exception e)
            {
                switch (lang)

                {
                    case Lang.ES:
                        AddLog($"Errorr al crear tabla de usuario - Tabla: {NomeTB} - Escribe: {oTableType} : {e.Message}", TypeMess.Error, lang);
                        break;

                }
                if (oUserTable != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTable);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    oUserTable = null;
                }


            }
            if (oUserTable != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTable);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                oUserTable = null;
            }

        }
        protected bool UserField_Exists(string TableName, string FieldName)
        {
            string strSql = "Select Count(*) From \"CUFD\" Where \"TableID\" = '" + TableName + "' And \"AliasID\" = '" + FieldName.Replace("U_", "") + "'";
            return (int)ExecuteSqlScalar(strSql) == 0 ? false : true;
        }
        protected void Remove_UserFields(string TableName, string FieldName, Lang lang)
        {
            if (FieldName.Contains("U_"))
                FieldName = FieldName.Replace("U_", "");

            Recordset oRec = (Recordset)SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            switch (lang)
            {
                case Lang.ES:
                    AddLog($"Borrando campo de usuario - Tabla: {TableName} - Escribe: {FieldName}", TypeMess.Atencion, lang);
                    break;

            }

            if (this.UserField_Exists(TableName, FieldName))
            {

                oRec.DoQuery("Select \"TableID\",\"FieldID\" From \"CUFD\" Where \"TableID\" = '" + TableName + "' And \"AliasID\" = '" + FieldName + "'");
                string TableId = (string)oRec.Fields.Item("TableID").Value;
                string FieldId = Convert.ToString(oRec.Fields.Item("FieldID").Value);

                if (oRec != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRec);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    oRec = null;
                }


                UserFieldsMD oUserFieldsMD = (UserFieldsMD)SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                oUserFieldsMD.GetByKey(TableId, Convert.ToInt32(FieldId));

                try
                {
                    if (oUserFieldsMD.Remove() != 0)
                        switch (lang)

                        {
                            case Lang.ES:
                                AddLog($"Errorr al borrar campo de tabla de usuario - Tabla: {TableId} - Campo: {FieldId} : {SBO_Company.GetLastErrorDescription()}", TypeMess.Error, lang);
                                break;

                        }
                    else
                    {
                        switch (lang)

                        {
                            case Lang.ES:
                                AddLog($"¡Campo eliminado con éxito! - Tabla: {TableId} - Campo: {FieldId} : {SBO_Company.GetLastErrorDescription()}", TypeMess.Exito, lang);
                                break;

                        }

                    }

                }
                catch (Exception e)
                {
                    switch (lang)

                    {
                        case Lang.ES:
                            AddLog($"Errorr al crear tabla de usuario - Tabla: {TableId} - Escribe: {TableId} : {e.Message}", TypeMess.Error, lang);
                            break;

                    }


                }
                finally
                {
                    if (oRec != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oRec);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                        oRec = null;
                    }
                    if (oUserFieldsMD != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldsMD);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                        oUserFieldsMD = null;
                    }

                }


            }

            if (oRec != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRec);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                oRec = null;
            }
        }

        private string AddDadosTax4Uf(string tax4Uf, string tax4Cod)
        {
            try
            {
                Recordset oRec4 = (Recordset)SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                string query = $@"select IFNULL(MAX(CAST(""Code"" as int)), 0) + 1 as ""Code"", IFNULL(MAX(CAST(""Name"" as int)), 0) + 1 as ""Name""  from ""@TAX4_UF""";
                oRec4.DoQuery(query);
                string code = "0";
                string name = "0";
                if (oRec4.RecordCount > 0)
                {
                    code = oRec4.Fields.Item("Code").Value.ToString();
                    name = oRec4.Fields.Item("Name").Value.ToString();
                    query = $@"insert into 
	                                ""@TAX4_UF""(""Code"",""Name"",""U_TAX4_Uf"",""U_TAX4_Cod"")
                                    values({code},{name},'{tax4Uf}','{tax4Cod}')";
                    oRec4.DoQuery(query);
                    return code;
                }
                else
                {
                    return "Error ao buscar Code na tabela @TAX4_UF";
                }

            }
            catch (Exception ex)
            {
                return "Error - " + ex.Message;
            }

        }

        private string AddDadosViaTransp(string ViaTransp)
        {
            try
            {
                Recordset oRec4 = (Recordset)SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                string query = $@"select IFNULL(MAX(CAST(""Code"" as int)), 0) + 1 as ""Code"", IFNULL(MAX(CAST(""Name"" as int)), 0) + 1 as ""Name""  from ""@TAX4_VT""";
                oRec4.DoQuery(query);
                string code = "0";
                string name = "0";
                if (oRec4.RecordCount > 0)
                {
                    code = oRec4.Fields.Item("Code").Value.ToString();
                    name = oRec4.Fields.Item("Name").Value.ToString();
                    query = $@"insert into 
	                                ""@TAX4_VT""(""Code"",""Name"",""U_TAX4_VT"")
                                    values({code},{name},'{ViaTransp}')";
                    oRec4.DoQuery(query);
                    return code;
                }
                else
                {
                    return "Error ao buscar Code na tabela @TAX4_UF";
                }

            }
            catch (Exception ex)
            {
                return "Error - " + ex.Message;
            }

        }

        private string AddDadosFormaImp(string FormaImp)
        {
            try
            {
                Recordset oRec4 = (Recordset)SBO_Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                string query = $@"select IFNULL(MAX(CAST(""Code"" as int)), 0) + 1 as ""Code"", IFNULL(MAX(CAST(""Name"" as int)), 0) + 1 as ""Name""  from ""@TAX4_FDI""";
                oRec4.DoQuery(query);
                string code = "0";
                string name = "0";
                if (oRec4.RecordCount > 0)
                {
                    code = oRec4.Fields.Item("Code").Value.ToString();
                    name = oRec4.Fields.Item("Name").Value.ToString();
                    query = $@"insert into 
	                                ""@TAX4_FDI""(""Code"",""Name"",""U_TAX4_FI"")
                                    values({code},{name},'{FormaImp}')";
                    oRec4.DoQuery(query);
                    return code;
                }
                else
                {
                    return "Error ao buscar Code na tabela @TAX4_UF";
                }

            }
            catch (Exception ex)
            {
                return "Error - " + ex.Message;
            }

        }

        #endregion
    }
}
