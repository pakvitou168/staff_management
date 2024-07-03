using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Build.Framework;
using System;

namespace WebApplication1.Data
{
    public class StaffUpdate
    {
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Gender { get; set; }
    }
}
