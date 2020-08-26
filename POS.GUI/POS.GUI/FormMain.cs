using Ionic.Zip;
using Microsoft.Win32;
using OKPOS.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.GUI
{
    public partial class FormMain : Form
    {
        #region Variables Globales        
        bool programaInstalado = false;
        string versionParaInstalar = "";
        string folderApplication = Application.LocalUserAppDataPath;
        string status = "";
        WebClient webClient;
        Stopwatch sw = new Stopwatch();
        #endregion
        public FormMain()
        {
            InitializeComponent();
            NetworkChange.NetworkAddressChanged += new
            NetworkAddressChangedEventHandler(AddressChangedCallback);
        }

        private void AddressChangedCallback(object sender, EventArgs e)
        {
            try
            {                
                throw new Exception("La conexión ha experimentado cambios. Asegurese que tiene conexión estable a internet.");
            }
            catch (Exception ex)
            {
                CheckForIllegalCrossThreadCalls = false;
                labelMensaje.Text = ex.Message;
                controlWaitBox1.SendToBack();
                buttonReintentar.Visible = true;
            }
        }

        public static string DownloadString(string address)
        {
            WebClient client = new WebClient();
            string reply = client.DownloadString(address);
            return reply;
        }

        private void backgroundWorkerBuscarActualizacion_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                versionParaInstalar = DownloadString("http://okpos.sufacturafacil.com/version.html");
            }
            catch (Exception ex)
            {
                System.Threading.Thread.Sleep(1000);
                versionParaInstalar = ReadRegistry("VERSION");
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private string ReadRegistry(string param)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\OKPOS2019");
            if (key != null)
            {
                if (key.GetValue(param) != null)
                {
                    return key.GetValue(param).ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        private void WriteRegistry(string name, string value)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\OKPOS2019");
            key.SetValue(name, value);
            key.Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            var version = ReadRegistry("VERSION");

            if (string.IsNullOrEmpty(version))
            {
                WriteRegistry("VERSION", "NO INSTALADA");
                version = ReadRegistry("VERSION");
            }

            if (version != "NO INSTALADA")
            {
                programaInstalado = true;
            }

            if (programaInstalado)
            {
                labelactual.Text = version;
            }
            else
            {
                labelactual.Text = version;
            }

            labelMensaje.Text = "";
            buttonReintentar.Visible = false;
            controlWaitBox1.Mostrar("Verificando requerimientos de la aplicación. Espere por favor...");
            backgroundWorkerBuscarActualizacion.RunWorkerAsync();
        }

        public void DownloadFile(string urlAddress, string location)
        {
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                Uri URL = urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri("http://" + urlAddress);
                sw.Start();

                try
                {
                    webClient.DownloadFileAsync(URL, location);
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Response.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            controlWaitBox1.ActualizarMensaje(string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00")), e.ProgressPercentage, string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00")));
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();

            if (e.Error != null)
            {
                if (e.Error is WebException)
                {
                    var httpWebResponse = (HttpWebResponse)((WebException)e.Error).Response;
                    if (httpWebResponse.StatusCode == HttpStatusCode.NotFound)
                    {
                        controlWaitBox1.Ocultar();
                        labelMensaje.Text = "La publicación no está correcta. No se puede descargar " + versionParaInstalar + ".zip";
                        buttonReintentar.Visible = true;
                    }
                }
                else
                {
                    controlWaitBox1.Ocultar();
                    labelMensaje.Text = e.Error.Message;
                    buttonReintentar.Visible = true;
                }
            }
            else
            {
                if (e.Cancelled == true)
                {
                    status = "canceled";
                }
                else
                {
                    status = "completed";

                    #region Nueva Region
                    if (status == "completed")
                    {
                        controlWaitBox1.ActualizarMensaje("Realizando últimos trabajos de actualización.");
                        backgroundWorkerLanzarApp.RunWorkerAsync("UPDATE_AND_RUN");
                    }
                    else
                    {
                        throw new Exception("No se ha podido descargar la actualización.");
                    }
                    #endregion
                }
            }
        }

        private void backgroundWorkerBuscarActualizacion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                {
                    if (versionParaInstalar == "NO INSTALADA")
                    {
                        controlWaitBox1.Ocultar();
                        buttonReintentar.Visible = true;
                        labelMensaje.Text = "OKPOS no se ha podido instalar. Verifique su conexión.";
                        return;
                    }
                    else if (versionParaInstalar != labelactual.Text)
                    {
                        if (OKPOSEjecutandose())
                        {
                            throw new Exception("El sistema tiene una nueva actualización, debe cerrar todas las instancias que esten ejecutandose de OKPOS2019.");
                        }

                        DialogResult dlg = MessageBox.Show(string.Format("Se ha encontrado la versión {0} disponible. ¿Desea instalarla?", versionParaInstalar), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dlg == DialogResult.Yes)
                        {
                            var direcctorioTrabajo = folderApplication + "\\" + versionParaInstalar;
                            var filetodownload = folderApplication + "\\" + versionParaInstalar + "\\" + versionParaInstalar + ".zip";
                            if (!Directory.Exists(direcctorioTrabajo))
                            {
                                Directory.CreateDirectory(direcctorioTrabajo);
                            }

                            List<string> strFiles = Directory.GetFiles(direcctorioTrabajo, "*", SearchOption.AllDirectories).ToList();
                            foreach (string fichero in strFiles)
                            {
                                File.Delete(fichero);
                            }

                            string remoteUri = "http://okpos.sufacturafacil.com//Publicaciones/";
                            string fileName = versionParaInstalar + ".zip", myStringWebResource = null;
                            WebClient myWebClient = new WebClient();

                            myStringWebResource = remoteUri + fileName;

                            DownloadFile(myStringWebResource, filetodownload);
                        }
                        else
                        {
                            controlWaitBox1.ActualizarMensaje("Iniciando OKPOS sin actualizar.");
                            backgroundWorkerLanzarApp.RunWorkerAsync("RUN_APP");
                        }
                    }
                    else
                    {
                        controlWaitBox1.ActualizarMensaje("Iniciando aplicación un momento por favor...");
                        backgroundWorkerLanzarApp.RunWorkerAsync("RUN_APP");
                    }
                }
                else
                {
                    buttonReintentar.Visible = true;
                }

            }
            catch (FieldAccessException ex)
            {
                controlWaitBox1.Ocultar();
                labelMensaje.Text = ex.Message;
                buttonReintentar.Visible = true;

            }
            catch (Exception ex)
            {
                controlWaitBox1.Ocultar();
                labelMensaje.Text = ex.Message;
                buttonReintentar.Visible = true;

            }
        }

        private bool OKPOSEjecutandose()
        {
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                if (p.ProcessName == "OKPOS")
                {
                    return true;
                }
            }
            return false;
        }

        private void backgroundWorkerLanzarApp_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (e.Argument.ToString() == "UPDATE_AND_RUN")
                {
                    var direcctorioTrabajo = folderApplication + "\\" + versionParaInstalar;
                    if (DescomprimirArchivo(direcctorioTrabajo, versionParaInstalar + ".zip"))
                    {
                        WriteRegistry("VERSION", versionParaInstalar);


                    }
                }
                var fileToExecute = folderApplication + "\\" + ReadRegistry("VERSION") + "\\OKPOS.exe";

                if (File.Exists(fileToExecute))
                {
                    var proc = Process.Start(fileToExecute,versionParaInstalar);

                    while (string.IsNullOrEmpty(proc.MainWindowTitle))
                    {
                        proc.Refresh();
                        controlWaitBox1.ActualizarMensaje("Estamos iniciando OKPOS.");
                    }
                }
                e.Result = "RUN_APP";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool DescomprimirArchivo(string direcctorioTrabajo, string archivo)
        {
            try
            {
                ZipFile zipFile = new ZipFile(direcctorioTrabajo + "\\" + archivo);
                zipFile.ExtractAll(direcctorioTrabajo);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void backgroundWorkerLanzarApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                labelMensaje.Text = "Ocurrió un problema:" + e.Error.Message;
            }
            else
            {
                buttonReintentar.Visible = true;
                if (e.Result.ToString() == "RUN_APP")
                {
                    Close();
                }
            }
        }

        private void buttonReintentar_Click(object sender, EventArgs e)
        {
            labelMensaje.Text = "";
            buttonReintentar.Visible = false;
            controlWaitBox1.Mostrar("Verificando requerimientos de la aplicación. Espere por favor...");
            backgroundWorkerBuscarActualizacion.RunWorkerAsync();
        }
    }
}
