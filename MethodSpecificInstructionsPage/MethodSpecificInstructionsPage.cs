using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Common;
using DAL;
using LSExtensionControlLib;
using LSSERVICEPROVIDERLib;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using System.Linq;

namespace MethodSpecificInstructionsPage
{

    [ComVisible(true)]
    [ProgId("MethodSpecificInstructionsPage.MethodSpecificInstructionsPage")]
    public partial class MethodSpecificInstructionsPage : UserControl, IExtensionControl, IMockSupportedPageExtension
    {
        private double productId;
        private INautilusDBConnection _ntlsCon;
        private IExtensionControlSite _site;

        private Product _product;
        private IDataLayer _dal;
        private MethodSpecificInstructions _instructions;
        private bool newInstructions;

        #region Ctor

        public MethodSpecificInstructionsPage()
        {
            InitializeComponent();
            this.BackColor = Color.FromName("Control");
        }

        #endregion

        #region Page Extension Interface

        public void SetReadOnly(bool readOnly)
        {
        }

        public void Internationalise()
        {
        }

        public void SetSite(object site)
        {
            if (site != null)
            {
                _site = site as IExtensionControlSite;
            }
        }

        public void SetupData()
        {
            bool flag;
            if (_site != null)
            {
                // Set the page name
                _site.SetPageName("הוראות ספציפיות לביצוע השיטה");

                // Get the record id
                _site.GetDoubleValue("PRODUCT_ID", out productId, out flag);

                InitUI();
            }
        }


        public void EnterPage()
        {
        }

        public void ExitPage()
        {
        }

        public void PreDisplay() // occurs only via Nautilus
        {
            Utils.CreateConstring(_ntlsCon);
            _dal = new DataLayer();
            Connect();
        }

        public void SetServiceProvider(object serviceProvider)
        {
            var sp = serviceProvider as NautilusServiceProvider;
            _ntlsCon = Utils.GetNtlsCon(sp);
        }

        public void SaveSettings(int hKey)
        {}

        public void RestoreSettings(int hKey)
        {
        }

        #endregion

        #region IMockSupportedPageExtension

        public void InitDal(IDataLayer dal)
        {
            _dal = dal;
            Connect();
        }

        
        private void btnNewMethod_Click(object sender, EventArgs e)
        {
            radPageView1.Enabled = true;
            txtProductName.Enabled = true;
//            txtClientName.Enabled = true;
            _instructions = new MethodSpecificInstructions();
            _product.MethodSpecificInstructions = _instructions;
            newInstructions = true;

            btnNewMethod.Enabled = false;

            InitUI();

        }


        public void SaveData() // implements both interfaces.
        {
            try
            {
                if (newInstructions)
                {
                    long id = _dal.GetNewID("SQ_U_METHOD_INSTRUCTION");
                    _instructions.U_METHOD_INSTRUCTION_ID = id;
                    _instructions.Name = "MSI-" + id;
                }

                _instructions.ChemicalNeutralization = txtChemicalNeutralization.Text.Trim();
                _instructions.MembranceFltration = txtMembranceFiltration.Text.Trim();
                _instructions.MaxDilution = txtMaxDilution.Text.Trim();


                if (radBtnNeutralizationType1.IsChecked)
                {
                    _instructions.NeutralizationType = 1;
                }
                else
                {
                    if (radBtnNeutralizationType2.IsChecked)
                    {
                        _instructions.NeutralizationType = 2;
                    }
                    else
                    {
                        _instructions.NeutralizationType = 3;

                    }
                }

                _instructions.Neutralization = radBtnNeutralization1.IsChecked ? "T" : "F";
                _instructions.Tween80 = radBtnTween2.IsChecked ? "T" : "F";
                _instructions.BathHeat = radBtnBathHeat2.IsChecked ? "T" : "F";
                _instructions.IPMUsage = radBtnIpmUsage2.IsChecked ? "T" : "F";
                _instructions.Pounding = radBtnPounding2.IsChecked ? "T" : "F";

                _instructions.MLA = chkMla.Checked ? "T" : "F";
                _instructions.BPS = chkBps.Checked ? "T" : "F";
                _instructions.MLB = chkMlb.Checked ? "T" : "F";
                _instructions.TSA = chkTsa.Checked ? "T" : "F";

                _instructions.Comments = txtComments.Text.Trim();

                _dal.SaveChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.Message);
            }
           
        }

        public void InitUI()
        {
            _product = _dal.GetProductById(productId);
            

            _instructions = _product.MethodSpecificInstructions;
            if (_instructions != null)
            {
                btnNewMethod.Enabled = false;

                txtProductName.Text = _product.Name;
                txtChemicalNeutralization.Text = _instructions.ChemicalNeutralization;
                txtMembranceFiltration.Text = _instructions.MembranceFltration;
                txtMaxDilution.Text = _instructions.MaxDilution;
              
                if (_instructions.NeutralizationType == 1)
                    radBtnNeutralizationType1.IsChecked = true;
                else
                {
                    if (_instructions.NeutralizationType == 2)
                        radBtnNeutralizationType2.IsChecked = true;
                    else
                        radBtnNeutralizationType3.IsChecked = true;
                }

                radBtnNeutralization1.IsChecked = _instructions.Neutralization == "T";
                if (radBtnNeutralization1.IsChecked)
                    panel1.Enabled = true;
                else
                    panel1.Enabled = false;
                
                radBtnTween2.IsChecked = _instructions.Tween80 == "T";
                radBtnBathHeat2.IsChecked = _instructions.BathHeat == "T";
                radBtnIpmUsage2.IsChecked = _instructions.IPMUsage == "T";
                radBtnPounding2.IsChecked = _instructions.Pounding == "T";

                chkMla.Checked = _instructions.MLA == "T";
                chkBps.Checked = _instructions.BPS == "T";
                chkMlb.Checked = _instructions.MLB == "T";
                chkTsa.Checked = _instructions.TSA == "T";

                txtComments.Text = _instructions.Comments;            

                var samples = _product.Samples.Where(s => s.SampleType == "Method Validation").ToList();

                dgvSamples.Columns.Add("Name", "שם דוגמא", "Name");
                dgvSamples.Columns.Add("CREATED_ON", "נוצרה בתאריך", "CREATED_ON");
                dgvSamples.Columns.Add("CreatedByOperator", "נוצרה על ידי", "CreatedByOperator.Name");
                dgvSamples.CellFormatting += new CellFormattingEventHandler(dgvSamples_CellFormatting);
                dgvSamples.DataSource = samples;

                RegisterDataChngesEvent();
            }
            else
            {
                radPageView1.Enabled = false;
                txtProductName.Enabled = false;
                //                txtClientName.Enabled = false;
            }

        }
        private void RegisterDataChngesEvent()
        {
            this.txtComments.TextChanged += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnNeutralization1.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnNeutralization2.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnNeutralizationType1.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnNeutralizationType3.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnNeutralizationType2.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnTween2.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnTween1.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnBathHeat2.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnBathHeat1.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnIpmUsage2.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnIpmUsage1.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnPounding2.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.radBtnPounding1.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.chkBps.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.chkMla.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.chkTsa.Click += new System.EventHandler(this.dataChanged_TextChanged);
            this.chkMlb.Click += new System.EventHandler(this.dataChanged_TextChanged);

           
        }

        public void SetExternalData(params string[] data)
        {
            productId = double.Parse(data[0]);
        }

        #endregion

        private void Connect()
        {
            _dal.Connect();
        }

        private void dgvSamples_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            e.CellElement.RightToLeft = false;
            e.CellElement.TextAlignment = ContentAlignment.BottomRight;
        }

        private void dataChanged_TextChanged(object sender, EventArgs e)
        {
            if (_site != null)
                _site.SetModifiedFlag();
        }

        private void radBtnNeutralization2_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            panel1.Enabled = !radBtnNeutralization2.IsChecked;
        }
    }
}

