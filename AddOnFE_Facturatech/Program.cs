using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using AddOnFE_Facturatech.Interfaz;
using AddOnFE_Facturatech.Methods;
using Application = SAPbouiCOM.Framework.Application;

namespace AddOnFE_Facturatech
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static SAPbobsCOM.Company SBO_Company;
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application oApp = null;
                if (args.Length < 1)
                {
                    oApp = new Application();
                }
                else
                {
                    //If you want to use an add-on identifier for the development license, you can specify an add-on identifier string as the second parameter.
                    //oApp = new Application(args[0], "XXXXX");
                    oApp = new Application(args[0]);
                }
                Application.SBO_Application.StatusBar.SetText($"Inicio del Add-On FE DIAN ...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                SBO_Company = (SAPbobsCOM.Company)Application.SBO_Application.Company.GetDICompany();

                bool CargueIni = General.version(SBO_Company);

                if (CargueIni == true)
                {
                    CreateTbls ct = new CreateTbls();
                    ct.Metadatos();
                    Application.SBO_Application.StatusBar.SetText("Creacion de tablas y campos de usuario", SAPbouiCOM.BoMessageTime.bmt_Long, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    Tables oTables = null;
                    oTables = new Tables(SBO_Company, Application.SBO_Application, CargueIni);
                    //oConnection.SBO_Application.StatusBar.SetText("Creacion de tablas y campos de usuario ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                }

                Menu MyMenu = new Menu();
                MyMenu.AddMenuItems();

                General.CargueInicial();
                oApp.RegisterMenuEventHandler(MyMenu.SBO_Application_MenuEvent);

                Procesos oProcesos = null;
                oProcesos = new Procesos();

                Application.SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
                Application.SBO_Application.StatusBar.SetText($"Carga Satisfactoria del Add-On FE DIAN", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                oApp.Run();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        static void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    break;
                default:
                    break;
            }
        }
    }
}
