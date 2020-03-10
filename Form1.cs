using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;

using SfmFhir;

namespace FhirTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Class1 c1 = new Class1();

            FhirClient sfmClient = new FhirClient("https://fhir-api.test1.forskrivning.no/api");

            var pat = sfmClient.Read<Patient>("123");

            SfmMedicationStatement sfmMS = new SfmMedicationStatement();
            
            sfmMS.ReseptID = Guid.NewGuid().ToString();
            String rID = sfmMS.ReseptID;
        }
    }
}
