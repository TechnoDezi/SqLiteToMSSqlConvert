namespace SqlLiteToMSSql
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSqlLiteFile = new System.Windows.Forms.TextBox();
            this.btnBrowseSqlLite = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMSSqlConn = new System.Windows.Forms.TextBox();
            this.btnTestMSSql = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnListTables = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnTransferSchema = new System.Windows.Forms.Button();
            this.btnTransferData = new System.Windows.Forms.Button();
            this.btnGenerateSchemaScript = new System.Windows.Forms.Button();
            this.btnGenerateDataScript = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnTestSqlLiteConn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sql Lite File";
            // 
            // txtSqlLiteFile
            // 
            this.txtSqlLiteFile.Location = new System.Drawing.Point(127, 6);
            this.txtSqlLiteFile.Name = "txtSqlLiteFile";
            this.txtSqlLiteFile.Size = new System.Drawing.Size(386, 20);
            this.txtSqlLiteFile.TabIndex = 1;
            // 
            // btnBrowseSqlLite
            // 
            this.btnBrowseSqlLite.Location = new System.Drawing.Point(519, 4);
            this.btnBrowseSqlLite.Name = "btnBrowseSqlLite";
            this.btnBrowseSqlLite.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSqlLite.TabIndex = 2;
            this.btnBrowseSqlLite.Text = "Browse...";
            this.btnBrowseSqlLite.UseVisualStyleBackColor = true;
            this.btnBrowseSqlLite.Click += new System.EventHandler(this.btnBrowseSqlLite_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "MS Sql Connection";
            // 
            // txtMSSqlConn
            // 
            this.txtMSSqlConn.Location = new System.Drawing.Point(127, 32);
            this.txtMSSqlConn.Name = "txtMSSqlConn";
            this.txtMSSqlConn.Size = new System.Drawing.Size(386, 20);
            this.txtMSSqlConn.TabIndex = 4;
            this.txtMSSqlConn.Text = "Data Source=.\\;Initial Catalog=PlanetGis_RAW;Integrated Security=True;MultipleAct" +
    "iveResultSets=true";
            // 
            // btnTestMSSql
            // 
            this.btnTestMSSql.Location = new System.Drawing.Point(519, 30);
            this.btnTestMSSql.Name = "btnTestMSSql";
            this.btnTestMSSql.Size = new System.Drawing.Size(75, 23);
            this.btnTestMSSql.TabIndex = 5;
            this.btnTestMSSql.Text = "Test";
            this.btnTestMSSql.UseVisualStyleBackColor = true;
            this.btnTestMSSql.Click += new System.EventHandler(this.btnTestMSSql_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGenerateDataScript);
            this.groupBox1.Controls.Add(this.btnGenerateSchemaScript);
            this.groupBox1.Controls.Add(this.btnTransferData);
            this.groupBox1.Controls.Add(this.btnTransferSchema);
            this.groupBox1.Controls.Add(this.btnListTables);
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(663, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            // 
            // btnListTables
            // 
            this.btnListTables.Location = new System.Drawing.Point(6, 19);
            this.btnListTables.Name = "btnListTables";
            this.btnListTables.Size = new System.Drawing.Size(75, 23);
            this.btnListTables.TabIndex = 0;
            this.btnListTables.Text = "List Tables";
            this.btnListTables.UseVisualStyleBackColor = true;
            this.btnListTables.Click += new System.EventHandler(this.btnListTables_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(10, 176);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(665, 403);
            this.txtOutput.TabIndex = 7;
            // 
            // btnTransferSchema
            // 
            this.btnTransferSchema.Location = new System.Drawing.Point(87, 19);
            this.btnTransferSchema.Name = "btnTransferSchema";
            this.btnTransferSchema.Size = new System.Drawing.Size(100, 23);
            this.btnTransferSchema.TabIndex = 1;
            this.btnTransferSchema.Text = "Transfer Schema";
            this.btnTransferSchema.UseVisualStyleBackColor = true;
            this.btnTransferSchema.Click += new System.EventHandler(this.btnTransferSchema_Click);
            // 
            // btnTransferData
            // 
            this.btnTransferData.Location = new System.Drawing.Point(193, 19);
            this.btnTransferData.Name = "btnTransferData";
            this.btnTransferData.Size = new System.Drawing.Size(100, 23);
            this.btnTransferData.TabIndex = 2;
            this.btnTransferData.Text = "Transfer Data";
            this.btnTransferData.UseVisualStyleBackColor = true;
            this.btnTransferData.Click += new System.EventHandler(this.btnTransferData_Click);
            // 
            // btnGenerateSchemaScript
            // 
            this.btnGenerateSchemaScript.Enabled = false;
            this.btnGenerateSchemaScript.Location = new System.Drawing.Point(299, 19);
            this.btnGenerateSchemaScript.Name = "btnGenerateSchemaScript";
            this.btnGenerateSchemaScript.Size = new System.Drawing.Size(139, 23);
            this.btnGenerateSchemaScript.TabIndex = 3;
            this.btnGenerateSchemaScript.Text = "Generate Schema Script";
            this.btnGenerateSchemaScript.UseVisualStyleBackColor = true;
            // 
            // btnGenerateDataScript
            // 
            this.btnGenerateDataScript.Enabled = false;
            this.btnGenerateDataScript.Location = new System.Drawing.Point(444, 19);
            this.btnGenerateDataScript.Name = "btnGenerateDataScript";
            this.btnGenerateDataScript.Size = new System.Drawing.Size(139, 23);
            this.btnGenerateDataScript.TabIndex = 4;
            this.btnGenerateDataScript.Text = "Generate Data Script";
            this.btnGenerateDataScript.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "SqLite Files (.sqlite)|*.sqlite|DB Files (.db)|*.db";
            // 
            // btnTestSqlLiteConn
            // 
            this.btnTestSqlLiteConn.Location = new System.Drawing.Point(600, 4);
            this.btnTestSqlLiteConn.Name = "btnTestSqlLiteConn";
            this.btnTestSqlLiteConn.Size = new System.Drawing.Size(75, 23);
            this.btnTestSqlLiteConn.TabIndex = 8;
            this.btnTestSqlLiteConn.Text = "Test";
            this.btnTestSqlLiteConn.UseVisualStyleBackColor = true;
            this.btnTestSqlLiteConn.Click += new System.EventHandler(this.btnTestSqlLiteConn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 591);
            this.Controls.Add(this.btnTestSqlLiteConn);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTestMSSql);
            this.Controls.Add(this.txtMSSqlConn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBrowseSqlLite);
            this.Controls.Add(this.txtSqlLiteFile);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Sql Lite to MS Sql";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSqlLiteFile;
        private System.Windows.Forms.Button btnBrowseSqlLite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMSSqlConn;
        private System.Windows.Forms.Button btnTestMSSql;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGenerateDataScript;
        private System.Windows.Forms.Button btnGenerateSchemaScript;
        private System.Windows.Forms.Button btnTransferData;
        private System.Windows.Forms.Button btnTransferSchema;
        private System.Windows.Forms.Button btnListTables;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnTestSqlLiteConn;
    }
}

