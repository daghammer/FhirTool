using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Hl7.Fhir.Validation;
using Hl7.Fhir.Specification.Source;

namespace FhirTool
{
    class SfmTask : Task
    {
        static string sfmProfile  =  "http://ehelse.no/fhir/StructureDefinition/sfm-Task"  ;
        public  SfmTask() : base()
        {

            this.Meta = new Meta(); 
            this.Meta.Profile.Append(sfmProfile);  
            Intent = TaskIntent.Order;
            Code = new CodeableConcept("http://ehelse.no/fhir/CodeSystem/sfm-task-types", "8", "Endring på multidosepasient", "SFM task code");
             
    
          
        }

        public SfmTask(Task t) : base()
        {
             // Is this possible?


        }
        void addFor(Patient pat)
        {
            Contained.Add(pat);

        }

        void addOwner(Practitioner pract)
        {
            Contained.Add(pract);

        }

        public Task getBase()
        {
            return (Task)this;
        }
    }
}
