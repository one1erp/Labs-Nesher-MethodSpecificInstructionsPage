using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

namespace TestMethodInstructions
{
    public partial class Form1 : Form
    {
        private IDataLayer _dal;


        public Form1()
        {
            InitializeComponent();
          

        }

        private void methodSpecificInstructionsPage1_Load(object sender, EventArgs e)
        {
            _dal = new MockDataLayer();

            methodSpecificInstructionsPage1.InitDal(_dal);
            methodSpecificInstructionsPage1.SetExternalData("2"); //קוסמטיקה = 22
            methodSpecificInstructionsPage1.InitUI();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            methodSpecificInstructionsPage1.SaveData();
        }
    }
}
