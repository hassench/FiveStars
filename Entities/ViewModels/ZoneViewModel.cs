using Entities.Models;
using System;

namespace Entities.ViewModels
{
    public class ZoneViewModel
    {
        public String  Note { get; set; }
        public int? niveau { get; set; }
        public Zone zone { get; set; }
    }
}
