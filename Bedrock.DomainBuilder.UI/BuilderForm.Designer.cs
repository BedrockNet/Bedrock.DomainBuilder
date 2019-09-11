namespace Bedrock.DomainBuilder.UI
{
    partial class BuilderForm
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
			this.btnBuild = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.cboDomain = new System.Windows.Forms.ComboBox();
			this.lblDomain = new System.Windows.Forms.Label();
			this.lblConnection = new System.Windows.Forms.Label();
			this.txtConnection = new System.Windows.Forms.TextBox();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.lblPath = new System.Windows.Forms.Label();
			this.txtNamespaceEntity = new System.Windows.Forms.TextBox();
			this.lblNamespaceEntity = new System.Windows.Forms.Label();
			this.txtNamespaceShared = new System.Windows.Forms.TextBox();
			this.lblNamespaceShared = new System.Windows.Forms.Label();
			this.chkIncludeSpatial = new System.Windows.Forms.CheckBox();
			this.chkCleanPath = new System.Windows.Forms.CheckBox();
			this.txtReset = new System.Windows.Forms.Button();
			this.chkIsValidatable = new System.Windows.Forms.CheckBox();
			this.chkIsIncludeProperties = new System.Windows.Forms.CheckBox();
			this.chkIsIncludeAuditFields = new System.Windows.Forms.CheckBox();
			this.chkIsIncludePrimaryKey = new System.Windows.Forms.CheckBox();
			this.chkEntityBuilder = new System.Windows.Forms.CheckBox();
			this.chkContextBuilder = new System.Windows.Forms.CheckBox();
			this.chkMappingBuilder = new System.Windows.Forms.CheckBox();
			this.gbBuilders = new System.Windows.Forms.GroupBox();
			this.chkAutoMapperProfileBuilder = new System.Windows.Forms.CheckBox();
			this.chkEntityPartialBuilder = new System.Windows.Forms.CheckBox();
			this.chkControllerApiBuilder = new System.Windows.Forms.CheckBox();
			this.chkControllerMvcBuilder = new System.Windows.Forms.CheckBox();
			this.chkBuilderToggleAll = new System.Windows.Forms.CheckBox();
			this.chkAppServiceInterfaceBuilder = new System.Windows.Forms.CheckBox();
			this.chkAppServiceBuilder = new System.Windows.Forms.CheckBox();
			this.chkContractBuilder = new System.Windows.Forms.CheckBox();
			this.chkAppContextBuilder = new System.Windows.Forms.CheckBox();
			this.chkServiceInterfaceBuilder = new System.Windows.Forms.CheckBox();
			this.chkEnumerationBuilder = new System.Windows.Forms.CheckBox();
			this.chkServiceBuilder = new System.Windows.Forms.CheckBox();
			this.gbSettings = new System.Windows.Forms.GroupBox();
			this.chkNetCore = new System.Windows.Forms.CheckBox();
			this.chkIsIncludeDeletableFields = new System.Windows.Forms.CheckBox();
			this.chkUsePropertyInclusions = new System.Windows.Forms.CheckBox();
			this.lblSchema = new System.Windows.Forms.Label();
			this.cboSchema = new System.Windows.Forms.ComboBox();
			this.chkIsTracked = new System.Windows.Forms.CheckBox();
			this.txtNamespaceControllerMvc = new System.Windows.Forms.TextBox();
			this.lblNamespaceControllerMvc = new System.Windows.Forms.Label();
			this.txtNamespaceControllerApi = new System.Windows.Forms.TextBox();
			this.lblNamespaceControllerApi = new System.Windows.Forms.Label();
			this.btnConnectionStringDialog = new System.Windows.Forms.Button();
			this.chkIsIncludeCrudEntity = new System.Windows.Forms.CheckBox();
			this.txtNamespaceAppService = new System.Windows.Forms.TextBox();
			this.lblNamespaceAppService = new System.Windows.Forms.Label();
			this.gbTypes = new System.Windows.Forms.GroupBox();
			this.lblControllerMvcType = new System.Windows.Forms.Label();
			this.cboControllerMvcType = new System.Windows.Forms.ComboBox();
			this.lblControllerApiType = new System.Windows.Forms.Label();
			this.cboControllerApiType = new System.Windows.Forms.ComboBox();
			this.lblAppServiceType = new System.Windows.Forms.Label();
			this.cboAppServiceType = new System.Windows.Forms.ComboBox();
			this.lblContractType = new System.Windows.Forms.Label();
			this.cboContractType = new System.Windows.Forms.ComboBox();
			this.lblServiceType = new System.Windows.Forms.Label();
			this.cboServiceType = new System.Windows.Forms.ComboBox();
			this.lblContextType = new System.Windows.Forms.Label();
			this.cboContextType = new System.Windows.Forms.ComboBox();
			this.lblPrimaryKeyType = new System.Windows.Forms.Label();
			this.cboPrimaryKeyType = new System.Windows.Forms.ComboBox();
			this.lblEntityType = new System.Windows.Forms.Label();
			this.cboEntityType = new System.Windows.Forms.ComboBox();
			this.lblContractName = new System.Windows.Forms.Label();
			this.txtContractName = new System.Windows.Forms.TextBox();
			this.txtNamespaceContract = new System.Windows.Forms.TextBox();
			this.lblNamespaceContract = new System.Windows.Forms.Label();
			this.chkIsIgnoreRootChildren = new System.Windows.Forms.CheckBox();
			this.chkIsCascadeDelete = new System.Windows.Forms.CheckBox();
			this.chkIsRecursiveCrud = new System.Windows.Forms.CheckBox();
			this.chkIsIncludeCrud = new System.Windows.Forms.CheckBox();
			this.txtCommonKey = new System.Windows.Forms.TextBox();
			this.chkIsUseCommonKey = new System.Windows.Forms.CheckBox();
			this.txtNamespaceEnumeration = new System.Windows.Forms.TextBox();
			this.lblNamespaceEnumeration = new System.Windows.Forms.Label();
			this.txtNamespaceService = new System.Windows.Forms.TextBox();
			this.lblNamespaceService = new System.Windows.Forms.Label();
			this.txtNamespaceMapping = new System.Windows.Forms.TextBox();
			this.lblNamespaceMapping = new System.Windows.Forms.Label();
			this.txtNamespaceContext = new System.Windows.Forms.TextBox();
			this.lblNamespaceContext = new System.Windows.Forms.Label();
			this.gbMessages = new System.Windows.Forms.GroupBox();
			this.lblStatusValue = new System.Windows.Forms.Label();
			this.lblStatus = new System.Windows.Forms.Label();
			this.lblLoCValue = new System.Windows.Forms.Label();
			this.lblLoC = new System.Windows.Forms.Label();
			this.lblBuilderValue = new System.Windows.Forms.Label();
			this.lblBuilder = new System.Windows.Forms.Label();
			this.lblSectionValue = new System.Windows.Forms.Label();
			this.lblSection = new System.Windows.Forms.Label();
			this.lblFileValue = new System.Windows.Forms.Label();
			this.lblFile = new System.Windows.Forms.Label();
			this.btnCopyToClipboard = new System.Windows.Forms.Button();
			this.txtMessages = new System.Windows.Forms.TextBox();
			this.chkIsOpenFolder = new System.Windows.Forms.CheckBox();
			this.pbBuilder = new System.Windows.Forms.ProgressBar();
			this.btnPause = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.gbBuilders.SuspendLayout();
			this.gbSettings.SuspendLayout();
			this.gbTypes.SuspendLayout();
			this.gbMessages.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnBuild
			// 
			this.btnBuild.Location = new System.Drawing.Point(726, 816);
			this.btnBuild.Name = "btnBuild";
			this.btnBuild.Size = new System.Drawing.Size(75, 23);
			this.btnBuild.TabIndex = 501;
			this.btnBuild.Text = "Build";
			this.btnBuild.UseVisualStyleBackColor = true;
			this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(1051, 816);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(75, 23);
			this.btnExit.TabIndex = 504;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// cboDomain
			// 
			this.cboDomain.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboDomain.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDomain.FormattingEnabled = true;
			this.cboDomain.Location = new System.Drawing.Point(147, 21);
			this.cboDomain.Name = "cboDomain";
			this.cboDomain.Size = new System.Drawing.Size(162, 21);
			this.cboDomain.TabIndex = 0;
			this.cboDomain.SelectionChangeCommitted += new System.EventHandler(this.cboDomain_SelectionChangeCommitted);
			// 
			// lblDomain
			// 
			this.lblDomain.AutoSize = true;
			this.lblDomain.Location = new System.Drawing.Point(98, 24);
			this.lblDomain.Name = "lblDomain";
			this.lblDomain.Size = new System.Drawing.Size(43, 13);
			this.lblDomain.TabIndex = 3;
			this.lblDomain.Text = "Domain";
			// 
			// lblConnection
			// 
			this.lblConnection.AutoSize = true;
			this.lblConnection.Location = new System.Drawing.Point(80, 58);
			this.lblConnection.Name = "lblConnection";
			this.lblConnection.Size = new System.Drawing.Size(61, 13);
			this.lblConnection.TabIndex = 4;
			this.lblConnection.Text = "Connection";
			// 
			// txtConnection
			// 
			this.txtConnection.Location = new System.Drawing.Point(147, 55);
			this.txtConnection.Name = "txtConnection";
			this.txtConnection.Size = new System.Drawing.Size(368, 20);
			this.txtConnection.TabIndex = 1;
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(147, 87);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(420, 20);
			this.txtPath.TabIndex = 2;
			// 
			// lblPath
			// 
			this.lblPath.AutoSize = true;
			this.lblPath.Location = new System.Drawing.Point(112, 90);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(29, 13);
			this.lblPath.TabIndex = 6;
			this.lblPath.Text = "Path";
			// 
			// txtNamespaceEntity
			// 
			this.txtNamespaceEntity.Location = new System.Drawing.Point(147, 153);
			this.txtNamespaceEntity.Name = "txtNamespaceEntity";
			this.txtNamespaceEntity.Size = new System.Drawing.Size(420, 20);
			this.txtNamespaceEntity.TabIndex = 4;
			// 
			// lblNamespaceEntity
			// 
			this.lblNamespaceEntity.AutoSize = true;
			this.lblNamespaceEntity.Location = new System.Drawing.Point(42, 157);
			this.lblNamespaceEntity.Name = "lblNamespaceEntity";
			this.lblNamespaceEntity.Size = new System.Drawing.Size(99, 13);
			this.lblNamespaceEntity.TabIndex = 8;
			this.lblNamespaceEntity.Text = "Namespace (Entity)";
			// 
			// txtNamespaceShared
			// 
			this.txtNamespaceShared.Location = new System.Drawing.Point(147, 463);
			this.txtNamespaceShared.Name = "txtNamespaceShared";
			this.txtNamespaceShared.Size = new System.Drawing.Size(420, 20);
			this.txtNamespaceShared.TabIndex = 11;
			// 
			// lblNamespaceShared
			// 
			this.lblNamespaceShared.AutoSize = true;
			this.lblNamespaceShared.Location = new System.Drawing.Point(34, 466);
			this.lblNamespaceShared.Name = "lblNamespaceShared";
			this.lblNamespaceShared.Size = new System.Drawing.Size(107, 13);
			this.lblNamespaceShared.TabIndex = 10;
			this.lblNamespaceShared.Text = "Namespace (Shared)";
			// 
			// chkIncludeSpatial
			// 
			this.chkIncludeSpatial.AutoSize = true;
			this.chkIncludeSpatial.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIncludeSpatial.Location = new System.Drawing.Point(35, 535);
			this.chkIncludeSpatial.Name = "chkIncludeSpatial";
			this.chkIncludeSpatial.Size = new System.Drawing.Size(96, 17);
			this.chkIncludeSpatial.TabIndex = 105;
			this.chkIncludeSpatial.Text = "Include Spatial";
			this.chkIncludeSpatial.UseVisualStyleBackColor = true;
			// 
			// chkCleanPath
			// 
			this.chkCleanPath.AutoSize = true;
			this.chkCleanPath.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkCleanPath.Location = new System.Drawing.Point(152, 604);
			this.chkCleanPath.Name = "chkCleanPath";
			this.chkCleanPath.Size = new System.Drawing.Size(78, 17);
			this.chkCleanPath.TabIndex = 106;
			this.chkCleanPath.Text = "Clean Path";
			this.chkCleanPath.UseVisualStyleBackColor = true;
			// 
			// txtReset
			// 
			this.txtReset.Location = new System.Drawing.Point(969, 816);
			this.txtReset.Name = "txtReset";
			this.txtReset.Size = new System.Drawing.Size(75, 23);
			this.txtReset.TabIndex = 503;
			this.txtReset.Text = "Reset";
			this.txtReset.UseVisualStyleBackColor = true;
			this.txtReset.Click += new System.EventHandler(this.txtReset_Click);
			// 
			// chkIsValidatable
			// 
			this.chkIsValidatable.AutoSize = true;
			this.chkIsValidatable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsValidatable.Location = new System.Drawing.Point(53, 500);
			this.chkIsValidatable.Name = "chkIsValidatable";
			this.chkIsValidatable.Size = new System.Drawing.Size(78, 17);
			this.chkIsValidatable.TabIndex = 100;
			this.chkIsValidatable.Text = "Validatable";
			this.chkIsValidatable.UseVisualStyleBackColor = true;
			// 
			// chkIsIncludeProperties
			// 
			this.chkIsIncludeProperties.AutoSize = true;
			this.chkIsIncludeProperties.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsIncludeProperties.Location = new System.Drawing.Point(152, 500);
			this.chkIsIncludeProperties.Name = "chkIsIncludeProperties";
			this.chkIsIncludeProperties.Size = new System.Drawing.Size(73, 17);
			this.chkIsIncludeProperties.TabIndex = 101;
			this.chkIsIncludeProperties.Text = "Properties";
			this.chkIsIncludeProperties.UseVisualStyleBackColor = true;
			// 
			// chkIsIncludeAuditFields
			// 
			this.chkIsIncludeAuditFields.AutoSize = true;
			this.chkIsIncludeAuditFields.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsIncludeAuditFields.Location = new System.Drawing.Point(352, 500);
			this.chkIsIncludeAuditFields.Name = "chkIsIncludeAuditFields";
			this.chkIsIncludeAuditFields.Size = new System.Drawing.Size(80, 17);
			this.chkIsIncludeAuditFields.TabIndex = 103;
			this.chkIsIncludeAuditFields.Text = "Audit Fields";
			this.chkIsIncludeAuditFields.UseVisualStyleBackColor = true;
			// 
			// chkIsIncludePrimaryKey
			// 
			this.chkIsIncludePrimaryKey.AutoSize = true;
			this.chkIsIncludePrimaryKey.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsIncludePrimaryKey.Location = new System.Drawing.Point(249, 500);
			this.chkIsIncludePrimaryKey.Name = "chkIsIncludePrimaryKey";
			this.chkIsIncludePrimaryKey.Size = new System.Drawing.Size(81, 17);
			this.chkIsIncludePrimaryKey.TabIndex = 102;
			this.chkIsIncludePrimaryKey.Text = "Primary Key";
			this.chkIsIncludePrimaryKey.UseVisualStyleBackColor = true;
			// 
			// chkEntityBuilder
			// 
			this.chkEntityBuilder.AutoSize = true;
			this.chkEntityBuilder.Location = new System.Drawing.Point(6, 30);
			this.chkEntityBuilder.Name = "chkEntityBuilder";
			this.chkEntityBuilder.Size = new System.Drawing.Size(52, 17);
			this.chkEntityBuilder.TabIndex = 300;
			this.chkEntityBuilder.Text = "Entity";
			this.chkEntityBuilder.UseVisualStyleBackColor = true;
			this.chkEntityBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// chkContextBuilder
			// 
			this.chkContextBuilder.AutoSize = true;
			this.chkContextBuilder.Location = new System.Drawing.Point(139, 63);
			this.chkContextBuilder.Name = "chkContextBuilder";
			this.chkContextBuilder.Size = new System.Drawing.Size(62, 17);
			this.chkContextBuilder.TabIndex = 305;
			this.chkContextBuilder.Text = "Context";
			this.chkContextBuilder.UseVisualStyleBackColor = true;
			this.chkContextBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// chkMappingBuilder
			// 
			this.chkMappingBuilder.AutoSize = true;
			this.chkMappingBuilder.Location = new System.Drawing.Point(273, 63);
			this.chkMappingBuilder.Name = "chkMappingBuilder";
			this.chkMappingBuilder.Size = new System.Drawing.Size(67, 17);
			this.chkMappingBuilder.TabIndex = 306;
			this.chkMappingBuilder.Text = "Mapping";
			this.chkMappingBuilder.UseVisualStyleBackColor = true;
			this.chkMappingBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// gbBuilders
			// 
			this.gbBuilders.Controls.Add(this.chkAutoMapperProfileBuilder);
			this.gbBuilders.Controls.Add(this.chkEntityPartialBuilder);
			this.gbBuilders.Controls.Add(this.chkControllerApiBuilder);
			this.gbBuilders.Controls.Add(this.chkControllerMvcBuilder);
			this.gbBuilders.Controls.Add(this.chkBuilderToggleAll);
			this.gbBuilders.Controls.Add(this.chkAppServiceInterfaceBuilder);
			this.gbBuilders.Controls.Add(this.chkAppServiceBuilder);
			this.gbBuilders.Controls.Add(this.chkContractBuilder);
			this.gbBuilders.Controls.Add(this.chkAppContextBuilder);
			this.gbBuilders.Controls.Add(this.chkServiceInterfaceBuilder);
			this.gbBuilders.Controls.Add(this.chkEnumerationBuilder);
			this.gbBuilders.Controls.Add(this.chkServiceBuilder);
			this.gbBuilders.Controls.Add(this.chkEntityBuilder);
			this.gbBuilders.Controls.Add(this.chkContextBuilder);
			this.gbBuilders.Controls.Add(this.chkMappingBuilder);
			this.gbBuilders.Location = new System.Drawing.Point(604, 12);
			this.gbBuilders.Name = "gbBuilders";
			this.gbBuilders.Size = new System.Drawing.Size(522, 170);
			this.gbBuilders.TabIndex = 1002;
			this.gbBuilders.TabStop = false;
			this.gbBuilders.Text = "Builders";
			// 
			// chkAutoMapperProfileBuilder
			// 
			this.chkAutoMapperProfileBuilder.AutoSize = true;
			this.chkAutoMapperProfileBuilder.Location = new System.Drawing.Point(139, 127);
			this.chkAutoMapperProfileBuilder.Name = "chkAutoMapperProfileBuilder";
			this.chkAutoMapperProfileBuilder.Size = new System.Drawing.Size(113, 17);
			this.chkAutoMapperProfileBuilder.TabIndex = 314;
			this.chkAutoMapperProfileBuilder.Text = "AutoMapperProfile";
			this.chkAutoMapperProfileBuilder.UseVisualStyleBackColor = true;
			this.chkAutoMapperProfileBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// chkEntityPartialBuilder
			// 
			this.chkEntityPartialBuilder.AutoSize = true;
			this.chkEntityPartialBuilder.Location = new System.Drawing.Point(139, 30);
			this.chkEntityPartialBuilder.Name = "chkEntityPartialBuilder";
			this.chkEntityPartialBuilder.Size = new System.Drawing.Size(84, 17);
			this.chkEntityPartialBuilder.TabIndex = 301;
			this.chkEntityPartialBuilder.Text = "Entity Partial";
			this.chkEntityPartialBuilder.UseVisualStyleBackColor = true;
			this.chkEntityPartialBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// chkControllerApiBuilder
			// 
			this.chkControllerApiBuilder.AutoSize = true;
			this.chkControllerApiBuilder.Location = new System.Drawing.Point(408, 95);
			this.chkControllerApiBuilder.Name = "chkControllerApiBuilder";
			this.chkControllerApiBuilder.Size = new System.Drawing.Size(85, 17);
			this.chkControllerApiBuilder.TabIndex = 311;
			this.chkControllerApiBuilder.Text = "ControllerApi";
			this.chkControllerApiBuilder.UseVisualStyleBackColor = true;
			this.chkControllerApiBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// chkControllerMvcBuilder
			// 
			this.chkControllerMvcBuilder.AutoSize = true;
			this.chkControllerMvcBuilder.Location = new System.Drawing.Point(6, 127);
			this.chkControllerMvcBuilder.Name = "chkControllerMvcBuilder";
			this.chkControllerMvcBuilder.Size = new System.Drawing.Size(91, 17);
			this.chkControllerMvcBuilder.TabIndex = 312;
			this.chkControllerMvcBuilder.Text = "ControllerMvc";
			this.chkControllerMvcBuilder.UseVisualStyleBackColor = true;
			this.chkControllerMvcBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// chkBuilderToggleAll
			// 
			this.chkBuilderToggleAll.AutoSize = true;
			this.chkBuilderToggleAll.Location = new System.Drawing.Point(408, 127);
			this.chkBuilderToggleAll.Name = "chkBuilderToggleAll";
			this.chkBuilderToggleAll.Size = new System.Drawing.Size(37, 17);
			this.chkBuilderToggleAll.TabIndex = 313;
			this.chkBuilderToggleAll.Text = "All";
			this.chkBuilderToggleAll.UseVisualStyleBackColor = true;
			this.chkBuilderToggleAll.CheckedChanged += new System.EventHandler(this.chkBuilderToggleAll_CheckedChanged);
			// 
			// chkAppServiceInterfaceBuilder
			// 
			this.chkAppServiceInterfaceBuilder.AutoSize = true;
			this.chkAppServiceInterfaceBuilder.Location = new System.Drawing.Point(273, 95);
			this.chkAppServiceInterfaceBuilder.Name = "chkAppServiceInterfaceBuilder";
			this.chkAppServiceInterfaceBuilder.Size = new System.Drawing.Size(129, 17);
			this.chkAppServiceInterfaceBuilder.TabIndex = 310;
			this.chkAppServiceInterfaceBuilder.Text = "App Service Interface";
			this.chkAppServiceInterfaceBuilder.UseVisualStyleBackColor = true;
			this.chkAppServiceInterfaceBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// chkAppServiceBuilder
			// 
			this.chkAppServiceBuilder.AutoSize = true;
			this.chkAppServiceBuilder.Location = new System.Drawing.Point(139, 95);
			this.chkAppServiceBuilder.Name = "chkAppServiceBuilder";
			this.chkAppServiceBuilder.Size = new System.Drawing.Size(84, 17);
			this.chkAppServiceBuilder.TabIndex = 309;
			this.chkAppServiceBuilder.Text = "App Service";
			this.chkAppServiceBuilder.UseVisualStyleBackColor = true;
			this.chkAppServiceBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// chkContractBuilder
			// 
			this.chkContractBuilder.AutoSize = true;
			this.chkContractBuilder.Location = new System.Drawing.Point(6, 95);
			this.chkContractBuilder.Name = "chkContractBuilder";
			this.chkContractBuilder.Size = new System.Drawing.Size(66, 17);
			this.chkContractBuilder.TabIndex = 308;
			this.chkContractBuilder.Text = "Contract";
			this.chkContractBuilder.UseVisualStyleBackColor = true;
			this.chkContractBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// chkAppContextBuilder
			// 
			this.chkAppContextBuilder.AutoSize = true;
			this.chkAppContextBuilder.Location = new System.Drawing.Point(408, 63);
			this.chkAppContextBuilder.Name = "chkAppContextBuilder";
			this.chkAppContextBuilder.Size = new System.Drawing.Size(84, 17);
			this.chkAppContextBuilder.TabIndex = 307;
			this.chkAppContextBuilder.Text = "App Context";
			this.chkAppContextBuilder.UseVisualStyleBackColor = true;
			this.chkAppContextBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// chkServiceInterfaceBuilder
			// 
			this.chkServiceInterfaceBuilder.AutoSize = true;
			this.chkServiceInterfaceBuilder.Location = new System.Drawing.Point(408, 30);
			this.chkServiceInterfaceBuilder.Name = "chkServiceInterfaceBuilder";
			this.chkServiceInterfaceBuilder.Size = new System.Drawing.Size(107, 17);
			this.chkServiceInterfaceBuilder.TabIndex = 303;
			this.chkServiceInterfaceBuilder.Text = "Service Interface";
			this.chkServiceInterfaceBuilder.UseVisualStyleBackColor = true;
			this.chkServiceInterfaceBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// chkEnumerationBuilder
			// 
			this.chkEnumerationBuilder.AutoSize = true;
			this.chkEnumerationBuilder.Location = new System.Drawing.Point(6, 63);
			this.chkEnumerationBuilder.Name = "chkEnumerationBuilder";
			this.chkEnumerationBuilder.Size = new System.Drawing.Size(85, 17);
			this.chkEnumerationBuilder.TabIndex = 304;
			this.chkEnumerationBuilder.Text = "Enumeration";
			this.chkEnumerationBuilder.UseVisualStyleBackColor = true;
			this.chkEnumerationBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// chkServiceBuilder
			// 
			this.chkServiceBuilder.AutoSize = true;
			this.chkServiceBuilder.Location = new System.Drawing.Point(273, 30);
			this.chkServiceBuilder.Name = "chkServiceBuilder";
			this.chkServiceBuilder.Size = new System.Drawing.Size(62, 17);
			this.chkServiceBuilder.TabIndex = 302;
			this.chkServiceBuilder.Text = "Service";
			this.chkServiceBuilder.UseVisualStyleBackColor = true;
			this.chkServiceBuilder.Click += new System.EventHandler(this.Builder_Click);
			// 
			// gbSettings
			// 
			this.gbSettings.Controls.Add(this.chkNetCore);
			this.gbSettings.Controls.Add(this.chkIsIncludeDeletableFields);
			this.gbSettings.Controls.Add(this.chkUsePropertyInclusions);
			this.gbSettings.Controls.Add(this.lblSchema);
			this.gbSettings.Controls.Add(this.cboSchema);
			this.gbSettings.Controls.Add(this.chkIsTracked);
			this.gbSettings.Controls.Add(this.txtNamespaceControllerMvc);
			this.gbSettings.Controls.Add(this.lblNamespaceControllerMvc);
			this.gbSettings.Controls.Add(this.txtNamespaceControllerApi);
			this.gbSettings.Controls.Add(this.lblNamespaceControllerApi);
			this.gbSettings.Controls.Add(this.btnConnectionStringDialog);
			this.gbSettings.Controls.Add(this.chkIsIncludeCrudEntity);
			this.gbSettings.Controls.Add(this.txtNamespaceAppService);
			this.gbSettings.Controls.Add(this.lblNamespaceAppService);
			this.gbSettings.Controls.Add(this.gbTypes);
			this.gbSettings.Controls.Add(this.lblContractName);
			this.gbSettings.Controls.Add(this.txtContractName);
			this.gbSettings.Controls.Add(this.txtNamespaceContract);
			this.gbSettings.Controls.Add(this.lblNamespaceContract);
			this.gbSettings.Controls.Add(this.chkIsIgnoreRootChildren);
			this.gbSettings.Controls.Add(this.chkIsCascadeDelete);
			this.gbSettings.Controls.Add(this.chkIsRecursiveCrud);
			this.gbSettings.Controls.Add(this.chkIsIncludeCrud);
			this.gbSettings.Controls.Add(this.txtCommonKey);
			this.gbSettings.Controls.Add(this.chkIsUseCommonKey);
			this.gbSettings.Controls.Add(this.txtNamespaceEnumeration);
			this.gbSettings.Controls.Add(this.lblNamespaceEnumeration);
			this.gbSettings.Controls.Add(this.txtNamespaceService);
			this.gbSettings.Controls.Add(this.lblNamespaceService);
			this.gbSettings.Controls.Add(this.txtNamespaceMapping);
			this.gbSettings.Controls.Add(this.lblNamespaceMapping);
			this.gbSettings.Controls.Add(this.txtNamespaceContext);
			this.gbSettings.Controls.Add(this.lblNamespaceContext);
			this.gbSettings.Controls.Add(this.chkIncludeSpatial);
			this.gbSettings.Controls.Add(this.chkCleanPath);
			this.gbSettings.Controls.Add(this.chkIsValidatable);
			this.gbSettings.Controls.Add(this.chkIsIncludeProperties);
			this.gbSettings.Controls.Add(this.chkIsIncludePrimaryKey);
			this.gbSettings.Controls.Add(this.chkIsIncludeAuditFields);
			this.gbSettings.Controls.Add(this.lblDomain);
			this.gbSettings.Controls.Add(this.cboDomain);
			this.gbSettings.Controls.Add(this.lblConnection);
			this.gbSettings.Controls.Add(this.txtConnection);
			this.gbSettings.Controls.Add(this.lblPath);
			this.gbSettings.Controls.Add(this.txtPath);
			this.gbSettings.Controls.Add(this.lblNamespaceEntity);
			this.gbSettings.Controls.Add(this.txtNamespaceEntity);
			this.gbSettings.Controls.Add(this.txtNamespaceShared);
			this.gbSettings.Controls.Add(this.lblNamespaceShared);
			this.gbSettings.Location = new System.Drawing.Point(12, 12);
			this.gbSettings.Name = "gbSettings";
			this.gbSettings.Size = new System.Drawing.Size(576, 827);
			this.gbSettings.TabIndex = 1000;
			this.gbSettings.TabStop = false;
			this.gbSettings.Text = "Settings";
			// 
			// chkNetCore
			// 
			this.chkNetCore.AutoSize = true;
			this.chkNetCore.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkNetCore.Location = new System.Drawing.Point(249, 604);
			this.chkNetCore.Name = "chkNetCore";
			this.chkNetCore.Size = new System.Drawing.Size(76, 17);
			this.chkNetCore.TabIndex = 1013;
			this.chkNetCore.Text = ".NET Core";
			this.chkNetCore.UseVisualStyleBackColor = true;
			// 
			// chkIsIncludeDeletableFields
			// 
			this.chkIsIncludeDeletableFields.AutoSize = true;
			this.chkIsIncludeDeletableFields.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsIncludeDeletableFields.Location = new System.Drawing.Point(448, 500);
			this.chkIsIncludeDeletableFields.Name = "chkIsIncludeDeletableFields";
			this.chkIsIncludeDeletableFields.Size = new System.Drawing.Size(101, 17);
			this.chkIsIncludeDeletableFields.TabIndex = 1012;
			this.chkIsIncludeDeletableFields.Text = "Deletable Fields";
			this.chkIsIncludeDeletableFields.UseVisualStyleBackColor = true;
			// 
			// chkUsePropertyInclusions
			// 
			this.chkUsePropertyInclusions.AutoSize = true;
			this.chkUsePropertyInclusions.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkUsePropertyInclusions.Location = new System.Drawing.Point(16, 604);
			this.chkUsePropertyInclusions.Name = "chkUsePropertyInclusions";
			this.chkUsePropertyInclusions.Size = new System.Drawing.Size(115, 17);
			this.chkUsePropertyInclusions.TabIndex = 1011;
			this.chkUsePropertyInclusions.Text = "Property Inclusions";
			this.chkUsePropertyInclusions.UseVisualStyleBackColor = true;
			// 
			// lblSchema
			// 
			this.lblSchema.AutoSize = true;
			this.lblSchema.Location = new System.Drawing.Point(356, 24);
			this.lblSchema.Name = "lblSchema";
			this.lblSchema.Size = new System.Drawing.Size(46, 13);
			this.lblSchema.TabIndex = 1010;
			this.lblSchema.Text = "Schema";
			// 
			// cboSchema
			// 
			this.cboSchema.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboSchema.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSchema.FormattingEnabled = true;
			this.cboSchema.Location = new System.Drawing.Point(405, 21);
			this.cboSchema.Name = "cboSchema";
			this.cboSchema.Size = new System.Drawing.Size(162, 21);
			this.cboSchema.TabIndex = 1009;
			// 
			// chkIsTracked
			// 
			this.chkIsTracked.AutoSize = true;
			this.chkIsTracked.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsTracked.Location = new System.Drawing.Point(426, 572);
			this.chkIsTracked.Name = "chkIsTracked";
			this.chkIsTracked.Size = new System.Drawing.Size(66, 17);
			this.chkIsTracked.TabIndex = 1008;
			this.chkIsTracked.Text = "Tracked";
			this.chkIsTracked.UseVisualStyleBackColor = true;
			// 
			// txtNamespaceControllerMvc
			// 
			this.txtNamespaceControllerMvc.Location = new System.Drawing.Point(147, 430);
			this.txtNamespaceControllerMvc.Name = "txtNamespaceControllerMvc";
			this.txtNamespaceControllerMvc.Size = new System.Drawing.Size(420, 20);
			this.txtNamespaceControllerMvc.TabIndex = 1006;
			// 
			// lblNamespaceControllerMvc
			// 
			this.lblNamespaceControllerMvc.AutoSize = true;
			this.lblNamespaceControllerMvc.Location = new System.Drawing.Point(3, 433);
			this.lblNamespaceControllerMvc.Name = "lblNamespaceControllerMvc";
			this.lblNamespaceControllerMvc.Size = new System.Drawing.Size(138, 13);
			this.lblNamespaceControllerMvc.TabIndex = 1007;
			this.lblNamespaceControllerMvc.Text = "Namespace (ControllerMvc)";
			// 
			// txtNamespaceControllerApi
			// 
			this.txtNamespaceControllerApi.Location = new System.Drawing.Point(147, 398);
			this.txtNamespaceControllerApi.Name = "txtNamespaceControllerApi";
			this.txtNamespaceControllerApi.Size = new System.Drawing.Size(420, 20);
			this.txtNamespaceControllerApi.TabIndex = 1004;
			// 
			// lblNamespaceControllerApi
			// 
			this.lblNamespaceControllerApi.AutoSize = true;
			this.lblNamespaceControllerApi.Location = new System.Drawing.Point(10, 401);
			this.lblNamespaceControllerApi.Name = "lblNamespaceControllerApi";
			this.lblNamespaceControllerApi.Size = new System.Drawing.Size(132, 13);
			this.lblNamespaceControllerApi.TabIndex = 1005;
			this.lblNamespaceControllerApi.Text = "Namespace (ControllerApi)";
			// 
			// btnConnectionStringDialog
			// 
			this.btnConnectionStringDialog.Location = new System.Drawing.Point(521, 53);
			this.btnConnectionStringDialog.Name = "btnConnectionStringDialog";
			this.btnConnectionStringDialog.Size = new System.Drawing.Size(46, 23);
			this.btnConnectionStringDialog.TabIndex = 1003;
			this.btnConnectionStringDialog.Text = "Select";
			this.btnConnectionStringDialog.UseVisualStyleBackColor = true;
			this.btnConnectionStringDialog.Click += new System.EventHandler(this.btnConnectionStringDialog_Click);
			// 
			// chkIsIncludeCrudEntity
			// 
			this.chkIsIncludeCrudEntity.AutoSize = true;
			this.chkIsIncludeCrudEntity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsIncludeCrudEntity.Location = new System.Drawing.Point(289, 572);
			this.chkIsIncludeCrudEntity.Name = "chkIsIncludeCrudEntity";
			this.chkIsIncludeCrudEntity.Size = new System.Drawing.Size(121, 17);
			this.chkIsIncludeCrudEntity.TabIndex = 1002;
			this.chkIsIncludeCrudEntity.Text = "Include Crud (Entity)";
			this.chkIsIncludeCrudEntity.UseVisualStyleBackColor = true;
			// 
			// txtNamespaceAppService
			// 
			this.txtNamespaceAppService.Location = new System.Drawing.Point(147, 363);
			this.txtNamespaceAppService.Name = "txtNamespaceAppService";
			this.txtNamespaceAppService.Size = new System.Drawing.Size(420, 20);
			this.txtNamespaceAppService.TabIndex = 10;
			// 
			// lblNamespaceAppService
			// 
			this.lblNamespaceAppService.AutoSize = true;
			this.lblNamespaceAppService.Location = new System.Drawing.Point(10, 366);
			this.lblNamespaceAppService.Name = "lblNamespaceAppService";
			this.lblNamespaceAppService.Size = new System.Drawing.Size(131, 13);
			this.lblNamespaceAppService.TabIndex = 56;
			this.lblNamespaceAppService.Text = "Namespace (App Service)";
			// 
			// gbTypes
			// 
			this.gbTypes.Controls.Add(this.lblControllerMvcType);
			this.gbTypes.Controls.Add(this.cboControllerMvcType);
			this.gbTypes.Controls.Add(this.lblControllerApiType);
			this.gbTypes.Controls.Add(this.cboControllerApiType);
			this.gbTypes.Controls.Add(this.lblAppServiceType);
			this.gbTypes.Controls.Add(this.cboAppServiceType);
			this.gbTypes.Controls.Add(this.lblContractType);
			this.gbTypes.Controls.Add(this.cboContractType);
			this.gbTypes.Controls.Add(this.lblServiceType);
			this.gbTypes.Controls.Add(this.cboServiceType);
			this.gbTypes.Controls.Add(this.lblContextType);
			this.gbTypes.Controls.Add(this.cboContextType);
			this.gbTypes.Controls.Add(this.lblPrimaryKeyType);
			this.gbTypes.Controls.Add(this.cboPrimaryKeyType);
			this.gbTypes.Controls.Add(this.lblEntityType);
			this.gbTypes.Controls.Add(this.cboEntityType);
			this.gbTypes.Location = new System.Drawing.Point(12, 634);
			this.gbTypes.Name = "gbTypes";
			this.gbTypes.Size = new System.Drawing.Size(555, 171);
			this.gbTypes.TabIndex = 1001;
			this.gbTypes.TabStop = false;
			this.gbTypes.Text = "Base Types";
			// 
			// lblControllerMvcType
			// 
			this.lblControllerMvcType.AutoSize = true;
			this.lblControllerMvcType.Location = new System.Drawing.Point(301, 140);
			this.lblControllerMvcType.Name = "lblControllerMvcType";
			this.lblControllerMvcType.Size = new System.Drawing.Size(72, 13);
			this.lblControllerMvcType.TabIndex = 208;
			this.lblControllerMvcType.Text = "ControllerMvc";
			// 
			// cboControllerMvcType
			// 
			this.cboControllerMvcType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboControllerMvcType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboControllerMvcType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboControllerMvcType.FormattingEnabled = true;
			this.cboControllerMvcType.Location = new System.Drawing.Point(373, 137);
			this.cboControllerMvcType.Name = "cboControllerMvcType";
			this.cboControllerMvcType.Size = new System.Drawing.Size(162, 21);
			this.cboControllerMvcType.TabIndex = 209;
			// 
			// lblControllerApiType
			// 
			this.lblControllerApiType.AutoSize = true;
			this.lblControllerApiType.Location = new System.Drawing.Point(23, 140);
			this.lblControllerApiType.Name = "lblControllerApiType";
			this.lblControllerApiType.Size = new System.Drawing.Size(66, 13);
			this.lblControllerApiType.TabIndex = 206;
			this.lblControllerApiType.Text = "ControllerApi";
			// 
			// cboControllerApiType
			// 
			this.cboControllerApiType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboControllerApiType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboControllerApiType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboControllerApiType.FormattingEnabled = true;
			this.cboControllerApiType.Location = new System.Drawing.Point(95, 137);
			this.cboControllerApiType.Name = "cboControllerApiType";
			this.cboControllerApiType.Size = new System.Drawing.Size(162, 21);
			this.cboControllerApiType.TabIndex = 207;
			// 
			// lblAppServiceType
			// 
			this.lblAppServiceType.AutoSize = true;
			this.lblAppServiceType.Location = new System.Drawing.Point(302, 102);
			this.lblAppServiceType.Name = "lblAppServiceType";
			this.lblAppServiceType.Size = new System.Drawing.Size(65, 13);
			this.lblAppServiceType.TabIndex = 68;
			this.lblAppServiceType.Text = "App Service";
			// 
			// cboAppServiceType
			// 
			this.cboAppServiceType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboAppServiceType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboAppServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAppServiceType.FormattingEnabled = true;
			this.cboAppServiceType.Location = new System.Drawing.Point(373, 99);
			this.cboAppServiceType.Name = "cboAppServiceType";
			this.cboAppServiceType.Size = new System.Drawing.Size(162, 21);
			this.cboAppServiceType.TabIndex = 205;
			// 
			// lblContractType
			// 
			this.lblContractType.AutoSize = true;
			this.lblContractType.Location = new System.Drawing.Point(42, 102);
			this.lblContractType.Name = "lblContractType";
			this.lblContractType.Size = new System.Drawing.Size(47, 13);
			this.lblContractType.TabIndex = 66;
			this.lblContractType.Text = "Contract";
			// 
			// cboContractType
			// 
			this.cboContractType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboContractType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboContractType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboContractType.FormattingEnabled = true;
			this.cboContractType.Location = new System.Drawing.Point(95, 99);
			this.cboContractType.Name = "cboContractType";
			this.cboContractType.Size = new System.Drawing.Size(162, 21);
			this.cboContractType.TabIndex = 204;
			// 
			// lblServiceType
			// 
			this.lblServiceType.AutoSize = true;
			this.lblServiceType.Location = new System.Drawing.Point(324, 60);
			this.lblServiceType.Name = "lblServiceType";
			this.lblServiceType.Size = new System.Drawing.Size(43, 13);
			this.lblServiceType.TabIndex = 64;
			this.lblServiceType.Text = "Service";
			// 
			// cboServiceType
			// 
			this.cboServiceType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboServiceType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboServiceType.FormattingEnabled = true;
			this.cboServiceType.Location = new System.Drawing.Point(373, 60);
			this.cboServiceType.Name = "cboServiceType";
			this.cboServiceType.Size = new System.Drawing.Size(162, 21);
			this.cboServiceType.TabIndex = 203;
			// 
			// lblContextType
			// 
			this.lblContextType.AutoSize = true;
			this.lblContextType.Location = new System.Drawing.Point(324, 24);
			this.lblContextType.Name = "lblContextType";
			this.lblContextType.Size = new System.Drawing.Size(43, 13);
			this.lblContextType.TabIndex = 62;
			this.lblContextType.Text = "Context";
			// 
			// cboContextType
			// 
			this.cboContextType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboContextType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboContextType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboContextType.FormattingEnabled = true;
			this.cboContextType.Location = new System.Drawing.Point(373, 21);
			this.cboContextType.Name = "cboContextType";
			this.cboContextType.Size = new System.Drawing.Size(162, 21);
			this.cboContextType.TabIndex = 201;
			// 
			// lblPrimaryKeyType
			// 
			this.lblPrimaryKeyType.AutoSize = true;
			this.lblPrimaryKeyType.Location = new System.Drawing.Point(27, 63);
			this.lblPrimaryKeyType.Name = "lblPrimaryKeyType";
			this.lblPrimaryKeyType.Size = new System.Drawing.Size(62, 13);
			this.lblPrimaryKeyType.TabIndex = 60;
			this.lblPrimaryKeyType.Text = "Primary Key";
			// 
			// cboPrimaryKeyType
			// 
			this.cboPrimaryKeyType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboPrimaryKeyType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboPrimaryKeyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPrimaryKeyType.FormattingEnabled = true;
			this.cboPrimaryKeyType.Location = new System.Drawing.Point(95, 60);
			this.cboPrimaryKeyType.Name = "cboPrimaryKeyType";
			this.cboPrimaryKeyType.Size = new System.Drawing.Size(162, 21);
			this.cboPrimaryKeyType.TabIndex = 202;
			// 
			// lblEntityType
			// 
			this.lblEntityType.AutoSize = true;
			this.lblEntityType.Location = new System.Drawing.Point(56, 24);
			this.lblEntityType.Name = "lblEntityType";
			this.lblEntityType.Size = new System.Drawing.Size(33, 13);
			this.lblEntityType.TabIndex = 58;
			this.lblEntityType.Text = "Entity";
			// 
			// cboEntityType
			// 
			this.cboEntityType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboEntityType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboEntityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboEntityType.FormattingEnabled = true;
			this.cboEntityType.Location = new System.Drawing.Point(95, 21);
			this.cboEntityType.Name = "cboEntityType";
			this.cboEntityType.Size = new System.Drawing.Size(162, 21);
			this.cboEntityType.TabIndex = 200;
			// 
			// lblContractName
			// 
			this.lblContractName.AutoSize = true;
			this.lblContractName.Location = new System.Drawing.Point(63, 122);
			this.lblContractName.Name = "lblContractName";
			this.lblContractName.Size = new System.Drawing.Size(78, 13);
			this.lblContractName.TabIndex = 53;
			this.lblContractName.Text = "Contract Name";
			// 
			// txtContractName
			// 
			this.txtContractName.Location = new System.Drawing.Point(147, 119);
			this.txtContractName.Name = "txtContractName";
			this.txtContractName.Size = new System.Drawing.Size(420, 20);
			this.txtContractName.TabIndex = 3;
			// 
			// txtNamespaceContract
			// 
			this.txtNamespaceContract.Location = new System.Drawing.Point(147, 327);
			this.txtNamespaceContract.Name = "txtNamespaceContract";
			this.txtNamespaceContract.Size = new System.Drawing.Size(420, 20);
			this.txtNamespaceContract.TabIndex = 9;
			// 
			// lblNamespaceContract
			// 
			this.lblNamespaceContract.AutoSize = true;
			this.lblNamespaceContract.Location = new System.Drawing.Point(28, 330);
			this.lblNamespaceContract.Name = "lblNamespaceContract";
			this.lblNamespaceContract.Size = new System.Drawing.Size(113, 13);
			this.lblNamespaceContract.TabIndex = 51;
			this.lblNamespaceContract.Text = "Namespace (Contract)";
			// 
			// chkIsIgnoreRootChildren
			// 
			this.chkIsIgnoreRootChildren.AutoSize = true;
			this.chkIsIgnoreRootChildren.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsIgnoreRootChildren.Location = new System.Drawing.Point(146, 572);
			this.chkIsIgnoreRootChildren.Name = "chkIsIgnoreRootChildren";
			this.chkIsIgnoreRootChildren.Size = new System.Drawing.Size(123, 17);
			this.chkIsIgnoreRootChildren.TabIndex = 111;
			this.chkIsIgnoreRootChildren.Text = "Ignore Root Children";
			this.chkIsIgnoreRootChildren.UseVisualStyleBackColor = true;
			// 
			// chkIsCascadeDelete
			// 
			this.chkIsCascadeDelete.AutoSize = true;
			this.chkIsCascadeDelete.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsCascadeDelete.Location = new System.Drawing.Point(29, 572);
			this.chkIsCascadeDelete.Name = "chkIsCascadeDelete";
			this.chkIsCascadeDelete.Size = new System.Drawing.Size(102, 17);
			this.chkIsCascadeDelete.TabIndex = 110;
			this.chkIsCascadeDelete.Text = "Cascade Delete";
			this.chkIsCascadeDelete.UseVisualStyleBackColor = true;
			// 
			// chkIsRecursiveCrud
			// 
			this.chkIsRecursiveCrud.AutoSize = true;
			this.chkIsRecursiveCrud.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsRecursiveCrud.Location = new System.Drawing.Point(450, 535);
			this.chkIsRecursiveCrud.Name = "chkIsRecursiveCrud";
			this.chkIsRecursiveCrud.Size = new System.Drawing.Size(99, 17);
			this.chkIsRecursiveCrud.TabIndex = 109;
			this.chkIsRecursiveCrud.Text = "Recursive Crud";
			this.chkIsRecursiveCrud.UseVisualStyleBackColor = true;
			// 
			// chkIsIncludeCrud
			// 
			this.chkIsIncludeCrud.AutoSize = true;
			this.chkIsIncludeCrud.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsIncludeCrud.Location = new System.Drawing.Point(346, 535);
			this.chkIsIncludeCrud.Name = "chkIsIncludeCrud";
			this.chkIsIncludeCrud.Size = new System.Drawing.Size(86, 17);
			this.chkIsIncludeCrud.TabIndex = 104;
			this.chkIsIncludeCrud.Text = "Include Crud";
			this.chkIsIncludeCrud.UseVisualStyleBackColor = true;
			// 
			// txtCommonKey
			// 
			this.txtCommonKey.Location = new System.Drawing.Point(249, 533);
			this.txtCommonKey.Name = "txtCommonKey";
			this.txtCommonKey.Size = new System.Drawing.Size(80, 20);
			this.txtCommonKey.TabIndex = 108;
			// 
			// chkIsUseCommonKey
			// 
			this.chkIsUseCommonKey.AutoSize = true;
			this.chkIsUseCommonKey.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsUseCommonKey.Location = new System.Drawing.Point(152, 535);
			this.chkIsUseCommonKey.Name = "chkIsUseCommonKey";
			this.chkIsUseCommonKey.Size = new System.Drawing.Size(88, 17);
			this.chkIsUseCommonKey.TabIndex = 107;
			this.chkIsUseCommonKey.Text = "Common Key";
			this.chkIsUseCommonKey.UseVisualStyleBackColor = true;
			// 
			// txtNamespaceEnumeration
			// 
			this.txtNamespaceEnumeration.Location = new System.Drawing.Point(147, 188);
			this.txtNamespaceEnumeration.Name = "txtNamespaceEnumeration";
			this.txtNamespaceEnumeration.Size = new System.Drawing.Size(420, 20);
			this.txtNamespaceEnumeration.TabIndex = 5;
			// 
			// lblNamespaceEnumeration
			// 
			this.lblNamespaceEnumeration.AutoSize = true;
			this.lblNamespaceEnumeration.Location = new System.Drawing.Point(41, 191);
			this.lblNamespaceEnumeration.Name = "lblNamespaceEnumeration";
			this.lblNamespaceEnumeration.Size = new System.Drawing.Size(100, 13);
			this.lblNamespaceEnumeration.TabIndex = 39;
			this.lblNamespaceEnumeration.Text = "Namespace (Enum)";
			// 
			// txtNamespaceService
			// 
			this.txtNamespaceService.Location = new System.Drawing.Point(147, 223);
			this.txtNamespaceService.Name = "txtNamespaceService";
			this.txtNamespaceService.Size = new System.Drawing.Size(420, 20);
			this.txtNamespaceService.TabIndex = 6;
			// 
			// lblNamespaceService
			// 
			this.lblNamespaceService.AutoSize = true;
			this.lblNamespaceService.Location = new System.Drawing.Point(32, 226);
			this.lblNamespaceService.Name = "lblNamespaceService";
			this.lblNamespaceService.Size = new System.Drawing.Size(109, 13);
			this.lblNamespaceService.TabIndex = 37;
			this.lblNamespaceService.Text = "Namespace (Service)";
			// 
			// txtNamespaceMapping
			// 
			this.txtNamespaceMapping.Location = new System.Drawing.Point(147, 292);
			this.txtNamespaceMapping.Name = "txtNamespaceMapping";
			this.txtNamespaceMapping.Size = new System.Drawing.Size(420, 20);
			this.txtNamespaceMapping.TabIndex = 8;
			// 
			// lblNamespaceMapping
			// 
			this.lblNamespaceMapping.AutoSize = true;
			this.lblNamespaceMapping.Location = new System.Drawing.Point(27, 295);
			this.lblNamespaceMapping.Name = "lblNamespaceMapping";
			this.lblNamespaceMapping.Size = new System.Drawing.Size(114, 13);
			this.lblNamespaceMapping.TabIndex = 35;
			this.lblNamespaceMapping.Text = "Namespace (Mapping)";
			// 
			// txtNamespaceContext
			// 
			this.txtNamespaceContext.Location = new System.Drawing.Point(147, 258);
			this.txtNamespaceContext.Name = "txtNamespaceContext";
			this.txtNamespaceContext.Size = new System.Drawing.Size(420, 20);
			this.txtNamespaceContext.TabIndex = 7;
			// 
			// lblNamespaceContext
			// 
			this.lblNamespaceContext.AutoSize = true;
			this.lblNamespaceContext.Location = new System.Drawing.Point(32, 261);
			this.lblNamespaceContext.Name = "lblNamespaceContext";
			this.lblNamespaceContext.Size = new System.Drawing.Size(109, 13);
			this.lblNamespaceContext.TabIndex = 33;
			this.lblNamespaceContext.Text = "Namespace (Context)";
			// 
			// gbMessages
			// 
			this.gbMessages.Controls.Add(this.lblStatusValue);
			this.gbMessages.Controls.Add(this.lblStatus);
			this.gbMessages.Controls.Add(this.lblLoCValue);
			this.gbMessages.Controls.Add(this.lblLoC);
			this.gbMessages.Controls.Add(this.lblBuilderValue);
			this.gbMessages.Controls.Add(this.lblBuilder);
			this.gbMessages.Controls.Add(this.lblSectionValue);
			this.gbMessages.Controls.Add(this.lblSection);
			this.gbMessages.Controls.Add(this.lblFileValue);
			this.gbMessages.Controls.Add(this.lblFile);
			this.gbMessages.Controls.Add(this.btnCopyToClipboard);
			this.gbMessages.Controls.Add(this.txtMessages);
			this.gbMessages.Location = new System.Drawing.Point(604, 194);
			this.gbMessages.Name = "gbMessages";
			this.gbMessages.Size = new System.Drawing.Size(522, 603);
			this.gbMessages.TabIndex = 1003;
			this.gbMessages.TabStop = false;
			this.gbMessages.Text = "Messages";
			// 
			// lblStatusValue
			// 
			this.lblStatusValue.AutoSize = true;
			this.lblStatusValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatusValue.Location = new System.Drawing.Point(63, 437);
			this.lblStatusValue.Name = "lblStatusValue";
			this.lblStatusValue.Size = new System.Drawing.Size(67, 13);
			this.lblStatusValue.TabIndex = 57;
			this.lblStatusValue.Text = "Status Value";
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(19, 437);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(40, 13);
			this.lblStatus.TabIndex = 56;
			this.lblStatus.Text = "Status:";
			// 
			// lblLoCValue
			// 
			this.lblLoCValue.AutoSize = true;
			this.lblLoCValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLoCValue.Location = new System.Drawing.Point(63, 525);
			this.lblLoCValue.Name = "lblLoCValue";
			this.lblLoCValue.Size = new System.Drawing.Size(56, 13);
			this.lblLoCValue.TabIndex = 55;
			this.lblLoCValue.Text = "LoC Value";
			// 
			// lblLoC
			// 
			this.lblLoC.AutoSize = true;
			this.lblLoC.Location = new System.Drawing.Point(30, 525);
			this.lblLoC.Name = "lblLoC";
			this.lblLoC.Size = new System.Drawing.Size(29, 13);
			this.lblLoC.TabIndex = 54;
			this.lblLoC.Text = "LoC:";
			// 
			// lblBuilderValue
			// 
			this.lblBuilderValue.AutoSize = true;
			this.lblBuilderValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblBuilderValue.Location = new System.Drawing.Point(63, 459);
			this.lblBuilderValue.Name = "lblBuilderValue";
			this.lblBuilderValue.Size = new System.Drawing.Size(69, 13);
			this.lblBuilderValue.TabIndex = 53;
			this.lblBuilderValue.Text = "Builder Value";
			// 
			// lblBuilder
			// 
			this.lblBuilder.AutoSize = true;
			this.lblBuilder.Location = new System.Drawing.Point(17, 459);
			this.lblBuilder.Name = "lblBuilder";
			this.lblBuilder.Size = new System.Drawing.Size(42, 13);
			this.lblBuilder.TabIndex = 52;
			this.lblBuilder.Text = "Builder:";
			// 
			// lblSectionValue
			// 
			this.lblSectionValue.AutoSize = true;
			this.lblSectionValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSectionValue.Location = new System.Drawing.Point(63, 503);
			this.lblSectionValue.Name = "lblSectionValue";
			this.lblSectionValue.Size = new System.Drawing.Size(73, 13);
			this.lblSectionValue.TabIndex = 51;
			this.lblSectionValue.Text = "Section Value";
			// 
			// lblSection
			// 
			this.lblSection.AutoSize = true;
			this.lblSection.Location = new System.Drawing.Point(14, 503);
			this.lblSection.Name = "lblSection";
			this.lblSection.Size = new System.Drawing.Size(46, 13);
			this.lblSection.TabIndex = 50;
			this.lblSection.Text = "Section:";
			// 
			// lblFileValue
			// 
			this.lblFileValue.AutoSize = true;
			this.lblFileValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFileValue.Location = new System.Drawing.Point(63, 481);
			this.lblFileValue.Name = "lblFileValue";
			this.lblFileValue.Size = new System.Drawing.Size(53, 13);
			this.lblFileValue.TabIndex = 49;
			this.lblFileValue.Text = "File Value";
			// 
			// lblFile
			// 
			this.lblFile.AutoSize = true;
			this.lblFile.Location = new System.Drawing.Point(32, 481);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(26, 13);
			this.lblFile.TabIndex = 48;
			this.lblFile.Text = "File:";
			// 
			// btnCopyToClipboard
			// 
			this.btnCopyToClipboard.Location = new System.Drawing.Point(440, 574);
			this.btnCopyToClipboard.Name = "btnCopyToClipboard";
			this.btnCopyToClipboard.Size = new System.Drawing.Size(75, 23);
			this.btnCopyToClipboard.TabIndex = 401;
			this.btnCopyToClipboard.Text = "Copy";
			this.btnCopyToClipboard.UseVisualStyleBackColor = true;
			this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
			// 
			// txtMessages
			// 
			this.txtMessages.Location = new System.Drawing.Point(6, 14);
			this.txtMessages.Multiline = true;
			this.txtMessages.Name = "txtMessages";
			this.txtMessages.ReadOnly = true;
			this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtMessages.Size = new System.Drawing.Size(510, 365);
			this.txtMessages.TabIndex = 400;
			this.txtMessages.WordWrap = false;
			// 
			// chkIsOpenFolder
			// 
			this.chkIsOpenFolder.AutoSize = true;
			this.chkIsOpenFolder.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkIsOpenFolder.Location = new System.Drawing.Point(636, 820);
			this.chkIsOpenFolder.Name = "chkIsOpenFolder";
			this.chkIsOpenFolder.Size = new System.Drawing.Size(84, 17);
			this.chkIsOpenFolder.TabIndex = 500;
			this.chkIsOpenFolder.Text = "Open Folder";
			this.chkIsOpenFolder.UseVisualStyleBackColor = true;
			// 
			// pbBuilder
			// 
			this.pbBuilder.Location = new System.Drawing.Point(12, 845);
			this.pbBuilder.Name = "pbBuilder";
			this.pbBuilder.Size = new System.Drawing.Size(1114, 12);
			this.pbBuilder.TabIndex = 42;
			// 
			// btnPause
			// 
			this.btnPause.Location = new System.Drawing.Point(807, 816);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(75, 23);
			this.btnPause.TabIndex = 502;
			this.btnPause.Text = "Pause";
			this.btnPause.UseVisualStyleBackColor = true;
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(888, 816);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 1004;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// BuilderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1136, 860);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnPause);
			this.Controls.Add(this.pbBuilder);
			this.Controls.Add(this.chkIsOpenFolder);
			this.Controls.Add(this.gbMessages);
			this.Controls.Add(this.gbSettings);
			this.Controls.Add(this.gbBuilders);
			this.Controls.Add(this.txtReset);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnBuild);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "BuilderForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Domain Builder";
			this.gbBuilders.ResumeLayout(false);
			this.gbBuilders.PerformLayout();
			this.gbSettings.ResumeLayout(false);
			this.gbSettings.PerformLayout();
			this.gbTypes.ResumeLayout(false);
			this.gbTypes.PerformLayout();
			this.gbMessages.ResumeLayout(false);
			this.gbMessages.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cboDomain;
        private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.TextBox txtConnection;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtNamespaceEntity;
        private System.Windows.Forms.Label lblNamespaceEntity;
        private System.Windows.Forms.TextBox txtNamespaceShared;
        private System.Windows.Forms.Label lblNamespaceShared;
        private System.Windows.Forms.CheckBox chkIncludeSpatial;
        private System.Windows.Forms.CheckBox chkCleanPath;
        private System.Windows.Forms.Button txtReset;
        private System.Windows.Forms.CheckBox chkIsValidatable;
        private System.Windows.Forms.CheckBox chkIsIncludeProperties;
        private System.Windows.Forms.CheckBox chkIsIncludeAuditFields;
        private System.Windows.Forms.CheckBox chkIsIncludePrimaryKey;
        private System.Windows.Forms.CheckBox chkEntityBuilder;
        private System.Windows.Forms.CheckBox chkContextBuilder;
        private System.Windows.Forms.CheckBox chkMappingBuilder;
        private System.Windows.Forms.GroupBox gbBuilders;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.CheckBox chkEnumerationBuilder;
        private System.Windows.Forms.CheckBox chkServiceBuilder;
        private System.Windows.Forms.TextBox txtNamespaceEnumeration;
        private System.Windows.Forms.Label lblNamespaceEnumeration;
        private System.Windows.Forms.TextBox txtNamespaceService;
        private System.Windows.Forms.Label lblNamespaceService;
        private System.Windows.Forms.TextBox txtNamespaceMapping;
        private System.Windows.Forms.Label lblNamespaceMapping;
        private System.Windows.Forms.TextBox txtNamespaceContext;
        private System.Windows.Forms.Label lblNamespaceContext;
        private System.Windows.Forms.GroupBox gbMessages;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.CheckBox chkServiceInterfaceBuilder;
        private System.Windows.Forms.TextBox txtCommonKey;
        private System.Windows.Forms.CheckBox chkIsUseCommonKey;
        private System.Windows.Forms.CheckBox chkIsRecursiveCrud;
        private System.Windows.Forms.CheckBox chkIsIncludeCrud;
        private System.Windows.Forms.CheckBox chkIsOpenFolder;
        private System.Windows.Forms.CheckBox chkIsCascadeDelete;
        private System.Windows.Forms.CheckBox chkIsIgnoreRootChildren;
        private System.Windows.Forms.CheckBox chkAppContextBuilder;
        private System.Windows.Forms.Button btnCopyToClipboard;
        private System.Windows.Forms.CheckBox chkContractBuilder;
        private System.Windows.Forms.Label lblContractName;
        private System.Windows.Forms.TextBox txtContractName;
        private System.Windows.Forms.TextBox txtNamespaceContract;
        private System.Windows.Forms.Label lblNamespaceContract;
        private System.Windows.Forms.GroupBox gbTypes;
        private System.Windows.Forms.Label lblContractType;
        private System.Windows.Forms.ComboBox cboContractType;
        private System.Windows.Forms.Label lblServiceType;
        private System.Windows.Forms.ComboBox cboServiceType;
        private System.Windows.Forms.Label lblContextType;
        private System.Windows.Forms.ComboBox cboContextType;
        private System.Windows.Forms.Label lblPrimaryKeyType;
        private System.Windows.Forms.ComboBox cboPrimaryKeyType;
        private System.Windows.Forms.Label lblEntityType;
        private System.Windows.Forms.ComboBox cboEntityType;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblLoCValue;
        private System.Windows.Forms.Label lblLoC;
        private System.Windows.Forms.Label lblBuilderValue;
        private System.Windows.Forms.Label lblBuilder;
        private System.Windows.Forms.Label lblSectionValue;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Label lblFileValue;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.ProgressBar pbBuilder;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.CheckBox chkAppServiceInterfaceBuilder;
        private System.Windows.Forms.CheckBox chkAppServiceBuilder;
        private System.Windows.Forms.TextBox txtNamespaceAppService;
        private System.Windows.Forms.Label lblNamespaceAppService;
        private System.Windows.Forms.Label lblAppServiceType;
        private System.Windows.Forms.ComboBox cboAppServiceType;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox chkIsIncludeCrudEntity;
        private System.Windows.Forms.Button btnConnectionStringDialog;
        private System.Windows.Forms.CheckBox chkBuilderToggleAll;
        private System.Windows.Forms.CheckBox chkControllerApiBuilder;
        private System.Windows.Forms.CheckBox chkControllerMvcBuilder;
        private System.Windows.Forms.TextBox txtNamespaceControllerMvc;
        private System.Windows.Forms.Label lblNamespaceControllerMvc;
        private System.Windows.Forms.TextBox txtNamespaceControllerApi;
        private System.Windows.Forms.Label lblNamespaceControllerApi;
        private System.Windows.Forms.Label lblControllerMvcType;
        private System.Windows.Forms.ComboBox cboControllerMvcType;
        private System.Windows.Forms.Label lblControllerApiType;
        private System.Windows.Forms.ComboBox cboControllerApiType;
        private System.Windows.Forms.CheckBox chkIsTracked;
        private System.Windows.Forms.Label lblSchema;
        private System.Windows.Forms.ComboBox cboSchema;
        private System.Windows.Forms.CheckBox chkEntityPartialBuilder;
        private System.Windows.Forms.CheckBox chkUsePropertyInclusions;
        private System.Windows.Forms.CheckBox chkIsIncludeDeletableFields;
        private System.Windows.Forms.CheckBox chkNetCore;
        private System.Windows.Forms.CheckBox chkAutoMapperProfileBuilder;
    }
}

