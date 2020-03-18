using System;
using System.Collections.Generic;
using System.Text;


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

namespace SfmFhir
{
    public class SfmMedicationStatement : MedicationStatement
    {
        SfmExtReseptamendment reseptamendment;

        public SfmMedicationStatement() : base()
        {
            reseptamendment = new SfmExtReseptamendment();

            this.AddExtension("test", reseptamendment);
 
        }

        int validate()
        {
            int returncode = 0;

            // Example from https://stackoverflow.com/questions/53399139/fhir-net-api-stu3-validating

            // https://www.devdays.com/wp-content/uploads/2019/02/DD18-US-Validation-in-NET-and-Java-Ewout-Kramer-James-Agnew-2018-06-19.pdf

            // https://blog.fire.ly/2016/10/27/validation-and-other-new-features-in-the-net-api-stack/


            // setup the resolver to use specification.zip, and a folder with custom profiles
            var source = new CachedResolver(new MultiResolver(
                                               new DirectorySource(@"C:\Users\dag\source\repos\FhirTool\SfmFhir\Profiles\"),
                                               ZipSource.CreateValidationSource()));

            // prepare the settings for the validator
            var ctx = new ValidationSettings()
            {
                ResourceResolver = source,
                GenerateSnapshot = true,
                Trace = false,
                EnableXsdValidation = true,
                ResolveExternalReferences = false
            };

            var validator = new Validator(ctx);

            // validate the resource; optionally enter a custom profile url as 2nd parameter
            var result = validator.Validate(this);



            return returncode;
        }

        public String ReseptID { 
            get { 
         
                string ret = String.Empty;
                foreach ( Identifier ident in this.Identifier)
                {
                    if( ident.Type.Text == "ReseptID" )
                    {
                        ret = ident.Value;
                    }
                }
                return ret;
            }

            set
            {
                Identifier ident = new Identifier();
                ident.Type = new CodeableConcept();
                ident.Type.Text = "ReseptID";
                ident.Value = value;
                this.Identifier.Add(ident);
            }
        }

    }
}
