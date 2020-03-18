using System;
using System.Collections.Generic;
using System.Text;

using Hl7.Fhir.Model;

namespace SfmFhir
{
    public class SfmExtReseptamendment : Extension
    {
        Date reseptDate;
        Date expirationDate;
        
        public SfmExtReseptamendment() : base()
        {
            this.Url = "http://ehelse.no/fhir/StructureDefinition/sfm-reseptamendment";  // ???

            reseptDate = Date.Today();
            expirationDate = new Date(DateTime.Now.AddYears(1).Year, DateTime.Now.AddYears(1).Month, DateTime.Now.AddYears(1).Day);
            this.AddExtension("reseptdate", reseptDate);
            this.AddExtension("expirationdate", expirationDate);
        }
        
        public Date ReseptDate
        {
            get
            {
                return reseptDate;
            }
            set
            {
                reseptDate = value;
            }
        }

    }
}
