using SAPbouiCOM.Framework;
using System;
using Application = SAPbouiCOM.Framework.Application;
using AddOnFE_Facturatech.Interfaz;

namespace AddOnFE_Facturatech
{
    class Menu
    {
        public static SAPbouiCOM.Form oForm;
        public static SAPbouiCOM.MenuItem oMenu;
        public void AddMenuItems()
        {
            SAPbouiCOM.Menus oMenus = null;
            SAPbouiCOM.MenuItem oMenuItem = null;

            oMenus = Application.SBO_Application.Menus;

            SAPbouiCOM.MenuCreationParams oCreationPackage = null;
            oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));
            oMenuItem = Application.SBO_Application.Menus.Item("43520"); // moudles'

            string sPath = null;
            //Primer Menu
            sPath = System.Windows.Forms.Application.StartupPath;

            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
            oCreationPackage.UniqueID = "AddOnFE_Facturatech";
            oCreationPackage.String = "Facturación Electrónica";
            oCreationPackage.Enabled = true;
            oCreationPackage.Position = -1;
            oCreationPackage.Image = sPath + "\\Resources\\Menu\\UI.bmp";

            oMenus = oMenuItem.SubMenus;

            try
            {
                // Eliminar el menú si ya existe
                if (Application.SBO_Application.Menus.Exists("AddOnFE_Facturatech"))
                {
                    Application.SBO_Application.Menus.RemoveEx("AddOnFE_Facturatech");
                }
                //  If the manu already exists this code will fail
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception e)
            {
                Application.SBO_Application.SetStatusBarMessage($"Error creando el menú: {e.Message}", SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }

            try
            {
                // Get the menu collection of the newly added pop-up item
                oMenuItem = Application.SBO_Application.Menus.Item("AddOnFE_Facturatech");
                oMenus = oMenuItem.SubMenus;

                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "AddOnFE_Facturatech.Parametrizacion";
                oCreationPackage.String = "Parametrización";
                oCreationPackage.Image = sPath + "\\Resources\\Menu\\parametrizacion.bmp";
                oMenus.AddEx(oCreationPackage);

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "AddOnFE_Facturatech.NumAutori";
                oCreationPackage.String = "Numeraciones Autorizadas";
                oCreationPackage.Image = sPath + "\\Resources\\Menu\\NumAutori.bmp";
                oMenus.AddEx(oCreationPackage);

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "AddOnFE_Facturatech.ConfigInterfaces";
                oCreationPackage.String = "Configuración de Interfaces";
                oCreationPackage.Image = sPath + "\\Resources\\Menu\\configuracion.bmp";
                oMenus.AddEx(oCreationPackage);

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "AddOnFE_Facturatech.TipDocDIAN";
                oCreationPackage.String = "Tipos Doc. DIAN";
                oCreationPackage.Image = sPath + "\\Resources\\Menu\\TiposDoc.bmp";
                oMenus.AddEx(oCreationPackage);

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "AddOnFE_Facturatech.Monitor";
                oCreationPackage.String = "Monitor Facturación Electrónica";
                oCreationPackage.Image = sPath + "\\Resources\\Menu\\Monitor.bmp";
                //oCreationPackage.Image = Environment.CurrentDirectory.ToString() + @"\Resources\Menu\Monitor.bmp";
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception er)
            {
                Application.SBO_Application.SetStatusBarMessage("Menu Already Exists", SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if (pVal.BeforeAction && pVal.MenuUID == "AddOnFE_Facturatech.Parametrizacion")
                {
                    ParametrizacionFE activeForm = new ParametrizacionFE();
                    activeForm.Show();
                }
                if (pVal.BeforeAction && pVal.MenuUID == "AddOnFE_Facturatech.NumAutori")
                {
                    try
                    {
                        oForm = Application.SBO_Application.Forms.Item("FORM_FE_0002");
                        oForm.Visible = true;
                    }
                    catch
                    {
                        oMenu = Application.SBO_Application.Menus.Item("51200");
                        int i;
                        string MenuUID = "";
                        for (i = 0; (i <= (oMenu.SubMenus.Count - 1)); i++)
                        {
                            string tablaname = oMenu.SubMenus.Item(i).String;
                            if (tablaname.Contains("FEDIAN_NUMAUTORI"))
                            {
                                MenuUID = oMenu.SubMenus.Item(i).UID;
                                break;
                            }

                        }
                        Application.SBO_Application.ActivateMenuItem(MenuUID);
                    }
                    //NumAutori activeForm = new NumAutori();
                    //activeForm.Show();
                }
                if (pVal.BeforeAction && pVal.MenuUID == "AddOnFE_Facturatech.ConfigInterfaces")
                {
                    try
                    {
                        oForm = Application.SBO_Application.Forms.Item("FORM_FE_0003");
                        oForm.Visible = true;
                    }
                    catch
                    {
                        oMenu = Application.SBO_Application.Menus.Item("51200");
                        int i;
                        string MenuUID = "";
                        for (i = 0; (i <= (oMenu.SubMenus.Count - 1)); i++)
                        {
                            string tablaname = oMenu.SubMenus.Item(i).String;
                            if (tablaname.Contains("FEDIAN_INTERF_CFG"))
                            {
                                MenuUID = oMenu.SubMenus.Item(i).UID;
                                break;
                            }

                        }
                        Application.SBO_Application.ActivateMenuItem(MenuUID);
                    }
                }
                if (pVal.BeforeAction && pVal.MenuUID == "AddOnFE_Facturatech.TipDocDIAN")
                {
                    try
                    {
                        oForm = Application.SBO_Application.Forms.Item("FORM_FE_0004");
                        oForm.Visible = true;
                    }
                    catch
                    {
                        oMenu = Application.SBO_Application.Menus.Item("51200");
                        int i;
                        string MenuUID = "";
                        for (i = 0; (i <= (oMenu.SubMenus.Count - 1)); i++)
                        {
                            string tablaname = oMenu.SubMenus.Item(i).String;
                            if (tablaname.Contains("FEDIAN_CODDOC"))
                            {
                                MenuUID = oMenu.SubMenus.Item(i).UID;
                                break;
                            }

                        }
                        Application.SBO_Application.ActivateMenuItem(MenuUID);
                    }
                    //TipDocDIAN activeForm = new TipDocDIAN();
                    //activeForm.Show();
                }
                if (pVal.BeforeAction && pVal.MenuUID == "AddOnFE_Facturatech.Monitor")
                {
                    MonitorFE activeForm = new MonitorFE();
                    activeForm.Show();
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }

    }
}
