using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
// using System.Threading.Tasks;
using System.Windows.Forms;

using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;

using Hl7.Fhir.Serialization;


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
             

            //FhirClient sfmClient = new FhirClient("https://fhir-api.test1.forskrivning.no/api");
            FhirClient sfmClient = new FhirClient("http://hapi.fhir.org/baseR4/");
            sfmClient.PreferredFormat = ResourceFormat.Json;

            // var task = sfmClient.Read<Task>("http://hapi.fhir.org/baseR4/Task/642258");
            SfmTask sTask = new SfmTask();

            Task t = (Task)sTask;

             

            var task =  sfmClient.Read<Task>(ResourceIdentity.Build("Task", "642258"));

            if ( task != null )
            {
                textBox1.Text += "Task: " + task.Id + Environment.NewLine;
                textBox1.Text += "Text.div: " + task.Text.Div + Environment.NewLine;
                if (task.Extension.Count >0 )
                {
                    Extension extension = task.Extension[0];
                    string u = extension.Url;
                }

                foreach (string p in task.Meta.Profile)
                    textBox1.Text += "Profile: "  + p + Environment.NewLine;

                textBox1.Text += "Code: " + task.Code.Text + Environment.NewLine;
                textBox1.Text += "Status: " + task.Status.ToString() + Environment.NewLine;
                textBox1.Text += "Intent: " + task.Intent.ToString() + Environment.NewLine;
                textBox1.Text += "Priority: " + task.Priority.ToString() + Environment.NewLine;

                 foreach ( Resource r in task.Contained)
                {
                    textBox1.Text += "Contained resource: " +r.ResourceType.ToString() + Environment.NewLine;
                    textBox1.Text += "Id: " + r.Id + Environment.NewLine;

                    switch (r.ResourceType )
                    {
                        case ResourceType.Patient:
                            Patient p = (Patient)r;

                            foreach (string pr in p.Meta.Profile)
                                textBox1.Text += "Profile: " + pr + Environment.NewLine;

                            foreach (Identifier i in p.Identifier)
                            {
                                textBox1.Text += "Patient.identifier: " + i.Value + "(" + i.Use.ToString() + ")" + Environment.NewLine;
                            }

                            break;

                    }
                }
 
            }



            // SfmMedicationStatement sfmMS = new SfmMedicationStatement();
            // sfmMS.ReseptID = Guid.NewGuid().ToString();
            // String rID = sfmMS.ReseptID;
        }

        private void btnPatient_Click(object sender, EventArgs e)
        {
            SfmPatient p = new SfmPatient("Dag", "Hammer");
           
            p.middlename = "Severin";
            p.FNR = "11111100099";
            textBox1.Text = p.patient.ToJson(new FhirJsonSerializationSettings() { Pretty = true, AppendNewLine = true }); 
        }
    }
}
