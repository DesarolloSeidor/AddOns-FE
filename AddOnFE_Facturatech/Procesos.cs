using AddOnFE_Facturatech.Methods;
using SAPbouiCOM;
using System;
using Application = SAPbouiCOM.Framework.Application;

namespace AddOnFE_Facturatech
{
    class Procesos
    {
        public static SAPbouiCOM.Application SBO_Application;
        public static SAPbobsCOM.Company oCompany;
        public static string user = "";
        public Procesos()
        {
            try
            {
                oCompany = Program.SBO_Company;
                SBO_Application = Application.SBO_Application;
                //Creacion de timer para actualziacion de formulario Monitor Log
                StartMonitorSAPB1();
                //Cargue inicial de parametrizacion
                user = Program.SBO_Company.UserName;
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage("Exception " + ex.Message, BoMessageTime.bmt_Medium, false);
                Log.EscribirLogFileTXT("Procesos: " + ex.Message);
            }
        }
        //Definicion timer
        public static void StartMonitorSAPB1()
        {
            #region TimerVerificaEstados
            // Alternate method: create a Timer with an interval argument to the constructor.
            //aTimer = new System.Timers.Timer(2000);

            // Create a timer with a five second interval.
            Variables.aTimer = new System.Timers.Timer(Properties.Settings.Default.TimerStatus);

            // Hook up the Elapsed event for the timer. 
            Variables.aTimer.Elapsed += OnTimedEventStatus;

            // Have the timer fire repeated events (true is the default)
            Variables.aTimer.AutoReset = true;

            // Start the timer
            Variables.aTimer.Enabled = true;
            #endregion TimerVerificaEstados

            #region TimerReSend
            // Alternate method: create a Timer with an interval argument to the constructor.
            //aTimer = new System.Timers.Timer(2000);

            // Create a timer with a five second interval.
            Variables.bTimer = new System.Timers.Timer(Properties.Settings.Default.TimerResend);

            // Hook up the Elapsed event for the timer. 
            Variables.bTimer.Elapsed += OnTimedEventReSend;

            // Have the timer fire repeated events (true is the default)
            Variables.bTimer.AutoReset = true;

            // Start the timer
            Variables.bTimer.Enabled = true;
            #endregion TimerReSend

            #region TimerAddDTE
            // Alternate method: create a Timer with an interval argument to the constructor.
            //aTimer = new System.Timers.Timer(2000);

            // Create a timer with a five second interval.
            Variables.bTimer = new System.Timers.Timer(Properties.Settings.Default.TimerResend);

            // Hook up the Elapsed event for the timer. 
            Variables.bTimer.Elapsed += OnTimedEventAddDTEMonitor;

            // Have the timer fire repeated events (true is the default)
            Variables.bTimer.AutoReset = true;

            // Start the timer
            Variables.bTimer.Enabled = true;
            #endregion TimerAddDTE
        }

        //Timer verificar estado
        public static void OnTimedEventStatus(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (Variables.banderaVerificaEstados == true && Variables.senalActiva == true)
                {
                    Variables.banderaVerificaEstados = false;
                    Send.Verifystatus();
                    Variables.banderaVerificaEstados = true;
                }
            }
            catch (Exception ex)
            {
                Log.EscribirLogFileTXT("TimerVerificaEstados: " + ex.Message);
                Variables.banderaVerificaEstados = true;
            }
        }

        //Timer Reenviar Fallidos
        public static void OnTimedEventReSend(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (Variables.banderaReenviar == true && Variables.senalActiva == true)
                {
                    Variables.banderaReenviar = false;
                    //AutoReSend();
                    Variables.banderaReenviar = true;
                }
            }
            catch (Exception ex)
            {
                Log.EscribirLogFileTXT("OnTimedEventReSend: " + ex.Message);
                Variables.banderaReenviar = true;
            }
        }

        //Timer Agregar DTE al monitor
        public static void OnTimedEventAddDTEMonitor(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (Variables.banderaAgregarDoc == true && Variables.senalActiva == true)
                {
                    Variables.banderaAgregarDoc = false;
                    General.AddDTEMonitor();
                    Variables.banderaAgregarDoc = true;
                }
            }
            catch (Exception ex)
            {
                Log.EscribirLogFileTXT("OnTimedEventAddDTEMonitor: " + ex.Message);
                Variables.banderaAgregarDoc = true;
            }
        }

    }
}
