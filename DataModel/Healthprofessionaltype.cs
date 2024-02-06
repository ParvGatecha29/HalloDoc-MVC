using System;
using System.Collections.Generic;

namespace HalloDoc.DataModel;

public partial class Healthprofessionaltype
{
    public int Healthprofessionalid { get; set; }

    public string Professionname { get; set; } = null!;

    public DateTime Createddate { get; set; }

    public bool? Isactive { get; set; }

    public bool? Isdeleted { get; set; }
}
