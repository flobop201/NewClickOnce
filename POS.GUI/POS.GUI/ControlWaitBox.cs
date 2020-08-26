using System;
using System.Linq;
using System.Windows.Forms;

namespace OKPOS
{
    public partial class ControlWaitBox : UserControl
    {
        public bool disablecontrols = false;
        private int seconds = 0;
        public ControlWaitBox()
        {
            InitializeComponent();
            this.Visible = false;
        }

        public void Mostrar(string mensaje, bool disable = false)
        {
            disablecontrols = disable;
            Visible = true;

            this.BringToFront();
            if (disablecontrols)
            {
                var controls = (from c in this.Parent.Controls.OfType<Control>()
                                where (!c.Equals(this))
                                select c);
                foreach (var control in controls)
                    control.Visible = false;

            }
            ActualizarMensaje(mensaje);
            this.Visible = true;
        }

        public void Ocultar()
        {
            //radWaitingBar1.EndWaiting();
            if (disablecontrols)
            {
                var controls = (from c in this.Parent.Controls.OfType<Control>()
                                where (!c.Equals(this))
                                select c);
                foreach (var control in controls)
                    control.Visible = true;
            }
            this.Visible = false;
        }

        internal void ActualizarMensaje(string mensaje, int percent = 0, string downloadmessage = "")
        {

            if (InvokeRequired)
                lblMensaje.Invoke((MethodInvoker)delegate
                {
                    lblMensaje.Text = mensaje + (downloadmessage.Length>0 ?" | " + downloadmessage: "");
                    if (percent > 0)
                    {
                        progressBar1.Style = ProgressBarStyle.Blocks;
                        progressBar1.Value = percent;

                    }
                    else
                    {
                        progressBar1.Style = ProgressBarStyle.Marquee;
                    }
                });
            else
            {
                lblMensaje.Text = mensaje + (downloadmessage.Length > 0 ? " | " + downloadmessage : "");

                if (percent > 0)
                {
                    progressBar1.Style = ProgressBarStyle.Blocks;
                    progressBar1.Value = percent;
                }
                else
                {
                    progressBar1.Style = ProgressBarStyle.Marquee;
                }
            }
        }
    }
}
