namespace POS.GUI
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.backgroundWorkerBuscarActualizacion = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.labelactual = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorkerLanzarApp = new System.ComponentModel.BackgroundWorker();
            this.buttonReintentar = new System.Windows.Forms.Button();
            this.labelMensaje = new System.Windows.Forms.Label();
            this.controlWaitBox1 = new OKPOS.ControlWaitBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorkerBuscarActualizacion
            // 
            this.backgroundWorkerBuscarActualizacion.WorkerSupportsCancellation = true;
            this.backgroundWorkerBuscarActualizacion.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerBuscarActualizacion_DoWork);
            this.backgroundWorkerBuscarActualizacion.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerBuscarActualizacion_RunWorkerCompleted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Italic);
            this.label2.Location = new System.Drawing.Point(43, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Versión Actual:";
            // 
            // labelactual
            // 
            this.labelactual.AutoSize = true;
            this.labelactual.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Italic);
            this.labelactual.Location = new System.Drawing.Point(174, 249);
            this.labelactual.Name = "labelactual";
            this.labelactual.Size = new System.Drawing.Size(108, 23);
            this.labelactual.TabIndex = 4;
            this.labelactual.Text = "Verificando..";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::OKPOS.GUI.Properties.Resources.logo1;
            this.pictureBox1.Location = new System.Drawing.Point(32, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(525, 123);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // backgroundWorkerLanzarApp
            // 
            this.backgroundWorkerLanzarApp.WorkerSupportsCancellation = true;
            this.backgroundWorkerLanzarApp.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerLanzarApp_DoWork);
            this.backgroundWorkerLanzarApp.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerLanzarApp_RunWorkerCompleted);
            // 
            // buttonReintentar
            // 
            this.buttonReintentar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(63)))), ((int)(((byte)(45)))));
            this.buttonReintentar.FlatAppearance.BorderSize = 0;
            this.buttonReintentar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReintentar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonReintentar.ForeColor = System.Drawing.Color.White;
            this.buttonReintentar.Location = new System.Drawing.Point(449, 249);
            this.buttonReintentar.Name = "buttonReintentar";
            this.buttonReintentar.Size = new System.Drawing.Size(108, 27);
            this.buttonReintentar.TabIndex = 8;
            this.buttonReintentar.Text = "Reintentar";
            this.buttonReintentar.UseVisualStyleBackColor = false;
            this.buttonReintentar.Click += new System.EventHandler(this.buttonReintentar_Click);
            // 
            // labelMensaje
            // 
            this.labelMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.labelMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(63)))), ((int)(((byte)(45)))));
            this.labelMensaje.Location = new System.Drawing.Point(32, 142);
            this.labelMensaje.Name = "labelMensaje";
            this.labelMensaje.Size = new System.Drawing.Size(525, 105);
            this.labelMensaje.TabIndex = 9;
            this.labelMensaje.Text = "Mensaje";
            this.labelMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // controlWaitBox1
            // 
            this.controlWaitBox1.BackColor = System.Drawing.Color.White;
            this.controlWaitBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlWaitBox1.Location = new System.Drawing.Point(32, 139);
            this.controlWaitBox1.Name = "controlWaitBox1";
            this.controlWaitBox1.Size = new System.Drawing.Size(525, 105);
            this.controlWaitBox1.TabIndex = 10;
            this.controlWaitBox1.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(579, 288);
            this.Controls.Add(this.buttonReintentar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelactual);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelMensaje);
            this.Controls.Add(this.controlWaitBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(595, 327);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(595, 327);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OKPOS";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorkerBuscarActualizacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelactual;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLanzarApp;
        private System.Windows.Forms.Button buttonReintentar;
        private System.Windows.Forms.Label labelMensaje;
        private OKPOS.ControlWaitBox controlWaitBox1;
    }
}