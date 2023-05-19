﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.Domain.SectionOne.ViewModels.Purch
{
    public class PurchViewModel
    {
        public int Federal_law { get; set; }
        public long Id { get; set; }
        public string Method_of_purchasing { get; set; }
        public string Purchase_object { get; set; }

        /*     public string Subject_of_the_electronic_auction { get; set; }*/

        /* public string Indificator { get; set; }*/
        /* public string Num_of_lot {get; set; }*/

        /*  public string Name_of_lot { get; set; }*/
        public long NMCK { get; set; }
        /*    public string Currency { get; set; }*/

        /*    public long NMCK_currency { get; set; }
            public string Currency_contract { get; set; }
            public string OKDP_classification { get; set; }
            public string OKPD_classification { get; set; }
        public string OKPD2_classification { get; set; }
        public string Code_position { get; set; }

        
    */

        public string Name_of_customer { get; set; }
        public string Hosting_organization { get; set; }
        public DateTimeOffset Set_data { get; set; }
        public DateTimeOffset Update_data { get; set; }
        public string Purchase_stage { get; set; }

        public string Features_of_placing { get; set; }
        /*  public int Num_Of_Applications { get; set; }*/

        public DateTimeOffset Start_data { get; set; }
        public DateTimeOffset End_data { get; set; }

        public DateTimeOffset Date_of_electronic_auction { get; set; }
        /* public int Num_of_ships { get; set; }*/

       
    }
}
