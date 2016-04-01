﻿using System.ComponentModel.DataAnnotations;
using SIT.Data.Interfaces;

namespace SIT.Models
{
    public class IssueLabel : IDentificatable<int>
    {
        [Key]
        public int Id { get; set; }

        public int IssueId { get; set; }
        public virtual Issue Issue { get; set; }

        public int LabelId { get; set; }
        public virtual Label Label { get; set; }
    }
}
