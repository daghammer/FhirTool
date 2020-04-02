using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    /// <summary>
    /// Helper class for SfmPatient
    /// </summary>
    public class SfmPatient
    {
        private Patient p = null;
        static string ProfileSfmPatientURL = "http://ehelse.no/fhir/StructureDefinition/sfm-Patient";
        static string ExtCitizenURL = "http://hl7.org/fhir/StructureDefinition/patient-citizenship";
        static string ExtMiddlenameURL = "http://hl7.no/fhir/StructureDefinition/no-basis-middlename";
        public SfmPatient()
        {
            p = new Patient();
            SfmInitPatient();
        }

        public SfmPatient( Patient existingPat)
        {
            p = existingPat;
            SfmInitPatient();

        }

        public SfmPatient (string given, string family)
        {
            p = new Patient();
            p.Name.Add(new HumanName().WithGiven(given).AndFamily(family));
            SfmInitPatient();

        }
        void SfmInitPatient()
        {
            if (p == null) return;

            p.Id = Guid.NewGuid().ToString();

            if (p.Meta == null)
            {
                p.Meta = new Meta();

            }


            p.Meta.VersionId = "1";
            p.Meta.LastUpdatedElement = Instant.Now();

            p.Meta.Profile = new List<string>() { ProfileSfmPatientURL };


            // Wait with this for now! 
            // Add extension for Citizenship  
            //Extension citizenship = new Extension(ExtCitizenURL, )
            //p.Extension.Add(citizenship);

            p.Active = true;
            p.Gender = AdministrativeGender.Male;
            

        }

        public Patient patient
        {
            get
            {
                return p;
            }

            set
            {
                p = value;
            }
        }

        /// <summary>
        /// Method for accessing middlename
        /// Assume exactly one HunmanName
        /// </summary>
        public String middlename
        {
            get
            {
                if (p.Name.Count == 0) return null;
                
                return ExtensionExtensions.GetStringExtension(p.Name[0], ExtMiddlenameURL); // Get extension "middlename" from the first humanname
            }
            set
            {
                Extension middle = new Extension(ExtMiddlenameURL, new FhirString(value));
                if (p.Name.Count == 0) // In case human name does not exist
                {
                    HumanName hn = new HumanName();
                }

                ExtensionExtensions.SetStringExtension(p.Name[0], ExtMiddlenameURL, value);
                     
                
            }
        }
        public String FNR
        {
            get
            {
                string fnr = null;
                foreach (Identifier i in p.Identifier)
                {
                    if (i.System == "urn:oid:2.16.578.1.12.4.1.4.1")
                    {
                        fnr = i.Value;
                    }
                }
                return fnr;
            }
            set
            {

                foreach (Identifier i in p.Identifier)
                {
                    if (i.System == "urn:oid:2.16.578.1.12.4.1.4.1")
                    {
                        p.Identifier.Remove(i);
                    }
                }

                var id = new Identifier();
                id.System = "urn:oid:2.16.578.1.12.4.1.4.1";
                id.Value = value;
                id.Use = Identifier.IdentifierUse.Official;
                p.Identifier.Add(id);


            }
        }   
    }
}
