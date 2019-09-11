using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Bedrock.DomainBuilder;
using Bedrock.DomainBuilder.Builder;
using Bedrock.DomainBuilder.Enumerations;

using Microsoft.Data.ConnectionUI;

namespace Bedrock.DomainBuilder.UI
{
    public partial class BuilderForm : Form
    {
		#region Fields
		private const string EmptyStringText = "<empty>";
		#endregion

		#region Constructors
		public BuilderForm()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region Properties
        protected BuildManager BuildManager { get; set; }
        #endregion

        #region Event Handlers
        private async void btnBuild_Click(object sender, EventArgs e)
        {
            try
            {
                if (!EnsureActiveBuilders())
                    return;

                InitializeFormForBuild();

                var builds = BuildManager.Build();

                await Task.WhenAll(builds.ToArray());

                PostBuild();
            }
            catch (Exception ex)
            {
                var message = string.Format("There was an error.{0}{0}{1}", Environment.NewLine, ex.Message);

                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableForm(true);
            }
        }

        private void btnConnectionStringDialog_Click(object sender, EventArgs e)
        {
            var dataConnectionDialog = new DataConnectionDialog();
            DataSource.AddStandardDataSources(dataConnectionDialog);

            if (DataConnectionDialog.Show(dataConnectionDialog) == DialogResult.OK)
                txtConnection.Text = dataConnectionDialog.ConnectionString;
        }

        private void chkBuilderToggleAll_CheckedChanged(object sender, EventArgs e)
        {
            ToggleBuilders(chkBuilderToggleAll.Checked);
        }

        private void Builder_OnBuilderStarted(eBuilder builder)
        {
            SetStatus(eBuildStatus.Started, builder);
        }

        private void Builder_OnBuilderInitialized(eBuilder builder)
        {
            SetStatus(eBuildStatus.Initialized, builder);
        }

        private void Builder_OnBuilderBuilding(eBuilder builder)
        {
            SetStatus(eBuildStatus.Building, builder);
        }

        private void Builder_OnBuilderFileCountUpdate(eBuilder builder, int fileCount)
        {
            if (BuildManager.Statistics.IsAllBuildersReportedFileCount)
            {
                InvokeInternal(() => pbBuilder.Maximum = BuildManager.Statistics.TotalFilesToBuild);
                BuildManager.PauseBuild(false);
            }
        }

        private void Builder_OnFileBuilding(eBuilder builder, string file) { }

        private void Builder_OnBuilderPassComplete(eBuilder builder, string buildSection)
        {
            SetSection(buildSection, builder);
        }

        private void Builder_OnFileBuilt(eBuilder builder, string file, int loc)
        {
            SetFile(file, builder);
            SetLoC();
            SetProgressBar();
        }

        private void Builder_OnBuilderComplete(eBuilder builder, int filesCreated, int loc)
        {
            if (BuildManager.Statistics.IsBuildComplete && !BuildManager.IsCancelled)
            {
                SetStatus(eBuildStatus.Complete, builder);

                SetMessage
                (
                    string.Concat
                    (
                        BuildManager.Statistics.GetBuildCompleteMessage(),
                        Environment.NewLine, BuildManager.Statistics.GetFilesCreatedMessage(),
                        Environment.NewLine, BuildManager.Statistics.GetLoCMessage(),
                        Environment.NewLine, BuildManager.Statistics.GetWarningsMessage()
                    )
                );
            }
        }

        private void Builder_OnBuilderWarning(BuildWarning warning)
        {
            SetMessage(BuildStatistics.GetWarningMessage(warning));
        }

        private void cboDomain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BuildManager.Settings.Domain = (eDomain)cboDomain.SelectedValue;
            InitializeDomainSpecific();
        }

        private void Builder_Click(object sender, EventArgs e)
        {
            BuilderClicked();
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMessages.Text))
            {
                Clipboard.SetText(txtMessages.Text);
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            BuildManager.PauseBuild(!BuildManager.IsPaused);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            BuildManager.CancelBuild();
        }

        private void txtReset_Click(object sender, EventArgs e)
        {
            Initialize();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Form Methods
        // This double-buffers the entire form and all child controls,
        // to prevent visible screen paint artifacts on update
        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;
                createParams.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return createParams;
            }
        }
        #endregion

        #region Private Methods (Initialize)
        private void Initialize()
        {
            BuildManager = new BuildManager(eDomain.Template, AddBuilderEventHandlers);
            InitializeForm();
        }

        private void InitializeForm()
        {
            InitializeDomains();
            InitializeConnectionString();
            InitializeSchemas();
            InitializeDomainPath();
            InitializeNamespaceEntity();
            InitializeNamespaceShared();
            InitializeEntityTypes();
            InitializePrimaryKeyTypes();
            InitializeIsValidatable();
            InitializeIsIncludeProperties();
            InitializeIsIncludePrimaryKey();
            InitializeIsIncludeAuditFields();
            InitializeIsIncludeDeletableFields();
            InitializeIsIncludeSpatial();
            InitializeIsCleanPath();
            InitializeIsNetCore();
            InitializeStatusValue();
            InitializeBuilderValue();
            InitializeFileValue();
            InitializeSectionValue();
            InitializeActiveBuilders();
            InitializeNamespaceContext();
            InitializeNamespaceMapping();
            InitializeNamespaceService();
            InitializeNamespaceEnumeration();
            InitializeContextTypes();
            InitializeServiceTypes();
            InitializeMessages();
            InitializeIsIncludeCommonKey();
            InitializeCommonKey();
            InitializeIsIncludeCrud();
            InitializeIsIncludeCrudEntity();
            InitializeIsRecursiveCrud();
            InitializeIsOpenFolder();
            InitializeLocValue();
            InitializeCascadeDelete();
            InitializeIgnoreRootChildren();
            InitializeNamespaceContract();
            InitializeContractName();
            InitializeContractTypes();
            InitializeNamespaceAppService();
            InitializeAppServiceTypes();
            InitializeNamespaceControllerApi();
            InitializeControllerApiTypes();
            InitializeNamespaceControllerMvc();
            InitializeControllerMvcTypes();
            InitializeProgressbar();
            InitializeIsTracked();
            InitializeUsePropertyInclusions();
        }

        private void InitializeDomains()
        {
            InitializeComboBox(cboDomain, BuildManager.Settings.Domain);
        }

        private void InitializeSchemas()
        {
            cboSchema.DataSource = BuildManager.StoreSchemas;
        }

        private void InitializeConnectionString()
        {
            txtConnection.Text = BuildManager.Settings.ConnectionString;
        }

        private void InitializeDomainPath()
        {
            txtPath.Text = BuildManager.Settings.DomainPath;
        }

        private void InitializeNamespaceEntity()
        {
            txtNamespaceEntity.Text = BuildManager.Settings.NamespaceEntity;
        }

        private void InitializeNamespaceShared()
        {
            txtNamespaceShared.Text = BuildManager.Settings.NamespaceShared;
        }

        private void InitializeEntityTypes()
        {
            InitializeComboBox<eEntityType>(cboEntityType, BuildManager.Settings.EntityType);
        }

        private void InitializePrimaryKeyTypes()
        {
            InitializeComboBox<ePrimaryKeyType>(cboPrimaryKeyType, BuildManager.Settings.PrimaryKeyType);
        }

        private void InitializeIsValidatable()
        {
            chkIsValidatable.Checked = BuildManager.Settings.IsValidatable;
        }

        private void InitializeIsIncludeProperties()
        {
            chkIsIncludeProperties.Checked = BuildManager.Settings.IsIncludeProperties;
        }

        private void InitializeIsIncludePrimaryKey()
        {
            chkIsIncludePrimaryKey.Checked = BuildManager.Settings.IsIncludePrimaryKey;
        }

        private void InitializeIsIncludeAuditFields()
        {
            chkIsIncludeAuditFields.Checked = BuildManager.Settings.IsIncludeAuditFields;
        }

        private void InitializeIsIncludeDeletableFields()
        {
            chkIsIncludeDeletableFields.Checked = BuildManager.Settings.IsIncludeDeletableFields;
        }

        private void InitializeIsIncludeSpatial()
        {
            chkIncludeSpatial.Checked = BuildManager.Settings.IsIncludeSpatialType;
        }

        private void InitializeIsCleanPath()
        {
            chkCleanPath.Checked = BuildManager.Settings.IsCleanPath;
        }

        private void InitializeIsNetCore()
        {
            chkNetCore.Checked = BuildManager.Settings.IsNetCore;
        }

        private void InitializeNamespaceContext()
        {
            txtNamespaceContext.Text = BuildManager.Settings.NamespaceContext;
        }

        private void InitializeNamespaceMapping()
        {
            txtNamespaceMapping.Text = BuildManager.Settings.NamespaceMapping;
        }

        private void InitializeNamespaceService()
        {
            txtNamespaceService.Text = BuildManager.Settings.NamespaceService;
        }

        private void InitializeNamespaceEnumeration()
        {
            txtNamespaceEnumeration.Text = BuildManager.Settings.NamespaceEnumeration;
        }

        private void InitializeContextTypes()
        {
            InitializeComboBox<eContextType>(cboContextType, BuildManager.Settings.ContextType);
        }

        private void InitializeServiceTypes()
        {
            InitializeComboBox<eServiceType>(cboServiceType, BuildManager.Settings.ServiceType);
        }

        private void InitializeProgressbar()
        {
            pbBuilder.Value = 0;
            pbBuilder.Step = 1;
        }

        private void InitializeIsTracked()
        {
            chkIsTracked.Checked = BuildManager.Settings.IsTracked;
        }

        private void InitializeUsePropertyInclusions()
        {
            chkUsePropertyInclusions.Checked = BuildManager.Settings.IsUsePropertyInclusions;
        }

        private void InitializeFormForBuild()
        {
            EnableForm(false);
            UpdateSettings();

            InitializeBuilderValue();
            InitializeFileValue();
            InitializeSectionValue();
            InitializeLocValue();
            InitializeMessages();
            InitializeProgressbar();
        }

        private void InitializeDomainSpecific()
        {
            InitializeConnectionString();
            InitializeSchemas();
            InitializeDomainPath();
            InitializeNamespaceEntity();
            InitializeNamespaceContext();
            InitializeNamespaceMapping();
            InitializeNamespaceService();
            InitializeNamespaceEnumeration();
            InitializeNamespaceContract();
            InitializeNamespaceAppService();
            InitializeNamespaceControllerApi();
            InitializeNamespaceControllerMvc();
        }

        private void InitializeStatusValue()
        {
            SetFontStyleItalic(lblStatusValue);
            lblStatusValue.Text = EmptyStringText;
        }

        private void InitializeBuilderValue()
        {
            SetFontStyleItalic(lblBuilderValue);
            lblBuilderValue.Text = EmptyStringText;
        }

        private void InitializeFileValue()
        {
            SetFontStyleItalic(lblFileValue);
            lblFileValue.Text = EmptyStringText;
        }

        private void InitializeSectionValue()
        {
            SetFontStyleItalic(lblSectionValue);
            lblSectionValue.Text = EmptyStringText;
        }

        private void InitializeLocValue()
        {
            SetFontStyleItalic(lblLoCValue);
            lblLoCValue.Text = EmptyStringText;
        }

        private void InitializeNamespaceContract()
        {
            txtNamespaceContract.Text = BuildManager.Settings.NamespaceContract;
        }

        private void InitializeContractName()
        {
            txtContractName.Text = BuildManager.Settings.ContractName;
        }

        private void InitializeContractTypes()
        {
            InitializeComboBox<eContractType>(cboContractType, BuildManager.Settings.ContractType);
        }

        private void InitializeNamespaceAppService()
        {
            txtNamespaceAppService.Text = BuildManager.Settings.NamespaceAppService;
        }

        private void InitializeAppServiceTypes()
        {
            InitializeComboBox<eAppServiceType>(cboAppServiceType, BuildManager.Settings.AppServiceType);
        }

        private void InitializeNamespaceControllerApi()
        {
            txtNamespaceControllerApi.Text = BuildManager.Settings.NamespaceControllerApi;
        }

        private void InitializeControllerApiTypes()
        {
            InitializeComboBox<eControllerApiType>(cboControllerApiType, BuildManager.Settings.ControllerApiType);
        }

        private void InitializeNamespaceControllerMvc()
        {
            txtNamespaceControllerMvc.Text = BuildManager.Settings.NamespaceControllerMvc;
        }

        private void InitializeControllerMvcTypes()
        {
            InitializeComboBox<eControllerMvcType>(cboControllerMvcType, BuildManager.Settings.ControllerMvcType);
        }

        private void InitializeActiveBuilders()
        {
            BuildManager.Settings.ActiveBuilders.Each(b =>
            {
                switch (b)
                {
                    case eBuilder.Entity:
                        {
                            chkEntityBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.EntityPartial:
                        {
                            chkEntityPartialBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.Mapping:
                        {
                            chkMappingBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.Context:
                        {
                            chkContextBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.Service:
                        {
                            chkServiceBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.ServiceInterface:
                        {
                            chkServiceInterfaceBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.Enumeration:
                        {
                            chkEnumerationBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.AppContext:
                        {
                            chkAppContextBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.Contract:
                        {
                            chkContractBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.AppService:
                        {
                            chkAppServiceBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.AppServiceInterface:
                        {
                            chkAppServiceInterfaceBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.ControllerApi:
                        {
                            chkControllerApiBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.ControllerMvc:
                        {
                            chkControllerMvcBuilder.Checked = true;
                            break;
                        }
                    case eBuilder.AutoMapperProfile:
                        {
                            chkAutoMapperProfileBuilder.Checked = true;
                            break;
                        }
                    default:
                        throw new NotSupportedException(string.Concat("Unsupported builder:  ", b.ToString()));
                }
            });

            chkBuilderToggleAll.Checked = BuildManager.Settings.ActiveBuilders.Count() == Enum.GetValues(typeof(eBuilder)).Length;
        }

        private void InitializeMessages()
        {
            txtMessages.Text = string.Empty;
        }

        private void InitializeIsIncludeCommonKey()
        {
            chkIsUseCommonKey.Checked = BuildManager.Settings.IsUseCommonKey;
        }

        private void InitializeCommonKey()
        {
            txtCommonKey.Text = BuildManager.Settings.CommonKey;
        }

        private void InitializeIsIncludeCrud()
        {
            chkIsIncludeCrud.Checked = BuildManager.Settings.IsIncludeCrud;
        }

        private void InitializeIsIncludeCrudEntity()
        {
            chkIsIncludeCrudEntity.Checked = BuildManager.Settings.IsIncludeCrudEntity;
        }

        private void InitializeIsRecursiveCrud()
        {
            chkIsRecursiveCrud.Checked = BuildManager.Settings.IsRecursiveCrud;
        }

        private void InitializeIsOpenFolder()
        {
            chkIsOpenFolder.Checked = BuildManager.Settings.IsOpenFolder;
        }

        private void InitializeCascadeDelete()
        {
            chkIsCascadeDelete.Checked = BuildManager.Settings.IsCascadeOnDelete;
        }

        private void InitializeIgnoreRootChildren()
        {
            chkIsIgnoreRootChildren.Checked = BuildManager.Settings.IsIgnoreRootChildren;
        }
        #endregion

        #region Private Methods
        private void AddBuilderEventHandlers(eBuilder builderType, IBuilder builder)
        {
            AddBuilderEventHandlersDefault(builder);
            AddBuilderEventHandlersIndividual(builderType, builder);
        }

        private void AddBuilderEventHandlersDefault(IBuilder builder)
        {
            builder.OnBuilderStarted += Builder_OnBuilderStarted;
            builder.OnBuilderInitialized += Builder_OnBuilderInitialized;
            builder.OnBuilderBuilding += Builder_OnBuilderBuilding;
            builder.OnBuilderFileCountUpdate += Builder_OnBuilderFileCountUpdate;
            builder.OnFileBuilding += Builder_OnFileBuilding;
            builder.OnBuilderPassComplete += Builder_OnBuilderPassComplete;
            builder.OnFileBuilt += Builder_OnFileBuilt;
            builder.OnBuilderComplete += Builder_OnBuilderComplete;
            builder.OnBuilderWarning += Builder_OnBuilderWarning;
        }

        private void AddBuilderEventHandlersIndividual(eBuilder builderType, IBuilder builder)
        {
            switch (builderType)
            {
                case eBuilder.Entity:
                    {
                        var builderEntity = (BuilderEntity)builder;
                        break;
                    }
                case eBuilder.EntityPartial:
                    {
                        var builderEntity = (BuilderEntityPartial)builder;
                        break;
                    }
                case eBuilder.Mapping:
                    {
                        var builderMapping = (BuilderMapping)builder;
                        break;
                    }
                case eBuilder.Context:
                    {
                        var builderContext = (BuilderContext)builder;
                        break;
                    }
                case eBuilder.Service:
                    {
                        var builderService = (BuilderService)builder;
                        break;
                    }
                case eBuilder.ServiceInterface:
                    {
                        var builderServiceInterface = (BuilderServiceInterface)builder;
                        break;
                    }
                case eBuilder.Enumeration:
                    {
                        var builderEnumeration = (BuilderEnumeration)builder;
                        break;
                    }
                case eBuilder.AppContext:
                    {
                        var builderAppContext = (BuilderAppContext)builder;
                        break;
                    }
                case eBuilder.Contract:
                    {
                        var builderContract = (BuilderContract)builder;
                        break;
                    }
                case eBuilder.AppService:
                    {
                        var builderAppService = (BuilderAppService)builder;
                        break;
                    }
                case eBuilder.AppServiceInterface:
                    {
                        var builderAppServiceInterface = (BuilderAppServiceInterface)builder;
                        break;
                    }
                case eBuilder.ControllerApi:
                    {
                        var builderControllerApi = (BuilderControllerApi)builder;
                        break;
                    }
                case eBuilder.ControllerMvc:
                    {
                        var builderControllerMvc = (BuilderControllerMvc)builder;
                        break;
                    }
                case eBuilder.AutoMapperProfile:
                    {
                        var builderControllerMvc = (BuilderAutoMapperProfile)builder;
                        break;
                    }
                default:
                    throw new NotSupportedException(string.Concat("Unsupported builder:  ", builderType.ToString()));
            }
        }

        private bool EnsureActiveBuilders()
        {
            if (!BuildManager.EnsureActiveBuilders())
            {
                MessageBox.Show("Choose at least one Builder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        private void UpdateSettings()
        {
            BuildManager.Settings.Domain = (eDomain)cboDomain.SelectedValue;
            BuildManager.Settings.StoreSchemaNamespace = (string)cboSchema.SelectedValue;
            BuildManager.Settings.ConnectionString = txtConnection.Text;
            BuildManager.Settings.DomainPath = txtPath.Text;
            BuildManager.Settings.NamespaceEntity = txtNamespaceEntity.Text;
            BuildManager.Settings.NamespaceShared = txtNamespaceShared.Text;
            BuildManager.Settings.EntityType = (eEntityType)cboEntityType.SelectedValue;
            BuildManager.Settings.PrimaryKeyType = (ePrimaryKeyType)cboPrimaryKeyType.SelectedValue;
            BuildManager.Settings.IsValidatable = chkIsValidatable.Checked;
            BuildManager.Settings.IsIncludeProperties = chkIsIncludeProperties.Checked;
            BuildManager.Settings.IsIncludePrimaryKey = chkIsIncludePrimaryKey.Checked;
            BuildManager.Settings.IsIncludeAuditFields = chkIsIncludeAuditFields.Checked;
            BuildManager.Settings.IsIncludeDeletableFields = chkIsIncludeDeletableFields.Checked;
            BuildManager.Settings.IsIncludeSpatialType = chkIncludeSpatial.Checked;
            BuildManager.Settings.IsCleanPath = chkCleanPath.Checked;
            BuildManager.Settings.IsNetCore = chkNetCore.Checked;
            BuildManager.Settings.NamespaceContext = txtNamespaceContext.Text;
            BuildManager.Settings.NamespaceMapping = txtNamespaceMapping.Text;
            BuildManager.Settings.NamespaceService = txtNamespaceService.Text;
            BuildManager.Settings.NamespaceEnumeration = txtNamespaceEnumeration.Text;
            BuildManager.Settings.ContextType = (eContextType)cboContextType.SelectedValue;
            BuildManager.Settings.ServiceType = (eServiceType)cboServiceType.SelectedValue;
            BuildManager.Settings.IsUseCommonKey = chkIsUseCommonKey.Checked;
            BuildManager.Settings.CommonKey = txtCommonKey.Text;
            BuildManager.Settings.IsIncludeCrud = chkIsIncludeCrud.Checked;
            BuildManager.Settings.IsIncludeCrudEntity = chkIsIncludeCrudEntity.Checked;
            BuildManager.Settings.IsRecursiveCrud = chkIsRecursiveCrud.Checked;
            BuildManager.Settings.IsOpenFolder = chkIsOpenFolder.Checked;
            BuildManager.Settings.IsCascadeOnDelete = chkIsCascadeDelete.Checked;
            BuildManager.Settings.IsIgnoreRootChildren = chkIsIgnoreRootChildren.Checked;
            BuildManager.Settings.NamespaceContract = txtNamespaceContract.Text;
            BuildManager.Settings.ContractName = txtContractName.Text;
            BuildManager.Settings.ContractType = (eContractType)cboContractType.SelectedValue;
            BuildManager.Settings.NamespaceAppService = txtNamespaceAppService.Text;
            BuildManager.Settings.AppServiceType = (eAppServiceType)cboAppServiceType.SelectedValue;
            BuildManager.Settings.NamespaceControllerApi = txtNamespaceControllerApi.Text;
            BuildManager.Settings.ControllerApiType = (eControllerApiType)cboControllerApiType.SelectedValue;
            BuildManager.Settings.NamespaceControllerMvc = txtNamespaceControllerMvc.Text;
            BuildManager.Settings.ControllerMvcType = (eControllerMvcType)cboControllerMvcType.SelectedValue;
            BuildManager.Settings.IsTracked = chkIsTracked.Checked;
            BuildManager.Settings.IsUsePropertyInclusions = chkUsePropertyInclusions.Checked;
        }

        private void EnableForm(bool isEnabled)
        {
            EnableForm(this, isEnabled);

            InvokeInternal(() =>
            {
                btnPause.Enabled = true;
                btnStop.Enabled = true;
                txtMessages.Enabled = true;

				if (!isEnabled)
					Cursor.Current = Cursors.WaitCursor;
				else
					Cursor.Current = Cursors.Default;
			});
		}

        private void EnableForm(Control container, bool isEnabled)
        {
            InvokeInternal(() =>
            {
                foreach (Control control in container.Controls)
                {
                    if (control is Panel || control is GroupBox)
                        EnableForm(control, isEnabled);
                    else
                        control.Enabled = isEnabled;
                }
            });
        }

        private void InitializeComboBox<T>(ComboBox control, T defaultValue)
            where T : struct, IConvertible
        {
            var types = Enum.GetValues(typeof(T))
                            .Cast<T>()
                            .Select(d => new { DisplayMember = d.ToString(), ValueMember = d })
                            .ToList();

            control.DataSource = types;
            control.DisplayMember = nameof(control.DisplayMember);
            control.ValueMember = nameof(control.ValueMember);
            control.SelectedValue = defaultValue;
        }

        private void SetStatus(eBuildStatus status, eBuilder builder)
        {
            InvokeInternal(() =>
            {
                SetFontStyle(lblStatusValue, FontStyle.Regular);

                lblStatusValue.Text = status.ToString().AddSpacesBeforeCapitals();
                lblStatusValue.Refresh();
            });

            SetBuilder(builder);
        }

        private void SetBuilder(eBuilder builder)
        {
            InvokeInternal(() =>
            {
                SetFontStyle(lblBuilderValue, FontStyle.Regular);

                lblBuilderValue.Text = builder.ToString();
                lblBuilderValue.Refresh();
            });
        }

        private void SetFile(string file, eBuilder builder)
        {
            InvokeInternal(() =>
            {
                SetFontStyle(lblFileValue, FontStyle.Regular);

                lblFileValue.Text = file;
                lblFileValue.Refresh();
            });

            SetBuilder(builder);
        }

        private void SetSection(string buildSection, eBuilder builder)
        {
            InvokeInternal(() =>
            {
                SetFontStyle(lblSectionValue, FontStyle.Regular);

                lblSectionValue.Text = buildSection.ToString().AddSpacesBeforeCapitals();
                lblSectionValue.Refresh();
            });

            SetBuilder(builder);
        }

        private void SetLoC()
        {
            InvokeInternal(() =>
            {
                SetFontStyle(lblLoCValue, FontStyle.Regular);

                lblLoCValue.Text = BuildManager.Statistics.LoCTotal.AddThousandthsPlaceCommas();
                lblLoCValue.Refresh();
            });
        }

        private void SetMessage(string message)
        {
            InvokeInternal(() =>
            {
                txtMessages.Text += string.Concat(message, Environment.NewLine);
            });
        }

        private void SetFontStyleItalic(Control control)
        {
            SetFontStyle(control, FontStyle.Italic);
        }

        private void SetFontStyle(Control control, FontStyle fontStyle)
        {
            control.Font = new Font(control.Font, fontStyle);
        }

        private void SetProgressBar()
        {
            InvokeInternal(() =>
            {
                pbBuilder.Value += 1;
                pbBuilder.Refresh();
            });
        }

        private void InvokeInternal(Action action)
        {
            Invoke((MethodInvoker)delegate
            {
				action?.Invoke();
			});
        }

        private void PostBuild()
        {
            if (BuildManager.IsCancelled)
                Initialize();
            else if (BuildManager.Settings.IsOpenFolder)
                Process.Start(BuildManager.Settings.DomainPath);

            EnableForm(true);
        }

        private void ToggleBuilders(bool isActive)
        {
            chkEntityBuilder.Checked = isActive;
            chkEntityPartialBuilder.Checked = isActive;
            chkServiceBuilder.Checked = isActive;
            chkServiceInterfaceBuilder.Checked = isActive;
            chkEnumerationBuilder.Checked = isActive;
            chkContextBuilder.Checked = isActive;
            chkMappingBuilder.Checked = isActive;
            chkAppContextBuilder.Checked = isActive;
            chkContractBuilder.Checked = isActive;
            chkAppServiceBuilder.Checked = isActive;
            chkAppServiceInterfaceBuilder.Checked = isActive;
            chkControllerApiBuilder.Checked = isActive;
            chkControllerMvcBuilder.Checked = isActive;
            chkAutoMapperProfileBuilder.Checked = isActive;

            BuilderClicked();
        }

        private void BuilderClicked()
        {
            var isCheckedEntity = chkEntityBuilder.Checked;
            var isCheckedEntityPartial = chkEntityPartialBuilder.Checked;
            var isCheckedMapping = chkMappingBuilder.Checked;
            var isCheckedContext = chkContextBuilder.Checked;
            var isCheckedService = chkServiceBuilder.Checked;
            var isCheckedServiceInterface = chkServiceInterfaceBuilder.Checked;
            var isCheckedEnumeration = chkEnumerationBuilder.Checked;
            var isCheckedAppContext = chkAppContextBuilder.Checked;
            var isCheckedContract = chkContractBuilder.Checked;
            var isCheckedAppService = chkAppServiceBuilder.Checked;
            var isCheckedAppServiceInterface = chkAppServiceInterfaceBuilder.Checked;
            var isCheckedControllerApi = chkControllerApiBuilder.Checked;
            var isCheckedControllerMvc = chkControllerMvcBuilder.Checked;
            var isCheckedAutoMapperProfile = chkAutoMapperProfileBuilder.Checked;

            BuildManager.Settings.ActiveBuilders.Clear();

            if (isCheckedEntity)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.Entity);

            if (isCheckedEntityPartial)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.EntityPartial);

            if (isCheckedMapping)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.Mapping);

            if (isCheckedContext)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.Context);

            if (isCheckedService)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.Service);

            if (isCheckedServiceInterface)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.ServiceInterface);

            if (isCheckedEnumeration)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.Enumeration);

            if (isCheckedAppContext)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.AppContext);

            if (isCheckedContract)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.Contract);

            if (isCheckedAppService)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.AppService);

            if (isCheckedAppServiceInterface)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.AppServiceInterface);

            if (isCheckedControllerApi)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.ControllerApi);

            if (isCheckedControllerMvc)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.ControllerMvc);

            if (isCheckedAutoMapperProfile)
                BuildManager.Settings.ActiveBuilders.Add(eBuilder.AutoMapperProfile);

            BuildManager.UpdateActiveBuilders();
        }
        #endregion
    }
}
