using SAPbobsCOM;


namespace AddOnFE_Facturatech.Methods
{
    public partial class CreateTbls : DB
    {
        public CreateTbls()
        {

        }
        SAPbobsCOM.Recordset oRecordset;
        Lang lang;
        string[,] validValues = null;
        public void Metadatos()
        {
            #region Tabla parametrización
            AddUserTable("FEDIAN_PARAMG", "Parametrizacion General", BoUTBTableType.bott_NoObject, lang: lang);
            validValues = new string[5, 2];
            validValues[0, 0] = "C";
            validValues[0, 1] = "Carvajal";
            validValues[1, 0] = "CC";
            validValues[1, 1] = "Certicamara";
            validValues[2, 0] = "D";
            validValues[2, 1] = "Dispapeles";
            validValues[3, 0] = "F";
            validValues[3, 1] = "Febos";
            validValues[4, 0] = "FT";
            validValues[4, 1] = "Facturatech";
            AddUserField("FEDIAN_PARAMG", "Proveedor", "Proveedor", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, validValues, null, null, 0, lang: lang);
            AddUserField("FEDIAN_PARAMG", "NIT_Emisor", "NIT Emisor", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 17, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_PARAMG", "Email_Usuario", "Usuario Portal", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 60, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_PARAMG", "Clave_Usuario", "Clave Portal", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_PARAMG", "Token", "Token", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, null, null, null, 0, lang: lang);
            validValues = new string[2, 2];
            validValues[0, 0] = "01";
            validValues[0, 1] = "Produccion";
            validValues[1, 0] = "02";
            validValues[1, 1] = "Pruebas";
            AddUserField("FEDIAN_PARAMG", "Ambiente", "Ambiente", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, validValues, null, null, 0, lang: lang);
            AddUserField("FEDIAN_PARAMG", "idEmpresa", "ID Empresa", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_PARAMG", "NReenvios", "Numero de Reenvios", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_PARAMG", "IReenvios", "Intervalo de Reenvios", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            #endregion

            #region tabla Configuracion Interfaces
            AddUserTable("FEDIAN_INTERF_CFG", "Configuracion Interfaces", BoUTBTableType.bott_NoObject, lang: lang);
            AddUserField("FEDIAN_INTERF_CFG", "URL", "URL Proveedor", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 200, null, null, null, 0, lang: lang);
            #endregion

            #region tabla Codigos de documento DIAN
            AddUserTable("FEDIAN_CODDOC", "Codigos de documento DIAN", BoUTBTableType.bott_NoObject, lang: lang);
            #endregion

            #region tabla Monitor FE DIAN
            AddUserTable("FEDIAN_MONITORLOG", "Monitor FE DIAN", BoUTBTableType.bott_NoObject, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "DocType", "Tipo documento", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Folio", "Numero Documento", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Prefijo", "Prefijo", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "ObjType", "Tipo Objeto", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "DocNum", "Numero Interno", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Resultado", "Descripcion Estado", BoFieldTypes.db_Memo, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Status", "Codigo Estado", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "ProcessID", "ID Proceso", BoFieldTypes.db_Memo, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Fecha_Envio", "Fecha Envio", BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Hora_Envio", "Hora Envio", BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_Time, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Usuario_Envio", "Usaurio Envio", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Fecha_ReEnvio", "Fecha Re-Envio", BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Hora_ReEnvio", "Hora Re-Envio", BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_Time, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Usuario_ReEnvio", "Usaurio Re-Envio", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Det_Peticion", "Detalle Peticion", BoFieldTypes.db_Memo, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Respuesta_Int", "Respuesta Interfaz", BoFieldTypes.db_Memo, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Archivo_PDF", "Archivo PDF", BoFieldTypes.db_Memo, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "Enlace_XML", "Enlace XML", BoFieldTypes.db_Memo, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "ID_Seguimiento", "ID Seguimiento", BoFieldTypes.db_Memo, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_MONITORLOG", "NReenvios", "Numero Reenvios", BoFieldTypes.db_Memo, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null, 0, lang: lang);

            #endregion

            #region tabla Version FE DIAN
            AddUserTable("FEDIAN_VERSION", "Version FE DIAN", BoUTBTableType.bott_NoObject, lang: lang);
            #endregion

            #region Campos Factura venta
            AddUserField("OINV", "SEI_FEConcepNC", "(FE) Concepto Nota Credito", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "FEDIAN_CONCEP_NC", 0, lang: lang);
            AddUserField("OINV", "SEI_FEConcepND", "(FE) Concepto Nota Debito", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "FEDIAN_CONCEP_ND", 0, lang: lang);
            AddUserField("OINV", "SEI_FEConcepND", "(FE) Concepto Nota Ajuste", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "FEDIAN_CONCEP_NA", 0, lang: lang);
            AddUserField("OINV", "SEI_FEMedPago", "(FE) Medio de pago", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "FEDIAN_MEDPAGO", 0, lang: lang);
            AddUserField("OINV", "SEI_FETipOper", "(FE) Tipo de Operacion", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "FEDIAN_TIPOPERA", 0, lang: lang);
            AddUserField("OINV", "SEI_FEDescu", "(FE) Concepto descuento", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "FEDIAN_DESCU", 0, lang: lang);
            AddUserField("OINV", "SEI_FEIncoTerm", "(FE) IncoTerms", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "FEDIAN_INCOTERMS", 0, lang: lang);
            validValues = new string[2, 2];
            validValues[0, 0] = "Y";
            validValues[0, 1] = "Si";
            validValues[1, 0] = "N";
            validValues[1, 1] = "No";
            AddUserField("OINV", "SEI_Export", "(FE) Factura Exportacion", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, validValues, null, null, 0, lang: lang);

            AddUserField("INV1", "SEI_FEDescu", "(FE) Concepto descuento", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "FEDIAN_DESCU", 0, lang: lang);
            AddUserField("INV1", "SEI_FEUMDIAN", "(FE) Unidad de Medida DIAN", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, null, 0, lang: lang);
            validValues = new string[1, 2];
            validValues[0, 0] = "01";
            validValues[0, 1] = "Valor Comercial";
            AddUserField("INV1", "SEI_FETipPrec", "(FE) Tipo de Precio", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, validValues, "01", null, 0, lang: lang);

            AddUserField("ORIN", "SEI_MotivoN", "(FE) Motivo Nota de crédito/débito", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, null, 0, lang: lang);
            AddUserField("ORIN", "SEI_FechaInicio", "(FE) Fecha Inicial Mes", BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, null, 0, lang: lang);
            AddUserField("ORIN", "SEI_FechaFin", "(FE) Fecha Final Mes", BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, null, 0, lang: lang);



            AddUserField("OSTC", "SEI_FETributo", "(FE) Identificador Tributo", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "FEDIAN_TRIBU", 0, lang: lang);
            AddUserField("OWHT", "SEI_FETributo", "(FE) Identificador Tributo", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "FEDIAN_TRIBU", 0, lang: lang);
            AddUserField("OITM", "SEI_FEIdent", "(FE) Identificacion del Articulo", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "FEDIAN_IDENT_ARTI", 0, lang: lang);
            AddUserField("OITM", "SEI_FEModelo", "(FE) Modelo", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "FEDIAN_IDENT_ARTI", 0, lang: lang);
            AddUserField("OADM", "SEI_FEActEco", "(FE) Actividad Economica", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null, 0, lang: lang);
            AddUserField("OADM", "SEI_RYT", "(FE) Responsabilidades y Tributos", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null, 0, lang: lang);
            AddUserField("OADM", "SEI_FETipDoc", "(FE) Tipo de documento", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null, 0, lang: lang);
            validValues = new string[2, 2];
            validValues[0, 0] = "01";
            validValues[0, 1] = "Residente";
            validValues[1, 0] = "02";
            validValues[1, 1] = "No Residente";
            AddUserField("OADM", "SEI_FEProced", "(FE) Tipo Procedencia", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, validValues, null, null, 0, lang: lang);
            validValues = new string[3, 2];
            validValues[0, 0] = "48";
            validValues[0, 1] = "Responsable del Impuesto sobre las ventas - IVA";
            validValues[1, 0] = "49";
            validValues[1, 1] = "No responsables del IVA";
            validValues[2, 0] = "No Aplica";
            validValues[2, 1] = "No aplica";
            AddUserField("OADM", "SEI_FERegFis", "(FE) Regimen Fiscal", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, validValues, null, null, 0, lang: lang);
            validValues = new string[3, 2];
            validValues[0, 0] = "48";
            validValues[0, 1] = "Responsable del Impuesto sobre las ventas - IVA";
            validValues[1, 0] = "49";
            validValues[1, 1] = "No responsables del IVA";
            validValues[2, 0] = "No Aplica";
            validValues[2, 1] = "No aplica";
            AddUserField("OCRD", "SEI_FERegFis", "(FE) Regimen Fiscal", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, validValues, null, null, 0, lang: lang);

            AddUserField("OADM", "SEI_LYFAC", "(FE) Texto Encabezado", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null, 0, lang: lang);
            AddUserField("OADM", "HBT_MunMed", "Municipio", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, "HBT_MUNICIPIO", 0, lang: lang);
            AddUserField("OADM", "HBT_TipEnt", "Tipo de entidad", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, null, 0, lang: lang);
            validValues = new string[2, 2];
            validValues[0, 0] = "1";
            validValues[0, 1] = "Total sin retenciones";
            validValues[1, 0] = "2";
            validValues[1, 1] = "Total con retenciones";
            AddUserField("OADM", "SEI_VALETRAS", "(FE) Valores en letras", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 10, validValues, "1", null, 0, lang: lang);

            #endregion

            #region tabla Codigo de descuentos
            AddUserTable("FEDIAN_DESCU", "Codigo de descuentos", BoUTBTableType.bott_NoObject, lang: lang);
            #endregion

            #region tabla Unidades de medida
            AddUserTable("FEDIAN_UM", "Unidades de medida", BoUTBTableType.bott_NoObject, lang: lang);
            AddUserField("FEDIAN_UM", "Descripcion", "Descripcion DIAN", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 200, null, null, null, 0, lang: lang);
            #endregion

            #region tabla Concepto Nota Credito
            AddUserTable("FEDIAN_CONCEP_NC", "Concepto Nota Credito", BoUTBTableType.bott_NoObject, lang: lang);
            #endregion

            #region tabla Concepto Nota Debito
            AddUserTable("FEDIAN_CONCEP_ND", "Concepto Nota Debito", BoUTBTableType.bott_NoObject, lang: lang);
            #endregion

            #region tabla Medios de Pago
            AddUserTable("FEDIAN_MEDPAGO", "Medios de Pago", BoUTBTableType.bott_NoObject, lang: lang);
            #endregion

            #region tabla Responsabilidades Fiscales
            AddUserTable("FEDIAN_RESPONSA", "Responsabilidades Fiscales", BoUTBTableType.bott_NoObject, lang: lang);
            #endregion

            #region tabla Tributos
            AddUserTable("FEDIAN_TRIBU", "Tributos", BoUTBTableType.bott_NoObject, lang: lang);
            #endregion

            #region tabla Tipos de Operacion
            AddUserTable("FEDIAN_TIPOPERA", "Tipos de Operacion", BoUTBTableType.bott_NoObject, lang: lang);
            #endregion

            #region tabla Homomlogacion UM
            AddUserTable("FEDIAN_HOMOL_UM", "Homomlogacion UM", BoUTBTableType.bott_NoObject, lang: lang);
            AddUserField("FEDIAN_HOMOL_UM", "SAP_UM", "Unidad de medida SAP", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_HOMOL_UM", "DIAN_UM", "Unidad de medida DIAN", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            #endregion

            #region tabla Identificacion del Articulo
            AddUserTable("FEDIAN_IDENT_ARTI", "Identificacion del Articulo", BoUTBTableType.bott_NoObject, lang: lang);
            #endregion

            #region tabla Respons y Tributos SN
            AddUserTable("FEDIAN_SN", "Respons y Tributos SN", BoUTBTableType.bott_MasterData, lang: lang);
            #endregion

            #region tabla Responsabilidades SN
            AddUserTable("FEDIAN_SNRES", "Responsabilidades SN", BoUTBTableType.bott_MasterDataLines, lang: lang);
            AddUserField("FEDIAN_SNRES", "Codigo", "Codigo Responsabilidad", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_SNRES", "Desc", "Descripcion Responsabilidad", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, null, null, null, 0, lang: lang);
            #endregion

            #region tabla Tributos SN
            AddUserTable("FEDIAN_SNTRI", "Tributos SN", BoUTBTableType.bott_MasterDataLines, lang: lang);
            AddUserField("FEDIAN_SNTRI", "Codigo", "Codigo Tributo", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_SNTRI", "Desc", "Descripcion Tributo", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, null, null, null, 0, lang: lang);
            #endregion

            #region tabla Numeracion Autorizada DIAN
            AddUserTable("FEDIAN_NUMAUTORI", "Numeracion Autorizada DIAN", BoUTBTableType.bott_NoObject, lang: lang);
            AddUserField("FEDIAN_NUMAUTORI", "DocDIAN", "Tipo de documento", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, "FEDIAN_CODDOC", 0, lang: lang);
            AddUserField("FEDIAN_NUMAUTORI", "NumResol", "Numero Resolucion", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_NUMAUTORI", "FechaResol", "Fecha Resolucion", BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_None, 30, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_NUMAUTORI", "ClaveTec", "Clave Tecnica", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_NUMAUTORI", "FechaDesde", "Fecha Desde", BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_NUMAUTORI", "FechaHasta", "Fecha Hasta", BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_NUMAUTORI", "InitialNum", "Secuencia Inicio", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 15, null, null, null, 0, lang: lang);
            AddUserField("FEDIAN_NUMAUTORI", "LastNum", "Secuencia Final", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 15, null, null, null, 0, lang: lang);
            //AddUserField("FEDIAN_NUMAUTORI", "posicionXCufe", "posicionXCufe", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 15, null, null, null, 0, lang: lang);
            //AddUserField("FEDIAN_NUMAUTORI", "posicionYCufe", "posicionYCufe", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 15, null, null, null, 0, lang: lang);
            //AddUserField("FEDIAN_NUMAUTORI", "rotacionCufe", "rotacionCufe", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 15, null, null, null, 0, lang: lang);
            //AddUserField("FEDIAN_NUMAUTORI", "fuenteCufe", "fuenteCufe", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 15, null, null, null, 0, lang: lang);
            //AddUserField("FEDIAN_NUMAUTORI", "posicionXQr", "posicionXQr", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 15, null, null, null, 0, lang: lang);
            //AddUserField("FEDIAN_NUMAUTORI", "posicionYQr", "posicionYQr", BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 15, null, null, null, 0, lang: lang);
            #endregion

            #region tabla Cond de Entrega (INCOTERMS)
            AddUserTable("FEDIAN_INCOTERMS", "Cond de Entrega (INCOTERMS)", BoUTBTableType.bott_NoObject, lang: lang);
            #endregion

        }
    }
}
