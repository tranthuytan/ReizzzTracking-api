﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.ResultViewModel
{
    public class ResultViewModel
    {
        public bool Success { get; set; } = false;
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
