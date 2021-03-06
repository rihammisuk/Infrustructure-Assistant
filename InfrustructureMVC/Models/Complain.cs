//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InfrustructureMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Complain
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public Nullable<int> InfrustructureId { get; set; }
        public string Description { get; set; }
        public string StudentId { get; set; }
        public Nullable<int> RoomId { get; set; }

        [DataType(DataType.Date)]
        public string ComplainDate { get; set; }
        public Nullable<bool> ComplainStatus { get; set; }
    
        public virtual Classroom Classroom { get; set; }
        public virtual Infrustructure Infrustructure { get; set; }
    }
}
