using System;

namespace Ispit.data.Models
{
    public class RezultatPretrage
    {
        public int Id { get; set; }
        public virtual Uputnica Uputnica { get; set; }
        public int UputnicaId { get; set; }

        public LabPretraga LabPretraga { get; set; }
        public int LabPretragaId  { get; set; }


        public int? ModalitetId { get; set; }
        public Modalitet Modalitet { get; set; }
        public double? NumerickaVrijednost { get; set; }
    }
}
