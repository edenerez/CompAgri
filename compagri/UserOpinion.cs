//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompAgri
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserOpinion
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Connection_Id { get; set; }
        public Nullable<bool> Opinion { get; set; }
    
        public virtual Connection Connection { get; set; }
        public virtual User User { get; set; }
    }
}
